using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace FengSharp.OneCardAccess.Common
{
    public static class SessionState
    {
        static object lockobj = new object();
        const string DefaultCacheManagerName = "SessionState Cache Manager";
        const int DefaultSessionExpirationMinutes = 20;
        static string cacheManagerName = ConfigurationManager.AppSettings["CacheManagerName"];
        static CacheManager _cacheManager = null;
        static CacheManager cacheManager
        {
            get
            {
                if (_cacheManager == null)
                {
                    _cacheManager = (CacheManager)CacheFactory.GetCacheManager(string.IsNullOrWhiteSpace(cacheManagerName) ?
                        DefaultCacheManagerName : cacheManagerName);
                }
                return _cacheManager;
            }
        }
        static int _SessionExpirationMinutes = 0;
        static int SessionExpirationMinutes
        {
            get
            {
                if (_SessionExpirationMinutes == 0)
                {
                    if (!int.TryParse(ConfigurationManager.AppSettings["SessionExpirationMinutes"], out _SessionExpirationMinutes))
                    {
                        _SessionExpirationMinutes = DefaultSessionExpirationMinutes;
                    }
                }
                return _SessionExpirationMinutes;
            }
        }
        public static void JoinSession(string ticket, Session session)
        {
            lock (lockobj)
            {
                if (!cacheManager.Contains(ticket))
                    cacheManager.Add(ticket, session, CacheItemPriority.Normal, null, new SlidingTime(TimeSpan.FromMinutes(SessionExpirationMinutes)));
            }
        }
        public static void LeaveSession(string ticket)
        {
            lock (lockobj)
            {
                if (cacheManager.Contains(ticket))
                {
                    cacheManager.Remove(ticket);
                }
            }
        }
        public static Session GetSession(string ticket)
        {
            lock (lockobj)
            {
                return cacheManager.GetData(ticket) as Session;
            }
        }
    }
}
