using Baohe.constants;
using Utils;

namespace Baohe.appointment.content
{
    internal class WuhanContent : AppointmentContent
    {
        public WuhanContent() : base()
        {

        }

        protected override void BuildDefaultDoctorOrder()
        {
            base.BuildDefaultDoctorOrder();
            DoctorOrder.AddOrUpdate("GH_HosProId", "17");
            DoctorOrder.AddOrUpdate("GH_HosProName", "湖北");
            DoctorOrder.AddOrUpdate("GH_HosCityId", "172");
            DoctorOrder.AddOrUpdate("GH_HosCityName", "武汉");
        }

        protected override void BuildNumberDoctorOrder()
        {
            base.BuildNumberDoctorOrder();
            DoctorOrder.AddOrUpdate(Constant.WaitingInfor, $"{MiaoInfo["CommendScope"]}");
        }
    }
}
