using Jikong.common;
using Jikong.login;
using Utils;

namespace Jikong.appointment
{
    internal class YuyueHpvContent : JikongContent
    {
        private static string url = "https://hscx.whcdc.org/vaccineServer/RegApiManage/lockNumberForHpv";
        public Order Order { get; private set; }
        public YuyueHpvContent(Order order, JikongLogin user) : base(url, user)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("itemName", Order.ItemName);
            AddContent("itemCode", Order.ItemCode);
            AddContent("minuteHourRegTotal", Order.MinuteHourRegTotal);
            AddContent("minuteHourRegOdd", Order.MinuteHourRegOdd);
            AddContent("visitDate", Order.VisitDate);
            AddContent("visitTime", Order.VisitTime);
            AddContent("amOrPm", Order.AmOrPm);
            AddContent("patientId", Order.UserId);
            AddContent("scheduleCode", Order.ScheduleCode);
            AddContent("scheduleInfoCode", Order.ScheduleInfoCode);
            AddContent("checkType", Order.CheckType);
            AddContent("checkCode", "03AFY_a8VeKAaHJ5JPLSK-xCKyJrdA3pmOH9wAAia6I_zqzNrhh7zJfRp3gU7xD3X8CTZT575YnmQDIxdYKUjblnED2knCsO8BdpoEaFeLEDNeVFnseI2XWfO9KjJC3Fn79fAJdlWnB9Y897wKNL08js5z1Wqq8AHOawhzB0ccGmZqVbWSd4MQFOxYjZxyE5EroKTXni-H9MH9YVGARhiTK-0y9yAt7SXp1t-avrEsXJOkAs5escpDWSnKGhEc7NcMLYKOMOc4ZFUbqC1vl_dR_kEOB7ZPR9LxyZIY9to25PsE3dq3joThGyLJVfNdIYeqwSBaBbiP4QHodoXF1fgO0Iv9Od_y6ioveJSr6MDCdJDlElcHz7B4zbv9Uz93DAsZROgWaPsTNyQuyEnCjgI9Fmfm-Z9hd8K333iQWtRlGqyzpGCYHn20SWgJYtQPJ36THAK9qWpTYGnqibuPAazn5q06kLKWOcz0Wh44Ma7U2rHiVDMGIE3gPFOY-ScW5P3uwL3o510sC6Br9YVfCKR1ykW5OVTFID14M8L5KoqyGH3BlOOyRvsqZsg");
            AddContent("type", Order.Type);
        }
    }
}
