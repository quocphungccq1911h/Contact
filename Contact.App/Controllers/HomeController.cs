using Contact.App.Services.Contact;
using Contact.Domain.PostViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            if (Request.Cookies["name"] != null)
            {
                model.Name = Request.Cookies["name"];
                model.Email = Request.Cookies["email"];
                model.Phone = Request.Cookies["phone"];
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(PostContactCustomerVM model)
        {

            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            try
            {
                var result = await _service.CreateContact(model);
                if (result.IsSuccessed)
                {
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Append("name", model.Name, options);
                    Response.Cookies.Append("email", model.Email, options);
                    Response.Cookies.Append("phone", model.Phone, options);
                    options.Expires = DateTime.Now.AddDays(7);

                    TempData["SuccessMsg"] = "Cám ơn bạn đã liên hệ";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMsg"] = result.Message;
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult Create()
        {

            return View();
        }
        public IActionResult Read()
        {
            //string key = "DemoCookie";
            var CookieValue1 = Request.Cookies["name"];
            var CookieValue2 = Request.Cookies["email"];
            var CookieValue3 = Request.Cookies["phone"];
            return View();
        }
        //public IActionResult Remove()
        //{
        //    string key = "DemoCookie";
        //    string value = DateTime.Now.ToString();

        //    CookieOptions options = new CookieOptions();
        //    options.Expires = DateTime.Now.AddDays(-1);
        //    Response.Cookies.Append(key, value, options);
        //    return View();
        //}

    }
}
