using System.ComponentModel.DataAnnotations;

namespace Contact.Domain.PostViewModel
{
    public class PostContactCustomerVM
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
    }
}
