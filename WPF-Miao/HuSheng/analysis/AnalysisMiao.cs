using HuSheng.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        #endregion Check Nine
    }
}
