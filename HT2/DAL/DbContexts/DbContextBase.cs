using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.DbContexts;

public class DbContextBase: DbContext
{
	public DbContextBase()
		: base(new DbContextOptionsBuilder<DbContextBase>()
			.UseInMemoryDatabase(databaseName: "Test")
			.Options)
	{ }

	public DbSet<Book> Books { get; set; }
	public DbSet<Rating> Ratings { get; set; }
	public DbSet<Review> Reviews { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		MapBooks(builder);
		MapRatings(builder);
		MapReviews(builder);
	}

	private void MapBooks(ModelBuilder builder)
	{
		builder.Entity<Book>()
			.Property(x => x.BookId)
			.ValueGeneratedOnAdd();

		builder.Entity<Book>()
			.HasMany(x => x.Ratings)
			.WithOne(x => x.Book)
			.HasForeignKey(x => x.RatingId)
			.HasForeignKey(x => x.BookId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.Entity<Book>()
			.HasMany(x => x.Reviews)
			.WithOne(x => x.Book)
			.HasForeignKey(x => x.ReviewId)
			.HasForeignKey(x => x.BookId)
			.OnDelete(DeleteBehavior.Cascade);
	}

	private void MapRatings(ModelBuilder builder)
	{
		builder.Entity<Rating>()
			.Property(x => x.RatingId)
			.ValueGeneratedOnAdd();
	}

	private void MapReviews(ModelBuilder builder)
	{
		builder.Entity<Review>()
			.Property(x => x.ReviewId)
			.ValueGeneratedOnAdd();
	}
}
