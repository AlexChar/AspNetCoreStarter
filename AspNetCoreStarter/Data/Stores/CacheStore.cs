using System.Collections.Generic;
using AspNetCoreStarter.Data.Models;

namespace AspNetCoreStarter.Data.Stores
{
    public class CacheStore : ICacheStore
    {
        public Dictionary<int, Book> Cache { get; set; }

        public CacheStore()
        {
            Cache = new Dictionary<int, Book>();
        }
    }
}