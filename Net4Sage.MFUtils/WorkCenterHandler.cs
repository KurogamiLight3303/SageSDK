using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net4Sage.DataAccessModel;

namespace Net4Sage.MFUtils
{
    public class WorkCenterHandler
    {
        private readonly SageSession SysSession;
        private Context context;
        public WorkCenterHandler(ref SageSession session)
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

        public int DaysWorkedForWeek(int WorkCenterKey)
        {
            int answer = 0;
            WorkCenter workCenter = GetWorkCenter(WorkCenterKey);

            if (workCenter.MonStart1.HasValue)
                answer++;
            if (workCenter.ThuStart1.HasValue)
                answer++;
            if (workCenter.WedStart1.HasValue)
                answer++;
            if (workCenter.TueStart1.HasValue)
                answer++;
            if (workCenter.FriStart1.HasValue)
                answer++;
            if (workCenter.SatStart1.HasValue)
                answer++;
            if (workCenter.SunStart1.HasValue)
                answer++;
            return answer;
        }
        public int DaysWorkedForMonth(int WorkCenterKey, int year, int month)
        {
            int answer = 0;
            WorkCenter workCenter = GetWorkCenter(WorkCenterKey);
            DateTime date = new DateTime(year, month, 1), endDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            while (date <= endDate)
            {
                if (IsDayWorked(workCenter, date.DayOfWeek))
                    answer++;
                date = date.AddDays(1);
            }
            return answer;
        }
        public DateTime LastWorkDayOfMonth(int WorkCenterKey, int year, int month)
        {
            WorkCenter workCenter = GetWorkCenter(WorkCenterKey);
            DateTime date = new DateTime(year, month, 1), endDate = new DateTime(year, month, DateTime.DaysInMonth(year, month)), answer = date;
            while (date <= endDate)
            {
                if (IsDayWorked(workCenter, date.DayOfWeek))
                    answer = date;
                date = date.AddDays(1);
            }
            return answer;
        }

        public DateTime LastWorkDayOfWeek(int WorkCenterKey, DateTime startDay)
        {
            WorkCenter workCenter = GetWorkCenter(WorkCenterKey);
            DateTime date = startDay, endDate = startDay.AddDays(1), answer = date;
            while (date <= endDate)
            {
                if (IsDayWorked(workCenter, date.DayOfWeek))
                    answer = date;
                date = date.AddDays(1);
            }
            return date;
        }

        public bool IsDayWorked(int WorkCenterKey, DayOfWeek day)
        {
            WorkCenter workCenter = GetWorkCenter(WorkCenterKey);

            return IsDayWorked(workCenter, day);
        }

        public bool IsDayWorked(WorkCenter workCenter, DayOfWeek day)
        {
            if (workCenter.MonStart1.HasValue && day == DayOfWeek.Monday)
                return true;
            if (workCenter.ThuStart1.HasValue && day == DayOfWeek.Thursday)
                return true;
            if (workCenter.WedStart1.HasValue && day == DayOfWeek.Wednesday)
                return true;
            if (workCenter.TueStart1.HasValue && day == DayOfWeek.Tuesday)
                return true;
            if (workCenter.FriStart1.HasValue && day == DayOfWeek.Friday)
                return true;
            if (workCenter.SatStart1.HasValue && day == DayOfWeek.Saturday)
                return true;
            if (workCenter.SunStart1.HasValue && day == DayOfWeek.Sunday)
                return true;
            return false;
        }
        public WorkCenter GetWorkCenter(int WorkCenterKey)
        {
            return context.WorkCenters.Where(p => p.WorkCenterKey == WorkCenterKey).FirstOrDefault();
        }

        public double GetWeekWorkHours(int workCenterKey)
        {
            WorkCenter workCenter = GetWorkCenter(workCenterKey);

            return GetWeekWorkHours(workCenter);
        }

        public double GetWorkHours(int workCenterKey, DayOfWeek day)
        {
            return GetWorkHours(GetWorkCenter(workCenterKey), day);
        }

        public double GetWorkHours(WorkCenter workCenter, DayOfWeek day)
        {
            double min = 0;
            switch (day)
            {
                case DayOfWeek.Monday:
                    if (workCenter.MonStart1.HasValue && workCenter.MonStop1.HasValue)
                        min += workCenter.MonStop1.Value.TimeOfDay.Subtract(workCenter.MonStart1.Value.TimeOfDay).TotalMinutes;
                    if (workCenter.MonStart2.HasValue && workCenter.MonStop2.HasValue)
                        min += workCenter.MonStop2.Value.TimeOfDay.Subtract(workCenter.MonStart2.Value.TimeOfDay).TotalMinutes;
                    if (workCenter.MonStart3.HasValue && workCenter.MonStop3.HasValue)
                        min += workCenter.MonStop3.Value.TimeOfDay.Subtract(workCenter.MonStart3.Value.TimeOfDay).TotalMinutes;
                    if (workCenter.MonStart4.HasValue && workCenter.MonStop4.HasValue)
                        min += workCenter.MonStop4.Value.TimeOfDay.Subtract(workCenter.MonStart4.Value.TimeOfDay).TotalMinutes;
                    break;
                case DayOfWeek.Thursday:
                    if (workCenter.ThuStart1.HasValue && workCenter.ThuStop1.HasValue)
                        min += workCenter.ThuStop1.Value.TimeOfDay.Subtract(workCenter.ThuStart1.Value.TimeOfDay).TotalMinutes;
                    if (workCenter.ThuStart2.HasValue && workCenter.ThuStop2.HasValue)
                        min += workCenter.ThuStop2.Value.TimeOfDay.Subtract(workCenter.ThuStart2.Value.TimeOfDay).TotalMinutes;
                    if (workCenter.ThuStart3.HasValue && workCenter.ThuStop3.HasValue)
                        min += workCenter.ThuStop3.Value.TimeOfDay.Subtract(workCenter.ThuStart3.Value.TimeOfDay).TotalMinutes;
                    if (workCenter.ThuStart4.HasValue && workCenter.ThuStop4.HasValue)
                        min += workCenter.ThuStop4.Value.TimeOfDay.Subtract(workCenter.ThuStart4.Value.TimeOfDay).TotalMinutes;
                    break;
                case DayOfWeek.Wednesday:
                    if (workCenter.WedStart1.HasValue && workCenter.WedStop1.HasValue)
                        min += workCenter.WedStop1.Value.TimeOfDay.Subtract(workCenter.WedStart1.Value.TimeOfDay).TotalMinutes;
                    if (workCenter.WedStart2.HasValue && workCenter.WedStop2.HasValue)
                        min += workCenter.WedStop2.Value.TimeOfDay.Subtract(workCenter.WedStart2.Value.TimeOfDay).TotalMinutes;
                    if (workCenter.WedStart3.HasValue && workCenter.WedStop3.HasValue)
                        min += workCenter.WedStop3.Value.TimeOfDay.Subtract(workCenter.WedStart3.Value.TimeOfDay).TotalMinutes;
                    if (workCenter.WedStart4.HasValue && workCenter.WedStop4.HasValue)
                        min += workCenter.WedStop4.Value.TimeOfDay.Subtract(workCenter.WedStart4.Value.TimeOfDay).TotalMinutes;
                    break;
                case DayOfWeek.Tuesday:
                    if (workCenter.TueStart1.HasValue && workCenter.TueStop1.HasValue)
                        min += workCenter.TueStop1.Value.TimeOfDay.Subtract(workCenter.TueStart1.Value.TimeOfDay).TotalMinutes;
                    if (workCenter.TueStart2.HasValue && workCenter.TueStop2.HasValue)
                        min += workCenter.TueStop2.Value.TimeOfDay.Subtract(workCenter.TueStart2.Value.TimeOfDay).TotalMinutes;
                    if (workCenter.TueStart3.HasValue && workCenter.TueStop3.HasValue)
                        min += workCenter.TueStop3.Value.TimeOfDay.Subtract(workCenter.TueStart3.Value.TimeOfDay).TotalMinutes;
                    if (workCenter.TueStart4.HasValue && workCenter.TueStop4.HasValue)
                        min += workCenter.TueStop4.Value.TimeOfDay.Subtract(workCenter.TueStart4.Value.TimeOfDay).TotalMinutes;
                    break;
                case DayOfWeek.Friday:
                    if (workCenter.FriStart1.HasValue && workCenter.FriStop1.HasValue)
                        min += workCenter.FriStop1.Value.TimeOfDay.Subtract(workCenter.FriStart1.Value.TimeOfDay).TotalMinutes;
                    if (workCenter.FriStart2.HasValue && workCenter.FriStop2.HasValue)
                        min += workCenter.FriStop2.Value.TimeOfDay.Subtract(workCenter.FriStart2.Value.TimeOfDay).TotalMinutes;
                    if (workCenter.FriStart3.HasValue && workCenter.FriStop3.HasValue)
                        min += workCenter.FriStop3.Value.TimeOfDay.Subtract(workCenter.FriStart3.Value.TimeOfDay).TotalMinutes;
                    if (workCenter.FriStart4.HasValue && workCenter.FriStop4.HasValue)
                        min += workCenter.FriStop4.Value.TimeOfDay.Subtract(workCenter.FriStart4.Value.TimeOfDay).TotalMinutes;
                    break;
                case DayOfWeek.Saturday:
                    if (workCenter.SatStart1.HasValue && workCenter.SatStop1.HasValue)
                        min += workCenter.SatStop1.Value.TimeOfDay.Subtract(workCenter.SatStart1.Value.TimeOfDay).TotalMinutes;
                    if (workCenter.SatStart2.HasValue && workCenter.SatStop2.HasValue)
                        min += workCenter.SatStop2.Value.TimeOfDay.Subtract(workCenter.SatStart2.Value.TimeOfDay).TotalMinutes;
                    if (workCenter.SatStart3.HasValue && workCenter.SatStop3.HasValue)
                        min += workCenter.SatStop3.Value.TimeOfDay.Subtract(workCenter.SatStart3.Value.TimeOfDay).TotalMinutes;
                    if (workCenter.SatStart4.HasValue && workCenter.SatStop4.HasValue)
                        min += workCenter.SatStop4.Value.TimeOfDay.Subtract(workCenter.SatStart4.Value.TimeOfDay).TotalMinutes;
                    break;
                case DayOfWeek.Sunday:
                    if (workCenter.SunStart1.HasValue && workCenter.SunStop1.HasValue)
                        min += workCenter.SunStop1.Value.TimeOfDay.Subtract(workCenter.SunStart1.Value.TimeOfDay).TotalMinutes;
                    if (workCenter.SunStart2.HasValue && workCenter.SunStop2.HasValue)
                        min += workCenter.SunStop2.Value.TimeOfDay.Subtract(workCenter.SunStart2.Value.TimeOfDay).TotalMinutes;
                    if (workCenter.SunStart3.HasValue && workCenter.SunStop3.HasValue)
                        min += workCenter.SunStop3.Value.TimeOfDay.Subtract(workCenter.SunStart3.Value.TimeOfDay).TotalMinutes;
                    if (workCenter.SunStart4.HasValue && workCenter.SunStop4.HasValue)
                        min += workCenter.SunStop4.Value.TimeOfDay.Subtract(workCenter.SunStart4.Value.TimeOfDay).TotalMinutes;
                    break;
            }
            return min / 60;
        }

        public double GetWeekWorkHours(WorkCenter workCenter)
        {
            double min = 0;
            if (workCenter.MonStart1.HasValue && workCenter.MonStop1.HasValue)
                min += workCenter.MonStop1.Value.TimeOfDay.Subtract(workCenter.MonStart1.Value.TimeOfDay).TotalMinutes;
            if (workCenter.MonStart2.HasValue && workCenter.MonStop2.HasValue)
                min += workCenter.MonStop2.Value.TimeOfDay.Subtract(workCenter.MonStart2.Value.TimeOfDay).TotalMinutes;
            if (workCenter.MonStart3.HasValue && workCenter.MonStop3.HasValue)
                min += workCenter.MonStop3.Value.TimeOfDay.Subtract(workCenter.MonStart3.Value.TimeOfDay).TotalMinutes;
            if (workCenter.MonStart4.HasValue && workCenter.MonStop4.HasValue)
                min += workCenter.MonStop4.Value.TimeOfDay.Subtract(workCenter.MonStart4.Value.TimeOfDay).TotalMinutes;

            if (workCenter.ThuStart1.HasValue && workCenter.ThuStop1.HasValue)
                min += workCenter.ThuStop1.Value.TimeOfDay.Subtract(workCenter.ThuStart1.Value.TimeOfDay).TotalMinutes;
            if (workCenter.ThuStart2.HasValue && workCenter.ThuStop2.HasValue)
                min += workCenter.ThuStop2.Value.TimeOfDay.Subtract(workCenter.ThuStart2.Value.TimeOfDay).TotalMinutes;
            if (workCenter.ThuStart3.HasValue && workCenter.ThuStop3.HasValue)
                min += workCenter.ThuStop3.Value.TimeOfDay.Subtract(workCenter.ThuStart3.Value.TimeOfDay).TotalMinutes;
            if (workCenter.ThuStart4.HasValue && workCenter.ThuStop4.HasValue)
                min += workCenter.ThuStop4.Value.TimeOfDay.Subtract(workCenter.ThuStart4.Value.TimeOfDay).TotalMinutes;

            if (workCenter.WedStart1.HasValue && workCenter.WedStop1.HasValue)
                min += workCenter.WedStop1.Value.TimeOfDay.Subtract(workCenter.WedStart1.Value.TimeOfDay).TotalMinutes;
            if (workCenter.WedStart2.HasValue && workCenter.WedStop2.HasValue)
                min += workCenter.WedStop2.Value.TimeOfDay.Subtract(workCenter.WedStart2.Value.TimeOfDay).TotalMinutes;
            if (workCenter.WedStart3.HasValue && workCenter.WedStop3.HasValue)
                min += workCenter.WedStop3.Value.TimeOfDay.Subtract(workCenter.WedStart3.Value.TimeOfDay).TotalMinutes;
            if (workCenter.WedStart4.HasValue && workCenter.WedStop4.HasValue)
                min += workCenter.WedStop4.Value.TimeOfDay.Subtract(workCenter.WedStart4.Value.TimeOfDay).TotalMinutes;

            if (workCenter.TueStart1.HasValue && workCenter.TueStop1.HasValue)
                min += workCenter.TueStop1.Value.TimeOfDay.Subtract(workCenter.TueStart1.Value.TimeOfDay).TotalMinutes;
            if (workCenter.TueStart2.HasValue && workCenter.TueStop2.HasValue)
                min += workCenter.TueStop2.Value.TimeOfDay.Subtract(workCenter.TueStart2.Value.TimeOfDay).TotalMinutes;
            if (workCenter.TueStart3.HasValue && workCenter.TueStop3.HasValue)
                min += workCenter.TueStop3.Value.TimeOfDay.Subtract(workCenter.TueStart3.Value.TimeOfDay).TotalMinutes;
            if (workCenter.TueStart4.HasValue && workCenter.TueStop4.HasValue)
                min += workCenter.TueStop4.Value.TimeOfDay.Subtract(workCenter.TueStart4.Value.TimeOfDay).TotalMinutes;

            if (workCenter.FriStart1.HasValue && workCenter.FriStop1.HasValue)
                min += workCenter.FriStop1.Value.TimeOfDay.Subtract(workCenter.FriStart1.Value.TimeOfDay).TotalMinutes;
            if (workCenter.FriStart2.HasValue && workCenter.FriStop2.HasValue)
                min += workCenter.FriStop2.Value.TimeOfDay.Subtract(workCenter.FriStart2.Value.TimeOfDay).TotalMinutes;
            if (workCenter.FriStart3.HasValue && workCenter.FriStop3.HasValue)
                min += workCenter.FriStop3.Value.TimeOfDay.Subtract(workCenter.FriStart3.Value.TimeOfDay).TotalMinutes;
            if (workCenter.FriStart4.HasValue && workCenter.FriStop4.HasValue)
                min += workCenter.FriStop4.Value.TimeOfDay.Subtract(workCenter.FriStart4.Value.TimeOfDay).TotalMinutes;

            if (workCenter.SatStart1.HasValue && workCenter.SatStop1.HasValue)
                min += workCenter.SatStop1.Value.TimeOfDay.Subtract(workCenter.SatStart1.Value.TimeOfDay).TotalMinutes;
            if (workCenter.SatStart2.HasValue && workCenter.SatStop2.HasValue)
                min += workCenter.SatStop2.Value.TimeOfDay.Subtract(workCenter.SatStart2.Value.TimeOfDay).TotalMinutes;
            if (workCenter.SatStart3.HasValue && workCenter.SatStop3.HasValue)
                min += workCenter.SatStop3.Value.TimeOfDay.Subtract(workCenter.SatStart3.Value.TimeOfDay).TotalMinutes;
            if (workCenter.SatStart4.HasValue && workCenter.SatStop4.HasValue)
                min += workCenter.SatStop4.Value.TimeOfDay.Subtract(workCenter.SatStart4.Value.TimeOfDay).TotalMinutes;

            if (workCenter.SunStart1.HasValue && workCenter.SunStop1.HasValue)
                min += workCenter.SunStop1.Value.TimeOfDay.Subtract(workCenter.SunStart1.Value.TimeOfDay).TotalMinutes;
            if (workCenter.SunStart2.HasValue && workCenter.SunStop2.HasValue)
                min += workCenter.SunStop2.Value.TimeOfDay.Subtract(workCenter.SunStart2.Value.TimeOfDay).TotalMinutes;
            if (workCenter.SunStart3.HasValue && workCenter.SunStop3.HasValue)
                min += workCenter.SunStop3.Value.TimeOfDay.Subtract(workCenter.SunStart3.Value.TimeOfDay).TotalMinutes;
            if (workCenter.SunStart4.HasValue && workCenter.SunStop4.HasValue)
                min += workCenter.SunStop4.Value.TimeOfDay.Subtract(workCenter.SunStart4.Value.TimeOfDay).TotalMinutes;


            return min / 60;
        }

        public Tuple<double, double, double, double> GetDayWorkDist(int workCenterKey, DayOfWeek day)
        {
            return GetDayWorkDist(GetWorkCenter(workCenterKey), day);
        }

        public Tuple<double, double, double, double> GetDayWorkDist(WorkCenter workCenter, DayOfWeek day)
        {
            double min = 0;
            switch (day)
            {
                case DayOfWeek.Monday:
                    if (workCenter.MonStart1.HasValue && workCenter.MonStop1.HasValue)
                        min += workCenter.MonStop1.Value.TimeOfDay.Subtract(workCenter.MonStart1.Value.TimeOfDay).TotalMinutes;

                    if (workCenter.MonStart2.HasValue && workCenter.MonStop2.HasValue)
                        min += workCenter.MonStop2.Value.TimeOfDay.Subtract(workCenter.MonStart2.Value.TimeOfDay).TotalMinutes;

                    if (workCenter.MonStart3.HasValue && workCenter.MonStop3.HasValue)
                        min += workCenter.MonStop3.Value.TimeOfDay.Subtract(workCenter.MonStart3.Value.TimeOfDay).TotalMinutes;

                    if (workCenter.MonStart4.HasValue && workCenter.MonStop4.HasValue)
                        min += workCenter.MonStop4.Value.TimeOfDay.Subtract(workCenter.MonStart4.Value.TimeOfDay).TotalMinutes;

                    return new Tuple<double, double, double, double>(
                        workCenter.MonStart1.HasValue && workCenter.MonStop1.HasValue ? workCenter.MonStop1.Value.TimeOfDay.Subtract(workCenter.MonStart1.Value.TimeOfDay).TotalMinutes * 100 / min : 0,
                        workCenter.MonStart2.HasValue && workCenter.MonStop2.HasValue ? workCenter.MonStop2.Value.TimeOfDay.Subtract(workCenter.MonStart2.Value.TimeOfDay).TotalMinutes * 100 / min : 0,
                        workCenter.MonStart3.HasValue && workCenter.MonStop3.HasValue ? workCenter.MonStop3.Value.TimeOfDay.Subtract(workCenter.MonStart3.Value.TimeOfDay).TotalMinutes * 100 / min : 0,
                        workCenter.MonStart4.HasValue && workCenter.MonStop4.HasValue ? workCenter.MonStop4.Value.TimeOfDay.Subtract(workCenter.MonStart4.Value.TimeOfDay).TotalMinutes * 100 / min : 0
                        );
                case DayOfWeek.Thursday:
                    if (workCenter.ThuStart1.HasValue && workCenter.ThuStop1.HasValue)
                        min += workCenter.ThuStop1.Value.TimeOfDay.Subtract(workCenter.ThuStart1.Value.TimeOfDay).TotalMinutes;

                    if (workCenter.ThuStart2.HasValue && workCenter.ThuStop2.HasValue)
                        min += workCenter.ThuStop2.Value.TimeOfDay.Subtract(workCenter.ThuStart2.Value.TimeOfDay).TotalMinutes;

                    if (workCenter.ThuStart3.HasValue && workCenter.ThuStop3.HasValue)
                        min += workCenter.ThuStop3.Value.TimeOfDay.Subtract(workCenter.ThuStart3.Value.TimeOfDay).TotalMinutes;

                    if (workCenter.ThuStart4.HasValue && workCenter.ThuStop4.HasValue)
                        min += workCenter.ThuStop4.Value.TimeOfDay.Subtract(workCenter.ThuStart4.Value.TimeOfDay).TotalMinutes;

                    return new Tuple<double, double, double, double>(
                                            workCenter.ThuStart1.HasValue && workCenter.ThuStop1.HasValue ? workCenter.ThuStop1.Value.TimeOfDay.Subtract(workCenter.ThuStart1.Value.TimeOfDay).TotalMinutes * 100 / min : 0,
                                            workCenter.ThuStart2.HasValue && workCenter.ThuStop2.HasValue ? workCenter.ThuStop2.Value.TimeOfDay.Subtract(workCenter.ThuStart2.Value.TimeOfDay).TotalMinutes * 100 / min : 0,
                                            workCenter.ThuStart3.HasValue && workCenter.ThuStop3.HasValue ? workCenter.ThuStop3.Value.TimeOfDay.Subtract(workCenter.ThuStart3.Value.TimeOfDay).TotalMinutes * 100 / min : 0,
                                            workCenter.ThuStart4.HasValue && workCenter.ThuStop4.HasValue ? workCenter.ThuStop4.Value.TimeOfDay.Subtract(workCenter.ThuStart4.Value.TimeOfDay).TotalMinutes * 100 / min : 0
                                            );
                case DayOfWeek.Wednesday:
                    if (workCenter.WedStart1.HasValue && workCenter.WedStop1.HasValue)
                        min += workCenter.WedStop1.Value.TimeOfDay.Subtract(workCenter.WedStart1.Value.TimeOfDay).TotalMinutes;

                    if (workCenter.WedStart2.HasValue && workCenter.WedStop2.HasValue)
                        min += workCenter.WedStop2.Value.TimeOfDay.Subtract(workCenter.WedStart2.Value.TimeOfDay).TotalMinutes;

                    if (workCenter.WedStart3.HasValue && workCenter.WedStop3.HasValue)
                        min += workCenter.WedStop3.Value.TimeOfDay.Subtract(workCenter.WedStart3.Value.TimeOfDay).TotalMinutes;

                    if (workCenter.WedStart4.HasValue && workCenter.WedStop4.HasValue)
                        min += workCenter.WedStop4.Value.TimeOfDay.Subtract(workCenter.WedStart4.Value.TimeOfDay).TotalMinutes;
                    return new Tuple<double, double, double, double>(
                                            workCenter.WedStart1.HasValue && workCenter.WedStop1.HasValue ? workCenter.WedStop1.Value.TimeOfDay.Subtract(workCenter.WedStart1.Value.TimeOfDay).TotalMinutes * 100 / min : 0,
                                            workCenter.WedStart2.HasValue && workCenter.WedStop2.HasValue ? workCenter.WedStop2.Value.TimeOfDay.Subtract(workCenter.WedStart2.Value.TimeOfDay).TotalMinutes * 100 / min : 0,
                                            workCenter.WedStart3.HasValue && workCenter.WedStop3.HasValue ? workCenter.WedStop3.Value.TimeOfDay.Subtract(workCenter.WedStart3.Value.TimeOfDay).TotalMinutes * 100 / min : 0,
                                            workCenter.WedStart4.HasValue && workCenter.WedStop4.HasValue ? workCenter.WedStop4.Value.TimeOfDay.Subtract(workCenter.WedStart4.Value.TimeOfDay).TotalMinutes * 100 / min : 0
                                            );
                case DayOfWeek.Tuesday:
                    if (workCenter.TueStart1.HasValue && workCenter.TueStop1.HasValue)
                    {
                        min += workCenter.TueStop1.Value.TimeOfDay.Subtract(workCenter.TueStart1.Value.TimeOfDay).TotalMinutes;
                    }

                    if (workCenter.TueStart2.HasValue && workCenter.TueStop2.HasValue)
                    {
                        min += workCenter.TueStop2.Value.TimeOfDay.Subtract(workCenter.TueStart2.Value.TimeOfDay).TotalMinutes;
                    }

                    if (workCenter.TueStart3.HasValue && workCenter.TueStop3.HasValue)
                    {
                        min += workCenter.TueStop3.Value.TimeOfDay.Subtract(workCenter.TueStart3.Value.TimeOfDay).TotalMinutes;
                    }

                    if (workCenter.TueStart4.HasValue && workCenter.TueStop4.HasValue)
                    {
                        min += workCenter.TueStop4.Value.TimeOfDay.Subtract(workCenter.TueStart4.Value.TimeOfDay).TotalMinutes;
                    }
                    return new Tuple<double, double, double, double>(
                                            workCenter.TueStart1.HasValue && workCenter.TueStop1.HasValue ? workCenter.TueStop1.Value.TimeOfDay.Subtract(workCenter.TueStart1.Value.TimeOfDay).TotalMinutes * 100 / min : 0,
                                            workCenter.TueStart2.HasValue && workCenter.TueStop2.HasValue ? workCenter.TueStop2.Value.TimeOfDay.Subtract(workCenter.TueStart2.Value.TimeOfDay).TotalMinutes * 100 / min : 0,
                                            workCenter.TueStart3.HasValue && workCenter.TueStop3.HasValue ? workCenter.TueStop3.Value.TimeOfDay.Subtract(workCenter.TueStart3.Value.TimeOfDay).TotalMinutes * 100 / min : 0,
                                            workCenter.TueStart4.HasValue && workCenter.TueStop4.HasValue ? workCenter.TueStop4.Value.TimeOfDay.Subtract(workCenter.TueStart4.Value.TimeOfDay).TotalMinutes * 100 / min : 0
                                            );
                case DayOfWeek.Friday:
                    if (workCenter.FriStart1.HasValue && workCenter.FriStop1.HasValue)
                    {
                        min += workCenter.FriStop1.Value.TimeOfDay.Subtract(workCenter.FriStart1.Value.TimeOfDay).TotalMinutes;
                    }

                    if (workCenter.FriStart2.HasValue && workCenter.FriStop2.HasValue)
                    {
                        min += workCenter.FriStop2.Value.TimeOfDay.Subtract(workCenter.FriStart2.Value.TimeOfDay).TotalMinutes;
                    }

                    if (workCenter.FriStart3.HasValue && workCenter.FriStop3.HasValue)
                    {
                        min += workCenter.FriStop3.Value.TimeOfDay.Subtract(workCenter.FriStart3.Value.TimeOfDay).TotalMinutes;
                    }

                    if (workCenter.FriStart4.HasValue && workCenter.FriStop4.HasValue)
                    {
                        min += workCenter.FriStop4.Value.TimeOfDay.Subtract(workCenter.FriStart4.Value.TimeOfDay).TotalMinutes;
                    }
                    return new Tuple<double, double, double, double>(
                                            workCenter.FriStart1.HasValue && workCenter.FriStop1.HasValue ? workCenter.FriStop1.Value.TimeOfDay.Subtract(workCenter.FriStart1.Value.TimeOfDay).TotalMinutes * 100 / min : 0,
                                            workCenter.FriStart2.HasValue && workCenter.FriStop2.HasValue ? workCenter.FriStop2.Value.TimeOfDay.Subtract(workCenter.FriStart2.Value.TimeOfDay).TotalMinutes * 100 / min : 0,
                                            workCenter.FriStart3.HasValue && workCenter.FriStop3.HasValue ? workCenter.FriStop3.Value.TimeOfDay.Subtract(workCenter.FriStart3.Value.TimeOfDay).TotalMinutes * 100 / min : 0,
                                            workCenter.FriStart4.HasValue && workCenter.FriStop4.HasValue ? workCenter.FriStop4.Value.TimeOfDay.Subtract(workCenter.FriStart4.Value.TimeOfDay).TotalMinutes * 100 / min : 0
                                            );
                case DayOfWeek.Saturday:
                    if (workCenter.SatStart1.HasValue && workCenter.SatStop1.HasValue)
                    {
                        min += workCenter.SatStop1.Value.TimeOfDay.Subtract(workCenter.SatStart1.Value.TimeOfDay).TotalMinutes;
                    }

                    if (workCenter.SatStart2.HasValue && workCenter.SatStop2.HasValue)
                    {
                        min += workCenter.SatStop2.Value.TimeOfDay.Subtract(workCenter.SatStart2.Value.TimeOfDay).TotalMinutes;
                    }

                    if (workCenter.SatStart3.HasValue && workCenter.SatStop3.HasValue)
                    {
                        min += workCenter.SatStop3.Value.TimeOfDay.Subtract(workCenter.SatStart3.Value.TimeOfDay).TotalMinutes;
                    }

                    if (workCenter.SatStart4.HasValue && workCenter.SatStop4.HasValue)
                    {
                        min += workCenter.SatStop4.Value.TimeOfDay.Subtract(workCenter.SatStart4.Value.TimeOfDay).TotalMinutes;
                    }
                    return new Tuple<double, double, double, double>(
                                            workCenter.SatStart1.HasValue && workCenter.SatStop1.HasValue ? workCenter.SatStop1.Value.TimeOfDay.Subtract(workCenter.SatStart1.Value.TimeOfDay).TotalMinutes * 100 / min : 0,
                                            workCenter.SatStart2.HasValue && workCenter.SatStop2.HasValue ? workCenter.SatStop2.Value.TimeOfDay.Subtract(workCenter.SatStart2.Value.TimeOfDay).TotalMinutes * 100 / min : 0,
                                            workCenter.SatStart3.HasValue && workCenter.SatStop3.HasValue ? workCenter.SatStop3.Value.TimeOfDay.Subtract(workCenter.SatStart3.Value.TimeOfDay).TotalMinutes * 100 / min : 0,
                                            workCenter.SatStart4.HasValue && workCenter.SatStop4.HasValue ? workCenter.SatStop4.Value.TimeOfDay.Subtract(workCenter.SatStart4.Value.TimeOfDay).TotalMinutes * 100 / min : 0
                                            );
                case DayOfWeek.Sunday:
                    if (workCenter.SunStart1.HasValue && workCenter.SunStop1.HasValue)
                    {
                        min += workCenter.SunStop1.Value.TimeOfDay.Subtract(workCenter.SunStart1.Value.TimeOfDay).TotalMinutes;
                    }

                    if (workCenter.SunStart2.HasValue && workCenter.SunStop2.HasValue)
                    {
                        min += workCenter.SunStop2.Value.TimeOfDay.Subtract(workCenter.SunStart2.Value.TimeOfDay).TotalMinutes;
                    }

                    if (workCenter.SunStart3.HasValue && workCenter.SunStop3.HasValue)
                    {
                        min += workCenter.SunStop3.Value.TimeOfDay.Subtract(workCenter.SunStart3.Value.TimeOfDay).TotalMinutes;
                    }

                    if (workCenter.SunStart4.HasValue && workCenter.SunStop4.HasValue)
                    {
                        min += workCenter.SunStop4.Value.TimeOfDay.Subtract(workCenter.SunStart4.Value.TimeOfDay).TotalMinutes;
                    }
                    return new Tuple<double, double, double, double>(
                                            workCenter.SunStart1.HasValue && workCenter.SunStop1.HasValue ? workCenter.SunStop1.Value.TimeOfDay.Subtract(workCenter.SunStart1.Value.TimeOfDay).TotalMinutes * 100 / min : 0,
                                            workCenter.SunStart2.HasValue && workCenter.SunStop2.HasValue ? workCenter.SunStop2.Value.TimeOfDay.Subtract(workCenter.SunStart2.Value.TimeOfDay).TotalMinutes * 100 / min : 0,
                                            workCenter.SunStart3.HasValue && workCenter.SunStop3.HasValue ? workCenter.SunStop3.Value.TimeOfDay.Subtract(workCenter.SunStart3.Value.TimeOfDay).TotalMinutes * 100 / min : 0,
                                            workCenter.SunStart4.HasValue && workCenter.SunStop4.HasValue ? workCenter.SunStop4.Value.TimeOfDay.Subtract(workCenter.SunStart4.Value.TimeOfDay).TotalMinutes * 100 / min : 0
                                            );
            }

            return null;
        }
    }
}
