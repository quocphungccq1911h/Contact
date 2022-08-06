using Contact.Core.Repository;
using Contact.Domain.PostViewModel;
using Contact.Domain.ResultAPI;
using Contact.Domain.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using static Contact.Service.Constant.Constants;

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
                IPAddress = x.IPAddress
            }).OrderBy(x => x.CreateDate));
            return data;
        }
        public async Task<ApiResult<bool>> Add(PostContactCustomerVM model)
        {
            int countNumber = await _repository.GetAll()
                .Where(x=>x.Phone.Equals(model.Phone) 
                && x.CreateDate.Value.Day.Equals(DateTime.Now.Day) 
                && x.CreateDate.Value.Month.Equals(DateTime.Now.Month) 
                && x.CreateDate.Value.Year.Equals(DateTime.Now.Year)).CountAsync();
            int countIpAddress = await _repository.GetAll()
                .Where(x=>x.IPAddress.Equals(GetLocalIPAddress())
                && x.CreateDate.Value.Day.Equals(DateTime.Now.Day)
                && x.CreateDate.Value.Month.Equals(DateTime.Now.Month)
                && x.CreateDate.Value.Year.Equals(DateTime.Now.Year)).CountAsync();
            if (countNumber > LimitContact.PhoneNumberLimit || countIpAddress > LimitContact.IpAddressLimit)
            {
                return new ApiErrorResult<bool>("Đạt giới hạn liên hệ trong ngày. Không thể liên hệ thêm");
            }
            var data = new Core.Models.ContactCustomer()
            {
                Content = model.Content,
                CreateDate = DateTime.Now,
                Email = model.Email,
                Name = model.Name,
                Phone = model.Phone,
                IPAddress = GetLocalIPAddress()
            };
            
            await _repository.AddAsync(data);
            var result = await _repository.SaveNowAsync();
            if (result)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Liên hệ không thành công");
        }
        // get IP
        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
