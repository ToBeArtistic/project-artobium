using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace artobium.Pages;

public class DevelopmentPageModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;

    public DevelopmentPageModel(ILogger<PrivacyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}

