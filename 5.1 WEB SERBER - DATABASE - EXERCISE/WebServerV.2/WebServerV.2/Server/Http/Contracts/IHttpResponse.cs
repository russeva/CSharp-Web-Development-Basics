namespace WebServerV._2.Server.Http.Contracts
{
    using WebServerV._2.Server.Enums;

    public interface IHttpResponse
    {
        IHttpHeaderCollection HeaderCollection { get; }

        IHttpCookieCollection CookieCollection { get; }

        HttpResponseStatusCode StatusCode { get; }
    }
}
