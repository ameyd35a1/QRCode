using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RES_QRCode.Helper;
using RES_QRCode.Models;

namespace RES_QRCode.Services
{
    class ApiServices
    {
        public async Task<string> LoginAsync(string userName, string password)
        {
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", userName),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            };

            //return "token";

            //var request = new HttpRequestMessage(
            //    HttpMethod.Post, Constants.BaseApiAddress + "Token");
            //request.Content = new FormUrlEncodedContent(keyValues);

            var inputContent = new { username = userName, password = password };
            var inputjson = JsonConvert.SerializeObject(inputContent);

            var request = new HttpRequestMessage(
                HttpMethod.Get, Constants.BaseApiAddress + "/api/userlogin");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Add("login", inputjson);

            var response = await client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            //JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(content);

            //var accessTokenExpiration = jwtDynamic.Value<DateTime>(".expires");
            //var accessToken = jwtDynamic.Value<string>("access_token");

            //Settings.AccessTokenExpirationDate = accessTokenExpiration;

            //Debug.WriteLine(accessTokenExpiration);

            //Debug.WriteLine(content);

            //TODO: Remove below content
            string accessToken = string.Empty;
            if (!response.IsSuccessStatusCode)
            {
                Settings.AccessToken = string.Empty;
                Settings.AccessTokenExpirationDate = DateTime.Now.AddMinutes(-1);
                Settings.Username = string.Empty;
            }
            else
            {
                accessToken = content;
                Settings.AccessTokenExpirationDate = DateTime.Now.AddHours(10);
                Settings.Role = content.Replace("\"", "");
                Settings.Username = userName;
            }


            return accessToken;
        }

        internal async Task<ResponseMessageModel> AddUserAsync(string username, string password, string role)
        {
            var inputContent = new { username = username, password = password, role = role };
            var inputjson = JsonConvert.SerializeObject(inputContent);
            var httpContent = new StringContent(inputjson, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(
                HttpMethod.Post, Constants.BaseApiAddress + "/api/userlogin");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Content = httpContent;

            var response = await client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            return new ResponseMessageModel
            {
                ResponseMessage = content != string.Empty ? content.Replace("\"", "") : string.Empty,
                ResponseStatus = response.IsSuccessStatusCode
            };
        }


        internal async Task<ResponseMessageModel> CheckInAsync(string accessToken, string id, string modifiedBy, bool resetFlag)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post, Constants.BaseApiAddress + "/api/scanqrcode?userid=" + id + "&modifiedby=" + modifiedBy + "&resetflag=" + resetFlag.ToString());
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            return new ResponseMessageModel
            {
                ResponseMessage = content != string.Empty ? content.Replace("\"", "") : string.Empty,
                ResponseStatus = response.IsSuccessStatusCode
            };
        }

        internal async Task<EmployeeDetails> GetDetailsAsync(string accessToken, string employeeID)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get, Constants.BaseApiAddress + "/api/scanqrcode?userid=" + employeeID);

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode && content != "null")
            {

                JObject emp = JsonConvert.DeserializeObject<dynamic>(content);

                return new EmployeeDetails
                {
                    ID = emp.Value<string>("EnterpriseID"),
                    Name = emp.Value<string>("EmpName"),
                    Level = emp.Value<string>("CareerLevel"),
                    Location = emp.Value<string>("WorkLocation"),
                    EntryMarked = emp.Value<bool>("EntryMarked")
                };
            }
            else
            {
                return null;
            }
        }        
    }
}
