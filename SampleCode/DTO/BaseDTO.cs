using System.Linq;
using System.Text;

namespace SampleCode.DTO
{
    public abstract class BaseDTO
    {        
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