﻿using StudentSystem2016.Models;
using StudentSystem2016.Servises.EntityServise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem2016.Servises.ProjectServise
{
    public class EncriptServises : IEncriptServises
    {
        HashServise _servise = new HashServise();
        private string _hash { get; set; }

        public EncriptServises()
        {
            Hash model = new Hash();
            model = _servise.GetByID(1);
            _hash = model.Name;
        }
        public string EncryptData(string toEncrypted)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(toEncrypted);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(_hash));
                using (TripleDESCryptoServiceProvider tripeDescryptProvider = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripeDescryptProvider.CreateEncryptor();
                    byte[] result = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(result, 0, result.Length);
                }
            }
        }
        public string DencryptData(string toDencrypted)
        {
            byte[] data = Convert.FromBase64String(toDencrypted);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(_hash));
                using (TripleDESCryptoServiceProvider tripeDescryptProvider = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripeDescryptProvider.CreateDecryptor();
                    byte[] result = transform.TransformFinalBlock(data, 0, data.Length);
                    return UTF8Encoding.UTF8.GetString(result);
                }
            }
        }
    }
}
