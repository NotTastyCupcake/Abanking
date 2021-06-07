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
    public class LanguagesController : Controller
    {
        #region Поля контролерра
        private readonly PersonContext _context;

        public LanguagesController(PersonContext context)
        {
            _context = context;
        }
        #endregion

        #region Список языков программирования
        public async Task<IActionResult> Index()
        {
            return View(await _context.Languages.ToListAsync());
        }
        #endregion

        #region Добавление языка программировая
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLanguage,NameLanguage")] Language language)
        {
            if (ModelState.IsValid)
            {
                _context.Add(language);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(language);
        }
        #endregion

        #region Изменить язык программирования
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language = await _context.Languages.FindAsync(id);
            if (language == null)
            {
                return NotFound();
            }
            return View(language);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLanguage,NameLanguage")] Language language)
        {
            if (id != language.IdLanguage)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(language);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LanguageExists(language.IdLanguage))
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
            return View(language);
        }
        #endregion

        #region Удалить язык программирования
        // POST: Languages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var language = await _context.Languages.FindAsync(id);
            _context.Languages.Remove(language);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Дополнительные методы
        private bool LanguageExists(int id)
        {
            return _context.Languages.Any(e => e.IdLanguage == id);
        }
        #endregion
    }
}
