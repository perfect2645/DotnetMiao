using HuSheng.session;

namespace HuSheng.analysis
{
    internal static class AnalysisMiao
    {
        #region Check Nine

        public static bool IsNine(this string miaoName)
        {
            if (string.IsNullOrEmpty(miaoName))
            {
                return false;
            }

            if (miaoName.Contains(Constants.NineJia))
            {
                return true;
            }

            return false;
        }

        public static bool IsDoseOne(this string miaoName)
        {
            if (string.IsNullOrEmpty(miaoName))
            {
                return false;
            }

            if (miaoName.Contains(Constants.DoseOne))
            {
                return true;
            }

            return false;
        }

        #endregion Check Nine
    }
}
