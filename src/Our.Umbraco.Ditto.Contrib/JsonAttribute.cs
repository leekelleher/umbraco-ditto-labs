using Newtonsoft.Json;

namespace Our.Umbraco.Ditto
{
    public class JsonAttribute : CurrentContentAsAttribute
    {
        public override object ProcessValue()
        {
            if (Value == null)
                return null;

            var strVal = (Value is string) == false
                ? JsonConvert.SerializeObject(Value)
                : Value.ToString();

            if (string.IsNullOrWhiteSpace(strVal) || DetectIsJson(strVal) == false)
                return null;

            return JsonConvert.DeserializeObject(strVal, Context.PropertyDescriptor.PropertyType);
        }

        private bool DetectIsJson(string input)
        {
            input = input.Trim();
            return input.StartsWith("{") && input.EndsWith("}")
                || input.StartsWith("[") && input.EndsWith("]");
        }
    }
}