using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class QuotesController : Controller
{
    private readonly QuoteService _quoteService;

    public QuotesController(QuoteService quoteService)
    {
        _quoteService = quoteService;
    }

    public async Task<IActionResult> Index()
    {
        var quotes = await _quoteService.GetQuotesAsync();
        return View(quotes);
    }
}
