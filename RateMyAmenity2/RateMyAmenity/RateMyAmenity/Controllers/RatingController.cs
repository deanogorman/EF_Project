﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RateMyAmenity.Models;
using System.Web.Security;

namespace RateMyAmenity.Controllers
{
    public class RatingController : Controller
    {
        private RateMyAmenityContext db = new RateMyAmenityContext();

        //
        // GET: /Rating/

        public ActionResult Index()
        {
            return View(db.Ratings.ToList());
        }

        //
        // GET: /Rating/Details/5

        public ActionResult Details(int id = 0)
        {
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        //
        // GET: /Rating/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Rating/Create

        [HttpPost]
        public ActionResult Create(Rating rating, int id)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    rating.UserId = (Guid)Membership.GetUser().ProviderUserKey;

                    var amenityRating = db.Amenities.Find(id);
                    rating.AmenityID = amenityRating.AmenityID;

                    db.Ratings.Add(rating);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException)
                ModelState.AddModelError("", "Unable to save changes due an unforeseen error.  Please try again.");
            } 

            return View(rating);
        }

        //
        // GET: /Rating/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        //
        // POST: /Rating/Edit/5

        [HttpPost]
        public ActionResult Edit(Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rating);
        }

        //
        // GET: /Rating/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        //
        // POST: /Rating/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Rating rating = db.Ratings.Find(id);
            db.Ratings.Remove(rating);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}