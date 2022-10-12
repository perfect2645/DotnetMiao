namespace Base.model
{
    public class DspVal
    {
        public DspVal(string dsp, string value)
        {
            Display = dsp;
            Value = value;
        }

        public DspVal(string dsp)
        {
            Display = dsp;
            Value = dsp;
        }
        public string Display { get; set; }
        public string Value { get; set; }
    }
}
