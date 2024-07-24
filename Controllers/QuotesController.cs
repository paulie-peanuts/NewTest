using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class QuotesController : Controller
{
    private readonly QuoteService _quoteService;
    private readonly QuoteDbContext _dbContext;

    public QuotesController(QuoteService quoteService, QuoteDbContext dbContext)
    {
        _quoteService = quoteService;
        _dbContext = dbContext;
    }

    public async Task<IActionResult> Index()
    {
        var quotes = await _quoteService.GetQuotesAsync();
        var viewModel = new SelectedQuoteViewModel { Quotes = quotes };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> SaveQuote(SelectedQuoteViewModel model)
    {
        Console.WriteLine("first");
        if (ModelState.IsValid)
        {
            try
            {
                Console.WriteLine("second");
                var selectedQuote = new SavedQuote { Text = model.SelectedQuote };

                _dbContext.SavedQuotes.Add(selectedQuote);
                Console.WriteLine(selectedQuote);
                Console.WriteLine("added");
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ModelState.AddModelError(string.Empty, "An error occurred while saving the quote.");
            }
        }
        else
        {
            // Log model state errors
            foreach (var error in ModelState)
            {
                Console.WriteLine(
                    $"Key: {error.Key}, Error: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}"
                );
            }
        }
        // Re-fetch quotes in case of an error
        model.Quotes = await _quoteService.GetQuotesAsync();
        return View("Index", model);
    }

    [HttpGet]
        public async Task<IActionResult> List()
    {
        var quotes = await _dbContext.SavedQuotes.ToListAsync();
        return View(quotes);
    }
}
