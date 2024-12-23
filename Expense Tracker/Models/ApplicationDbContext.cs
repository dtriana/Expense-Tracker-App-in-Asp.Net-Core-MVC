using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.Models
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Transaction> Transactions => Set<Transaction>();
        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Title = "Housing", Description = "Mortage, insurance, maintanance, HOA Fees", Icon = "🏠", Type = "Expense" },
                new Category { CategoryId = 2, Title = "Utilities", Description = "Electricity, water, internet bills", Icon = "💡", Type = "Expense" },
                new Category { CategoryId = 3, Title = "Travel", Description = "Expenses for business or personal travel", Icon = "✈️", Type = "Expense" },
                new Category { CategoryId = 4, Title = "Dinning Out", Description = "Dining out expenses", Icon = "🍔", Type = "Expense" },
                new Category { CategoryId = 5, Title = "Groceries", Description = "Groceries expenses", Icon = "🛒", Type = "Expense" },
                new Category { CategoryId = 6, Title = "Entertainment", Description = "Movies, events, and subscriptions", Icon = "🎬", Type = "Expense" },
                new Category { CategoryId = 7, Title = "Healthcare", Description = "Medical and health-related expenses", Icon = "💊", Type = "Expense" },
                new Category { CategoryId = 8, Title = "Education", Description = "Books, courses, and tuition fees", Icon = "📚", Type = "Expense" },
                new Category { CategoryId = 9, Title = "Miscellaneous", Description = "Other uncategorized expenses", Icon = "📦", Type = "Expense" },
                new Category { CategoryId = 10, Title = "Pets", Description = "Food, veterinarian, grooming", Icon = "🐾", Type = "Expense" },
                new Category { CategoryId = 101, Title = "Salary", Description = "Monthly or bi-weekly paycheck from employment", Icon = "💼", Type = "Income" },
                new Category { CategoryId = 102, Title = "Freelance", Description = "Income from freelance or gig work", Icon = "🖌️", Type = "Income" },
                new Category { CategoryId = 103, Title = "Investments", Description = "Income from dividends, interest, or capital gains", Icon = "📈", Type = "Income" },
                new Category { CategoryId = 104, Title = "Rental Income", Description = "Income from property rentals", Icon = "🏠", Type = "Income" },
                new Category { CategoryId = 105, Title = "Business Income", Description = "Income from a business you own", Icon = "🏢" , Type = "Income" },
                new Category { CategoryId = 106, Title = "Pension", Description = "Income from retirement funds or pensions", Icon = "🏦", Type = "Income" },
                new Category { CategoryId = 107, Title = "Gifts", Description = "Monetary gifts or windfalls", Icon = "🎁", Type = "Income" },
                new Category { CategoryId = 108, Title = "Miscellaneous", Description = "Other sources of income", Icon = "📦", Type = "Income" }
            );
        }


    }
}
