using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class NotifyChanged : INotifyPropertyChanged
    {
        protected virtual void NotifyUI(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void NotifyUI<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = (propertyExpression.Body as MemberExpression).Member.Name;
            NotifyUI(propertyName);
        }

        public virtual event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
}
