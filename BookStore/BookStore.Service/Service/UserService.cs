using BookStoreAPI.Core.DTO;
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
    public class UserService : IUserService
    {
        IUnitOfWorkRepository _unit;
        private User m_user;
        public UserService(IUnitOfWorkRepository unit)
        {
            _unit=unit;
            m_user=new User();
        }

        public async Task<User> CheckLogin(LoginDTO login)
        {
            var user = await _unit.User.GetAll();
            if (user != null)
            {
                var result = user.SingleOrDefault(u => u.User_Account == login.User_Account && u.User_Password == login.User_Password);
                if (result != null) return result;
                else return null;
            }
            return null;
        }

        public async Task<bool> CreateUser(User user)
        {
            if (user != null)
            {
                //var m_list = await GetAllUser();
                user.User_Id = Guid.NewGuid();
                user.Role_Id = 3;
                user.Is_User_Status= true;
                await _unit.User.Add(user);
                var result=_unit.Save();
                if(result >0)return true;
            }
            return false;
        }
       
        public async Task<bool> DeleteUser(Guid userId)
        {
            var m_update = _unit.User.SingleOrDefault(m_user, u => u.User_Id == userId);
            if (m_update != null)
            {
                m_update.Is_User_Status = false;
                _unit.User.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            var result= await _unit.User.GetAll();
            if(result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<User> GetUserById(Guid userId)
        {
           var result=await _unit.User.GetById(userId);
            if (result != null) return result;
            return null;
        }

        public async Task<IEnumerable<User>> GetUserByName(string name)
        {
            var users = await GetAllUser();
            var result = from b in users where (b.User_Name.ToLower().Trim().Contains(name.ToLower().Trim())) select b;
            if (result.Count() > 0)
            {
                return result;
            }
            return null;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var m_update = _unit.User.SingleOrDefault(m_user, u => u.User_Id == user.User_Id);
            if (m_update != null)
            {
                m_update.User_Name = user.User_Name;
                m_update.Role_Id= user.Role_Id;
                m_update.User_Account=user.User_Account;
                m_update.User_Password= user.User_Password;
                m_update.User_Email= user.User_Email;
                m_update.User_Address= user.User_Address;
                m_update.User_Phone= user.User_Phone;
                m_update.Is_User_Gender= user.Is_User_Gender;
                m_update.Is_User_Status= user.Is_User_Status;
                _unit.User.Update(m_update);
                var result= _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }

        public async Task<bool> RestoreUser(Guid userId)
        {
            var m_update = _unit.User.SingleOrDefault(m_user, u => u.User_Id == userId);
            if (m_update != null)
            {
                m_update.Is_User_Status = true;
                _unit.User.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }
    }
}
