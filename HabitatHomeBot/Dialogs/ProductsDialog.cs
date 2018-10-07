namespace HabitatHomeBot.Dialogs
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using BotAssets.Dialogs;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using Properties;
    using Services;
    using Services.Models;

    [Serializable]
    public class ProductsDialog : PagedCarouselDialog<TopProduct>
    {
        public override string Prompt
        {
            get { return string.Format(CultureInfo.CurrentCulture, Resources.ProductsDialog_Prompt, this.TotalCount); }
        }

        public int TotalCount { get; set; }
        public override PagedCarouselCards GetCarouselCards(int pageNumber, int pageSize)
        {
            HabitatHomeService habitatHomeService = new HabitatHomeService();
            var listtopProduct = habitatHomeService.TopProduct();
            var carouselCards = listtopProduct.Result.Select(topProduct => new HeroCard
            {
                Title = topProduct.DisplayName,
                Subtitle = topProduct.Description,
                Text = topProduct.ListPriceWithCurrency,
                Images = new List<CardImage> {new CardImage(StringConstants.HostUrl + topProduct.SummaryImageUrl, topProduct.DisplayName)},
                Buttons = new List<CardAction>
                {
                    new CardAction(ActionTypes.ImBack,title: Resources.ProductsDialog_Select,displayText:topProduct.DisplayName,
                        value:topProduct.DisplayName + "|" + topProduct.CatalogName + "|" + topProduct.ProductId)
                }
            });
            TotalCount = listtopProduct.Result.Count;
        
        return new PagedCarouselCards
            {
                Cards = carouselCards,
                TotalCount = listtopProduct.Result.Count
            };
        }

        public override async Task ProcessMessageReceived(IDialogContext context, string message)
        {
             string[] productParam = message.Split('|');
            bool resultjson = false;
            if (productParam.Length >= 3)
            {
                await context.PostAsync("OK, Please wait .. Let me add the product to your cart");

                HabitatHomeService obj = new HabitatHomeService();
                resultjson = obj.AddProduct(productParam[1], productParam[2]);
            }
            if (resultjson)
            {
                await context.PostAsync( $"Your product '{productParam[0]}' has been added to the cart");
                
                 context.Done(resultjson);
            }
            else
            {
                await context.PostAsync(string.Format(CultureInfo.CurrentCulture, Resources.ProductsDialog_InvalidOption, productParam[0]));
                await this.ShowProducts(context);
                context.Wait(this.MessageReceivedAsync);
            }
        }
    }
}