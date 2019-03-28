using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dojodachi.Models;
// ADD THIS LINE U BITCH
using Microsoft.AspNetCore.Http;


namespace Dojodachi.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            // created an instance of a pet named Louie
            Dachi Louie = new Dachi()
            {
                Happiness = 20,
                Fullness = 20,
                Energy = 100,
                Meals = 3
            };
            if (HttpContext.Session.GetInt32("Happiness") == null)
            {
                HttpContext.Session.SetInt32("Happiness", 20);
                HttpContext.Session.SetInt32("Fullness", 20);
                HttpContext.Session.SetInt32("Energy", 50);
                HttpContext.Session.SetInt32("Meals", 3);
                HttpContext.Session.SetString("Message", "I am only an eel");
            }
            // don't put ViewBag in if statement ya div
            ViewBag.Happiness = HttpContext.Session.GetInt32("Happiness");
            ViewBag.Fullness = HttpContext.Session.GetInt32("Fullness");
            ViewBag.Energy = HttpContext.Session.GetInt32("Energy");
            ViewBag.Meals = HttpContext.Session.GetInt32("Meals");
            ViewBag.Message = HttpContext.Session.GetString("Message");



            return View();

        }


        [Route("feed")]
        [HttpGet]
        public IActionResult Feed()
        {
            Random rand = new Random();
            int Food = rand.Next(5, 11);
            if (HttpContext.Session.GetInt32("Meals") <= 0)
            {
                HttpContext.Session.SetString("Message", "You can't feed your pet without any meals!");
                return RedirectToAction("Index");
            }
            else
            {
                int IsFull = rand.Next(0, 4);
                if (IsFull == 0)
                {
                    int? NewEnergy1 = HttpContext.Session.GetInt32("Energy") - 5;
                    HttpContext.Session.SetInt32("Energy", (int)NewEnergy1);
                    HttpContext.Session.SetString("Message", "Nah he did not like that. Try again!");

                }
                else
                {
                    int? NewFullness = Food + HttpContext.Session.GetInt32("Fullness");
                    // Resets the session. New session plus old session.
                    HttpContext.Session.SetInt32("Fullness", (int)NewFullness);

                    int temp = (int)HttpContext.Session.GetInt32("Meals") - 1;
                    HttpContext.Session.SetInt32("Meals", temp);

                    HttpContext.Session.SetString("Message", "Louie ate some food");
                    Console.WriteLine("Message");

                }
                // Adds what session was before

                return RedirectToAction("Index");
            }
        }
        [Route("play")]
        [HttpGet]
        public IActionResult Play()
        {
            Random rand = new Random();

            if (HttpContext.Session.GetInt32("Energy") <= 0)
            {
                HttpContext.Session.SetString("Message", "Louie is too tired to play!");
                return RedirectToAction("Index");
            }
            else
            {
                int IsHappy = rand.Next(0, 4);
                if (IsHappy == 0)
                {
                    int? NewEnergy1 = HttpContext.Session.GetInt32("Energy") - 5;
                    HttpContext.Session.SetInt32("Energy", (int)NewEnergy1);
                    HttpContext.Session.SetString("Message", "Nah he did not like that. Try again!");

                }
                int Happiness = rand.Next(5, 10);
                int? NewEnergy = HttpContext.Session.GetInt32("Energy") - 5;
                int? NewHappiness = HttpContext.Session.GetInt32("Happiness") + Happiness;
                HttpContext.Session.SetInt32("Happiness", (int)NewHappiness);
                HttpContext.Session.SetInt32("Energy", (int)NewEnergy);
                HttpContext.Session.SetString("Message", "Louie loves fetch!");
            }
            return RedirectToAction("Index");
        }
        [Route("work")]
        [HttpGet]
        public IActionResult Work()
        {
            Random rand = new Random();
            if (HttpContext.Session.GetInt32("Energy") <= 0)
            {
                HttpContext.Session.SetString("Message", "Louie is too tired to work!");
                return RedirectToAction("Index");
            }
            else
            {
                int Meals = rand.Next(1, 4);
                int? MealTotal = HttpContext.Session.GetInt32("Meals") + Meals;
                HttpContext.Session.SetInt32("Meals", (int)MealTotal);

                int? Work = HttpContext.Session.GetInt32("Energy");
                HttpContext.Session.SetInt32("Energy", (int)Work - 5);
                HttpContext.Session.SetString("Message", "Louie went to work!");

                return RedirectToAction("Index");
            }
        }
        [Route("sleep")]
        [HttpGet]
        public IActionResult Sleep()
        {
            if (HttpContext.Session.GetInt32("Energy") == 100)
            {
                HttpContext.Session.SetString("Message", "Louie is well rested!");
                return RedirectToAction("Index");
            }
            else
            {
                int? NewFullness = HttpContext.Session.GetInt32("Fullness");
                HttpContext.Session.SetInt32("Fullness", (int)NewFullness - 5);
                int? NewHappiness = HttpContext.Session.GetInt32("Happiness");
                HttpContext.Session.SetInt32("Happiness", (int)NewHappiness - 5);
                int? Sleep = HttpContext.Session.GetInt32("Energy");
                HttpContext.Session.SetInt32("Energy", (int)Sleep + 5);
                HttpContext.Session.SetString("Message", "Louie is tired and is going to sleep!");
            }

            return RedirectToAction("Index");
        }
        [Route("restart")]
        [HttpGet]
        public IActionResult Restart()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index");

        }
    }
}
