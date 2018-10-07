using Microsoft.Bot.Connector;

namespace HabitatHomeBot.Models
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Bot.Builder.Location;
    [Serializable]
    public class UserPreferences
    {
        public UserPreferences()
        {
            BillingAddresses = new Dictionary<string, string>
            {
                {StringConstants.HomeBillingAddress.ToLower(), "Vester Farimagsgade 3, 5th floor , 1606 Copenhagen V ,Denmark"},
                {StringConstants.WorkBillingAddress.ToLower(), "International House, 1 St Katharine\'s Way, St Katharine\'s & Wapping, London E1W 1UN"}
            };
        }

        string FormattedAddress(Place place)
        {
            return place.GetPostalAddress().FormattedAddress;
        } 

        public Dictionary<string, string> BillingAddresses { get; set; }
    }
}