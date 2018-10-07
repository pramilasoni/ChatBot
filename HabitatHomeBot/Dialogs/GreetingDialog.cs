using System.Threading.Tasks;
using HabitatHomeBot.BotAssets.Extensions;
using HabitatHomeBot.Properties;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace HabitatHomeBot.Dialogs
{
    public class GreetingDialog : IDialog<object>
    {
        private readonly IHabitatHomeBotDialogFactory _dialogFactory;
       
        public GreetingDialog( IHabitatHomeBotDialogFactory dialogFactory )
        { 
            this._dialogFactory = dialogFactory; 
        }
        public async Task StartAsync(IDialogContext context)
        {
            var reply = context.MakeMessage();

            var options = new[]
            {
                Resources.RootDialog_Welcome_Orders,
                Resources.RootDialog_Welcome_Support
            };
            reply.AddHeroCard(
                Resources.RootDialog_Welcome_Title,
                Resources.RootDialog_Welcome_Subtitle,
                options,
                new[] { "https://placeholdit.imgix.net/~text?txtsize=56&txt=Habitat%20Home&w=640&h=330" });

            await context.PostAsync(reply);

            context.Wait(this.OnOptionSelected);
        } 
        private async Task OnOptionSelected(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if (message.Text == Resources.RootDialog_Welcome_Orders)
            {
                context.Call(_dialogFactory.Create<ProductsDialog>(), Callback);

            }
            else if (message.Text == Resources.RootDialog_Welcome_Support)
            {
                await OtherDialog.StartOverAsync(context, Resources.RootDialog_Support_Message);
            }
            else
            {
               context.Done(message);
                 
            }
        }
        private async Task Callback(IDialogContext dialogContext, IAwaitable<object> result)
        {
            dialogContext.Done(result);
        } 
    }
}