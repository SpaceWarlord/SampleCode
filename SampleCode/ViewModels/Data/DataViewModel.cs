using CommunityToolkit.Mvvm.ComponentModel;
using SampleCode.ViewModels.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCode.ViewModels.Data
{
    public abstract partial class DataViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the person's ID.
        /// </summary>

        [ObservableProperty]
        private int _id;
        
        public override string ToString()
        {
            return GetType().GetProperties()
        .Select(info => (info.Name, Value: info.GetValue(this, null) ?? "(null)"))
        .Aggregate(
            new StringBuilder(),
            (sb, pair) => sb.AppendLine($"{pair.Name}: {pair.Value}"),
            sb => sb.ToString());
        }
    }
}
