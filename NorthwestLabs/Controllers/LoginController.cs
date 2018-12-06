﻿using NorthwestLabs.DAL;
using NorthwestLabs.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthwestLabs.Controllers
{
    public class LoginController : Controller
    {
        // New DB for NorthwestLabsContext
        NorthwestLabsContext db = new NorthwestLabsContext();

        // Getters and Setters for LoginUsername and Password
        public static string sUsername { get; set; }
        public static string sPassword { get; set; }
        // Getter and Setter for Successful Login message
        public static string sLogin { get; set; }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        //Login for existing customer -> will renavigate to the view they wanted after login
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult LoginSuccess(FormCollection form)
        {
            sUsername = form["username"].ToString();
            sPassword = form["password"].ToString();
            ViewBag.UnsuccessfulLogin = "Your Username and/or password did not match our records. Please try again";

            if ((sUsername == "Client") && (sPassword == "Success"))
            {
                sLogin = "You have successfully logged in.";
                return RedirectToAction("Index", "Home"); }
            if ((sUsername == "Labtech") && (sPassword == "Success"))
            {return RedirectToAction("LabTech", "Employee");}
            if ((sUsername == "Admin") && (sPassword == "Success"))
            { return RedirectToAction("Admin", "Employee"); }
            if ((sUsername == "Director") && (sPassword == "Success"))
            { return RedirectToAction("<<DIRECTOR VIEW AFTER LOGIN>>", "Employee"); }
            if ((sUsername == "Finance") && (sPassword == "Success"))
            {return RedirectToAction("LabTech", "Employee");}
            else
            {
                // This will navigate you back to the login page if authentication is unsuccessful
                return View("Login");
            }
        }

        //Returns view for new customer registration
        public ActionResult Registration()
        {
            return View();
        }

        //Registration for new customer
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Registration(Customers newcustomer)
        {
            newcustomer.logins.LoginUserName = newcustomer.LoginUserName;

            if (db.Login.Find(newcustomer.logins.LoginUserName) == null)
            {
                sLogin = "Registration complete! You have successfully logged in.";
                db.Customer.Add(newcustomer);
                db.SaveChanges();
            }
            else
            {
                ViewBag.error = "The Username you entered is already in use. Please use another Username.";
                return View();
            }

            // Renavigates to the home page and shows successful registration and login viewbag
            return RedirectToAction("Index", "Home");
        }
    }
}
