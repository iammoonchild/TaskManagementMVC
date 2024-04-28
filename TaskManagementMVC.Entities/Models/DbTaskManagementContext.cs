using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TaskManagementMVC.Entities.Models;

public partial class DbTaskManagementContext : DbContext
{
    public DbTaskManagementContext()
    {
    }

    public DbTaskManagementContext(DbContextOptions<DbTaskManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<LogType> LogTypes { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<TaskLog> TaskLogs { get; set; }

    public virtual DbSet<TaskState> TaskStates { get; set; }

    public virtual DbSet<TaskType> TaskTypes { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=dpg-codoama0si5c7394gpl0-a.oregon-postgres.render.com\n;Database=db_task_management;Username=db_task_management_user;Password=qBM7Pvlag7PGfEVmqz5EIWTsjQMCWxDN");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AspNetUsers_pkey");

            entity.Property(e => e.Avatar).HasColumnType("character varying");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_DATE");
            entity.Property(e => e.Email).HasColumnType("character varying");
            entity.Property(e => e.IsActive).HasDefaultValueSql("true");
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("false");
            entity.Property(e => e.IsPasswordChanged).HasDefaultValueSql("false");
            entity.Property(e => e.Name).HasColumnType("character varying");
            entity.Property(e => e.OldPassword).HasColumnType("character varying");
            entity.Property(e => e.Password).HasColumnType("character varying");
            entity.Property(e => e.PhoneNo).HasColumnType("character varying");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetUsers)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AspNetUsers_RoleId_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.AspNetUsers)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("AspNetUsers_TeamId_fkey");
        });

        modelBuilder.Entity<LogType>(entity =>
        {
            entity.HasKey(e => e.LogTypeId).HasName("LogTypes_pkey");

            entity.Property(e => e.LogTypeName).HasColumnType("character varying");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("Tasks_pkey");

            entity.Property(e => e.IsCompleted).HasDefaultValueSql("false");
            entity.Property(e => e.TaskDescription).HasColumnType("character varying");
            entity.Property(e => e.TaskTitle).HasColumnType("character varying");

            entity.HasOne(d => d.AssignedBy).WithMany(p => p.TaskAssignedBies)
                .HasForeignKey(d => d.AssignedById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_task_assignedby");

            entity.HasOne(d => d.AssignedTo).WithMany(p => p.TaskAssignedTos)
                .HasForeignKey(d => d.AssignedToId)
                .HasConstraintName("fk_task_assignedto_aspnetuserid");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.TaskCreatedBies)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fK_task_createdby_aspnetuserid");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.TaskModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("fk_task_modifiedby");

            entity.HasOne(d => d.TaskState).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.TaskStateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_taskstate_id");

            entity.HasOne(d => d.TaskType).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.TaskTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tasktype_id");
        });

        modelBuilder.Entity<TaskLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("TaskLogs_pkey");

            entity.Property(e => e.CommentedById).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_DATE");
            entity.Property(e => e.LogDescription).HasColumnType("character varying");
            entity.Property(e => e.LogTypeId).ValueGeneratedOnAdd();
            entity.Property(e => e.TaskId).ValueGeneratedOnAdd();
            entity.Property(e => e.TransferredById).ValueGeneratedOnAdd();
            entity.Property(e => e.TransferredFromId).ValueGeneratedOnAdd();
            entity.Property(e => e.TransferredToId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.CommentedBy).WithMany(p => p.TaskLogCommentedBies)
                .HasForeignKey(d => d.CommentedById)
                .HasConstraintName("fk_commentedbyid");

            entity.HasOne(d => d.LogType).WithMany(p => p.TaskLogs)
                .HasForeignKey(d => d.LogTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_logtypeid");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskLogs)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_taskid");

            entity.HasOne(d => d.TransferredBy).WithMany(p => p.TaskLogTransferredBies)
                .HasForeignKey(d => d.TransferredById)
                .HasConstraintName("fk_transbyid");

            entity.HasOne(d => d.TransferredFrom).WithMany(p => p.TaskLogTransferredFroms)
                .HasForeignKey(d => d.TransferredFromId)
                .HasConstraintName("fk_transfromid");

            entity.HasOne(d => d.TransferredTo).WithMany(p => p.TaskLogTransferredTos)
                .HasForeignKey(d => d.TransferredToId)
                .HasConstraintName("fk_transtoid");
        });

        modelBuilder.Entity<TaskState>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("TaskStates_pkey");

            entity.Property(e => e.StateName).HasColumnType("character varying");
        });

        modelBuilder.Entity<TaskType>(entity =>
        {
            entity.HasKey(e => e.TaskTypeId).HasName("TaskTypes_pkey");

            entity.Property(e => e.TaskTypeName).HasColumnType("character varying");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("Teams_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_DATE");
            entity.Property(e => e.Pmid).HasColumnName("PMId");

            entity.HasOne(d => d.Pm).WithMany(p => p.Teams)
                .HasForeignKey(d => d.Pmid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pmid");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("UserRoles_pkey");

            entity.Property(e => e.RoleName).HasColumnType("character varying");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
