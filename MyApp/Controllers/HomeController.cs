using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Services;
using MyApp.Contract.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApp.Entity.Poco;


namespace MyApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserFormService _userFormService;

        public HomeController()
        {
            _userFormService = new UserFormService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            //try
            //{
            //    var userForm = new UserForm
            //    {
            //        Field1 = string.Format("Field - {0}", DateTime.Now.ToString()),
            //        Field2 = string.Format("Field - {0}", DateTime.Now.ToString()),
            //        Field3 = string.Format("Field - {0}", DateTime.Now.ToString()),
            //        Field4 = string.Format("Field - {0}", DateTime.Now.ToString()),
            //        Field5 = string.Format("Field - {0}", DateTime.Now.ToString()),
            //        IsActive = true,
            //        CreatedOn = DateTime.Now
            //    };

            //    var totalCount = await _userFormService.AllFromCount();

            //    // Some record exists
            //    if (totalCount > 0)
            //    {
            //        var activeForm = _userFormService.GetActiveForm().Result;
            //        if(activeForm != null) {
            //            activeForm.IsActive = false;
            //            await _userFormService.Update(activeForm);
            //        }
            //    }

            //    var result = await _userFormService.CreateNewFrom(userForm);

            //}
            //catch (Exception e)
            //{

            //}

            return View();

        }

        [HttpGet]
        public async Task<JsonResult> GetFormById(int Id)
        {
            var formDetail = await _userFormService.GetFormById(Id);
            return Json(formDetail, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetHistory(int Id)
        {
            var result = await _userFormService.GetAllForms();
            var allForms = result.Where(x => x.UserId == Id).OrderByDescending(x => x.CreatedOn);
            return Json(allForms, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> CreateNewForm(UserForm newForm)
        {
            try
            {
                var activeFormsCount = await _userFormService.GetActiveFormsCount(newForm.UserId);

                // Some active forms exists for this user
                if (activeFormsCount > 0)
                {
                    // Disable active form for this user
                    await _userFormService.DisableActiveForm(newForm.UserId);
                }

                var count = await _userFormService.CreateNewFrom(newForm);
                if (count > 0)
                {
                    //TODO :: add some logging
                    return Json(new { success = true });
                }

                // TODO :: Add some logging
                return SendErrorResponse();
            }
            catch (Exception e)
            {
                // TODO :: Add some logging
                return SendErrorResponse();
            }
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteForm(int Id)
        {
            if (Id > 0)
            {
                var count = await _userFormService.DisableActiveForm(Id);
                if (count > 0)
                {
                    return Json(new { success = true });
                }
            }

            return SendErrorResponse();
        }

        private JsonResult SendErrorResponse()
        {
            return Json(new { success = false });
        }
    }
}
