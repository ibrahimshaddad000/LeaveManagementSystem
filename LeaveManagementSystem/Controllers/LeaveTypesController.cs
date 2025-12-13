using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeaveManagementSystem.Data;
using AutoMapper;
using LeaveManagementSystem.Models.LeaveTypes;
using LeaveManagementSystem.Services;

namespace LeaveManagementSystem.Controllers;

public class LeaveTypesController(ApplicationDbContext _context, IMapper _mapper, ILeaveTypeService _leavetypeservice ) : Controller
{

    private const string NameExistsErrorMessage = "The name you have entered already exists";
    

    // GET: LeaveTypes
    
    public async Task<IActionResult> Index()
    {
        var viewData = await _leavetypeservice.GetAll();
        return View( viewData);
    }

    // GET: LeaveTypes/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }

        var leaveType = await _leavetypeservice.Get<LeaveTypeReadOnlyVM>(id.Value);

        if(leaveType == null)
        {
            return NotFound();
        }

        return View(leaveType);


    }

    // GET: LeaveTypes/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: LeaveTypes/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LeaveTypeCreateVM leaveTypeCreate)
    {

        if (await _leavetypeservice.CheckIfLeaveTypeNameExists(leaveTypeCreate.Name))
        {
            ModelState.AddModelError(nameof(leaveTypeCreate.Name), NameExistsErrorMessage);
        }

        if (ModelState.IsValid)
        {
           await _leavetypeservice.Create(leaveTypeCreate);
            return RedirectToAction(nameof(Index));
        }
        return View(leaveTypeCreate);
    }

    
    // GET: LeaveTypes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leaveType = await _leavetypeservice.Get<LeaveTypeEditVM>(id.Value);

        if (leaveType == null)
        {
            return NotFound();
        }

        return View(leaveType);
    }

    // POST: LeaveTypes/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, LeaveTypeEditVM leaveTypeEdit)
    {

        if (id != leaveTypeEdit.Id)
        {
            return NotFound();
        }



        if (await _leavetypeservice.CheckIfLeaveTypeNameExistsForEdit(leaveTypeEdit))
        {
            ModelState.AddModelError(nameof(leaveTypeEdit.Name), NameExistsErrorMessage );
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _leavetypeservice.Edit(leaveTypeEdit);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_leavetypeservice.LeaveTypeExists(leaveTypeEdit.Id))
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
        return View(leaveTypeEdit);
    }

   

    // GET: LeaveTypes/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leaveType = await _leavetypeservice.Get<LeaveTypeReadOnlyVM>(id.Value);
            
        if (leaveType == null)
        {
            return NotFound();
        }

        return View(leaveType);
    }

    // POST: LeaveTypes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _leavetypeservice.Remove(id);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }




}
