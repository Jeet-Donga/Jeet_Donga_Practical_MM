using JeetDonga_Practical.BLL.Services;
using JeetDonga_Practical.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JeetDonga_Practical.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View(_service.GetAll());
        }

        [HttpPost]
        public IActionResult Save(UserModel model)
        {
            _service.Save(model); // insert or update
            return RedirectToAction("Index");
        }

        public JsonResult Delete(int id)
        {
            _service.Delete(id);
            return Json(true);
        }
        public JsonResult GetStates()
        {
            return Json(_service.GetStates());
        }

        public JsonResult GetCities(int stateId)
        {
            return Json(_service.GetCities(stateId));
        }
    }
}