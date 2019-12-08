﻿using Newtonsoft.Json.Linq;

namespace FlashPayCrawler.RPC
{
    public class JsonPRCresponse
    {
        public string jsonrpc { get; set; }
        public long id { get; set; }
        public JArray result { get; set; }
    }
}
