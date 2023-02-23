namespace TravelInsuranceAPI.Models
{
    public class EmployeeClass

    {
        public class Emp
        {
            public int id { get; set; }

            public string name { get; set; }

            public string address { get; set; }
            public string department { get; set; }

            internal void Remove(Emp employee)
            {
                throw new NotImplementedException();
            }
        }
    }
}

