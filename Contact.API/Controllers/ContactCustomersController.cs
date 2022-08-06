using Contact.Domain.PostViewModel;
using Contact.Service.ContactCustomer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Contact.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactCustomersController : ControllerBase
    {
        private readonly IContactCustomerService _service;
        public ContactCustomersController(IContactCustomerService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GET()
        {
            var data = await _service.Get();
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> POST(PostContactCustomerVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var res = await _service.Add(model);
                if (!res.IsSuccessed)
                {
                    return BadRequest(res);
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
