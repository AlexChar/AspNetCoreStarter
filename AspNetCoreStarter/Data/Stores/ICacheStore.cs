using System.Collections.Generic;
using AspNetCoreStarter.Data.Models;

namespace AspNetCoreStarter.Data.Stores
{
    public interface ICacheStore
    {
        Dictionary<int, Book> Cache { get; set; }
    }
}