using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DBKursovaia.MVVMHelpers
{
    /// <summary>
    /// Observable object with INotifyPropertyChanged implemented
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <returns><c>true</c>, if property was set, <c>false</c> otherwise.</returns>
        /// <param name="backingStore">Backing store.</param>
        /// <param name="value">Value.</param>
        /// <param name="validateValue">Validates value.</param>
        /// <param name="propertyName">Property name.</param>
        /// <param name="onChanged">On changed.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        protected virtual bool SetProperty<T>(
            ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null,
            Func<T, T, bool> validateValue = null)
        {
            //if value didn't change
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            //if value changed but didn't validate
            if (validateValue != null && !validateValue(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }


        /// <summary>
        /// Occurs when property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }

    //public abstract class ObservableObject : INotifyPropertyChanged
    //{
    //    public event PropertyChangedEventHandler PropertyChanged;

    //    private readonly Dictionary<string, object> _backingFieldValues = new Dictionary<string, object>();

    //    /// <summary>
    //    /// Gets a property value from the internal backing field
    //    /// </summary>
    //    protected T GetProperty<T>([CallerMemberName] string propertyName = null)
    //    {
    //        if (_backingFieldValues.TryGetValue(propertyName, out object value))
    //            return value == null ? default(T) : (T)value;
    //        return default(T);
    //    }

    //    /// <summary>
    //    /// Saves a property value to the internal backing field
    //    /// </summary>
    //    protected bool SetProperty<T>(T newValue, [CallerMemberName] string propertyName = null)
    //    {
    //        if (IsEqual(GetProperty<T>(propertyName), newValue)) return false;
    //        _backingFieldValues[propertyName] = newValue;
    //        OnPropertyChanged(propertyName);
    //        return true;
    //    }

    //    /// <summary>
    //    /// Sets a property value to the backing field
    //    /// </summary>
    //    protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
    //    {
    //        if (IsEqual(field, newValue)) return false;
    //        field = newValue;
    //        OnPropertyChanged(propertyName);
    //        return true;
    //    }

    //    protected virtual void OnPropertyChanged<T>(Expression<Func<T>> selectorExpression)
    //    {
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(GetNameFromExpression(selectorExpression)));
    //    }

    //    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    //    {
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //    }

    //    private bool IsEqual<T>(T field, T newValue)
    //    {
    //        return EqualityComparer<T>.Default.Equals(field, newValue);
    //        // Alternative: return Equals(field, newValue);
    //    }

    //    private string GetNameFromExpression<T>(Expression<Func<T>> selectorExpression)
    //    {
    //        var body = (MemberExpression)selectorExpression.Body;
    //        var propertyName = body.Member.Name;
    //        return propertyName;
    //    }
    //}
}
