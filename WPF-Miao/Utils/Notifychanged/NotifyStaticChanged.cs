using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class NotifyStaticChanged
    {
        #region StaticPropertyChanged

        public static event System.ComponentModel.PropertyChangedEventHandler StaticPropertyChanged;

        public static void NotifyStatic(string propertyName)
        {
            if (StaticPropertyChanged != null)
                StaticPropertyChanged(null, new PropertyChangedEventArgs(propertyName));
        }

        public static void NotifyStatic<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = (propertyExpression.Body as MemberExpression).Member.Name;
            NotifyStatic(propertyName);
        }

        #endregion StaticPropertyChanged
    }
}
