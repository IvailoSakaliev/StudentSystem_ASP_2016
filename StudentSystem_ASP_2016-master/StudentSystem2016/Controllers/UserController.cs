﻿using StudentSystem2016.Models;
using StudentSystem2016.Servises.EntityServise;
using StudentSystem2016.Servises.ProjectServise;
using StudentSystem2016.VModels.Models.Orders;
using StudentSystem2016.VModels.Models.User;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace StudentSystem2016.Controllers
{

    public class UserController : Controller
    {
        UserServise _servise = new UserServise();
        EncriptServises _encript = new EncriptServises();
        OrderServise _order = new OrderServise();
        ProductServise _product = new ProductServise();

        private static int id;
        private static int UserID;
        private static int loginID;
        private static string fronyImage;
        public ActionResult Index()
        {
            return View();
        }
        //[UserFilter]
        [HttpGet]
        public ActionResult Details()
        {
            id = int.Parse(Session["User_ID"].ToString());
            OrderList itemVM = new OrderList();
            List<User> user = _servise.GetAll(x=> x.LoginID == id);
            int userID = user[0].ID;
            List<Order> orders = _order.GetAll(x => x.UserID == userID);
            itemVM = PopulateOrdersInformation(itemVM, orders);
            itemVM.CurrentUser = PopulateUserInformation(itemVM.CurrentUser, user[0]);
            itemVM.Product = PopulateProductINformation(itemVM.Product, orders);
            itemVM.QuantityOrderList = Populatequntity(itemVM.QuantityOrderList, orders);

            return View(itemVM);
        }

        private List<int> Populatequntity(List<int> quantityOrderList, List<Order> orders)
        {
            foreach (var item in orders)
            {
                quantityOrderList.Add(item.Quantity);
            }
            return quantityOrderList;
        }

        private IList<Product> PopulateProductINformation(IList<Product> product, List<Order> orders)
        {
            foreach (var item in orders)
            {
                product.Add(_product.GetByID(item.SubjectID));
            }
            return product;
        }

        private OrderList PopulateOrdersInformation(OrderList itemVM, List<Order> orders)
        {
            string OrderNumber = "";
            int count = 0;
            int quantityOfOrder = 0;
            double totalPrice = 0;
            List<Order> list = new List<Order>();

            for (int i = 0; i < orders.Count; i++)
            {
                if (OrderNumber == orders[i].OrderNumber)
                {
                    count++;
                    quantityOfOrder += orders[i].Quantity;
                    totalPrice += orders[i].Total;
                    OrderNumber = orders[i].OrderNumber;
                }
                else
                {
                    list.Add(orders[i]);
                    OrderNumber = orders[i].OrderNumber;
                    if (i == 0)
                    {
                        count++;
                        quantityOfOrder += orders[i].Quantity;
                        totalPrice += orders[i].Total;
                    }
                    else
                    {
                        itemVM.ProductCount.Add(count);
                        itemVM.QuantityList.Add(quantityOfOrder);
                        itemVM.TotalPriceList.Add(totalPrice);
                        count = 1;
                        quantityOfOrder = orders[i].Quantity;
                        totalPrice = orders[i].Total;
                    }

                }


            }

            itemVM.ProductCount.Add(count);
            itemVM.QuantityList.Add(quantityOfOrder);
            itemVM.TotalPriceList.Add(totalPrice);

            for (int i = 0; i < list.Count; i++)
            {
                itemVM.Items.Add(list[i]);
            }
            return itemVM;
        }

        private UserEditVm PopulateUserInformation(UserEditVm currentUser, User user)
        {
            currentUser.FirstName = _encript.DencryptData(user.Name);
            currentUser.SecondName = _encript.DencryptData(user.SecondName);
            currentUser.City = _encript.DencryptData(user.City);
            currentUser.Adress = _encript.DencryptData(user.Adress);
            currentUser.Telephone = _encript.DencryptData(user.Telephone);
            currentUser.Image = user.Image;
            return currentUser;
        }


        [HttpPost]
        public JsonResult GetInfoForUser()
        {
            return Json("ok");
        }

        [HttpGet]
        public ActionResult ChangeDetails()
        {
            UserEditVm model = new UserEditVm();
            List<User> entity = _servise.GetAll(x=> x.LoginID ==id);
            model.FirstName = _encript.DencryptData(entity[0].Name);
            model.SecondName = _encript.DencryptData(entity[0].SecondName);
            model.City = _encript.DencryptData(entity[0].City);
            model.Adress = _encript.DencryptData(entity[0].Adress);
            model.Telephone = _encript.DencryptData(entity[0].Telephone);
            UserID = entity[0].ID;
            model.Image = entity[0].Image;
            fronyImage = entity[0].Image;
            loginID = entity[0].LoginID;

            return View(model);
        }

        [HttpPost]
        public ActionResult ChangeDetails(UserEditVm model, HttpPostedFileBase[] photo)
        {

            User entity = new User();
            entity.ID = UserID;
            entity.Name = _encript.EncryptData(model.FirstName);
            entity.SecondName = _encript.EncryptData(model.SecondName);
            entity.City = _encript.EncryptData(model.City);
            entity.Adress = _encript.EncryptData(model.Adress);
            entity.Telephone = _encript.EncryptData(model.Telephone);

            if (photo[0] != null)
            {
                entity.Image = GetImagePath(photo);
                Addimage(photo);
            }
            else
            {
                entity.Image = fronyImage;
            }
           
            entity.LoginID = loginID;

            _servise.Save(entity);

            return RedirectToAction("Details");
        }
        private string GetImagePath(HttpPostedFileBase[] photo)
        {
            return "../images/Galery/" + photo[0].FileName;
        }

        private void Addimage(HttpPostedFileBase[] photo)
        {
            ImageServise _image = new ImageServise();
            foreach (HttpPostedFileBase item in photo)
            {
                string pic = System.IO.Path.GetFileName(item.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Images/Galery"), pic);
                // file is uploaded
                item.SaveAs(path);
            }


        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ChangePassword(string password)
        {
            Login user = new Login();
            LoginServise _login = new LoginServise();
            user = _login.GetByID(id);
            user.Password = _encript.EncryptData(password);
            _login.Save(user);

            return Json("ok");
        }
        [HttpGet]
        public ActionResult ChangeEmail()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ChangeEmail(string email)
        {
            Login user = new Login();
            LoginServise _login = new LoginServise();
            user = _login.GetByID(id);
            user.Email = _encript.EncryptData(email);
            _login.Save(user);

            return Json("ok");
        }

        [HttpPost]
        public JsonResult ChangeEmailUser()
        {
            return Json("ok");
        }

        [HttpPost]
        public JsonResult ChangePasswordlUser()
        {
            return Json("ok");
        }

        [HttpGet]
        public ActionResult DetailsUserOrders()
        {
            id = int.Parse(Session["User_ID"].ToString());
            OrderList itemVM = new OrderList();
            List<User> user = _servise.GetAll(x => x.LoginID == id);
            int userID = user[0].ID;
            List<Order> orders = _order.GetAll(x => x.UserID == userID);
            itemVM = PopulateOrdersInformation(itemVM, orders);
            itemVM.CurrentUser = PopulateUserInformation(itemVM.CurrentUser, user[0]);
            itemVM.Product = PopulateProductINformation(itemVM.Product, orders);
            itemVM.QuantityOrderList = Populatequntity(itemVM.QuantityOrderList, orders);

            return View(itemVM);
        }
    }

}