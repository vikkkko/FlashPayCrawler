﻿using FlashPayCrawler.libs;
using FlashPayCrawler.RPC;
using Newtonsoft.Json.Linq;
using System;

namespace FlashPayCrawler.Apis
{
    public class BaseApi
    {
        protected string netnode { get; set; }

        httpHelper hh = new httpHelper();

        protected Monitor monitor;

        public BaseApi(string node)
        {
            netnode = node;
            monitor = new Monitor();
        }

        protected JArray getJAbyKV(string key, object value)
        {
            return new JArray
                        {
                            new JObject
                            {
                                {
                                    key,
                                    value.ToString()
                                }
                            }
                        };
        }

        protected JArray getJAbyJ(JObject J)
        {
            return new JArray
                        {
                            J
                        };
        }

        public object getRes(JsonRPCrequest req, string reqAddr)
        {
            JArray result = new JArray();
            try
            {
                point(req.method);
                switch (req.method) {
                    case "getnodetype":
                        JArray JA = new JArray
                        {
                            new JObject {
                                { "nodeType",netnode }
                            }
                        };
                        result = JA;
                        break;
                }
                if (result != null && result.Count > 0 && result[0]["errorCode"] != null)
                {
                    JsonPRCresponse_Error resE = new JsonPRCresponse_Error(req.id, (int)result[0]["errorCode"], (string)result[0]["errorMsg"], (string)result[0]["errorData"]);

                    return resE;
                }
                if (result.Count == 0)
                {
                    JsonPRCresponse_Error resE = new JsonPRCresponse_Error(req.id, -1, "No Data", "Data does not exist");

                    return resE;
                }
            }
            catch (Exception e)
            {
                JsonPRCresponse_Error resE = new JsonPRCresponse_Error(req.id, -100, "Parameter Error", e.Message);

                return resE;

            }

            JsonPRCresponse res = new JsonPRCresponse();
            res.jsonrpc = req.jsonrpc;
            res.id = req.id;
            res.result = result;

            return res;
        }

        protected void point(string method)
        {
            if (monitor != null)
            {
                monitor.point(method);
            }
        }
    }
}