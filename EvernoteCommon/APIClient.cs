using System;
using System.Net;
using System.Net.Http;

namespace EvernoteCommon
{
    public class AccountData
    {
        public int Amount { get; set; }
        public string CodeTitrePrefix { get; set; }
        public string Currency { get; set; }
        public string Email { get; set; }
        public string Lastname { get; set; }
    }

    public class APIClient
    {
        string myUri = "http://localhost:57905/";
        string emailUri = "api_end_point";
        string resetUri = "http://localhost:57905/api/UpdateDetails/";

        public HttpResponseMessage UpdateDetails(string lastname)
        {
            using (var client = new HttpClient())
            {
                AccountData data = new AccountData { Lastname = lastname };
                client.BaseAddress = new Uri(resetUri);

                var response = client.PutAsJsonAsync(resetUri + lastname+ "/", data).Result;
                if (response.IsSuccessStatusCode)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
            }
        }

        public HttpResponseMessage PutCashInUserAccount(string codeTitre, string currency, int amount, string usercode)
        {
            using (var client = new HttpClient())
            {
                AccountData data = new AccountData { CodeTitrePrefix = codeTitre, Currency = currency, Amount = amount};
                client.BaseAddress = new Uri(myUri);

                var response = client.PutAsJsonAsync(myUri + usercode, data).Result;
                if (response.IsSuccessStatusCode)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
            }
        }

        public HttpResponseMessage VerifyEmail(string email)
        {
            using (var client = new HttpClient())
            {
                AccountData data = new AccountData { Email = email };
                client.BaseAddress = new Uri(emailUri);

                var response = client.PutAsJsonAsync(emailUri + email + "/", data).Result;
                if (response.IsSuccessStatusCode)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
            }
        }
        
    }
}
