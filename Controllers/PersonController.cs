using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Work.Models;

namespace Work.Controllers
{
    public class PersonController : Controller
    {
        #region Полня контролерра
        private readonly PersonContext _context;

        public PersonController(PersonContext context)
        {
            _context = context;
        }
        #endregion

        #region Выводит список
        public async Task<IActionResult> Index()
        {
            var personContext = _context.Persons.Include(m => m.Department).Include(m => m.Language);
            return View(await personContext.ToListAsync());
        }
        #endregion

        #region Добавляет сотрудника
        public IActionResult Add()
        {
            PopulateDepartmentsDropDownList();
            PopulateLanguageDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("IdPerson,FirstName,LastName,Age,Gender,IdDepartment,IdLanguage")] MainPersonModel mainPersonModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mainPersonModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateDepartmentsDropDownList(mainPersonModel.IdDepartment);
            PopulateLanguageDropDownList(mainPersonModel.IdLanguage);
            return View(mainPersonModel);
        }
        #endregion

        #region Изменение
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainPersonModel = await _context.Persons.FindAsync(id);
            if (mainPersonModel == null)
            {
                return NotFound();
            }
            PopulateDepartmentsDropDownList(mainPersonModel.IdDepartment);
            PopulateLanguageDropDownList(mainPersonModel.IdLanguage);
            return View(mainPersonModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPerson,FirstName,LastName,Age,Gender,IdDepartment,IdLanguage")] MainPersonModel mainPersonModel)
        {
            if (id != mainPersonModel.IdPerson)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mainPersonModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MainPersonModelExists(mainPersonModel.IdPerson))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateDepartmentsDropDownList(mainPersonModel.IdDepartment);
            PopulateLanguageDropDownList(mainPersonModel.IdLanguage);
            return View(mainPersonModel);
        }
        #endregion

        #region Удаление
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainPersonModel = await _context.Persons
                .Include(m => m.Department)
                .Include(m => m.Language)
                .FirstOrDefaultAsync(m => m.IdPerson == id);
            if (mainPersonModel == null)
            {
                return NotFound();
            }

            return View(mainPersonModel);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mainPersonModel = await _context.Persons.FindAsync(id);
            _context.Persons.Remove(mainPersonModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Дополнительные методы
        /// <summary>
        /// SelectList названий отделов из таблицы отделов
        /// </summary>
        /// <param name="selectedDepartment">ID отдела</param>
        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = from d in _context.Deportaments
                                   orderby d.NameDepart
                                   select d;
            ViewBag.IdDepartment = new SelectList(departmentsQuery, "IdDepartment", "NameDepart", selectedDepartment);
        }

        /// <summary>
        /// SelectList названий языка из таблицы языков
        /// </summary>
        /// <param name="selectedLanguages">ID Языка</param>
        private void PopulateLanguageDropDownList(object selectedLanguages = null)
        {
            var languageQuery = from d in _context.Languages
                                orderby d.NameLanguage
                                select d;
            ViewBag.IdLanguage = new SelectList(languageQuery, "IdLanguage", "NameLanguage", selectedLanguages);
        }

        /// <returns>Выводит список имен из базы данных.</returns>
        public ActionResult AutocompleteSearch(string term)
        {
             var models =  _context.Persons.Where(a => a.FirstName.Contains(term))
                    .Select(a => new { value = a.FirstName })
                    .Distinct();
            return Json(models);
        }

        private bool MainPersonModelExists(int id)
        {
            return _context.Persons.Any(e => e.IdPerson == id);
        }
        #endregion
    }
}
