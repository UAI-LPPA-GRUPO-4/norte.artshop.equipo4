﻿using ArtMarket.Controllers;
using BusinessLogic;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComprasOnline.Controllers
{
	public class ProductController : BaseController
	{
		public IProductManagement ProductManagement { get; set; }

		public ProductController()
		{
			ProductManagement = new ProductManagement();
		}

		// GET: Product
		public ActionResult Index()
		{
			return View(ProductManagement.GetAllProducts());
		}

		public ActionResult Create()
		{
			// todo: pasarle la lista de artistas para armar el select option
			return View();
		}

		[HttpPost]
		public ActionResult Create(FormCollection form)
		{
			//var listModel = db.ValidateModel(artist);
			//if (ModelIsValid(listModel))
			//  return View(artist);
			//try
			//{
			//  db.Create(artist);
			//  return RedirectToAction("Index");

			//}
			//catch (Exception ex)
			//{
			//  Logger.Instance.LogException(ex);
			//  ViewBag.MessageDanger = ex.Message;
			//  return View(artist);
			//}

			var product = new Product();

			product.Title = form["title"];
			product.Description = form["description"];
			product.ArtistId = Convert.ToInt32(form["artistId"]);
			product.Image = form["image"];
			product.QuantitySold = Convert.ToInt32(form["quantitySold"]);
			product.AvgStars = double.Parse(form["avgStars"], System.Globalization.CultureInfo.InvariantCulture);
			product.Price = Convert.ToInt32(form["price"]);

			CheckAuditPattern(product, false);
			ProductManagement.AddProduct(product);
			return RedirectToAction("Index");
		}

		public ActionResult Modify(int id)
		{
			Product product = ProductManagement.Get(id);
			return View(product);
		}

		public ActionResult Details(int id)
		{
			Product product = ProductManagement.Get(id);
			return View(product);
		}


		public ActionResult Search(string filter = null)
		{
			IList<Product> prod = null;
			if (string.IsNullOrEmpty(filter))
				prod = ProductManagement.GetAllProducts();
			else
				prod = ProductManagement.GetAllProducts().Where(x => x.Title == filter || x.Description.Contains(filter)).ToList();

			return View(prod);
		}


		[HttpPost]
		public ActionResult DoUpdate(Product product)
		{
			CheckAuditPattern(product, true);
			ProductManagement.Update(product);
			return RedirectToAction("Index");
		}

		public ActionResult Delete(int id)
		{
			ProductManagement.Delete(id);
			return RedirectToAction("Index");
		}
	}
}