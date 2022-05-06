using BazingoBooks.DataAccess;
using BazingoBooks.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BazingoBooksWebApp.Controllers
{
    [Area("Admin")]

    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<BazingoBooks.Models.CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
            return View(objCoverTypeList);
        }

        // Create GET Action Method
        public IActionResult Create()
        {
            return View();
        }

        // Create POST Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BazingoBooks.Models.CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Your new Cover Type was created successfully";
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
            var coverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

            if (coverTypeFromDbFirst == null)
            {
                return NotFound();
            }

            return View(coverTypeFromDbFirst);
        }

        // Edit POST Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BazingoBooks.Models.CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
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
