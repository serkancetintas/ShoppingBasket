using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ShoppingBasket.Api.Middlewares.Models
{
    public class ErrorModel
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string IpAddress { get; set; }
        public string UserName { get; set; }
        public string ErrorMessage { get; set; }
        public bool? HasSession { get; set; }
        public string Session { get; set; }

        public Dictionary<string, string> RequestHeader { get; set; }
        public List<KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>> Headers { get; set; }
        public string RequestData { get; set; }

        public Exception Exception { get; set; }
        public DateTime CreatedDateTime { get; set; }

        
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
