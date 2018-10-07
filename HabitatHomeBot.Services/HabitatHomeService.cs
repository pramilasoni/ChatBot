using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HabitatHomeBot.Services.Models;
using Newtonsoft.Json;

namespace HabitatHomeBot.Services
{
    public class HabitatHomeService
    {
        private readonly string _baseUrl = "http://homestorefront.northeurope.cloudapp.azure.com/api/BabyBot";
        public bool AddProduct(string catalog, string productId)
        {
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    {"addToCart_CatalogName",catalog},
                    { "addToCart_ProductId",productId},
                    { "quantity","1"}

                };

                var content = new FormUrlEncodedContent(values);

                var response = client.PostAsync(_baseUrl + "/Cartbot/AddCartLine", content);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }

                return false;
            }
        }
        public async Task<MiniCart> MiniCart()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(_baseUrl + "/Cartbot/GetMinicart");
                var contentResp = await response.Result.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<MiniCart>(contentResp);
                return result;
            }
        }

        public async Task<bool> Fullfilment()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(_baseUrl + "/Cartbot/SetShippingMethods");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                return false;
            }
        }
        public async Task<bool> Payment()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(_baseUrl + "/Cartbot/SetPaymentMethods");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                return false;
            }
        }
        public async Task<string> CreateOrder()
        {
            using (var client = new HttpClient())
            {

                var response = client.GetAsync(_baseUrl + "/Cartbot/SubmitFinalOrder");

                var contentResp = await response.Result.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<Order>(contentResp);
                if (result.Data.Errors.Count<=0)
                {
                    Uri nextpagelinkUri = new Uri(result.Data.NextPageLink);
                    string orderId = HttpUtility.ParseQueryString(nextpagelinkUri.Query).Get("confirmationId");

                    return "Your Order has Confirmed , Your order Id is  " + orderId.Replace("Entity-Order-","");
                }

                return "Sorry , There are some problem in placing the order , Please call to customer support for placing the order" + String.Join(String.Empty, result.Data.Errors.ToArray());

            }
        }

        public async Task<IList<TopProduct>> TopProduct()
        {
            using (var client = new HttpClient())
            {

                var values = new Dictionary<string, string>
                {
                    {"relationshipFieldId", "{A96D901B-BF4B-4960-9F55-65485F2C2FE7}"},
                    {"currentCatalogItemId", "{803E72C3-5ADC-EBE3-7AEE-5956712C95C8}"}
                };

                var content = new FormUrlEncodedContent(values);
                var response = client.PostAsync(_baseUrl + "/Cartbot/GetPromotedProductsJsonResult", content);
                string contentResp = await response.Result.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IList<TopProduct>>(contentResp);
                return result;
            }
        }
    }
}
