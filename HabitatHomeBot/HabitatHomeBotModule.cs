using System.Configuration;
using Autofac;
using HabitatHomeBot.Dialogs;
using HabitatHomeBot.Services.Models;
using HabitatHomeBot.BotAssets;
using HabitatHomeBot.BotAssets.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Builder.Location;
using Microsoft.Bot.Builder.Scorables;
using Microsoft.Bot.Connector;

namespace HabitatHomeBot
{
    public class HabitatHomeBotModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<HabitatHomeBotDialogFactory>()
                .Keyed<IHabitatHomeBotDialogFactory>(FiberModule.Key_DoNotSerialize)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
  
            builder.RegisterType<ShowCartDialog>() 
                .InstancePerDependency();
            builder.RegisterType<ProductsDialog>() 
                .InstancePerDependency();
            builder.RegisterType<GreetingDialog>() 
                .InstancePerDependency();
            builder.RegisterType<OrderDialog>()
                .InstancePerDependency();
            builder.RegisterType<LuisHabitatBotDialog>()
                .As<IDialog<object>>() 
                .InstancePerDependency(); 
             
            // Location Dialog
            // ctor signature: LocationDialog(string apiKey, string channelId, string prompt, LocationOptions options = LocationOptions.None, LocationRequiredFields requiredFields = LocationRequiredFields.None, LocationResourceManager resourceManager = null);
            builder.RegisterType<LocationDialog>()
                .WithParameter("apiKey", ConfigurationManager.AppSettings["MicrosoftBingMapsKey"])
                .WithParameter("options", LocationOptions.SkipFavorites | LocationOptions.SkipFinalConfirmation | LocationOptions.UseNativeControl | LocationOptions.ReverseGeocode)
                .WithParameter("requiredFields", LocationRequiredFields.StreetAddress | LocationRequiredFields.Locality | LocationRequiredFields.Country)
                .WithParameter("resourceManager", new HabitatHomeBotResourceManager())
                .InstancePerDependency();

          
        }
    }
}