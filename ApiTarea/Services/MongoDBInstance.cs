using Examen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examen.Services
{
    static class MongoDBInstance
    {
        private readonly static MongoDbContext _instance = new MongoDbContext("192.168.1.10");

        public static MongoDbContext getInstance
        {
            get
            {
                return _instance;
            }
        }

    }
}