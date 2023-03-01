using TravelInsuranceAPI.Controllers;
using TravelInsuranceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelInsuranceAPI.Controllers;


namespace TravelInsuranceAPI.Services.IRepository
{
    public interface ITravelRepository
    {
        Task<APIResponse> GetEmpDetails(int id);
        Task<APIResponse> CreateEmpDetails(emp_info Emp);
        Task<APIResponse> UpdateEmpDetails(emp_info Emp);
        Task<APIResponse> DeleteEmpDetails(int id);
        Task<APIResponse> CreateUserDetails(WhatsAppInfo Info);

        Task<List<APIResponse>> SearchName(string name);


    }
}


