using System.Web.Mvc;

namespace BlockchainExplorerWeb.Controllers
{
    /// <summary>
    /// Контроллер информационных страниц.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Отобразить главную страницу.
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Отобразить страницу о приложении.
        /// </summary>
        public ActionResult About()
        {

            return View();
        }
    }
}