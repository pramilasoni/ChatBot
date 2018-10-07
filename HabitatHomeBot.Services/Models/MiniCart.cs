using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitatHomeBot.Services.Models
{
    public class Line
    {
        public string ExternalCartLineId { get; set; }
        public string Image { get; set; }
        public string DisplayName { get; set; }
        public string ProductUrl { get; set; }
        public object ColorInformation { get; set; }
        public object SizeInformation { get; set; }
        public object StyleInformation { get; set; }
        public object GiftCardAmountInformation { get; set; }
        public string Quantity { get; set; }
        public string LinePrice { get; set; }
        public string LineTotal { get; set; }
        public object ShippingOptions { get; set; }
        public List<object> DiscountOfferNames { get; set; }
        public string LineDiscount { get; set; }
        public string ProductId { get; set; }
        public List<object> Errors { get; set; }
        public List<object> Info { get; set; }
        public List<object> Warnings { get; set; }
        public bool HasErrors { get; set; }
        public bool HasInfo { get; set; }
        public bool HasWarnings { get; set; }
        public bool Success { get; set; }
        public object Url { get; set; }
        public object ContentEncoding { get; set; }
        public object ContentType { get; set; }
        public object Data { get; set; }
        public int JsonRequestBehavior { get; set; }
        public object MaxJsonLength { get; set; }
        public object RecursionLimit { get; set; }
    }

    public class MiniCart
    {
        public List<Line> Lines { get; set; }
        public string Email { get; set; }
        public string Subtotal { get; set; }
        public string TaxTotal { get; set; }
        public string Total { get; set; }
        public string ShippingTotal { get; set; }
        public double TotalAmount { get; set; }
        public List<object> Shipments { get; set; }
        public List<object> Payments { get; set; }
        public List<object> Parties { get; set; }
        public object AccountingParty { get; set; }
        public string Discount { get; set; }
        public List<object> PromoCodes { get; set; }
        public List<object> Errors { get; set; }
        public List<object> Info { get; set; }
        public List<object> Warnings { get; set; }
        public bool HasErrors { get; set; }
        public bool HasInfo { get; set; }
        public bool HasWarnings { get; set; }
        public bool Success { get; set; }
        public object Url { get; set; }
        public object ContentEncoding { get; set; }
        public object ContentType { get; set; }
        public object Data { get; set; }
        public int JsonRequestBehavior { get; set; }
        public object MaxJsonLength { get; set; }
        public object RecursionLimit { get; set; }
    }
}