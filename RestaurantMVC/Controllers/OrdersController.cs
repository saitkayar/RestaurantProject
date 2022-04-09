﻿using Microsoft.AspNetCore.Mvc;
using RestaurantMVC.DataAccess.Abstract;
using RestaurantMVC.DataAccess.Concrete;
using RestaurantMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantMVC.Controllers
{
    public class OrdersController : Controller
    {
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private ITableRepository _tableRepository;


        public OrdersController()
        {
            _orderRepository =new OrderRepository();
            _productRepository = new ProductRepository();
            _tableRepository = new TableRepository();
        }

        public IActionResult Index()
        {
     
            ViewBag.orders =_orderRepository.GetByProduct();
            return View();
        }
        public IActionResult Add()
        {
            ViewBag.products = _productRepository.Getall();
            ViewBag.tables = _tableRepository.Getall();
            return View();
        }
        public IActionResult Update(int id)
        {
            ViewBag.products = _productRepository.Getall();
            ViewBag.tables = _tableRepository.Getall();
            var order = this._orderRepository.GetById(id);
            if (order == null)
            {
                RedirectToAction("Index");
            }
            ViewBag.product = order;
            return View();

        }
        public IActionResult DeleteById(int id)
        {
            this._orderRepository.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Save(Order order)
        {
            //string route = (order.Id == 0) ? "Add" : "Update";
            //if (order. == null)
            //{
            //    return RedirectToAction(route, "Please enter Name");

            //}
            //if (order.SurName == null)
            //{
            //    return RedirectToAction(route, "please enter surname");

            //}
            if (order.Id == 0)
            {
                this._orderRepository.Add(order);

            }
            else
            {
                this._orderRepository.Update(order);
            }
            return RedirectToAction("Index");

        }
    }
}
