using Net4Sage.CIUtils;
using Net4Sage.DataAccessModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net4Sage.SOUtils
{
    public class ShipmentHandler
    {
        public SageSession SysSession { get; private set; }
        private Context _context;
        private SqlCommand _cmd;
        public bool UseNationalAcct { get; private set; }

        public ShipmentHandler(ref SageSession session)
        {
            SysSession = session;
            System.Data.EntityClient.EntityConnectionStringBuilder connectionString = new System.Data.EntityClient.EntityConnectionStringBuilder()
            {
                Metadata = "res://*/DataModel.csdl|res://*/DataModel.ssdl|res://*/DataModel.msl",
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = SysSession.GetConnectionString()
            };
            _context = new Context(connectionString.ToString());
            CreateWorkTables();
        }
        private void CreateWorkTables()
        {
            try
            {
                _cmd = new SqlCommand();
                _cmd.Connection = SysSession.GetConnection();

                string pickWrk =
                    "(LineDistKey INT NOT NULL, LineKey INT NOT NULL, OrderKey INT NOT NULL, TranType INT NOT NULL, CrHold INT NOT NULL, LineHold INT NOT NULL, OrderHold INT NOT NULL" +
                    ", AllowImmedShipFromPick bit null, CustKey INT NULL, DeliveryMethod int NULL, DeliveryMethodID varchar(20) NULL, Expiration Datetime null, FreightAmt decimal(15,8) null" +
                    ", ItemDesc varchar(300) null, ItemKey int null, LineHoldReason varchar(200) NULL, LineNbr int null, OrderHoldReason varchar(200) NULL, OrderLineUnitMeasKey int null" +
                    ", QtyOrdered decimal(15,8) null, QtyOnBO decimal(15,8) null, QtyToPick decimal(15,8) null, QtyPendingShip decimal(15,8) null, ShipDate datetime null, ShipMethKey int Null" +
                    ", ShipToAddrKey int NULL, ShipToCustAddrKey int NULL, ShipUnitMeasKey int null, ShipWhseKey int NULL, ShipFOBKey int NULL, TranNoRelChngOrd int NULL, TranTypeText varchar(50) NULL" +
                    ", ShipPriority int NULL, RcvgWhseKey int NULL, AllowSubItem bit null, AllowAutoSub bit null, CustID varchar(30) null, CustName varchar(100) null, CustReqShipComplete bit null" +
                    ", ShipToCustAddrID varchar(50) null, ShipToCustAddress varchar(300) null, RcvgWhseID varchar(30), ItemReqShipComplete bit null, OrderLineUnitMeasID varchar(10) null, ShipUnitMeasID varchar(10) null" +
                    ", ShipMethID varchar(10), ShipWhseID varchar(30), TrackQtyAtBin bit null, TrackMeth smallint null, WhseUseBin bit null, QtyToPickByOrderUOM decimal(15,8) null, ItemType smallint null" +
                    ", ItemID varchar(30) null, AllowDecimalQty bit null, StockUnitMeasKey int null, QtyAvail decimal(15,8), StockAllocSeq int null, CompItemQty bit null, KitShipLineKey int null" +
                    ", QtyPicked decimal(15,8) null, ShipLineDistKey int null, ShipLineKey int null, SubstituteItemKey int null, ShipLineUpdateCounter int null, ShipLineDistUpdateCounter int null" +
                    ", UpdateCounterviolation int null, ShipKey int null, ShipmentCommitStatus int null, InvtTranKey int null, QtyDist decimal(15,8) null, SubstiStatus int null, SubstituteItemDesc varchar(300)" +
                    ", PickStatus smallint null, ShortPick smallint null, StatusCustShipCompleteViolation varchar(5) null, StatusItemShipCompLeteViolation varchar(5) null, StatusDistWarning smallint null" +
                    ", StatusDistQtyShort varchar(5) null, StatusOverShipment smallint null, IsDistComplete smallint null, Status smallint null, QtyShort decimal(15,8) null, WhseBinKey int null, WhseBinID varchar(50) null, InvtLotNo varchar(50) null, InvtLotKey int null, InvtLotExpirationDate datetime null);";
                _cmd.CommandText = "CREATE TABLE #tsoCreatePickWrk1 " + pickWrk;
                _cmd.ExecuteNonQuery();

                _cmd.CommandText = "CREATE TABLE #tsoCreatePickWrk2 " + pickWrk;
                _cmd.ExecuteNonQuery();

                _cmd.CommandText = "CREATE TABLE #QtyAvailWrk (CompItemQty bit null, ItemKey int not null, ItemType smallint not null, KitLineKey int null"
                    + ", LineKey int not null, QtyAvail decimal(15,8) not null, StockUnitMeasKey int not null, TranType int not null, TranUnitMeasKey int not null" +
                    ", WhseKey int not null, TrackMeth smallint not null)";
                _cmd.ExecuteNonQuery();

                _cmd.CommandText = "CREATE TABLE #tsoPickSumTotal (PendQty decimal(15,8), UserKey int)";
                _cmd.ExecuteNonQuery();

                _cmd.CommandText = "CREATE TABLE #PickListOption (BinSortOrder smallint, InclAbbrShipToAddr smallint, InclBinLoc smallint, InclBTOKitList smallint, InclComments smallint, InclKitCompInd smallint, InclNonStock smallint, InclPriority smallint, InclShipCompIeteInd smallint, InclShipDate smallint, InclShipMethod smallint, InclShipToAddr smallint, InclSubInd smallint, RptFormat smallint, SortByZone smallint)";
                _cmd.ExecuteNonQuery();

                _cmd.CommandText = "CREATE TABLE #tarCreditCheckWrk (CustKey int not null, SOKey int not null, TranAmt decimal(15,8) not null, CreditStatus smallint null, SeqNo int null, CreditAuthUserID varchar(50) null" +
                    ", CreditApprovedAmt decimal(15,8) null, CustCreditLimitFailure smallint null, NACreditLimitFailure smallint null, CustAgingFailure smallint null)";
                _cmd.ExecuteNonQuery();

                _cmd.CommandText = "select * into #tsoCreatePickHdrWrk from tsoCreatePickHdrWrk where 1 = 2";
                _cmd.ExecuteNonQuery();

                _cmd.CommandText = "select * into #tsoPickListWrk from tsoPickListWrk where 1 = 2";
                _cmd.ExecuteNonQuery();

                _cmd.CommandText = "select * into #tsoGenShipHdrWrk from tsoGenShipHdrWrk where 1 = 2";
                _cmd.ExecuteNonQuery();

                _cmd.CommandText = "select * into #tsoGenShipLineWrk from tsoGenShipLineWrk where 1 = 2";
                _cmd.ExecuteNonQuery();

                _cmd.CommandText = "select * into #tsoShipmentLog from tsoShipmentLog where 1 = 2";
                _cmd.ExecuteNonQuery();

                _cmd.CommandText = "select * into #tsoPendShipment from tsoPendShipment where 1 = 2";
                _cmd.ExecuteNonQuery();

                _cmd.CommandText = "select * into #tsoShipLine from tsoShipLine where 1 = 2";
                _cmd.ExecuteNonQuery();

                _cmd.CommandText = "select * into #tsoShipLineDist from tsoShipLineDist where 1 = 2";
                _cmd.ExecuteNonQuery();

                _cmd.CommandText = "select * into #timInvtDistWrk from timInvtDistWrk where 1 = 2";
                _cmd.ExecuteNonQuery();

                _cmd.CommandText = "CREATE TABLE #tsoPickItemsToDist (AutoSubstituteOccurred bit null, CompItemQty bit null, CustAddrKey int null, CustShipCompleteViolation bit null" +
                    ", DistributionWarning int null, InvtTranKey int null, ItemKey int null, KitShipLineKey int null, PickedItemKey int null, PrioritySeqNo int null" +
                    ", QtyPicked decimal(15,8) null, QtyToPick decimal(15,8) null, ShipCompleteViolation int null, ShipLineKey int null, ShortPick int null, ShipLineUpdateCounter int null" +
                    ", SingleLotNotAvail bit null, TransitInvtTranKey int null, TranType int null, TranUOMKey int null, UpdateCounterviolation int null, UseSubstitutes bit null, WhseKey int null, PickLineID varchar(100) null)";
                _cmd.ExecuteNonQuery();

                _cmd.CommandText = "CREATE TABLE #InvtTranBinLotCount (ShipLineKey int, ItemKey int, WhseKey int, TrackMeth smallint, InvtTranKey int, DistBinCount int, MultipleBin int, DistLotCount int, MultipleLot int)";
                _cmd.ExecuteNonQuery();

                _cmd.CommandText = "CREATE TABLE #tsoGenShipLineKeys (ShipLineKey int)";
                _cmd.ExecuteNonQuery();

                _cmd.CommandText = "CREATE TABLE #tsoGenShipLineExKeys (ShipLineKey int)";
                _cmd.ExecuteNonQuery();

                _cmd.CommandText = "CREATE TABLE #tsoFrtMethod (ShipKey int, ShipLineKey int, FrtMethod smallint null)";
                _cmd.ExecuteNonQuery();

                _cmd.CommandText = "IF OBJECT_ID ('tempdb..#tsoPickListSeqWrk') IS NULL CREATE TABLE #tsoPickListSeqWrk (SeqNo INTEGER NOT NULL IDENTITY(1,1), TranType INTEGER NULL, TranKey INTEGER NULL) ELSE TRUNCATE TABLE #tsoPickListSeqWrk";
                _cmd.ExecuteNonQuery();
            }
            catch(Exception exc)
            {
                SysSession.WriteLog(exc.Message);
            }
        }
        private OperationResult PopulateWorkTables(string lines)
        {
            int? singleShipViolation = 0, retVal = 0;

            try
            {
                //Populate List Pick temp tables
                _cmd = new SqlCommand();
                _cmd.Connection = SysSession.GetConnection();
                _cmd.CommandText = "TRUNCATE TABLE #tsoCreatePickWrk1";
                _cmd.ExecuteNonQuery();

                _cmd.CommandText = "INSERT INTO #tsoCreatePickWrk1  (LineDistKey, LineKey, OrderKey, TranType, CrHold, LineHold, OrderHold)  SELECT tsoSOLineDist.SOLineDistKey,  tsoSOLineDist.SOLineKey, tsoSOLine.SOKey,  tsoSalesOrder.TranType, 0,0,0 FROM tsoSalesOrder WITH (NOLOCK)  JOIN tsoSOLine WITH (NOLOCK) ON tsoSalesOrder.SOKey = tsoSOLine.SOKey  JOIN tsoSOLineDist WITH (NOLOCK) ON tsoSOLine.SOLineKey = tsoSOLineDist.SOLineKey  JOIN tarCustomer WITH (NOLOCK) ON tsoSalesOrder.CustKey = tarCustomer.CustKey  WHERE tsoSalesOrder.Status = 1 AND tsoSalesOrder.CrHold = 0  AND tsoSOLine.Status = 1 AND tsoSOLineDist.Status = 1 AND tsoSOLineDist.QtyOpenToShip <> 0 AND tsoSOLineDist.DeliveryMeth =1 AND tarCustomer.Hold =0";
                if (UseNationalAcct)
                    _cmd.CommandText += " AND (tarCustomer.NationalAcctLevelKey IS NULL OR (tarCustomer.NationalAcctLevelKey IS NOT NULL AND tarCustomer.NationalAcctLevelKey IN  (SELECT NationalAcctLevelKey FROM tarNationalAcctLevel WITH (NOLOCK) WHERE NationalAcctKey IN  (SELECT NationalAcctKey FROM tarNationalAcct WITH (NOLOCK)  WHERE Hold = 0))))";

                _cmd.CommandText += " AND tsoSOLineDist.SOLineKey in (" + lines + ")";

                _cmd.ExecuteNonQuery();
            }catch(Exception exc)
            {
                return new OperationResult(false, "Fail to Populate List Pick: " + exc.Message);
            }

            try
            {
                //Create List Pick
                _cmd = new SqlCommand("spsoGetIMAvailQty", SysSession.GetConnection());
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.Parameters.AddWithValue("@iCompanyID", SysSession.CompanyID);
                _cmd.Parameters.AddWithValue("@iInclZeroAvail", 0); //Include non-inventary items or not
                _cmd.Parameters.AddWithValue("@iImmediatePick", 0); //Immediate Pick Flag
                _cmd.Parameters.AddWithValue("@iLockData", 0); //Lock the order line data or not
                _cmd.Parameters.AddWithValue("@iBusinessDate", SysSession.BusinessDate);

                _cmd.Parameters.Add(new SqlParameter("@oSingleShipmentViolation", singleShipViolation)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output
                });
                _cmd.Parameters.Add(new SqlParameter("@oRetVal", retVal)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output
                });
                _cmd.ExecuteNonQuery();

                switch ((int)_cmd.Parameters[6].Value)
                {
                    case 1:
                        break;
                    case 0:
                        return new OperationResult(false, "Failt to Creat List Pick");
                    case 2:
                        return new OperationResult(false, "Table Update Error Creating List Pick");
                    case 3:
                        return new OperationResult(false, "SP UOM Convertion Error Creating List Pick");
                    case 6:
                        return new OperationResult(false, "SP spsoDelPickOrphans Error Creating List Pick");
                    case 7:
                        return new OperationResult(false, "Lock Insert Error Creating List Pick");
                    default:
                        return new OperationResult(false, "Unknow Error Creating List Pick");
                }
            }
            catch(Exception exc)
            {
                return new OperationResult(false, "Error Creating List Pick: "+ exc.Message);
            }

            try
            {
                //Perform Credit Checks
                _cmd = new SqlCommand("spsoPickCreditCheck", SysSession.GetConnection());
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.Parameters.AddWithValue("@iCreditHoldAutoRelease", _context.SOOptions.Where(p => p.CompanyID == SysSession.CompanyID).Select(p => p.CreditHoldAutoRelease).First());
                _cmd.Parameters.AddWithValue("@iUserID", SysSession.UserID);
                _cmd.Parameters.Add(new SqlParameter("@oRetVal", retVal)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output
                });
                _cmd.ExecuteNonQuery();
                if ((int)_cmd.Parameters[2].Value == 1)
                    return new OperationResult(true);
            }
            catch(Exception e)
            {
                return new OperationResult(false, e.Message);
            }
            return new OperationResult(false, "Pick Credict Check Fail");
        }
        public OperationResult GenerateShipment(IEnumerable<int> soLinesKeys, DateTime? shipDate = null)
        {
            OperationResult answer;
            if (shipDate == null) shipDate = SysSession.BusinessDate;
            if ((answer = GenerateShipLines(soLinesKeys, shipDate)).Success)
                return GenerateShipment(shipDate);
            return answer;
        }
        public OperationResult GenerateShipLines(IEnumerable<int> soLinesKeys, DateTime? shipDate = null)
        {
            OperationResult answer;
            int? retVal = 0, sessionID = 0, pickList = 0, lockKey = 0;
            string pickNo = string.Empty;
            if (!shipDate.HasValue)
                shipDate = SysSession.BusinessDate;

            #region "populete temp tables"
            if(!(answer = PopulateWorkTables(string.Join(",", soLinesKeys))).Success) return answer;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = SysSession.GetConnection();

                cmd.CommandText = "INSERT INTO #tsoPickListSeqWrk (TranType, TranKey)  SELECT TranType, TranKey FROM #tsoCreatePickHdrWrk  WHERE CrHold = 0 AND Hold = 0";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO #tsoPickListSeqWrk (TranType, TranKey)  SELECT TranType, TranKey FROM #tsoCreatePickHdrWrk  WHERE CrHold = 1 OR Hold = 1";
                cmd.ExecuteNonQuery();
            }
            catch(Exception exc)
            {
                return new OperationResult(false, "Error Generating ShipLines: "+ exc.Message);
            }
            #endregion

            if ((answer = ExplodePickKits()).Success && (answer = CreatePickList(out pickList, out pickNo, out lockKey)).Success)
            {
                try
                {
                    _cmd = new SqlCommand("spsoCreateShipLines", SysSession.GetConnection());
                    _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    _cmd.Parameters.AddWithValue("@iCompanyID", SysSession.CompanyID);
                    _cmd.Parameters.AddWithValue("@iTranDate", shipDate);
                    _cmd.Parameters.AddWithValue("@iBusinessDate", SysSession.BusinessDate);
                    _cmd.Parameters.AddWithValue("@iEmptyBins", 0);
                    _cmd.Parameters.AddWithValue("@iEmptyRandomBins", 0);
                    _cmd.Parameters.AddWithValue("@iPickOrdQty", 0);
                    _cmd.Parameters.Add(new SqlParameter("@oSession", sessionID)
                    {
                        DbType = System.Data.DbType.Int32,
                        Direction = System.Data.ParameterDirection.Output
                    });
                    _cmd.Parameters.Add(new SqlParameter("@oRetVal", retVal)
                    {
                        DbType = System.Data.DbType.Int32,
                        Direction = System.Data.ParameterDirection.Output
                    });
                    _cmd.ExecuteNonQuery();
                    switch ((int)_cmd.Parameters[7].Value)
                    {
                        case 1:
                            return new OperationResult(true);
                        case 2:
                            return new OperationResult(false, "Error in creating record in tsoShipLine");
                        case 3:
                            return new OperationResult(false, "Error in creating record in tsoShipLinedist");
                        case 4:
                            return new OperationResult(false, "Error in creating automatic distribution");
                        default:
                            break;
                    }
                }
                catch (Exception exc)
                {
                    return new OperationResult(false, "Fail to create ship lines: "+ exc.Message);
                }
            }
            else
                return answer;
            return new OperationResult(false, "Fail to create ship lines"); ;
        }
        private OperationResult GenerateShipment(DateTime? shipDate = null)
        {
            int? retVal = 0, shipCount = 0, msgNo = 0, excCount = 0;
            string shipNo = string.Empty;

            try
            {
                _cmd = new SqlCommand("INSERT INTO #tsoGenShipLineKeys (ShipLineKey) SELECT ShipLineKey FROM #tsoCreatePickWrk2 WHERE AllowImmedShipFromPick = 1", SysSession.GetConnection());
                _cmd.ExecuteNonQuery();
            }
            catch(Exception exc)
            {
                return new OperationResult(false, "Fail to insert in tsoGenShipLineKeys: "+ exc.Message); ;
            }
            
            try
            {
                //-- This Procedure populates the work tables #tsoGenShipHdrWrk and #tsoGenShipLineWrk for the shipment generation process returns shipment counts
                _cmd = new SqlCommand("spsoGenShip", SysSession.GetConnection());
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.Parameters.AddWithValue("@CompanyID", SysSession.CompanyID);
                _cmd.Parameters.AddWithValue("@SessionID", 1);
                _cmd.Parameters.Add(new SqlParameter("@oShipCount", shipCount)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output
                });
                _cmd.Parameters.Add(new SqlParameter("@oExceptionCount", excCount)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output
                });
                _cmd.Parameters.Add(new SqlParameter("@oRetVal", retVal)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output
                });
                _cmd.Parameters.AddWithValue("@UsePickingComplete", 0);
                _cmd.ExecuteNonQuery();
                if ((int)_cmd.Parameters[4].Value != 1 || (int)_cmd.Parameters[2].Value <= 0)
                    return new OperationResult(false, "Error in spsoGenShip");
                shipCount = (int)_cmd.Parameters[2].Value;
            }
            catch (Exception exc)
            {
                return new OperationResult(false, "Error in spsoGenShip: "+ exc.Message);
            }
            try
            {
                _cmd = new SqlCommand("spsoGenerateShipments", SysSession.GetConnection());
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.Parameters.AddWithValue("@iSessionID", 1);
                _cmd.Parameters.AddWithValue("@iCompanyID", SysSession.CompanyID);
                _cmd.Parameters.AddWithValue("@iTranDate", shipDate);
                _cmd.Parameters.Add(new SqlParameter("@oShipCount", shipCount)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output
                });
                _cmd.Parameters.Add(new SqlParameter("@oRetVal", retVal)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output
                });
                _cmd.Parameters.Add(new SqlParameter("@oMsgNo", msgNo)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output
                });
                _cmd.Parameters.Add(new SqlParameter("@oLastShipNumber", shipNo)
                {
                    DbType = System.Data.DbType.String,
                    Direction = System.Data.ParameterDirection.Output
                });
                _cmd.ExecuteNonQuery();
                if((int)_cmd.Parameters[4].Value == 1 && (int)_cmd.Parameters[3].Value == shipCount)
                    return new OperationResult(true);
            }
            catch(Exception exc)
            {
                return new OperationResult(false, "Error in spsoGenerateShipments: " + exc.Message);
            }
            return new OperationResult(false, "Error in spsoGenerateShipments" );
        }
        private OperationResult ExplodePickKits()
        {
            try
            {
                int? retVal = 0;
                _cmd = new SqlCommand("spsoExplodePickKits", SysSession.GetConnection());
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.Parameters.Add(new SqlParameter("@oRetVal", retVal)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output
                });
                _cmd.ExecuteNonQuery();
                if ((int)_cmd.Parameters[0].Value == 1)
                    return new OperationResult(true);
            }
            catch(Exception exc)
            {
                return new OperationResult(false, "Fail to Explode Pick Kits: "+exc.Message);
            }
            return new OperationResult(false, "Fail to Explode Pick Kits");
        }
        private OperationResult CreatePickList(out int? pickListKey, out string pickListNo, out int? logicalLockKey)
        {
            int? retVal = 0;
            pickListKey = null;
            pickListNo = string.Empty;
            logicalLockKey = null;

            try
            {
                _cmd = new SqlCommand("spsoCreatePickList", SysSession.GetConnection());
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.Parameters.AddWithValue("@iCompanyID", SysSession.CompanyID);
                _cmd.Parameters.Add(new SqlParameter("@oPickListKey", pickListKey)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output
                });
                _cmd.Parameters.Add(new SqlParameter("@oPickListNo", pickListNo)
                {
                    DbType = System.Data.DbType.String,
                    Direction = System.Data.ParameterDirection.Output
                });
                _cmd.Parameters.Add(new SqlParameter("@oLogicalLockKey", logicalLockKey)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output
                });
                _cmd.Parameters.Add(new SqlParameter("@oRetVal", retVal)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output
                });
                _cmd.ExecuteNonQuery();
                if ((int)_cmd.Parameters[4].Value == 1)
                {
                    pickListKey = _cmd.Parameters[0].Value as int?;
                    pickListNo = _cmd.Parameters[1].Value as string;
                    logicalLockKey = _cmd.Parameters[2].Value as int?;
                    return new OperationResult(true);
                }
            }
            catch(Exception exc)
            {
                return new OperationResult(false, "Fail to Create Pick List: "+ exc.Message);
            }
            return new OperationResult(false, "Fail to Create Pick List");
        }
    }
}