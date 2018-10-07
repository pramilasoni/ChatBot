using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HabitatHomeBot.Models;
using HabitatHomeBot.Properties;
using HabitatHomeBot.Services;
using HabitatHomeBot.Services.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;

namespace HabitatHomeBot.Dialogs
{
    [LuisModel("3b28f6ac-8efd-4b31-9038-ea329e542138", "cd68e16ff8f24654835333a347ace810",domain:"westus.api.cognitive.microsoft.com")]
    [Serializable]
    public class LuisHabitatBotDialog : LuisDialog<object>
    {
        private readonly IHabitatHomeBotDialogFactory dialogFactory;

        public LuisHabitatBotDialog(IHabitatHomeBotDialogFactory dialogFactory)
        {
            this.dialogFactory = dialogFactory;
        }

        [LuisIntent(LuisIntents.None)]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I'm sorry I don't know what you mean.");
            context.Wait(MessageReceived);
        }

        [LuisIntent(LuisIntents.Greetings)]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
          context.Call(dialogFactory.Create<GreetingDialog>(), Callback);
        }
        [LuisIntent(LuisIntents.TopProduct)]
        public async Task TopProduct(IDialogContext context, LuisResult result)
        {
            context.Call(dialogFactory.Create<ProductsDialog>(), Callback);
        }
        [LuisIntent(LuisIntents.Order)]
        public async Task Order(IDialogContext context, LuisResult result)
        {
            context.Call(dialogFactory.Create<OrderDialog>(), Callback);
        }


        [LuisIntent(LuisIntents.ShowCart)]
        private async Task ShowCart(IDialogContext dialogContext, LuisResult result)
        {
            dialogContext.Call(dialogFactory.Create<ShowCartDialog>(), Callback);
           
        }

        private async Task Callback(IDialogContext dialogContext, IAwaitable<object> result)
        {
            dialogContext.Wait(MessageReceived);
        }
       
    }
}