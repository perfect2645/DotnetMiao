using renren.common;
using renren.session;
using System;
using Utils.stringBuilder;

namespace renren.search.patient
{
    internal class UserContent : RenrenBaseContent
    {
        public UserContent(string url) : base(url)
        {
            BuildHeaders();
            BuildContent();
        }

        private void BuildHeaders()
        {
            AddHeader(Constants.MedicToken, MainSession.PlatformSesstion[Constants.PublicKey].NotNullString());
        }

        private void BuildContent()
        {
            AddContent(MainSession.PlatformSesstion, Constants.OpenId);
            AddContent(MainSession.PlatformSesstion, Constants.HospitalId);
        }
    }
}
