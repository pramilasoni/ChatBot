using HabitatHomeBot.BotAssets.Properties;

namespace HabitatHomeBot.BotAssets
{
    using System;
    using Microsoft.Bot.Builder.Location;
    using Properties;

    [Serializable]
    public class HabitatHomeBotResourceManager : LocationResourceManager
    {
        public override string ConfirmationAsk => Resources.Location_ConfirmationAsk;
    }
}
