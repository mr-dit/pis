using System.Text.Json;

namespace pis_web_api
{
    public class MyJsonNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            return name;
        }
    }
}
