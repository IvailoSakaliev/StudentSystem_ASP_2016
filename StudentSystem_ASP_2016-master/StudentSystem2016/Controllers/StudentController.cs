﻿using BissnessLogic.Sercises;
using DataAcsess.Models;
using StudentSystem2016.Filters.Entityfilters;
using StudentSystem2016.VModels.Students;
using System.Web.Mvc;
using System;
using DataAcsess.Enum;

namespace StudentSystem2016.Controllers
{
    
    public class StudentController 
        : GenericController<Student, EditVM, StudentList, StudentFilter, StudentServise>
    {
        public StudentController()
            :base()
        {

        }

        public override Student PopulateItemToModel(EditVM model, Student entity)
        {
            entity.Name = model.Name;
            entity.LastName = model.LastName;
            return entity;
        }

        public override EditVM PopulateModelToItem(Student entity, EditVM model)
        {
            try
            {
                model.Name = entity.Name;
                model.LastName = entity.LastName;
                model.Course = entity.Course.ToString();
                model.Email = entity.Email;
                model.OKS = entity.OKS;
                
            }
            catch (NullReferenceException)
            {
                
            }
            return model;
        }
        public override SingIn PopulateRegisterInfomationInModel(SingIn entity, EditVM model)
        {
            entity.Name = model.Name;
            entity.LastName = model.LastName;
            entity.Username = model.Username;
            entity.Email = model.Email;
            if (model.Password == model.ConfirmPassword)
            {
                entity.Password = model.Password;
            }
           
            if (model.Role != Roles.Student)
            {
                entity.Role = RoleAnotation(model.Role);
            }
            else
            {
                entity.Role = 2;
            }
            
            return entity;
        }
    }
}