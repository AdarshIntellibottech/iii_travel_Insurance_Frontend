using Microsoft.AspNetCore.Mvc;
using System.Net;
using static TravelInsuranceAPI.Models.EmployeeClass;
using TravelInsuranceAPI.Services.Repositories;
using TravelInsuranceAPI.Services.IRepositories;
using log4net.Core;


namespace TravelInsuranceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelController : ControllerBase

    {
        List<Emp> emp_info = new List<Emp>();
        private readonly IConfiguration Configuration;
        // private readonly ILoggerManager logger;
        private readonly ITravelRepository _repository;
        private string path;
        public TravelController(IConfiguration configuration, ITravelRepository repository)
        {
            Configuration = configuration;

            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            path = Configuration["ErrorTextFile"];

        }


        [HttpGet("GetEmployee")]
        //[AuthorizeToken]
        [ProducesResponseType(typeof(APIResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<APIResponse>> GetEmpDetails(int id)
        {
            try
            {
                var result = await _repository.GetEmpDetails(id);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("CreateEmployee")]
       
        [ProducesResponseType(typeof(APIResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<APIResponse>> CreateEmpDetails([FromBody] emp_info Emp)
        {

            try
            {
                
                var result = await _repository.CreateEmpDetails(Emp);
                return Ok(result);
                
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpPut("UpdateEmployee")]
        [ProducesResponseType(typeof(APIResponse), (int)HttpStatusCode.OK)]
        //[AuthorizeToken]
        public async Task<ActionResult<APIResponse>> UpdateEmpDetails([FromBody] emp_info Emp)
        {
            try
            {
                var result = await _repository.UpdateEmpDetails(Emp);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }


        [HttpDelete("DeleteEmployee")]
        [ProducesResponseType(typeof(APIResponse), (int)HttpStatusCode.OK)]
       
        public async Task<ActionResult<APIResponse>> DeleteEmpDetails(int id)
        {
            try
            {
                var result = await _repository.DeleteEmpDetails(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("GetName")]
        
        [ProducesResponseType(typeof(List<APIResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<APIResponse>>> SearchName(string name)
        {
            try
            {
                var result = await _repository.SearchName(name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("StoreWhatsAppInfo")]
        [ProducesResponseType(typeof(APIResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<APIResponse>> CreateUserDetails([FromBody] WhatsAppInfo Info)
        {
            try
            {
                var result = await _repository.CreateUserDetails(Info);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }

}


