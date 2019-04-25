namespace WebServerV._2.Server.Http.Contracts
{
    public interface IHttpHeaderCollection
    {
        void Add(HttpHeader header);

        bool ContainsKey(string key);

        HttpHeader GetHeader(string key);
    }
}
