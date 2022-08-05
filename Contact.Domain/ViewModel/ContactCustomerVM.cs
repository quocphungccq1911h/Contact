using System;

namespace Contact.Domain.ViewModel
{
    public class ContactCustomerVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public DateTime? CreateDate { get; set; }
        public string IPAddress { get; set; }
    }
}
