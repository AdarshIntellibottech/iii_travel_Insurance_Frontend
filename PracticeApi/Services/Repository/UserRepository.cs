using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TravelInsuranceAPI.Controllers;
using TravelInsuranceAPI.Models;
using Microsoft.Extensions.Options;
using log4net.Core;
using NuGet.Common;
using System.IO;
using TravelInsuranceAPI.Services.IRepository;
using Dapper;
using Microsoft.OpenApi.Models;


namespace TravelInsuranceAPI.Services.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            _configuration = configuration;//?? throw new ArgumentNullException(nameof(configuration));
            //path = AppContext.BaseDirectory + _configuration["ErrorTextFile"];
            //baseURL = _configuration["baseURL"];
            //token = _configuration["token"];
            //this.logger = logger;
            //_clientFactory = clientFactory;
            //prefferedLangURL = _configuration["PrefferedLanguage"];
            //_appSettings = appSettings.Value;
            //_ERPRepository = erprepository;
        }


        #region refreshtoken

        public async Task<APIResponse> Authenticate(AuthenticateRequest model)
        //public async Task<TokenApiResponse> Authenticate(AuthenticateRequest model)
        {
            // UserModel user = new UserModel() { Id = model.Id };
            try
            {
                bool result = await VerifyApiUser (model.userName, model.password);
                    APIResponse Result = new APIResponse();
                if (result)
                {
                    //TokenRequest user = new TokenRequest() { phone = model.phoneNumber };
                    TokenRequest user = new TokenRequest() { phone = model.phoneNumber, userName = model.userName, token = null, password = model.password };

                    
                    var authToken = await GenerateJwtToken(user);
                    UserModel user1 = new UserModel() { Id = user.userName, phone = model.phoneNumber };
                    //UserModel user1 = new UserModel() { phone = model.phoneNumber };
                    
                    Result.isSuccess = true;
                    Result.statuscode = "200";
                    Result.result = new AuthenticateResponse(authToken, model.phoneNumber);
                  
                }
                return Result;
                


            }
            catch (Exception ex)
            {
                //logger.LogError(ex.Message);
                return (new APIResponse() { isSuccess = false, ErrorMessage = ex.Message });
            }

        }

        private async Task<string> GenerateJwtToken(TokenRequest user)
        {
            try
            {
                // generate token that is valid for 10 minutes
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("TravelInsuranceSecretKeyForEncryption");
                var id = user.phone;               

                //var minutes = Double.Parse(_configuration["TokenExpireTimeInMinutes"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    //Subject = new ClaimsIdentity(new[] { new Claim("phone", user.phone.ToString()) }),
                    Subject = new ClaimsIdentity(new[] { new Claim("phone", user.phone.ToString()), new Claim("userName", user.userName.ToString()), new Claim("password", user.password.ToString()) }),
                    Expires = DateTime.UtcNow.AddHours(Double.Parse(_configuration["TokenExpireTimeInMinutes"])),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                //logger.LogError(ex.Message);
                throw;
            }
        }

        //VerifyApiUser checks whether the requested username and password exists in the db and compares it
        public async Task<bool> VerifyApiUser(string username, string password)
        {
            var connection = new MySqlConnection
                (DecryptString(_configuration.GetValue<string>("ConnectionStrings:DefaultConnection")));

            ApiAccessInfo details = connection.QueryAsync<ApiAccessInfo>("SELECT * FROM tbl_api_access WHERE UserID = @Username ", new { Username = username }).Result.FirstOrDefault();



            if (details != null)
            {
                if(details.UserID == username && details.password == password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }

        public string DecryptString(string cipherText)
        {
            //byte[] iv = new byte[16];
            //byte[] buffer = Convert.FromBase64String(cipherText);
            //using (Aes aes = Aes.Create())
            //{
            //    aes.Key = Encoding.UTF8.GetBytes(key);//I have already defined "Key" in the above EncryptString function
            //    aes.IV = iv;
            //    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            //    using (MemoryStream memoryStream = new MemoryStream(buffer))
            //    {
            //        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
            //        {
            //            using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
            //            {
            //                return streamReader.ReadToEnd();
            //            }
            //        }
            //    }
            //}
            return cipherText;
        }


        #endregion
    }
}
