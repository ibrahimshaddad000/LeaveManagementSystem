using AutoMapper;
using LeaveManagementSystem.Data;
using LeaveManagementSystem.Models.LeaveTypes;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Services;

public class LeaveTypeService(ApplicationDbContext _context, IMapper _mapper) : ILeaveTypeService
{

    public async Task<List<LeaveTypeReadOnlyVM>> GetAll()
    {
        var data = await _context.LeaveTypes.ToListAsync();
        return _mapper.Map<List<LeaveTypeReadOnlyVM>>(data);
    }

    public async Task<T?> Get<T>(int id) where T : class
    {
        var data = await _context.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);
        if (data == null)
            return null;

        return _mapper.Map<T>(data);
    }

    public async Task Remove(int id)
    {
        var data = await _context.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);
        if (data != null)
        {
            _context.Remove(data);
            await _context.SaveChangesAsync();
        }

    }

    public async Task Edit(LeaveTypeEditVM leaveTypeEdit)
    {
        var leaveType = _mapper.Map<LeaveType>(leaveTypeEdit);
        _context.Update(leaveType);
        await _context.SaveChangesAsync();
    }

    public async Task Create(LeaveTypeCreateVM leaveTypeCreate)
    {
        var leaveType = _mapper.Map<LeaveType>(leaveTypeCreate);
        _context.Add(leaveType);
        await _context.SaveChangesAsync();
    }


    public bool LeaveTypeExists(int id)
    {
        return _context.LeaveTypes.Any(e => e.Id == id);
    }

    public async Task<bool> CheckIfLeaveTypeNameExists(string name)
    {
        return await _context.LeaveTypes.AnyAsync(lt => lt.Name.ToLower() == name.ToLower());
    }

    public async Task<bool> CheckIfLeaveTypeNameExistsForEdit(LeaveTypeEditVM leaveTypeEdit)
    {
        return await _context.LeaveTypes.AnyAsync(lt => lt.Name.ToLower() == leaveTypeEdit.Name.ToLower() && lt.Id != leaveTypeEdit.Id);
    }





}
