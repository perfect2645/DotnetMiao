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
            AddHeader(Constants.MedicToken, MainSession.PlatformSession[Constants.PublicKey].NotNullString());
        }

        private void BuildContent()
        {
            AddContent(MainSession.PlatformSession, Constants.OpenId);
            AddContent(MainSession.PlatformSession, Constants.HospitalId);
        }
    }
}
