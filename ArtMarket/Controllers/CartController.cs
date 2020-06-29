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
	[Authorize]
	public class CartController : BaseController
	{
		public CartManagement Management { get; set; }

		public ProductManagement ProductManagement { get; set; }
		// GET: Cart

		public CartController()
		{
			Management = new CartManagement();
			ProductManagement = new ProductManagement();
		}

		public ActionResult Index()
		{
			var user = TryGetUserId();
			var cart = Management.Get(user);
			return View("Index", cart);
		}

		public ActionResult Add(int id)
		{
			var user = TryGetUserId();
			var cart = Management.Get(user);

			var prod = ProductManagement.Get(id);

			var item = new CartItem() { CartId = cart.Id, ProductId = prod.Id, Price = prod.Price };

			CheckAuditPattern(item, true);

			Management.AddItem(item);

			return Index();
		}
	}
}