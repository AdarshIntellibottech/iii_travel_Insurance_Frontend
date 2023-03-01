using TravelInsuranceAPI.Controllers;
using TravelInsuranceAPI.Models;

namespace TravelInsuranceAPI.Services.IRepository
{
    public interface IUserRepository
    {
        Task<APIResponse> Authenticate(AuthenticateRequest model);
        //Task<TokenAPIResponse> Authenticate(AuthenticateRequest model);
    }
}
