using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imagizer.Controllers.API.V1
{
	public class ImageController : Controller
	{
		// GET: ImageController
		public ActionResult Index()
		{
			return View();
		}

		// GET: ImageController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: ImageController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: ImageController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: ImageController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: ImageController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: ImageController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: ImageController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
