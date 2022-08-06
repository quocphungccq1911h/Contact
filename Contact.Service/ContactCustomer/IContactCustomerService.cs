using Contact.Domain.PostViewModel;
using Contact.Domain.ResultAPI;
using Contact.Domain.ViewModel;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Service.ContactCustomer
{
    public interface IContactCustomerService
    {
        Task<IQueryable<ContactCustomerVM>> Get();
        Task<ApiResult<bool>> Add(PostContactCustomerVM model);
    }
}
