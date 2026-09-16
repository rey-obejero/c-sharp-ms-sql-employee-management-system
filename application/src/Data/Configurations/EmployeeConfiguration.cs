using EmployeeManagementSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagementSystem.Data.Configurations;

public sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees", table =>
            table.HasCheckConstraint("CK_Employees_Status", "[Status] IN ('Active', 'Inactive')"));

        builder.HasKey(e => e.EmployeeId);

        builder.Property(e => e.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.Email)
            .HasMaxLength(255)
            .IsRequired();

        builder.HasIndex(e => e.Email)
            .IsUnique()
            .HasDatabaseName("UX_Employees_Email");

        builder.Property(e => e.Phone)
            .HasMaxLength(30);

        builder.Property(e => e.Position)
            .HasMaxLength(100);

        builder.Property(e => e.HireDate)
            .HasColumnType("date");

        builder.Property(e => e.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(e => e.Salary)
            .HasColumnType("decimal(18,2)");

        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime2");

        builder.Property(e => e.UpdatedAt)
            .HasColumnType("datetime2");

        builder.HasIndex(e => new { e.LastName, e.FirstName })
            .HasDatabaseName("IX_Employees_Name");

        builder.HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Employees_Departments");
    }
}
