using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipePlatform.Models;

namespace RecipePlatform.DAL.Configurations
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.Property(r => r.Title).HasMaxLength(200).IsRequired();
            builder.Property(r => r.Description).HasMaxLength(1000);
            builder.Property(r => r.Ingredients).IsRequired();
            builder.Property(r => r.Instructions).IsRequired();

            builder.HasOne(r => r.Category)
                .WithMany(c => c.Recipes)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(r => r.User)
                .WithMany(u => u.Recipes)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
