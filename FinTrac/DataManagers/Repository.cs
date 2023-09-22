﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.User;

namespace DataManagers
{
    public class Repository
    {
        public List<User> Accounts { get; set; }


        public Repository() 
        { 
            Accounts = new List<User>();
        }
    }
}
