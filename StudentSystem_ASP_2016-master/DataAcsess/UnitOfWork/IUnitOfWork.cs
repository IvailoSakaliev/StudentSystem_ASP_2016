﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcsess.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rowback();
        void Dispose();
    }
}
