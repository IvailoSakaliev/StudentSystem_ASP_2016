﻿using StudentSystem2016.Models;
using StudentSystem2016.Servises.EntityServise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem2016.Servises.ProjectServise
{
    public class AuthenticationServises
        : LoginServise
    {
        public Login _LoggedUser { get; set; }
        private List<Login> list { get; set; }
        public LoginServise singIn { get; set; }
        private IEncriptServises _encript { get; set; }

        public AuthenticationServises()
        {
            this.singIn = new LoginServise();
            _encript = new EncriptServises();
        }
        public int AuthenticateUser(string email, string password, int state, int mode)
        {
            string decriptedEmail = "";
            string decriptedPassword = "";
            this.list = GetAll();
            foreach (var item in list)
            {
                decriptedEmail = _encript.DencryptData(item.Email);
                decriptedPassword = _encript.DencryptData(item.Password);
                if (mode == 1)
                {
                    if (decriptedEmail == email && decriptedPassword == password)
                    {
                        _LoggedUser = item;

                        return AddSession(state); ;
                    }
                    else
                    {
                        _LoggedUser = null;

                    }
                }
                else if (mode == 2)
                {
                    if (decriptedEmail == email)
                    {
                        _LoggedUser = item;

                        return AddSession(state); ;
                    }
                    else
                    {
                        _LoggedUser = null;

                    }
                }
            }

            return 0;

        }

        private int AddSession(int state)
        {
            if (_LoggedUser != null)
            {
                if (state == 1)
                {
                    if (singIn.IsConfirmRegistartion(_LoggedUser) == true)
                    {
                        return ReturnIdFromUser();
                    }
                    else
                    {
                        return -1;
                    }

                }
                else if (state == 2)
                {
                    return ReturnIdFromUser();
                }
            }
            return 0;
        }
        private int ReturnIdFromUser()
        {
            return _LoggedUser.ID;
        }


    }
}
