using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;

namespace artobium.Pages;

public class HeroPageModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IConfiguration _config;

    public List<Product> Products {get;private set;} = new();
    public HeroPageModel(ILogger<IndexModel> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }

    public async Task OnGetAsync()
    {
        var connString = _config.GetConnectionString("DefaultConnection");
        _logger.LogDebug(connString);
        await using var conn = new NpgsqlConnection(connString);
        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand(
            "SELECT id, name, description, price, stock FROM products ORDER BY name", conn);

        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            _logger.LogDebug(reader.ToString());
            Products.Add(new Product
            {
                Id          = reader.GetGuid(0),
                Name        = reader.GetString(1),
                Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                Price       = reader.GetDecimal(3),
                Stock       = reader.GetInt32(4),
            });
        }

        _logger.LogDebug(Products.Count.ToString());
    }
}

public class Product
{
    public Guid   Id          { get; set; }
    public string Name        { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal Price      { get; set; }
    public int    Stock       { get; set; }
}
