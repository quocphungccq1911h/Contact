using Contact.Domain.PostViewModel;
using Contact.Domain.ResultAPI;
using System.Threading.Tasks;

namespace Contact.App.Services.Contact
{
    public interface IContactService
    {
        Task<ApiResult<bool>> CreateContact(PostContactCustomerVM model);
    }
}
