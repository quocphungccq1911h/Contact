using Contact.App.Services.Contact;
using Contact.Domain.PostViewModel;
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
        public HomeController(IConfiguration configuration, IContactService service)
        {
            _configuration = configuration;
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(PostContactCustomerVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            try
            {
                var result = await _service.CreateContact(model);
                if (result.IsSuccessed)
                {
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
    }
}
