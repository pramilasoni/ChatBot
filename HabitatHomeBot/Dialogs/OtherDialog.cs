using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace HabitatHomeBot.Dialogs
{
    public class OtherDialog
    {   
        public static async Task StartOverAsync(IDialogContext context, string text)
        {
            var message = context.MakeMessage();
            message.Text = text;
            await StartOverAsync(context, message);
        }

        public static async Task StartOverAsync(IDialogContext context, IMessageActivity message)
        {
            await context.PostAsync(message);
            context.Done(message); 
        }
    }
} 