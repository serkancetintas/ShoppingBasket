using Newtonsoft.Json;

namespace ShoppingBasket.Api.Middlewares.Models
{
    public class WarningErrorModel
    {
        public string ErrorCode { get; set; }
        public string NotifyType { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
