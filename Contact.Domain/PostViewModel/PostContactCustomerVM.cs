using System.ComponentModel.DataAnnotations;

namespace Contact.Domain.PostViewModel
{
    public class PostContactCustomerVM
    {
        public string Name { get; set; }
        [Phone(ErrorMessage = "Số điện thoại không đúng định dạng!")]
        public string Phone { get; set; }
        [EmailAddress(ErrorMessage = "Định dạng email không đúng")]
        public string Email { get; set; }
        public string Content { get; set; }
    }
}
