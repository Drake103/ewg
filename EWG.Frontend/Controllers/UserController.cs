﻿using System.Linq;
using System.Web;
using System.Web.Mvc;
using EWG.Domain.Entities.Security;
using EWG.Frontend.Security;
using EWG.Frontend.ViewModels;
using EWG.Infrastructure.Dal;

namespace EWG.Frontend.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthenticator _authenticator;
        private readonly ICrudRepository<IdentityUser> _repository;

        public UserController(IAuthenticator authenticator, ICrudRepository<IdentityUser> repository)
        {
            _authenticator = authenticator;
            _repository = repository;
        }

        [HttpGet]
        public ActionResult New(string returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserInputModel userInputModel)
        {
            if (_repository.Get().Any(x => x.Email == userInputModel.Email))
            {
                ModelState.AddModelError("Email", "Email is already in use");
            }
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    Username = UserInputModel.DefaultUsername,
                    HashedPassword = HashPassword(userInputModel.Password),
                    Email = userInputModel.Email
                };
                _repository.Save(user);
                _authenticator.SetCookie(user.Email, user.Username);
                return RedirectToAction("Index", "Replays");
            }
            return View("New", userInputModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Show()
        {
            var user = _repository.Get().SingleOrDefault(x => x.Email == User.Identity.Name);
            if (user == null)
            {
                throw new HttpException(404, "Not found");
            }
            return View(user);
        }

        private static string HashPassword(string value)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            return BCrypt.Net.BCrypt.HashPassword(value, salt);
        }
    }
}
