using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.Models;
using TaskManagementMVC.Entities.ViewModels.Manager;
using TaskManagementMVC.Entities.ViewModels.UserViewModels;
using TaskManagementMVC.Repositories.IRepositories;
using TaskManagementMVC.Repositories.Utilities;

namespace TaskManagementMVC.Repositories.Repositories
{
    public class ManagerRepo : IManagerRepo
    {
        private readonly DbTaskManagementContext _context;
        public ManagerRepo(DbTaskManagementContext context)
        {
            _context = context;
        }

        public void SetTeamMembersData(TeamMembersViewModel viewModel, long PMId)
        {
            //here first register the team
            Team team = new Team
            {
                Pmid = PMId,
                CreatedDate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
            };
            _context.Add(team);
            _context.SaveChanges();
            var subject = "PTM | Team Invitation";
            foreach (var item in viewModel.FirstName)
            {
                var member = new AspNetUser
                {
                    Name = viewModel.FirstName[viewModel.FirstName.IndexOf(item)] + " " + viewModel.LastName[viewModel.FirstName.IndexOf(item)],
                    Email = viewModel.Email[viewModel.FirstName.IndexOf(item)],
                    RoleId = short.Parse(viewModel.Role[viewModel.FirstName.IndexOf(item)]),
                    Password = GenerateRandomPassword(8),
                    OldPassword = GenerateRandomPassword(8),
                    TeamId = team.TeamId,
                    IsPasswordChanged = false
                };
                var emailBody = EmailSender.GetEmailTemplateForInvitation(member.Email,member.Password);
                member.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(member.Password, HashType.SHA512);
                _context.AspNetUsers.Add(member);
                EmailSender.SendEmail(member.Email, emailBody, subject);
            }
            _context.SaveChanges();
        }
        private static Random random = new Random();

        public static string GenerateRandomPassword(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                int randomIndex = random.Next(validChars.Length);
                sb.Append(validChars[randomIndex]);
            }

            return sb.ToString();
        }

        public TeamMembersViewModel GetTeamMembersData(int managerId)
        {
            var data = _context.AspNetUsers.Where(x => x.CreatedBy == managerId).Select (x => new TeamMembersDataViewModel
            {
                TeamMemberId = x.Id,
                RoleId = x.RoleId,
                Name = x.Name,
                Email = x.Email
            });

            var viewModel = new TeamMembersViewModel();
            viewModel.GetTeamMembersData = data.ToList();
            viewModel.UserName = _context.AspNetUsers.Where(x => x.Id == managerId).Select(x => x.Name).FirstOrDefault();
            return viewModel;
        }

        public IQueryable<Team> GetTeams(int managerId)
        {
            return _context.Teams.Include(x => x.AspNetUsers).Where(x => x.Pmid == managerId);
        }

        public IQueryable<AspNetUser> GetTeamWorkDetails(int teamId)
        {
            return _context.AspNetUsers.Include(x => x.TaskAssignedTos).Where(x => x.TeamId == teamId);
        }

        public IQueryable<AspNetUser> GetTeamMembersDataForCalendar(long pMId)
        {
            var data =  _context.Teams.Where(x => x.Pmid == pMId).Select(x => x.TeamId).ToList();
            return _context.AspNetUsers.Include(x => x.TaskAssignedTos).Include(x => x.Teams).Where(x => data.Contains((long)x.TeamId));
        }

        public IQueryable<AspNetUser> GetTeamsForCalendar(long pMId)
        {
            var data = _context.Teams.Where(x => x.Pmid == pMId).Select(x => x.TeamId).ToList();
            return _context.AspNetUsers.Include(x => x.Teams).Where(x => data.Contains((long)x.TeamId) && x.RoleId == 2);
        }

        public void AddNewMember(MemberForm model)
        {
            var subject = "PTM | Team Invitation";
            var member = new AspNetUser
            {
                Name = model.FirstName + " " + model.LastName,
                Email = model.Email,
                RoleId = model.RoleId,
                Password = GenerateRandomPassword(8),
                OldPassword = GenerateRandomPassword(8),
                TeamId = model.TeamId,
                IsPasswordChanged = false
            };
            _context.Add(member);
            _context.SaveChanges();
            var emailBody = EmailSender.GetEmailTemplateForInvitation(member.Email, member.Password);
            member.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(member.Password, HashType.SHA512);
            _context.AspNetUsers.Add(member);
            EmailSender.SendEmail(member.Email, emailBody, subject);
        }
    }
}
