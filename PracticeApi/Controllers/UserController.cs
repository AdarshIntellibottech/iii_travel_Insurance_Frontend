using Microsoft.AspNetCore.Mvc;
using System.Net;
using static TravelInsuranceAPI.Models.EmployeeClass;
using TravelInsuranceAPI.Models;
using TravelInsuranceAPI.Services.IRepository;
using log4net.Core;
using NuGet.Protocol.Core.Types;
using static TravelInsuranceAPI.Models.AuthenticateResponse;

namespace TravelInsuranceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        // private readonly ILoggerManager logger;
        private readonly IUserRepository _repository;
        private string path;
        public UserController(IConfiguration configuration, IUserRepository repository)
        {
            Configuration = configuration;

            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            path = Configuration["ErrorTextFile"];

        }



        [HttpPost("~/user/Authenticate")]
        public async Task<ActionResult<APIResponse>> Authenticate(AuthenticateRequest model)
        //public async Task<ActionResult<TokenAPIResponse>> Authenticate(AuthenticateRequest model)
        {
            try
            {
                var result = await _repository.Authenticate(model);
                if (result.isSuccess)
                    return Ok(result);
                else
                    return new BadRequestObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
    }
}
