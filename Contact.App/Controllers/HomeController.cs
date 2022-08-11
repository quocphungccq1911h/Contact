using Contact.App.Services.Contact;
using Contact.Domain.PostViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Contact.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IContactService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(IConfiguration configuration, IContactService service, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            PostContactCustomerVM model = new PostContactCustomerVM();
            if (Request.Cookies[SystemConstants.Cookie] != null)
            {
                model.Name = Request.Cookies[SystemConstants.CookieContact.Name];
                model.Email = Request.Cookies[SystemConstants.CookieContact.Email];
                model.Phone = Request.Cookies[SystemConstants.CookieContact.Phone];
            }
            return View(model);
        }
        //[HttpPost]
        //public async Task<IActionResult> Index(PostContactCustomerVM model)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return View(ModelState);
        //    }
        //    try
        //    {

        //        var result = await _service.CreateContact(model);
        //        if (result.IsSuccessed)
        //        {
        //            CookieOptions options = new CookieOptions();
        //            Response.Cookies.Append(SystemConstants.Cookie, SystemConstants.Cookie, options);
        //            Response.Cookies.Append(SystemConstants.CookieContact.Name, model.Name, options);
        //            Response.Cookies.Append(SystemConstants.CookieContact.Email, model.Email, options);
        //            Response.Cookies.Append(SystemConstants.CookieContact.Phone, model.Phone, options);
        //            options.Expires = DateTime.Now.AddDays(7);

        //            TempData["SuccessMsg"] = "Cám ơn bạn đã liên hệ";
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            TempData["ErrorMsg"] = result.Message;
        //            return View();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        [HttpPost]
        public async Task<JsonResult> Index(PostContactCustomerVM model)
        {
            
            var result = await _service.CreateContact(model);
            if (result.IsSuccessed)
            {
                CookieOptions options = new CookieOptions();
                Response.Cookies.Append(SystemConstants.Cookie, SystemConstants.Cookie, options);
                Response.Cookies.Append(SystemConstants.CookieContact.Name, model.Name, options);
                Response.Cookies.Append(SystemConstants.CookieContact.Email, model.Email, options);
                Response.Cookies.Append(SystemConstants.CookieContact.Phone, model.Phone, options);
                options.Expires = DateTime.Now.AddDays(7);
            }
            return Json(result);
        }
        public IActionResult ContactSuccess()
        {
            return View();
        }

    }
}
