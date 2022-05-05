using BazingoBooks.DataAccess;
using BazingoBooks.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BazingoBooksWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _db;

        public CategoryController(ICategoryRepository db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<BazingoBooks.Models.Category> objCategoryList = _db.GetAll();
            return View(objCategoryList);
        }

        // Create GET Action Method
        public IActionResult Create()
        {
            return View();
        }

        // Create POST Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BazingoBooks.Models.Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                _db.Add(obj);
                _db.Save();
                TempData["success"] = "Your new Category was created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // Edit GET Action Method
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var categoryFromDbFirst = _db.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if(categoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(categoryFromDbFirst);
        }

        // Edit POST Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BazingoBooks.Models.Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                _db.Update(obj);
                _db.Save();
                TempData["success"] = "Your Category was updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // Delete GET Action Method
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var categoryFromDbFirst = _db.GetFirstOrDefault(u => u.Id == id);

            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(categoryFromDbFirst);
        }

        // Delete POST Action Method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Remove(obj);
            _db.Save();
            TempData["success"] = "Your Category has been deleted!";
            return RedirectToAction("Index");
        }
    }
}
