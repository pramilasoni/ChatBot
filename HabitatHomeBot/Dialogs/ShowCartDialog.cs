using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HabitatHomeBot.Properties;
using HabitatHomeBot.Services;
using HabitatHomeBot.Services.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace HabitatHomeBot.Dialogs
{
    public class ShowCartDialog: IDialog<object>
    {
        private const string CartWaitMessage = "Yes sure ,Wait a second .. Let me find the cart information for you";
        private const string WouldYouLikeToCompleteThePurchase = "Would you like to complete the purchase?";

        public async Task StartAsync(IDialogContext dialogContext)
        {
            await dialogContext.PostAsync(CartWaitMessage);

            var message = dialogContext.MakeMessage();

            message.Text = string.Format(CultureInfo.CurrentCulture,Resources.ShowCartDialog_MiniCart_Title);
            message.Attachments.Add(GetReceiptCard());
            await dialogContext.PostAsync(message);
            await dialogContext.PostAsync(WouldYouLikeToCompleteThePurchase);
            dialogContext.Done(message);
        }
         
        private Attachment GetReceiptCard()
        {
            HabitatHomeService obj = new HabitatHomeService();
            var listtopProduct = obj.MiniCart();
            var receiptItems = new List<ReceiptItem>();
            foreach (Line topProduct in listtopProduct.Result.Lines)
            {
                var receiptItem = new ReceiptItem(
                    title: Truncate(topProduct.DisplayName,15),
                    quantity: topProduct.Quantity,
                    price: topProduct.LinePrice,
                    image: new CardImage(StringConstants.HostUrl+topProduct.Image)                    );
                
                receiptItems.Add(receiptItem);
            }
            var receiptCard = new ReceiptCard()
            {
                Title = Resources.ShowCartDialog_MiniCart_Title,
                Items = receiptItems,
                Tax = listtopProduct.Result.TaxTotal,
                Total = listtopProduct.Result.Total
            };
             
            return receiptCard.ToAttachment();
        }
        public static string Truncate(  string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
        }
    }
} 