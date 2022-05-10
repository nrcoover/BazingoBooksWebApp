using BazingoBooks.DataAccess;
using BazingoBooks.DataAccess.Repository.IRepository;
using BazingoBooks.Models;
using BazingoBooks.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BazingoBooksWebApp.Controllers
{
    [Area("Admin")]

    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<BazingoBooks.Models.CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
            return View(objCoverTypeList);
        }

        // Upsert GET Action Method
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                // create product
                //ViewBag.CategoryList = CategoryList;
                //ViewData["CoverTypeList"] = CoverTypeList;
                return View(productVM);
            } 
            else
            {
                // update product
            }

            return View(productVM);
        }

        // Upsert POST Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                //_unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Your Cover Type was updated successfully";
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
            var coverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

            if (coverTypeFromDbFirst == null)
            {
                return NotFound();
            }

            return View(coverTypeFromDbFirst);
        }

        // Delete POST Action Method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Your Cover Type has been deleted!";
            return RedirectToAction("Index");
        }
    }
}
