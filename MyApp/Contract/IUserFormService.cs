using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Contract
{
    public interface IUserFormService
    {
        Task<IList<UserForm>> GetAllForms();

        Task<int> GetActiveFormsCount(int userId);

        Task<UserForm> GetFormById(int formId);

        Task<int> DisableActiveForm(int userId);

        Task<int> CreateNewFrom(UserForm userForm);
    }
}
