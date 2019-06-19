using System.Collections.Generic;

namespace WebServerV._2.Server.Http.Contracts
{
    public interface IHttpHeaderCollection : IEnumerable<ICollection<HttpHeader>>
    {
        void Add(HttpHeader header);

        void Add(string key, string value);

        bool ContainsKey(string key);

        ICollection<HttpHeader> GetHeader(string key);
    }
}
