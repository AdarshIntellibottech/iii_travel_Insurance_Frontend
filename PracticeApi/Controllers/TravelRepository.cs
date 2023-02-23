using Dapper;
using log4net.Core;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Asn1.X509;
using TravelInsuranceAPI.Controllers;
using TravelInsuranceAPI.Services.IRepositories;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Xml.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Web.Http.ModelBinding;
using Microsoft.AspNetCore.Components.Forms;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
//using Validation = PracticeApi.Controllers.Validation;

namespace TravelInsuranceAPI.Services.Repositories

{
    public class TravelRepository : ITravelRepository
    {
        private readonly IConfiguration _configuration;

        //private string path;
        //private static string key = "b14ca5898a4e4142aace2ea2143a2410";
       

        public TravelRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            //this.logger = logger;
            //path = AppContext.BaseDirectory + _configuration["ErrorTextFile"];
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
        public async Task<APIResponse> GetEmpDetails(int id)
        {
            APIResponse result = new APIResponse();
            try
            {
                var connection = new MySqlConnection
                (DecryptString(_configuration.GetValue<string>("ConnectionStrings:DefaultConnection")));

                emp_info details = connection.QueryAsync<emp_info>
                    ("SELECT * FROM emp_info WHERE id = @id", new { id = id }).Result.FirstOrDefault();


                if (details == null)
                {
                    result.isSuccess = false;
                    result.ErrorMessage = "No Details Found";
                    result.statuscode = "200";
                    result.result = new emp_info();
                    return result;
                }
                else
                {
                    result.isSuccess = true;
                    result.name = details.name;
                    result.statuscode = "200";
                    result.result = true;
                    return result;
                }

            }
            catch (Exception ex)
            {

                //logger.Error("Message: " + ex.Message + "\n InnerException: " + ex.InnerException + "\n StackTrace: " + ex.StackTrace + "\n Source: " + ex.Source + "\nTargetSite: " + ex.TargetSite);
                throw new Exception();
            }

        }

        public async Task<APIResponse> CreateEmpDetails(emp_info details)
        {
            APIResponse result = new APIResponse();
           
            try
            {

                //bool valid = Validate.datevalidation(DateTime.Today);
                using var connection = new MySqlConnection
                (DecryptString(_configuration.GetValue<string>("ConnectionStrings:DefaultConnection")));
                var dataCount = connection.QueryAsync<int>
                    ("SELECT count(*) FROM emp_info WHERE (id = @id )",
                    new { id = details.id }).Result;
                if (Convert.ToInt32(dataCount.FirstOrDefault()) > 0)
                {
                    var affected =
                    await connection.ExecuteAsync
                        ("UPDATE emp_info SET id=@id,where id=@id",
                                new { emp_info = details.id });


                    if (affected == 0)
                    {
                        result.isSuccess = false;
                        result.statuscode = "200";
                        result.result = false;
                        result.ErrorMessage = "No employee created";
                        return result;
                    }
                    else
                    {
                        result.isSuccess = true;
                        result.statuscode = "200";
                        result.result = true;
                        return result;
                    }
                }
                else
                {
                    
                
                    
                    

                var affected =
                    await connection.ExecuteAsync
                        ("INSERT INTO emp_info (id,name,address,department,dob,doj) " +
                        "VALUES ( @id, @name,@address,@department,@dob,@doj)",
                                new { emp_info = details.id, id = details.id, name = details.name, address = details.address, department = details.department, dob = details.dob, doj = details.doj });



                 

                    


                    if (affected == 0)
                    {
                        result.isSuccess = false;
                        result.statuscode = "200";
                        result.result = false;
                        result.ErrorMessage = "No employee created";
                        return result;
                    }
                    else
                    {
                        result.isSuccess = true;
                        result.statuscode = "200";
                        result.result = true;
                        return result;
                    }



                }

                






            }
            catch (Exception ex)
            {

                //logger.Error("Message: " + ex.Message + "\n InnerException: " + ex.InnerException + "\n StackTrace: " + ex.StackTrace + "\n Source: " + ex.Source + "\nTargetSite: " + ex.TargetSite);
                throw new Exception();
            }

        }
       

        public async Task<APIResponse> UpdateEmpDetails(emp_info details)
        {
            APIResponse result = new APIResponse();
            try
            {
                using var connection = new MySqlConnection
                (DecryptString(_configuration.GetValue<string>("ConnectionStrings:DefaultConnection")));

                var affected = await connection.ExecuteAsync
                        ("UPDATE emp_info SET id=@id,name=@name,address=@address,department=@department WHERE id=@id",
                                 new { id = details.id, name = details.name, address = details.address, department = details.department });


                if (affected == 0)
                {
                    result.isSuccess = false;
                    result.statuscode = "200";
                    result.result = false;
                    result.ErrorMessage = "No employee updated";
                    return result;
                }
                else
                {
                    result.isSuccess = true;
                    result.statuscode = "200";
                    result.result = true;
                    return result;
                }
            }
            catch (Exception ex)
            {
                //logger.Error("Message: " + ex.Message + "\n InnerException: " + ex.InnerException + "\n StackTrace: " + ex.StackTrace + "\n Source: " + ex.Source + "\nTargetSite: " + ex.TargetSite);
                throw new Exception();
            }

        }

        public async Task<APIResponse> DeleteEmpDetails(int id)
        {
            APIResponse result = new APIResponse();
            try
            {
                using var connection = new MySqlConnection
                (DecryptString(_configuration.GetValue<string>("ConnectionStrings:DefaultConnection")));

                var affected = await connection.ExecuteAsync("DELETE FROM emp_info WHERE id = @id",
                    new { id = id });

                if (affected == 0)
                {
                    result.isSuccess = false;
                    result.statuscode = "200";
                    result.result = false;
                    result.ErrorMessage = "No employee deleted";
                    return result;
                }
                else
                {
                    result.isSuccess = true;
                    result.statuscode = "200";
                    result.result = true;
                    return result;
                }
            }
            catch (Exception ex)
            {
                //logger.Error("Message: " + ex.Message + "\n InnerException: " + ex.InnerException + "\n StackTrace: " + ex.StackTrace + "\n Source: " + ex.Source + "\nTargetSite: " + ex.TargetSite);
                throw new Exception();
            }
        }
        public async Task<List<APIResponse>> SearchName(string name, List<APIResponse> result)
        {
            APIResponse Result = new APIResponse();
            try
            {
                using var connection = new MySqlConnection
                (DecryptString(_configuration.GetValue<string>("ConnectionStrings:DefaultConnection")));


                string qry = "SELECT * FROM emp_info WHERE name LIKE '%" + name + "%' order by name";
                var details = connection.QueryAsync<emp_info>(qry).Result.ToList();



                if (details == null)
                {
                    Result.isSuccess = true;
                    Result.ErrorMessage = "No detail Found";
                    Result.statuscode = "200";
                    Result.result = new List<emp_info>();
                    return result;
                }
                else
                {
                    Result.isSuccess = true;
                    Result.statuscode = "200";
                    Result.result = details.ToList();
                    return result;
                    
                }

            }
            catch (Exception ex)
            {
                //logger.Error("Message: " + ex.Message + "\n InnerException: " + ex.InnerException + "\n StackTrace: " + ex.StackTrace + "\n Source: " + ex.Source + "\nTargetSite: " + ex.TargetSite);
                throw new Exception();
            }

        }

        public Task<List<APIResponse>> SearchName(string name)
        {
            throw new NotImplementedException();
        }


        public async Task<APIResponse> CreateUserDetails(WhatsAppInfo details)
        {
            APIResponse result = new APIResponse();
            try
            {
                using var connection = new MySqlConnection
                (DecryptString(_configuration.GetValue<string>("ConnectionStrings:DefaultConnection")));

                WhatsAppInfo newDetails = new WhatsAppInfo
                {
                    policy_id = details.policy_id,
                    customer_name = details.customer_name,
                    phone_number = details.phone_number,
                    payment_status = details.payment_status,
                    whatsapp_notify_by_user = details.whatsapp_notify_by_user,
                    whatsapp_sent_status = details.whatsapp_sent_status,
                    policy_purchase_date = details.policy_purchase_date,
                    travel_start_date = details.travel_start_date,
                    access_token = details.access_token,
                    grand_code = details.grand_code
                };

                var affected = await connection.ExecuteAsync(
                "INSERT INTO tbl_whatsapp_info (PolicyId, CustomerName, PhoneNumber, PaymentStatus, WaNotifyByUser, WaSentStatus, PolicyPurchaseDate, TravelStartDate, AccessToken, GrandCode) " +
                "VALUES (@policy_id, @customer_name, @phone_number, @payment_status, @whatsapp_notify_by_user, @whatsapp_sent_status, @policy_purchase_date, @travel_start_date, @access_token, @grand_code)",
                new
                {
                    policy_id = details.policy_id,
                    customer_name = details.customer_name,
                    phone_number = details.phone_number,
                    payment_status = details.payment_status,
                    whatsapp_notify_by_user = details.whatsapp_notify_by_user,
                    whatsapp_sent_status = details.whatsapp_sent_status,
                    policy_purchase_date = details.policy_purchase_date,
                    travel_start_date = details.travel_start_date,
                    access_token = details.access_token,
                    grand_code = details.grand_code

                });

                if (affected == 0)
                {
                    result.isSuccess = false;
                    result.statuscode = "200";
                    result.result = false;
                    result.ErrorMessage = "No customer/policy ID info created";
                    return result;
                }
                else
                {
                    //result.isSuccess = true;
                    //result.statuscode = "200";
                    //result.result = true;
                
                    result.savedToDb = "Data saved successfully";

                    //--------------Call InsureMOApi-----------------
                    string[] responseOne = await CallInsureMoApi(newDetails);
                    Console.WriteLine("Array is not empty");
                    if(responseOne.Length !=0)
                    {
                        Console.WriteLine("array is present");
                    }
                    



                    return result;
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public bool CallInsureMoApi(WhatsAppInfo details)
        //{
        //    Console.WriteLine("hi");
        //    return true;
        //}
        public static async Task<string[]> CallInsureMoApi(WhatsAppInfo details)
        {
            var policyId = details.policy_id;
            var prdtCode = "PTJ";

            var requestBody = new
            {
                policyId = policyId,
                prdtCode = prdtCode
            };

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", details.access_token);

            httpClient.DefaultRequestHeaders.Add("grantCode", details.grand_code);

            var requestContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(requestBody));
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PostAsync("https://sandbox.gw.sg.ebaocloud.com/ddpc/1.0.0/api/pub/std/policy/printFiles", requestContent);

            
            var responseContent = await response.Content.ReadAsStringAsync();

            var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseContent);
            var links = new List<string>();

            if(responseObject.data != null)
            {
                foreach (var dataItem in responseObject.data)
                {
                    var dataLinks = dataItem.links;
                    foreach (var printFileType in dataLinks)
                    {
                        links.Add(printFileType.Value.ToString());
                    }
                }
                return links.ToArray();
            }
            else
            {
                return links.ToArray() ;
            }
            
           
        }
    }
}
