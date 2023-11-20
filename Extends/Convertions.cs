using HauCK.Entiities;
using HauCK.Model;
using Microsoft.EntityFrameworkCore;

namespace HauCK.Extends
{
    public static class Convertions
    {
        public static async Task<List<UserModel>> Convert(this IQueryable<User> assignments)
        {

            return await (from p in assignments
                          select new UserModel
                          {
                              Address = p.Address,
                              Email = p.Email,
                              FistName = p.FistName,
                              LastName = p.LastName,
                              NumberPhone = p.NumberPhone
                          }).ToListAsync();
        }
        public static async Task<List<AssignerModel>> Convert(this IQueryable<Assigner> assignments)
        {

            return await (from p in assignments
                          select new AssignerModel
                          {
                              CreateTime = p.CreateTime,
                              Guid = p.Guid,
                              RoleAssignmentID = p.RoleAssignmentID,
                              TaskId = p.TaskId,
                              UserId = p.UserId,
                              UserUpdateId = p.UserUpdateId
                          }).ToListAsync();
        }
        public static async Task<List<CommentModel>> Convert(this IQueryable<Comment> assignments)
        {

            return await (from p in assignments
                          select new CommentModel
                          {
                              Guid = p.Guid,
                              Assignment = p.Assignment,
                              Content = p.Content,
                              dateTime = p.dateTime,
                              UserId = p.UserId
                          }).ToListAsync();
        }
        public static CommentModel Convert(this Comment p)
        {

            return new CommentModel
                          {
                              Guid = p.Guid,
                              Assignment = p.Assignment,
                              Content = p.Content,
                              dateTime = p.dateTime,
                              UserId = p.UserId
                          };
        }
        public static Comment Convert(this CommentModel  p)
        {

            return new Comment
            {
                Guid = p.Guid,
                Assignment = p.Assignment,
                Content = p.Content,
                dateTime = p.dateTime,
                UserId = p.UserId
            };
        }
        public static AssignerModel Convert(this Assigner assignments)
        {

            return new AssignerModel
            {
                CreateTime = assignments.CreateTime,
                Guid = assignments.Guid,
                RoleAssignmentID = assignments.RoleAssignmentID,
                TaskId = assignments.TaskId,
                UserId = assignments.UserId,
                UserUpdateId = assignments.UserUpdateId
            };
        }
        public static Assigner Convert(this AssignerModel assignments)
        {
            return new Assigner
            {
                CreateTime = assignments.CreateTime,
                Guid = assignments.Guid,
                RoleAssignmentID = assignments.RoleAssignmentID,
                TaskId = assignments.TaskId,
                UserId = assignments.UserId,
                UserUpdateId = assignments.UserUpdateId,
                AssignmentID = assignments.AssignmentID
            };
        }
        public static async Task<List<AssignmentModel>> Convert(this IQueryable<Assignment> assignments)
        {

            return await (from p in assignments
                          select new AssignmentModel
                          {
                              AssignmentOwenr = p.AssignmentOwenr,
                              DateEnd = p.DateEnd,
                              DateStart = p.DateStart,
                              Describe = p.Describe,
                              Guid = p.Guid,
                              Name = p.Name,
                              Status = p.Status
                          }).ToListAsync();
        }
        public static AssignmentModel Convert(this Assignment assignments)
        {

            return new AssignmentModel()
            {
                AssignmentOwenr = assignments.AssignmentOwenr,
                DateEnd = assignments.DateEnd,
                DateStart = assignments.DateStart,
                Describe = assignments.Describe,
                Guid = assignments.Guid,
                Name = assignments.Name,
                Status = assignments.Status
            };
        }
    }
}
