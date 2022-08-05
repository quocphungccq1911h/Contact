using Contact.Core.Repository;
using Contact.Domain.PostViewModel;
using Contact.Domain.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Service.ContactCustomer
{
    public class ContactCustomerService : IContactCustomerService
    {
        private readonly IRepository<Core.Models.ContactCustomer> _repository;
        public ContactCustomerService(IRepository<Core.Models.ContactCustomer> repository)
        {
            _repository = repository;
        }
        public async Task<IQueryable<ContactCustomerVM>> Get()
        {
            var data = await Task.Run(() => _repository.Queryable().Select(x => new ContactCustomerVM()
            {
                CreateDate = x.CreateDate,
                Content = x.Content,
                Email = x.Email,
                ID = x.ID,
                Name = x.Name,
                Phone = x.Phone,
            }).OrderBy(x => x.CreateDate));
            return data;
        }
        public async Task<Core.Models.ContactCustomer> Add(PostContactCustomerVM model)
        {
            int count = await _repository.GetAll()
                .Where(x=>x.Phone.Equals(model.Phone) 
                && x.CreateDate.Value.Day.Equals(DateTime.Now.Day) 
                && x.CreateDate.Value.Month.Equals(DateTime.Now.Month) 
                && x.CreateDate.Value.Year.Equals(DateTime.Now.Year)).CountAsync();
            if (count > 5)
            {
                throw new Exception("Không thể liên hệ thêm");
            }
            var data = new Core.Models.ContactCustomer()
            {
                Content = model.Content,
                CreateDate = DateTime.Now,
                Email = model.Email,
                Name = model.Name,
                Phone = model.Phone
            };
            await _repository.AddAsync(data);
            await _repository.SaveNowAsync();
            return data;
        }
    }
}
