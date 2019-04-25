namespace WebServerV._2.Server.Http.Contracts
{
    using System.Collections.Generic;
    using WebServerV._2.Server.Enums;

    public interface IHttpRequest
    {
        Dictionary<string, string> FormData { get; }

        HttpHeaderCollection HeaderCollection { get; }

        string Url { get; }

        Dictionary<string, string> UrlParameters { get; }

        string Path { get; }

        Dictionary<string, string> QueryParameters { get; }

        HttpRequestMethod Method { get; }

        void AddUrlParameter(string key, string value);
        
    }
}
