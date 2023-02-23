using Microsoft.Build.Framework;
using Org.BouncyCastle.Math;

namespace TravelInsuranceAPI.Controllers
{
    public class WhatsAppInfo
    {
        public double policy_id { get; set; }

        public string customer_name { get; set; }

        public string phone_number { get; set; }

        public string payment_status { get; set; }

        public bool whatsapp_notify_by_user { get; set; }

        public bool whatsapp_sent_status { get; set; }

        public string policy_purchase_date { get; set; }

        public string travel_start_date { get; set; }

        public string access_token { get; set; }

        public string grand_code { get; set; }

        
    }
}
