namespace WebServerV._2.Server.Http
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Common;
    using Enums;
    using Exceptions;
    using Http.Contracts;

    
    class HttpRequest : IHttpRequest
    {
        private readonly string stringRequest;

        public HttpRequest(string stringRequest)
        {
            CoreValidator.ThrowIfNullOrEmpty(stringRequest, nameof(stringRequest));

            this.stringRequest = stringRequest;
            this.HeaderCollection = new HttpHeaderCollection();
            this.CookieCollection = new HttpCookieCollection();
            this.UrlParameters = new Dictionary<string, string>();
            this.QueryParameters = new Dictionary<string, string>();
            this.FormData = new Dictionary<string, string>();
            
			this.ParseRequest(stringRequest);
        }

        
        public IDictionary<string, string> FormData { get; private set; }

        public IHttpHeaderCollection HeaderCollection { get; private set; }

        public IHttpCookieCollection CookieCollection { get; private set; }

        public string Url { get; private set; }

        public IDictionary<string, string> UrlParameters { get; private set; }

        public IHttpSession Session { get; set; }

        public string Path { get; private set; }

        public IDictionary<string, string> QueryParameters { get; private set; }

        public HttpRequestMethod Method { get; private set; }



        public void AddUrlParameter(string key, string value)
        {
            
            this.UrlParameters[key] = value;
        }

        private void ParseRequest(string stringRequest)
        {
            var request = stringRequest;

            var requestLines = request
                 .Split(Environment.NewLine);

            

            string[] requestLine = requestLines[0].Trim()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if(requestLine.Length != 3 || requestLine[2].ToLower() != "http/1.1")
            {
                throw new BadRequestException("Invalid request line");
            }

            this.Method = this.ParseRequestMethod(requestLine[0].ToUpper());
            this.Url = requestLine[1];
            this.Path = this.Url
                .Split(new[] { '?', '#' }, StringSplitOptions.RemoveEmptyEntries)[0];
            this.ParseHeaders(requestLines);
            this.ParseParameters();

            var requestTrimmed = stringRequest.Trim().Split(Environment.NewLine);

            this.ParseRequestQuery(requestTrimmed.Last());
			this.ParseCookies();
            this.SetSession();

        }

        private void ParseRequestQuery(string requestLines)
        {
            if(this.Method == HttpRequestMethod.Post)
            {
                this.ParseQuery(requestLines,this.FormData);
            }
        }

        private void ParseParameters()
        {
            if(!this.Url.Contains("?"))
            {
                return;
            }

            string query = this.Url.Split("?")[1];

            this.ParseQuery(query, this.QueryParameters);
        }

        private void ParseQuery(string query, IDictionary<string, string> queryParameters)
        {
         
            if(!query.Contains("="))
            {
                return;
            }

            string[] queryPairs = query.Split("&");

            foreach (var pair in queryPairs)
            {
                var kvp = pair.Split("=");
                var key = kvp[0];
                var value = kvp[1];

                queryParameters.Add(key, value);
            }
            
        }

        private void ParseHeaders(string[] requestLines)
        {
            var endIndex = Array.IndexOf(requestLines, string.Empty);

            for (int i = 1; i < endIndex; i++)
            {
                string[] headerArgs = requestLines[i]
                    .Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries);

                HttpHeader newHeader = new HttpHeader(headerArgs[0], headerArgs[1].Trim());
                this.HeaderCollection.Add(newHeader);
            }

            if(!this.HeaderCollection.ContainsKey(HttpHeader.Host))
            {
                throw new BadRequestException("Bad Request");
            }
        }

        private void ParseCookies()
        {
            if (this.HeaderCollection.ContainsKey(HttpHeader.Cookie))
            {
                var allCookies = this.HeaderCollection.GetHeader(HttpHeader.Cookie);

                foreach (var cookie in allCookies)
                {
                    if (!cookie.Value.Contains('='))
                    {
                        return;
                    }
                    var cookieParts = cookie.Value
                        .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToList();

                    if (!cookieParts.Any())
                    {
                        continue;
                    }

                    foreach (var cookieEntity in cookieParts)
                    {
                        var cookieKPV = cookieEntity
                        .Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                        if (cookieKPV.Length == 2)
                        {
                            var cookieKey = cookieKPV[0].Trim();
                            var cookieValue = cookieKPV[1].Trim();

                            this.CookieCollection.Add(new HttpCookie(cookieKey, cookieValue, false));
                        }
                    }
                }
            }

        }

        private HttpRequestMethod ParseRequestMethod(string requestedMethod)
        {
            try
            {
                return Enum.Parse<HttpRequestMethod>(requestedMethod, true);
            }
            catch (Exception)
            {
                throw new BadRequestException("Unsuported method");
            }
            
        }

       

        private void SetSession()
        {
            if (this.CookieCollection.ContainsKey(SessionStore.SessionCookieKey))
            {
                var cookie = this.CookieCollection.GetCookieHeader(SessionStore.SessionCookieKey);
                var sessionId = cookie.Value;

                this.Session = SessionStore.Get(sessionId);
            }
            
        }
        public override string ToString() => this.stringRequest;

    }
}
