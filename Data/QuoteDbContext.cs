using System;
using Microsoft.EntityFrameworkCore;
// namespace NewTest.Data;

public class QuoteDbContext : DbContext
{

    public DbSet<SavedQuote> SavedQuotes { get; set; }
    public QuoteDbContext(DbContextOptions<QuoteDbContext> options) : base(options)
    {

    }
}