using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.Models;
using TaskManagementMVC.Entities.ViewModels.TasksViewModels;
using TaskManagementMVC.Repositories.IRepositories;
using TaskManagementMVC.Services.IServices;
using static TaskManagementMVC.Repositories.Enums.TaskUtilities;

namespace TaskManagementMVC.Services.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepo _taskRepo;
        private readonly IUserRepo _userRepo;
        public TaskService(ITaskRepo taskRepo, IUserRepo userRepo)
        {
            _taskRepo = taskRepo;
            _userRepo = userRepo;
        }

        public List<Dictionary<long, string>> GetComments(long taskId)
        {
            var task = _taskRepo.GetTaskFromTaskId(taskId);
            List<Dictionary<long, string>> comments = new List<Dictionary<long, string>>();
            foreach (var comment in task.TaskLogs.Where(x => x.CommentedById != null).OrderByDescending(x => x.CreatedDate))
            {
                var com = $"{comment.LogDescription}";
                comments.Add(new Dictionary<long, string> { { comment.LogId, com } });
            }
            return comments;
        }

        public TaskDetailsViewModel GetTaskDetails(long taskId)
        {
            var task = _taskRepo.GetTaskFromTaskId(taskId);
            TaskDetailsViewModel model = new TaskDetailsViewModel
            {
                Id = task.TaskId,
                Title = task.TaskTitle,
                Status = ((TaskStateEnum)task.TaskStateId).ToString(),
                Description = task.TaskDescription,
                Type = ((TaskTypeEnum)task.TaskTypeId).ToString(),
                AssignedTo = task.AssignedTo.Name,
                AssignedBy = task.AssignedBy.Name,
                CreatedBy = task.CreatedBy.Name,
                Avatar = task.AssignedTo.Avatar,
                Deadline = task.Deadline,
                Logs = new List<Dictionary<long, string>>(),
                Comment = "",
                Comments = new List<Dictionary<long, string>>()
            };
            foreach (var log in task.TaskLogs.OrderByDescending(x => x.CreatedDate))
            {
                Dictionary<long, string> logDict = new Dictionary<long, string>
                {
                    { log.LogId, log.LogDescription }
                };
                model.Logs.Add(logDict);
            }
            foreach (var comment in task.TaskLogs.Where(x => x.CommentedById != null).OrderByDescending(x => x.CreatedDate))
            {
                var com = $"{comment.LogDescription} by {comment.CommentedBy.Name} on {comment.CreatedDate}\n";
                model.Comments.Add(new Dictionary<long, string> { { comment.LogId, com } });
            }
            return model;
        }

        public void SaveComment(string com, long taskId, long userId)
        {
            var user = _userRepo.GetUserFromUserId(userId);
            TaskLog comment = new TaskLog
            {
                TaskId = taskId,
                LogDescription = $"{user.Name} Commented : {com} on {DateTime.Now}",
                LogTypeId = (int)TaskLogTypeEnum.Commented,
                CommentedById = userId
            };
            _taskRepo.AddTaskLog(comment);
        }

        public void UpdateTask(long taskId, TaskDetailsViewModel model, long userId)
        {
            AspNetUser user = _userRepo.GetUserFromUserId(userId);
            Entities.Models.Task task = _taskRepo.GetTaskFromTaskId(taskId);
            Entities.Models.Task taskForLog = task;

            bool isTitleUpdated = model.Title != taskForLog.TaskTitle;
            bool isDescriptionUpdated = model.Description != taskForLog.TaskDescription;
            bool isDeadlineUpdated = model.Deadline != taskForLog.Deadline;

            task.TaskTitle = model.Title;
            task.TaskDescription = model.Description;
            task.Deadline = model.Deadline;
            task.ModifiedBy = userId;


            if (isTitleUpdated || isDescriptionUpdated || isDeadlineUpdated)
            {

                _taskRepo.UpdateTask(task);
                string logMessage = $"Task updated by {user.Name} as ";
                if (isTitleUpdated)
                {
                    logMessage += $" Title updated from '{taskForLog.TaskTitle}' to '{task.TaskTitle}'.";
                }
                if (isDescriptionUpdated)
                {
                    logMessage += $" Description updated from '{taskForLog.TaskDescription}' to '{task.TaskDescription}'.";
                }
                if (isDeadlineUpdated)
                {
                    logMessage += $" Deadline updated from '{taskForLog.Deadline}' to '{task.Deadline}'.";
                }

                TaskLog taskLog = new TaskLog
                {
                    TaskId = taskId,
                    LogDescription = logMessage + $" on {DateTime.Now}",
                    LogTypeId = (int)TaskLogTypeEnum.Updated
                };
                _taskRepo.AddTaskLog(taskLog);
            }
        }
    }
}
