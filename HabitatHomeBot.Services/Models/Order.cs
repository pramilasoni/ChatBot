using System.Collections.Generic;

namespace HabitatHomeBot.Services.Models
{
    using System;

    [Serializable]
    public class Order
    {



        public string ContentEncoding { get; set; }
        public string ContentType { get; set; }
        public Data Data { get; set; }
        public int JsonRequestBehavior { get; set; }
        public string MaxJsonLength { get; set; }
        public string RecursionLimit { get; set; }


    }
    [Serializable]
    public class Data
    {
        public string NextPageLink { get; set; }
        public List<string> Errors { get; set; }
        public List<string> Info { get; set; }
        public List<string> Warnings { get; set; }
        public bool HasErrors { get; set; }
        public bool HasInfo { get; set; }
        public bool HasWarnings { get; set; }
        public bool Success { get; set; }
        public string Url { get; set; }
        public string ContentEncoding { get; set; }
        public string ContentType { get; set; } 
        public int JsonRequestBehavior { get; set; }
        public string MaxJsonLength { get; set; }
        public string RecursionLimit { get; set; }
    }
}
