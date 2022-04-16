using ProcessModel.Vaccine;

namespace ProcessModel.Inventory
{
    internal class VInventory
    {
        public VaccineInfo VaccineInfo { get; set; }

        public ushort AvaCount { get; set; }

        public VInventory()
        {
            VaccineInfo = new VaccineInfo();
            AvaCount = 0;
        }
    }
}
