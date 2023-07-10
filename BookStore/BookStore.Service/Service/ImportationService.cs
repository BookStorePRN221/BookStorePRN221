using BookStoreAPI.Core.DiplayDTO;
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
    public class ImportationService : IImportationService
    {
        IUnitOfWorkRepository _unit;
        IUserRepository _user;
        private Importation m_import;
        public ImportationService(IUnitOfWorkRepository unit, IUserRepository user)
        {
            _unit = unit;
            _user = user;
        }
        public async Task<bool> CreateImport(Importation import)
        {
            if (import != null)
            {
                //var m_list = await GetAllImport();
                import.Import_Id = Guid.NewGuid();
                import.Is_Import_Status = true;
                await _unit.Importation.Add(import);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }
        

        public async Task<bool> DeleteImport(Guid importId)
        {
            var m_update = _unit.Importation.SingleOrDefault(m_import, u => u.Import_Id==importId);
            if (m_update != null)
            {
                m_update.Is_Import_Status = false;
                _unit.Importation.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }
        public async Task<IEnumerable<Importation>> GetAllImport()
        {
            var result = await _unit.Importation.GetAll();
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<DisplayImportationDTO>> GetDiplayImport()
        {
            var importList = await _unit.Importation.GetAll();
            var userList= await _unit.User.GetAll();
            var display = new List<DisplayImportationDTO>();
            foreach (var item in importList)
            {
                var import = new DisplayImportationDTO();
                import.Import_Id = item.Import_Id;
                import.Import_Quantity = item.Import_Quantity;
                import.Import_Amount=item.Import_Amount;
                import.Import_Date_Done = item.Import_Date_Done;
                import.Is_Import_Status= item.Is_Import_Status;
                import.User_Name = GetNameUser(item.User_Id, userList);
                display.Add(import);
            }
            if (display.Count < 1) return null;
            return display;
        }
        
        public async Task<Page<DisplayImportationDTO>> GetPaginatedDisplayImport(int pageSize, int currentPage)
        {
            // Retrieve the full list of DisplayImportationDTO
            IEnumerable<DisplayImportationDTO> allItems = await GetDiplayImport();

            // Calculate the total number of items
            int totalItems = allItems.Count();

            // Apply pagination to get items for the current page
            List<DisplayImportationDTO> items = allItems
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Create a Paginable object with the paginated items and other details
            Page<DisplayImportationDTO> paginatedResult = new Page<DisplayImportationDTO>(
                items,
                totalItems,
                pageSize,
                currentPage
            );

            return paginatedResult;
        }

        private string GetNameUser(Guid user_Id, IEnumerable<User> userList)
        {
            var userName = (from u in userList where u.User_Id == user_Id select u.User_Name).FirstOrDefault();
            return userName;
        }

        public Task<Book> GetImportById(Guid importId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateImport(Importation import)
        {
            var m_update = _unit.Importation.SingleOrDefault(m_import, u => u.Import_Id== import.Import_Id);
            if (import != null)
            {
                m_update.Import_Quantity= import.Import_Quantity;
                m_update.Import_Amount= import.Import_Amount;
                _unit.Importation.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }

        public async Task<bool> RestoreImport(Guid importId)
        {
            var m_update = _unit.Importation.SingleOrDefault(m_import, u => u.Import_Id == importId);
            if (m_update != null)
            {
                m_update.Is_Import_Status = true;
                _unit.Importation.Update(m_update);
                var result = _unit.Save();
                if (result > 0) return true;
            }
            return false;
        }
    }
}
