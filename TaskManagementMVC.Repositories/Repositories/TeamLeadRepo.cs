using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.Models;
using TaskManagementMVC.Repositories.IRepositories;

namespace TaskManagementMVC.Repositories.Repositories;
public class TeamLeadRepo : ITeamLeadRepo
{
    private readonly DbTaskManagementContext _context;
    public TeamLeadRepo(DbTaskManagementContext context)
    {
        _context = context;
    }
    public long GetTeamIdFromUserId(long UserId)
    {
       return _context.AspNetUsers.FirstOrDefault(x => x.Id == UserId).TeamId ?? 0;
    }

    public IEnumerable<AspNetUser> GetTeamMembersDataForCalendar(long tLId)
    {
        var teamId = GetTeamIdFromUserId(tLId);
        return _context.AspNetUsers.Include(x => x.TaskAssignedTos).Include(x => x.Teams).Where(x => x.TeamId == teamId);
    }

    public IQueryable<AspNetUser> GetTeamUsersWithTasksFromTeamId(long teamId)
    {
        var temp = _context.Tasks.Include(x => x.AssignedTo).Include(x => x.CreatedBy)
            .Where(x => x.AssignedTo.TeamId == teamId);
        return _context.AspNetUsers.Include(x => x.TaskAssignedTos).Where(x => x.TeamId == teamId);
    }
}
