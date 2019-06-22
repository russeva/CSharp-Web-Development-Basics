namespace WebServerV._2.Server.Http
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Concurrent;

    public static class SessionStore
    {
        public const string SessionCookieKey = "SID";

        private static readonly ConcurrentDictionary<string, HttpSession> sessions =
            new ConcurrentDictionary<string, HttpSession>();

        public static HttpSession Get(string id)
        => sessions.GetOrAdd(id, _ => new HttpSession(id));
    }
}
