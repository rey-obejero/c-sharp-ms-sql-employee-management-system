using EmployeeManagementSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagementSystem.Data.Configurations;

public sealed class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("Departments");

        builder.HasKey(d => d.DepartmentId);

        builder.Property(d => d.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(d => d.Name)
            .IsUnique();

        builder.Property(d => d.CreatedAt)
            .HasColumnType("datetime2");
    }
}
