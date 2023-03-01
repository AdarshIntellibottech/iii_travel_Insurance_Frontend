using Dapper;
using log4net.Core;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Asn1.X509;
using TravelInsuranceAPI.Controllers;
using TravelInsuranceAPI.Services.IRepository;
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
using MessagePack.Formatters;
using System.ComponentModel;
//using Validation = PracticeApi.Controllers.Validation;

namespace TravelInsuranceAPI.Services.Repository

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
                    ("SELECT * FROM emp_info WHERE id = @id", new { id }).Result.FirstOrDefault();


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
                    new { details.id }).Result;
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
                                    new { emp_info = details.id, details.id, details.name, details.address, details.department, details.dob, details.doj });








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
                                 new { details.id, details.name, details.address, details.department });


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
                    new { id });

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
                    details.policy_id,
                    details.customer_name,
                    details.phone_number,
                    details.payment_status,
                    details.whatsapp_notify_by_user,
                    details.whatsapp_sent_status,
                    details.policy_purchase_date,
                    details.travel_start_date,
                    details.access_token,
                    details.grand_code

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
                    string[] insuremoresponse = await CallInsureMoApi(newDetails);
                    Console.WriteLine("array is not empty");
                    if (insuremoresponse.Length != 0)
                    {
                        Console.WriteLine("array is present");
                        //------------call whatsapp cloud api ----------
                        bool wacloudapiresponse = await CallWaCloudApi(newDetails, insuremoresponse);
                    }
                    else
                    {
                        result.isSuccess = false;
                        result.statuscode = "200";
                        result.result = false;
                    }


                    return result;
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<string[]> CallInsureMoApi(WhatsAppInfo details)
        {
            var policyId = details.policy_id;
            var prdtCode = "PTJ";

            var requestBody = new
            {
                policyId,
                prdtCode
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

            if (responseObject.data != null)
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
                return links.ToArray();
            }


        }

        public static async Task<bool> CallWaCloudApi(WhatsAppInfo details, string[] insureMoResponse)
        {
            string[] document = { "PolicySchedule", "DebitNote", "PolicyWording", "TaxInvoice" };
            var waToken = "EAAS7sCkNG2QBAGSwA6UqqFAi4PexDEhiQyX3g3a9btpAVV89z4tnPJx5IMCD5XGk1izu48IvH7JiZCzHLgewNoEZASqiDJSOqXGpvNPGQLrcRs3XgLpSmImaNZAbLGKiSUsFbsVPHlCJ3ReWOSRrOde55olonlhufg9LiUBQ3Y2SeKIc20C";
            for (int index = 0; index < insureMoResponse.Length; index++)
            {
                var requestBody = new
                {
                    messaging_product = "whatsapp",
                    to = details.phone_number,
                    type = "template",
                    template = new
                    {
                        name = "send_quotation",
                        language = new
                        {
                            code = "en"
                        },
                        components = new dynamic[]
                     {
                        new
                        {
                            type = "header",
                            parameters = new dynamic[]
                            {
                                new
                                {
                                    type = "document",
                                    document = new
                                    {
                                        link = insureMoResponse[index],
                                        filename = document[index]
                                    }
                                }
                            }
                        }
                     }
                    }
                };

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", waToken);
                var requestContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(requestBody));
                requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await httpClient.PostAsync("https://graph.facebook.com/v15.0/107611225534603/messages", requestContent);
                var statusNumber = (int)response.StatusCode;
                if (statusNumber == 200)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseContent);
                    Console.WriteLine(responseObject);

                    var obj = responseObject.messages["id"];
                    Console.WriteLine(responseObject);
                }






            }
            return true;

        }
    }
}
