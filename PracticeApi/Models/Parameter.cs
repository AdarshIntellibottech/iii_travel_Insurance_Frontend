using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Web.Http.Results;

namespace TravelInsuranceAPI.Models
{
    public class Parameter
    {
        
    }
    public class AppSettings
    {
        public string Secret { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeTokenAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            UserModel user = new UserModel();

            //var user = (UserModel)context.HttpContext.Items["User"];
            if (context.HttpContext.Items["User"] == null)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else
            {
                //Add Code to Check if the User Is active and if true
                user.Id = context.HttpContext.Items["User"].ToString();

            }
        }
    }

    //public class AuthenticateRequest
    //{

    //    public string Id { get; set; }

    //}

    public class AuthenticateRequest
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string phoneNumber { get; set; }
    }

    public class ApiAccessInfo
    {
        
        public string UserID { get; set; }
        public string password { get; set; }
        

        internal List<ApiAccessInfo> ToList()
        {
            throw new NotImplementedException();
        }
    }
    public class TokenRequest
    {
        public string token { get; set; }
        public string userName { get; set; }
        public string phone { get; set; }
        public string password { get; set; }

    }
    public class UserModel
    {
        public string Id { get; set; }
        public string phone { get; set; }
       
    }

    public class AuthenticateResponse
    {
        //public string UserName { get; set; }
        public string Phone { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }

        //public string Role { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiry { get; set; }
        //public string RToken { get; set; }
        //public DateTime RTokenExpiry { get; set; }


        public AuthenticateResponse( string token, string phone_number)
        {
           

            Phone = phone_number;
           
            Token = token;
            
        }

       
    }
}
