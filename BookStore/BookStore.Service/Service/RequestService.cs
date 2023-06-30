using Azure.Core;
using BookStoreAPI.Core.Interface;
using BookStoreAPI.Core.Model;
using Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class RequestService : IRequestService
    {
        IUnitOfWorkRepository _unit;
        private BookingRequest m_request;
        public RequestService(IUnitOfWorkRepository unit)
        { 
            _unit = unit;
        }

        public async Task<bool> CreateRequest(BookingRequest request)
        {
            if (request != null)
            {
                var m_list = await GetAllRequest();
                request.Request_Id = Guid.NewGuid();
                request.Is_Request_Status = 1;
                await _unit.Request.Add(request);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }
        public async Task<IEnumerable<BookingRequest>> GetAllRequest()
        {
            var result = await _unit.Request.GetAll();
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public Task<Book> GetRequestById(Guid requestId)
        {
            throw new NotImplementedException();
        }
      
        public async Task<bool> UpdateRequest(BookingRequest request)
        {
            var m_update = _unit.Request.SingleOrDefault(m_request, u => u.Request_Id == request.Request_Id);
            if (m_update != null)
            {
                m_update.Book_Id = request.Book_Id;
                m_update.Request_Image_Url = request.Request_Image_Url;
                m_update.Request_Book_Name= request.Request_Book_Name;
                m_update.Request_Quantity = request.Request_Quantity;
                m_update.Request_Price = request.Request_Price;
                m_update.Request_Amount = request.Request_Amount;
                m_update.Request_Date = DateTime.Now;
                m_update.Request_Date_Done = DateTime.Now;
                m_update.Request_Note = request.Request_Note;
                m_update.Is_Request_Status = 1;
                m_update.Is_RequestBook_Status=request.Is_RequestBook_Status;
                _unit.Request.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }
        public async Task<bool> DeleteRequest(Guid requestId)
        {
            var m_update = _unit.Request.SingleOrDefault(m_request, u => u.Request_Id==requestId);
            if (m_update != null)
            {
                m_update.Is_Request_Status = 0;
                _unit.Request.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }
        public async Task<bool> RestoreRequest(Guid requestId)
        {
            var m_update = _unit.Request.SingleOrDefault(m_request, u => u.Request_Id == requestId);
            if (m_update != null)
            {
                m_update.Is_Request_Status = 2;
                _unit.Request.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }
    }
}
