namespace WebServerV._2.Server.Http.Contracts
{
    using WebServerV._2.Server.Enums;

    public interface IHttpResponse
    {
        HttpHeaderCollection HeaderCollection { get; }

        HttpResponseStatusCode StatusCode { get; }
    }
}
