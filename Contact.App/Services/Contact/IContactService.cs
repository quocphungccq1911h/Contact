using Contact.Domain.PostViewModel;
using Contact.Domain.ResultAPI;
using Contact.Domain.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contact.App.Services.Contact
{
    public interface IContactService
    {
        Task<ApiResult<bool>> CreateContact(PostContactCustomerVM model);
        Task<List<ContactCustomerVM>> GetAll();
    }
}
