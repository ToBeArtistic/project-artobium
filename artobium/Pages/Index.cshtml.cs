using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;

namespace artobium.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public List<Product> Products {get;private set;} = new();
    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public async Task OnGetAsync()
    {

    }
}
