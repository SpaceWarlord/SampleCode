using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Dispatching;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SampleCode.ViewModels
{
    public abstract class BaseViewModel : ObservableValidator
    {

        public DispatcherQueue DispatcherQueue = DispatcherQueue.GetForCurrentThread();

        protected bool _isLoading = false;

        /// <summary>
        /// Gets or sets a value indicating whether the Users list is currently being updated. 
        /// </summary>
        public bool IsLoading
        {
            get => _isLoading;
            //set => Set(ref _isLoading, value);
            set => Set(ref _isLoading, value);
        }

        bool _IsModified = false;

        /// <summary>
        /// Gets or sets a value that indicates whether the underlying model has been modified. 
        /// </summary>   
        /// 

        public bool IsModified
        {
            get => _IsModified;
            set
            {
                if (value != _IsModified)
                {
                    _IsModified = value;
                    OnPropertyChanged();
                    /*
                    // Only record changes after the order has loaded. 
                    if (IsLoaded)
                    {
                        _IsModified = value;
                        OnPropertyChanged();
                        OnPropertyChanged(nameof(CanRevert));
                    }
                    */
                }
            }
        }

        bool _isNew = true;
        /// <summary>
        /// Gets or sets a value that indicates whether this is a new customer.
        /// </summary>
        public bool IsNew
        {
            get => _isNew;
            set
            {
                _isNew = value;
                OnPropertyChanged();
            }
        }

        public BaseViewModel()
        {
            //this.ErrorsChanged += Errors_Changed;
            //this.PropertyChanged += Property_Changed;
            //await dispatcherQueue.EnqueueAsync()
        }

        ~BaseViewModel()
        {
            ErrorsChanged -= Errors_Changed;
            PropertyChanged -= Property_Changed;
        }

        public string Errors => string.Join(Environment.NewLine, from ValidationResult e in GetErrors(null) select e.ErrorMessage);

        public void Property_Changed(object? sender, PropertyChangedEventArgs e)
        {
            //Debug.WriteLine("Changed: " + e.PropertyName);
            //Debug.WriteLine("errors name " + HasErrors.ToString());
            Debug.WriteLine("Errors is " + Errors);
            if (e.PropertyName != nameof(HasErrors))
            {
                OnPropertyChanged(nameof(HasErrors)); // Update HasErrors on every change, so I can bind to it.
            }
        }

        public void Errors_Changed(object? sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            //Debug.WriteLine("vvv " + nameof(Errors));
            Debug.WriteLine("Errors is " + Errors);
            OnPropertyChanged(nameof(Errors)); // Update Errors on every Error change, so I can bind to it.
        }


        /// <summary>
        /// Checks if a property already matches a desired value. Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners. This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected bool Set<T>(ref T storage, T value,
            [CallerMemberName] String propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
