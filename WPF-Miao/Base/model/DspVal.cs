using Utils;

namespace Base.model
{
    public class DspVal : NotifyChanged
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

        private string _display;
        public string Display 
        { 
            get { return _display; }
            set 
            { 
                _display = value;
                NotifyUI(() => Display);
            }
        }
        private string _value;
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                NotifyUI(() => Value);
            }
        }
    }
}
