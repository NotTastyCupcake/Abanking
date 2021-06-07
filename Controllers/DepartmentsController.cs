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
    public class DepartmentsController : Controller
    {
        #region Поля контролерра
        private readonly PersonContext _context;

        public DepartmentsController(PersonContext context)
        {
            _context = context;
        }
        #endregion

        #region Вывод списка
        public async Task<IActionResult> Index()
        {
            return View(await _context.Deportaments.ToListAsync());
        }
        #endregion

        #region Добавление отдела
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDepartment,NameDepart,Floor")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
        #endregion

        #region Редактирование
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Deportaments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDepartment,NameDepart,Floor")] Department department)
        {
            if (id != department.IdDepartment)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.IdDepartment))
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
            return View(department);
        }
        #endregion

        #region Удалить
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Deportaments
                .FirstOrDefaultAsync(m => m.IdDepartment == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Deportaments.FindAsync(id);
            _context.Deportaments.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Дополнительные методы
        /// <summary>
        /// Проверка - существует ID депортаминта или нет
        /// </summary>
        /// <param name="id">ID Депортаминта</param>
        /// <returns></returns>
        private bool DepartmentExists(int id)
        {
            return _context.Deportaments.Any(e => e.IdDepartment == id);
        }
        #endregion
    }
}
