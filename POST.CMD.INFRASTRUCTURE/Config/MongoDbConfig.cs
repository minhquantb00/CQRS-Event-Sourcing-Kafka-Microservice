﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POST.CMD.INFRASTRUCTURE.Config
{
    public class MongoDbConfig
    {
        public string ConnectionString { get; set; }
        public string Database {  get; set; }
        public string Collection {  get; set; }
    }
}
