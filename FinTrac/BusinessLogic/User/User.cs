﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.User
{
    public class User
    {

        public string Password { get; set; } = "";




        public User() { }





        public static bool ValidatePassword(string posiblePassword)
        {
            int minLength = 10;

            if(posiblePassword.Length < minLength)
            {
                throw new ExceptionValidateUser("ERROR ON PASSWORD");
            }
            return true;
        }





    }
}