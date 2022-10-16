using Utils;

namespace Tools.Encode
{
    internal class TestHmacSHA256
    {
        public TestHmacSHA256()
        {
            DarunfaEn();
        }

        private void DarunfaEn()
        {
            var data = "%7B%22apiVersion%22%3A%22t141%22%2C%22appVersion%22%3A%221.5.1%22%2C%22areaCode%22%3A%22CS000016%22%2C%22channel%22%3A%22online%22%2C%22clientid%22%3A%22a7ea53059fc868e2e3e2dd7c04027035%22%2C%22device_id%22%3A%22z3tGgpTr98k8dQdVg6xeeSpiAMOQEPtRuq3x%22%2C%22time%22%3A1665910639595%2C%22reRule%22%3A%224%22%2C%22token%22%3A%22c5d372fd360d5f7f2dbdae25e08cb394%22%2C%22viewSize%22%3A%22720x1184%22%2C%22networkType%22%3A%22wifi%22%2C%22isSimulator%22%3Afalse%2C%22osType%22%3A%224%22%2C%22scopeType%22%3A1%2C%22businessType%22%3A2%2C%22businessId%22%3A%2230520001%22%2C%22deliveryCircleType%22%3A%222%22%2C%22body%22%3A%7B%22store_id%22%3A%223052%22%2C%22addrId%22%3A%223A500128-F309-42D9-8A35-00C6E3A0D9E4%22%2C%22pay_code%22%3A%2245%22%2C%22pay_pwd%22%3A%22%22%2C%22phone_model%22%3A%22iPhone%20X%20(GSM%2BCDMA)%3CiPhone10%2C3%3E%22%2C%22orderNotesId%22%3A%22%22%2C%22preCashCard%22%3A%22%22%2C%22freshVoucherSeqs%22%3A%7B%7D%2C%22itemsVoucherSeqs%22%3A%7B%7D%2C%22proofSeqs%22%3A%7B%7D%2C%22deductVoucherSeqs%22%3A%7B%7D%2C%22packageExpress%22%3A%7B%223052%2Bday%22%3A%7B%22delivery_day%22%3A%222022-10-17%22%2C%22delivery_time%22%3A%2218%3A00-21%3A00%22%2C%22packageName%22%3A%22%E6%B3%89%E6%B0%B4%E5%BA%97-%E5%8D%8A%E6%97%A5%E8%BE%BE%22%2C%22packageId%22%3A%223052%2Bday%22%2C%22dayTxt%22%3A%22%E6%98%8E%E6%97%A5%22%2C%22dateTxt%22%3A%222022-10-17%22%2C%22timeTxt%22%3A%2218%3A00-21%3A00%22%2C%22oreTimeTxt%22%3A%2218%3A00-21%3A00%20(6%E5%85%83%E9%85%8D%E9%80%81%E8%B4%B9)%22%2C%22dayTabIndex%22%3A0%2C%22dayListScroll%22%3A303%2C%22chooseTimeTxt%22%3A%22%E6%98%8E%E6%97%A5%2018%3A00-21%3A00%22%7D%7D%7D%7D";

            var dataDecode = UnicodeConverter.Decode(data);

            var isSimulator = "false";

            var netWorktype = "wifi";

            var viewSize = "720x1184";

            var time = 1665910639595;

            var textToEn = $"{dataDecode}{isSimulator.ToString().ToLower()}{viewSize}{netWorktype}{time}";

            var seed = "@yx123*&^DKJ##CC";
            var seed2 = "@456yx#*^&HrUU99";

            var sha256 = Encryptor.HmacSHA256(textToEn, seed2);
            var base64 = Encryptor.EncryptBase64(sha256);
        }
    }
}
