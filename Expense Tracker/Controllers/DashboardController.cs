using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Expense_Tracker.Controllers
{
    public class DashboardController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly CultureInfo _us = new("en-US");
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            //Last 7 Days
            var startDate = DateTime.Today.AddDays(-6);
            var endDate = DateTime.Today;

            var selectedTransactions = await _context.Transactions
                .Include(x => x.Category)
                .Where(y => y.Date >= startDate && y.Date <= endDate)
                .ToListAsync();

            //Total Income
            var totalIncome = selectedTransactions
                .Where(i => i.Category.Type == "Income")
                .Sum(j => j.Amount);
            ViewBag.TotalIncome = totalIncome.ToString("c", _us);

            //Total Expense
            var totalExpense = selectedTransactions
                .Where(i => i.Category.Type == "Expense")
                .Sum(j => j.Amount);
            ViewBag.TotalExpense = totalExpense.ToString("c", _us);

            //Balance
            var balance = totalIncome - totalExpense;
            _us.NumberFormat.CurrencyNegativePattern = 1;
            ViewBag.Balance = balance.ToString("c", _us);

            //Doughnut Chart - Expense By Category
            ViewBag.DoughnutChartData = selectedTransactions
                .Where(i => i.Category.Type == "Expense")
                .GroupBy(j => j.Category.CategoryId)
                .Select(k => new
                {
                    categoryTitleWithIcon = k.First().Category.Icon + " " + k.First().Category.Title,
                    amount = k.Sum(j => j.Amount),
                    formattedAmount = k.Sum(j => j.Amount).ToString("c", _us),
                })
                .OrderByDescending(l => l.amount)
                .ToList();

            //Spline Chart - Income vs Expense

            //Income
            var incomeSummary = selectedTransactions
                .Where(i => i.Category.Type == "Income")
                .GroupBy(j => j.Date)
                .Select(k => new SplineChartData()
                {
                    Day = k.First().Date.ToString("dd-MMM"),
                    Income = k.Sum(l => l.Amount)
                })
                .ToList();

            //Expense
            var expenseSummary = selectedTransactions
                .Where(i => i.Category.Type == "Expense")
                .GroupBy(j => j.Date)
                .Select(k => new SplineChartData()
                {
                    Day = k.First().Date.ToString("dd-MMM"),
                    Expense = k.Sum(l => l.Amount)
                })
                .ToList();

            //Combine Income & Expense
            var last7Days = Enumerable.Range(0, 7)
                .Select(i => startDate.AddDays(i).ToString("dd-MMM"))
                .ToArray();

            ViewBag.SplineChartData = from day in last7Days
                                      join income in incomeSummary on day equals income.Day into dayIncomeJoined
                                      from income in dayIncomeJoined.DefaultIfEmpty()
                                      join expense in expenseSummary on day equals expense.Day into expenseJoined
                                      from expense in expenseJoined.DefaultIfEmpty()
                                      select new
                                      {
                                          day = day,
                                          income = income?.Income ?? 0,
                                          expense = expense?.Expense ?? 0,
                                      };
            //Recent Transactions
            ViewBag.RecentTransactions = await _context.Transactions
                .Include(i => i.Category)
                .OrderByDescending(j => j.Date)
                .Take(5)
                .ToListAsync();


            return View();
        }
    }

    public class SplineChartData
    {
        public string Day;
        public decimal Income;
        public decimal Expense;

    }
}
