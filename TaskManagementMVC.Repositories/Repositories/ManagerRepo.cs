﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.Data;
using TaskManagementMVC.Entities.Models;
using TaskManagementMVC.Entities.ViewModels.UserViewModels;
using TaskManagementMVC.Repositories.IRepositories;

namespace TaskManagementMVC.Repositories.Repositories
{
    public class ManagerRepo : IManagerRepo
    {
        private readonly DbTaskManagementContext _context;
        public ManagerRepo(DbTaskManagementContext context)
        {
            _context = context;
        }

        public void SetTeamMembersData(TeamMembersViewModel viewModel)
        {
            foreach (var item in viewModel.FirstName)
            {
                _context.AspNetUsers.Add(new AspNetUser
                {
                    Name = viewModel.FirstName[viewModel.FirstName.IndexOf(item)] + " " + viewModel.LastName[viewModel.FirstName.IndexOf(item)],
                    Email = viewModel.Email[viewModel.FirstName.IndexOf(item)],
                    RoleId = short.Parse(viewModel.Role[viewModel.FirstName.IndexOf(item)]),
                    Password = GenerateRandomPassword(8),
                    OldPassword = GenerateRandomPassword(8)

                });
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
    }
}