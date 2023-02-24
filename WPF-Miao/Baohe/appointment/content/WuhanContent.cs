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
            //DoctorOrder.AddOrUpdate("GH_HosProId", "10");
            //DoctorOrder.AddOrUpdate("GH_HosProName", "江苏");
            //DoctorOrder.AddOrUpdate("GH_HosCityId", "77");
            //DoctorOrder.AddOrUpdate("GH_HosCityName", "南京");
        }

        protected override void BuildNumberDoctorOrder()
        {
            base.BuildNumberDoctorOrder();
            DoctorOrder.AddOrUpdate(Constant.WaitingInfor, $"{MiaoInfo["CommendScope"]}");
        }
    }
}
