using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net4Sage.DataAccessModel;

namespace Net4Sage.MFUtils
{
    public class RouteHandler
    {
        private readonly SageSession SysSession;
        private Context context;
        public RouteHandler(ref SageSession session)
        {
            SysSession = session;
            System.Data.EntityClient.EntityConnectionStringBuilder connectionString = new System.Data.EntityClient.EntityConnectionStringBuilder()
            {
                Metadata = "res://*/DataModel.csdl|res://*/DataModel.ssdl|res://*/DataModel.msl",
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = SysSession.GetConnectionString()
            };
            context = new Context(connectionString.ToString());
        }

        public IEnumerable<MaterialRequeriment> GetMaterialRequeriments(int RouteKey, bool includeSE = false)
        {
            Route r = context.Routes.Where(p => p.RoutingKey == RouteKey).FirstOrDefault();
            if (r != null)
                return GetMaterialRequeriments(r, includeSE);
            return null;
        }

        public IEnumerable<MaterialRequeriment> GetMaterialRequeriments(Route route, bool includeSE = false)
        {
            List<MaterialRequeriment> answer = new List<MaterialRequeriment>();
            MaterialRequeriment req;
            Route temp;
            foreach(RouteDetail detail in route.Details.Where(p => p.MatItemKey.HasValue))
            {
                temp = GetStandardRoute(detail.MatItemKey.Value);
                if(temp != null)
                {
                    if (includeSE)
                    {
                        req = answer.Where(p => p.ItemKey == detail.MatItemKey).FirstOrDefault();
                        if (req == null)
                            answer.Add(new MaterialRequeriment()
                            {
                                ItemKey = detail.MatItemKey.Value,
                                Requeriment = detail.MatReqPc,
                                isSE = true
                            });
                        else
                            req.Requeriment += detail.MatReqPc;
                    }

                    foreach (MaterialRequeriment mr in GetMaterialRequeriments(temp, includeSE))
                    {
                        req = answer.Where(p => p.ItemKey == mr.ItemKey).FirstOrDefault();
                        if (req == null)
                            answer.Add(new MaterialRequeriment()
                            {
                                ItemKey = mr.ItemKey,
                                Requeriment = detail.MatReqPc * mr.Requeriment,
                            });
                        else
                            req.Requeriment += detail.MatReqPc * mr.Requeriment;
                    }
                }
                else
                {
                    req = answer.Where(p => p.ItemKey == detail.MatItemKey).FirstOrDefault();
                    if (req == null)
                        answer.Add(new MaterialRequeriment()
                        {
                            ItemKey = detail.MatItemKey.Value,
                            Requeriment = detail.MatReqPc,
                        });
                    else
                        req.Requeriment += detail.MatReqPc;
                }
            }

            return answer;
        }

        public Route GetStandardRoute(int itemKey)
        {
            return context.Routes.Where(p => p.ItemKey == itemKey && p.Active == 1 && p.RollUpFlag == 1).FirstOrDefault();
        }
    }
}
