using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace artobium.Pages;

public class InfoPageModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;

    public InfoPageModel(ILogger<PrivacyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}

