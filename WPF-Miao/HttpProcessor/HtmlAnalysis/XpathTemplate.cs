namespace HttpProcessor.HtmlAnalysis
{
    public static class XpathTemplate
    {
        public static string IdPath(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return string.Empty;
            }

            return XpathConst.Id.Replace(XpathConst.PlaceHolder, id);
        }

        public static string NamePath(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }

            return XpathConst.Name.Replace(XpathConst.PlaceHolder, name);
        }
    }
}
