namespace HabitatHomeBot.Dialogs
{
    using System.Collections.Generic;
    using Autofac;
    using BotAssets;
    using BotAssets.Dialogs;
    using Microsoft.Bot.Builder.Internals.Fibers;

    public class HabitatHomeBotDialogFactory : DialogFactory, IHabitatHomeBotDialogFactory
    {
        public HabitatHomeBotDialogFactory(IComponentContext scope)
            : base(scope)
        {
        }
         
    }
}