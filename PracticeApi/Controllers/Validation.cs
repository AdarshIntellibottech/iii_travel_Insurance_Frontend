using Microsoft.AspNetCore.Mvc;
using System.Net;
using static TravelInsuranceAPI.Models.EmployeeClass;
using TravelInsuranceAPI.Services.Repositories;
using TravelInsuranceAPI.Services.IRepositories;
using log4net.Core;
using System.ComponentModel.DataAnnotations;

namespace PracticeApi.Controllers

{
    public class Validate
    {
        public static bool datevalidation(DateTime dob, DateTime doj)
                    {
                        int DiffInYear = doj.Year - dob.Year;
                        if (DiffInYear >= 18)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                       
                    }

        internal static bool datevalidation(DateTime today)
        {
            throw new NotImplementedException();
        }
    }
}

/*internal void dateValidation()
    {
        throw new NotImplementedException();
    }
}*/

