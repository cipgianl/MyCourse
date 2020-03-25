using Microsoft.AspNetCore.Mvc;

namespace MyCourse.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index() {
            return Content("Sono Index, di Courses");
        }

        public IActionResult Detail(string id) {
            return Content($"Sono Detail, è stato scelto il corso: {id}");
        }        
    }
}