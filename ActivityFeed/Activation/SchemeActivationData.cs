﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActivityFeed.Activation
{
    public class SchemeActivationData
    {
        private const string ProtocolName = "wtsapp";

        public Type PageType { get; private set; }

        public Uri Uri { get; private set; }

        public Dictionary<string, string> Parameters { get; private set; } = new Dictionary<string, string>();

        public bool IsValid => PageType != null;

        public SchemeActivationData(Uri activationUri)
        {
            // By default, this handler expects URIs of the format:'wtsapp:MainPage?paramName1=paramValue1&paramName2=paramValue2'
            PageType = SchemeActivationConfig.GetPage(activationUri.AbsolutePath);

            if (!IsValid || string.IsNullOrEmpty(activationUri.Query))
            {
                return;
            }

            var uriQuery = HttpUtility.ParseQueryString(activationUri.Query);
            foreach (var paramKey in uriQuery.AllKeys)
            {
                Parameters.Add(paramKey, uriQuery.Get(paramKey));
            }
        }

        public SchemeActivationData(Type pageType, Dictionary<string, string> parameters = null)
        {
            PageType = pageType;
            Parameters = parameters;
            Uri = BuildUri();
        }

        private Uri BuildUri()
        {
            var pageKey = SchemeActivationConfig.GetPageKey(PageType);
            var uriBuilder = new UriBuilder($"{ProtocolName}:{pageKey}");
            var query = HttpUtility.ParseQueryString(string.Empty);

            foreach (var parameter in Parameters)
            {
                query.Set(parameter.Key, parameter.Value);
            }

            uriBuilder.Query = query.ToString();
            return new Uri(uriBuilder.ToString());
        }
    }
}
