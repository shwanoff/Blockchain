using BlockchainExplorerWeb.Models;
using PagedList;
using System.Web.Mvc;

namespace BlockchainExplorerWeb.Controllers
{
    /// <summary>
    /// Контроллер цепочки блоков.
    /// </summary>
    public class ChainController : Controller
    {
        /// <summary>
        /// Отображение формы добавления данных пользователю.
        /// </summary>
        public ActionResult AddBlock()
        {
            return View();
        }

        /// <summary>
        /// Получение данных с формы добавления данных от пользователя.
        /// </summary>
        /// <param name="data"> Добавляемые данные. </param>
        [HttpPost]
        public ActionResult AddBlock(Data data)
        {
            var block = Instance.Get().Chain.AddBlock(data.Content);
            return View("GetBlock", block);
        }

        /// <summary>
        /// Получить блок данных по его хешу и отобразить пользователю.
        /// </summary>
        /// <param name="hash"> Хеш блока. </param>
        public ActionResult GetBlock(string hash)
        {
            var block = Instance.Get().Chain.GetBlock(hash);
            if (block == null)
            {
                return new HttpNotFoundResult($"Block with hash {hash} not found.");
            }
            return View(block);
        }

        /// <summary>
        /// Выполнить синхронизацию цепочек с сервером.
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Sync(int? page)
        {
            int pageSize = 25;
            int pageNumber = (page ?? 1);
            var chain = Instance.Get().Chain;
            chain.Sync();
            return View("Index", chain.GetChainResult?.ToPagedList(pageNumber, pageSize));
        }

        /// <summary>
        /// Отобразить пользователю все блоки.
        /// </summary>
        /// <param name="page"> Пагинация. </param>
        public ActionResult Index(int? page)
        {
            int pageSize = 25;
            int pageNumber = (page ?? 1);
            var chain = Instance.Get().Chain;
            return View(chain.GetChainResult?.ToPagedList(pageNumber, pageSize));
        }    }
}