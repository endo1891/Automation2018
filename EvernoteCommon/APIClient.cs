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
    }

    public class APIClient
    {
        string cashUri = "api_end_point";
        string emailUri = "api_end_point";
        HttpResponseMessage response;

        /*public HttpResponseMessage PutCashInUserAccount(string codeTitre, string currency, int amount)
        {
            using (var client = new HttpClient())
            {
                AccountData data = new AccountData { CodeTitrePrefix = codeTitre, Currency = currency, Amount = amount};
                client.BaseAddress = new Uri(cashUri);

                var response = client.PutAsJsonAsync(cashUri + userCode, data).Result;
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
        */
    }
}
