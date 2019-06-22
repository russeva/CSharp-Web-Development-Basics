namespace WebServerV._2.Server.Handlers.Contracts
{
    using WebServerV._2.Server.Http.Contracts;

    public interface IRequestHandler
    {
        IHttpResponse Handle(IHttpContext httpContext);
    }
}
