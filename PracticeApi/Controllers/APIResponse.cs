namespace TravelInsuranceAPI.Controllers
{
    public class APIResponse
    {
        public bool isSuccess { get; set; }
        public string ErrorMessage { get; set; }

        public string name { get; set; }

        public string savedToDb { get; set; }


        public dynamic result { get; set; }
        public string statuscode { get; set; }



        public decimal TotalPages { get; set; }
    }
}
