using CommunityToolkit.Mvvm.ComponentModel;
using System.Linq;
using System.Text;

namespace SampleCode.ViewModels.Data
{
    public abstract partial class DataViewModel : BaseViewModel
    {       
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
