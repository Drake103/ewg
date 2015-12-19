using System;
using System.Linq;
using System.Web.Mvc;
using EWG.Domain.Entities.Security;
using EWG.Frontend.Security;
using EWG.Infrastructure.Dal;
using EWG.Web.ViewModels;

namespace EWG.Web.Controllers
{
    public class SessionController : Controller
    {
        private readonly IAuthenticator _authenticator;
        private readonly ICrudRepository<IdentityUser> _repository;
        private const string errorMessage = "Invalid username or password";

        public SessionController(IAuthenticator authenticator, ICrudRepository<IdentityUser> repository)
        {
            _authenticator = authenticator;
            _repository = repository;
        }

        [HttpGet]
        public ActionResult New(string returnUrl)
        {
            throw new NotImplementedException();

            /*return View(new SessionViewModel { ReturnUrl = returnUrl });*/
        }

        [HttpPost]
        public JsonResult Create(SessionViewModel sessionViewModel)
        {
            IdentityUser user = null;
            if (ModelState.IsValid)
            {
                user = _repository.Get().FirstOrDefault(x => x.Email == sessionViewModel.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, errorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                if (!BCrypt.Net.BCrypt.Verify(sessionViewModel.Password, user.HashedPassword))
                {
                    ModelState.AddModelError(string.Empty, errorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                _authenticator.SetCookie(user.Email, user.Username);
                var returnUrl = sessionViewModel.ReturnUrl;
                if (returnUrl != null)
                {
                    Uri returnUri;
                    if (Uri.TryCreate(returnUrl, UriKind.Relative, out returnUri))
                    {
                        return Json(new {success = true});
                    }
                }

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        public JsonResult IsAuthenticated()
        {
            var identityName = HttpContext.User.Identity.Name;

            if (!string.IsNullOrEmpty(identityName))
            {
                var identity = _repository.Get().First(x => x.Email == identityName);
                return Json(new { success = true, username = identity.Username }, JsonRequestBehavior.AllowGet);
            }

            return Json(new {success = false}, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public JsonResult Destroy()
        {
            _authenticator.SignOut();
            Session.Abandon();
            return Json(new {success = true}, JsonRequestBehavior.AllowGet);
        }
    }
}
