using MyApp.Entity.Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Contract.Services
{
    public interface IUserFormService
    {
        Task<IQueryable<UserForm>> GetAllForms();

        Task<int> GetActiveFormsCount(int userId);

        Task<UserForm> GetFormById(int formId);

        Task<int> DisableActiveForm(int userId);

        Task<int> CreateNewFrom(UserForm userForm);  
    }
}
