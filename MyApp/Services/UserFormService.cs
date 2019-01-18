using MyApp.Contract.Repository;
using MyApp.Contract.Services;
using MyApp.Entity.Poco;
using MyApp.Repository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MyApp.Services
{
    public class UserFormService : IUserFormService
    {
        private readonly IMyAppRepositoryAsync _repository;

        public UserFormService()
        {
            _repository = new MyAppRepositoryAsync();
        }

        public Task<IQueryable<UserForm>> GetAllForms()
        {
            var result = _repository.GetQueryAsync<UserForm>();
            return Task.FromResult(result);
        }

        public Task<int> GetActiveFormsCount(int userId)
        {
            var result = _repository.CountAsync<UserForm>(x => x.UserId == userId && x.IsActive);
            return result;
        }

        public Task<int> CreateNewFrom(UserForm userForm)
        {
            userForm.Id = 0;
            userForm.IsActive = true;
            userForm.CreatedOn = DateTime.Now;
            return Task.FromResult(_repository.InsertAsync(userForm));
        }

        public Task<int> DisableActiveForm(int userId)
        {
            var activeFrom = _repository.FirstOrDefaultAsync<UserForm>(x => x.UserId == userId && x.IsActive).Result;
            if (activeFrom != null)
            {
                activeFrom.IsActive = false;
                return Task.FromResult(_repository.UpdateAsync(activeFrom));
            }
            return Task.FromResult(0);
        }

        public async Task<UserForm> GetFormById(int formId)
        {
            return await _repository.FirstOrDefaultAsync<UserForm>(x => x.Id == formId && x.IsActive);
        }
    }
}