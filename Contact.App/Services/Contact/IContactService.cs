using Contact.Domain.PostViewModel;
using System.Threading.Tasks;

namespace Contact.App.Services.Contact
{
    public interface IContactService
    {
        Task<bool> CreateContact(PostContactCustomerVM model);
    }
}
