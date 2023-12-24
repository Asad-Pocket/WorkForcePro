using Microsoft.AspNetCore.Mvc;
using WorkForcePro.Data;
using WorkForcePro.Models;
using WorkForcePro.Repositories;

namespace WorkForcePro.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ApplicationDbContext _context;
        private readonly IEmployeeRepository _employeeRepository;
        public CompanyController(ICompanyRepository companyRepository, IEmployeeRepository employeeRepository, ApplicationDbContext dbContext)
        {
            _companyRepository = companyRepository;
            _context = dbContext;
            _employeeRepository = employeeRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Company company)
        {
            _companyRepository.Insert(company);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            _companyRepository.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int id)
        {
            var company = _companyRepository.GetById(id);
            return View(company);
        }
        public IActionResult Edit(int id)
        {
            var company = _companyRepository.GetById(id);
            return View(company);
        }
        [HttpPost]
        public IActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                _companyRepository.Update(company);
                return RedirectToAction("Index");
            }

            return View(company);
        }
        public JsonResult ShowCompanies()
        {
            var companies = _companyRepository.GetAllItem().ToList();
            return Json(companies);
        }
    }
}
