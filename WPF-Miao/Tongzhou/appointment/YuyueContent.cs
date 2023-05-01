using System.Collections.Generic;
using Tongzhou.common;
using Tongzhou.login;
using Utils;

namespace Tongzhou.appointment
{
    internal class YuyueContent : TongzhouPostContent
    {
        public Order Order { get; private set; }
        public YuyueContent(Order order, TongzhouLogin user) : base("addAppointRecord", "appoint.requestAppointRecordService", user)
        {
            Order = order;
            BuildContent();
            BuildSignHeaders();
        }

        private void BuildContent()
        {
            AddContent("mpiid", Order.MpiId);
            AddContent("patientName", Order.UserName);
            AddContent("organAppointId", Order.OrganAppointId);
            AddContent("scheduleId", Order.ScheduleId);
            AddContent("scheduleTimeId", Order.ScheduleTimeId);
            AddContent("orderNumSopt", Order.OrderNumSopt);
            AddContent("organId", Order.OrganId);
            AddContent("appointDepartId", Order.AppointDepartId);
            AddContent("appointDepartName", Order.AppointDepartName);
            AddContent("doctorId", Order.DoctorId);
            AddContent("workDate", Order.WorkDate);
            AddContent("workType", Order.WorkType);
            AddContent("startTime", Order.StartTime);
            AddContent("endTime", Order.EndTime);
            AddContent("orderNum", Order.OrderNum);
            AddContent("appointRoad", Order.AppointRoad);
            AddContent("appointStatus", Order.AppointStatus);
            AddContent("appointPath", Order.AppointPath);
            AddContent("appointUser", Order.AppointUser);
            AddContent("appointName", Order.AppointName);
            AddContent("appointOragn", Order.AppointOragn);
            AddContent("clinicPrice", Order.ClinicPrice);
            AddContent("transferId", Order.TransferId);
            AddContent("sourceLevel", Order.SourceLevel);
            AddContent("clinicId", Order.ClinicId);
            AddContent("ifCreateFollowPlan", Order.IfCreateFollowPlan);
            AddContent("cardId", Order.CardId);
            AddContent("triggerId", Order.TriggerId);
            AddContent("analyzeNvcData", Order.AnalyzeNvcData);
            AddContent("ruleString", Order.RuleString);
            AddContent("isRealTime", Order.IsRealTime);
            AddContent("cardType", Order.CardType);
            AddContent("appointRecordExt", BuildRecordExt());
        }

        private Dictionary<string, object> BuildRecordExt()
        {
            var recordExt = new Dictionary<string, object>();
            recordExt.AddOrUpdate("illSummaryTxt", Order.IllSummaryTxt);
            recordExt.AddOrUpdate("thirdChannel", Order.ThirdChannel);

            return recordExt;
        }
    }
}
