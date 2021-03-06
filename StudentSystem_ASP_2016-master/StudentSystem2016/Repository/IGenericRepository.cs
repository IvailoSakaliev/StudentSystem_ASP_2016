﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using StudentSystem2016.Models;

namespace StudentSystem2016.Repository
{
    public interface IGenericRepository1<Tentity> where Tentity : BaseModel, new()
    {
        void Delete(Expression<Func<Tentity, bool>> filter);
        void Delete(Tentity entity);
        void DeleteById(int id);
        List<Tentity> GetAll();
        List<Tentity> GetAll(Expression<Func<Tentity, bool>> filter);
        Tentity GetByID(int? id);
        Tentity GetLastElement();
        void Save(Tentity entity);
    }
}