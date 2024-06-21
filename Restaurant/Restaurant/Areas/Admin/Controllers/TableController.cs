using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMS.Business.Exceptions;
using RMS.Business.Services.Abstracts;
using RMS.Business.DTOs.TableDTOs;
using RMS.Business.Exceptions.TableEx;

namespace Restaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TableController : Controller
    {
        readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        public async Task<IActionResult> Index()
        {
            var tables = await _tableService.GetAllTables(x => x.IsDeleted == false);
            return View(tables);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TableCreateDto CreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                await _tableService.Create(CreateDTO);
            }
            catch (EntityNullReferenceException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (RMS.Business.Exceptions.FileNotFoundException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (FileContentypeException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (TableCapacityException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (NameSizeException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (NameFormatException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (DuplicateException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            TableUpdateDto table;
            try
            {
                table = await _tableService.GetTableForUpdate(id);
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View("Error");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

            return View(table);
        }

        [HttpPost]
        public async Task<IActionResult> Update(TableUpdateDto UpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                await _tableService.Update(UpdateDTO.Id, UpdateDTO);
            }
            catch(EntityNotFoundException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View("Error");
            }
            catch (EntityNullReferenceException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (RMS.Business.Exceptions.FileNotFoundException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (FileContentypeException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (TableCapacityException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (NameSizeException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (NameFormatException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (DuplicateException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var exsist = _tableService.GetTable(x => x.Id == id);
            if (exsist == null) return View("Error");

            try
            {
                await _tableService.SoftDeleteTable(id);
            }
            catch (EntityNullReferenceException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View("Error");
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError(ex.MyProperty, ex.Message);
                return View("Error");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Error");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
