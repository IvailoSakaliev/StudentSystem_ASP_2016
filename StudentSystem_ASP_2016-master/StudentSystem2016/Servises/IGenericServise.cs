﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using StudentSystem2016.Models;
using StudentSystem2016.Repository;

namespace StudentSystem2016.Servises
{
    public interface IGenericServise<TEntity> where TEntity : BaseModel, new()
    {
        GenericRepository<TEntity> _repo { get; set; }

        void Delete(Expression<Func<TEntity, bool>> filter);
        void Delete(TEntity entity);
        void DeleteById(int id);
        List<TEntity> GetAll();
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter);
        TEntity GetByID(int? id);
        TEntity GetLastElement();
        void Save(TEntity entity);
    }
}