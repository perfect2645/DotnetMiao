using Utils;

namespace Darunfa.viewmodel
{
    internal class CartInfo : NotifyChanged
    {
        private string _deliveryDay;
        public string DeliveryDay
        {
            get { return _deliveryDay; }
            set 
            { 
                _deliveryDay = value;
                NotifyUI(() => DeliveryDay);
            }
        }
    }
}
