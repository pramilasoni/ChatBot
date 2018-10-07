using HabitatHomeBot.BotAssets.Extensions;
using HabitatHomeBot.Models;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Location;

namespace HabitatHomeBot.Dialogs
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using BotAssets.Dialogs;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.Internals.Fibers;
    using Microsoft.Bot.Connector;
    using Properties;
    using Services;
    using Services.Models;

    [Serializable]
    public class OrderDialog : IDialog<object>
    {
        private const string OrderDialogWhereShouldIShipYourOrder = "Where should I ship your order?";
        private const string NoAddressWasSelectedMessage = "No address was selected.";
        private const string PleaseWaitYourOrderIsProcessingMessage = "Please wait, your order is processing..";
        private readonly IHabitatHomeBotDialogFactory dialogFactory;


        public OrderDialog(IHabitatHomeBotDialogFactory dialogFactory)
        {
            SetField.NotNull(out this.dialogFactory, nameof(dialogFactory), dialogFactory);
        }
        private static UserPreferences GetUserPreferences(IDialogContext context)
        {
            UserPreferences userPreferences = new UserPreferences();

            //context.UserData.TryGetValue(StringConstants.UserPreferencesKey, out userPreferences);

            return userPreferences;
        }
        public async Task StartAsync(IDialogContext dialogContext)
        {
            var prompt = OrderDialogWhereShouldIShipYourOrder;
            // Leverage DI to inject other parameters
            var locationDialog = this.dialogFactory.Create<LocationDialog>(
                new Dictionary<string, object>()
                {
                    { "prompt", prompt },
                    { "channelId", dialogContext.Activity.ChannelId },
                });
            dialogContext.Call(locationDialog, ShowLocationDialog);
        }

        private async Task ShowLocationDialog(IDialogContext context, IAwaitable<Place> result)
        {
            string reply;

            var place = await result;
            if (place == null)
            {
                reply = NoAddressWasSelectedMessage;
            }
            else
            {
                var formattedAddress = place.GetPostalAddress().FormattedAddress;

                context.UserData.UpdateValue<UserPreferences>(
                    StringConstants.UserPreferencesKey,
                    userPreferences =>
                    {
                        userPreferences.BillingAddresses = userPreferences.BillingAddresses ?? new Dictionary<string, string>();
                        userPreferences.BillingAddresses[StringConstants.HomeBillingAddress] = formattedAddress;
                    });

                reply = string.Format(CultureInfo.CurrentCulture, Resources.OrderDialog_Address_Entered,  formattedAddress);
            }

            await context.PostAsync(reply);
            await CreateOrderStep(context);
        }

        private async Task CreateOrderStep(IDialogContext dialogContext)
        {
            await dialogContext.PostAsync(PleaseWaitYourOrderIsProcessingMessage);

            HabitatHomeService obj = new HabitatHomeService();

            string orderResult = obj.CreateOrder().Result;
            await dialogContext.PostAsync(orderResult);
            dialogContext.Done(orderResult);
        }

    }
}