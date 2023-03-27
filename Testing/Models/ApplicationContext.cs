using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Testing.Models;

public partial class ApplicationContext : DbContext
{
    public ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.Property(e => e.Text).IsFixedLength();

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Answer_Question");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK_Theme");

            entity.Property(e => e.Name).IsFixedLength();
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.Property(e => e.Text).IsFixedLength();

            entity.HasOne(d => d.Test).WithMany(p => p.Questions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Question_Test");
        });

        modelBuilder.Entity<Result>(entity =>
        {

            entity.HasOne(d => d.Test).WithMany(p => p.Results)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Result_Test");

            entity.HasOne(d => d.User).WithMany(p => p.Results)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Result_User");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Name).IsFixedLength();
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasOne(d => d.Category).WithMany(p => p.Tests)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Test_Theme");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email).IsFixedLength();
            entity.Property(e => e.Password).IsFixedLength();
            entity.Property(e => e.UserName).IsFixedLength();

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
