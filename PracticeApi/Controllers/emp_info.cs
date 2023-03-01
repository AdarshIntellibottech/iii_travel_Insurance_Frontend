using Microsoft.Build.Framework;

namespace TravelInsuranceAPI.Services.IRepository
{
    public class emp_info
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string department { get; set; }
        public string dob { get; set; }
        public string doj { get; set; }
        
        //internal List<emp_info> ToList()
        //{
        //    throw new NotImplementedException();
        //}
    }
}

