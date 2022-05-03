using System.Text.RegularExpressions;

namespace challenge2.Biz
{
    public class StringincrementerService : IStringincrementerService
    {
        public StringincrementerService()
        {
        }
        public string CalculateIncreaseFromString(string inputString)
        {
            string response = string.Empty;
            if (!string.IsNullOrWhiteSpace(inputString))
                {
                string pattern = @"\d*[^0]*[0-9]$";
                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                var numberValue = regex.Match(inputString);
                if (!string.IsNullOrWhiteSpace(numberValue.Value) && decimal.TryParse(numberValue.Value, out decimal number))
                {
                    pattern = @"[a0-z9]*[a-zA-Z]+[0]*[^1-9]";
                    regex = new Regex(pattern, RegexOptions.IgnoreCase);
                    var stringValue = regex.Match(inputString);
                    number++;
                    response = $"{stringValue.Value}{number}";
                }
                else
                {
                    response = $"{inputString}1";
                }
            }
            return response;
        }
    }
}