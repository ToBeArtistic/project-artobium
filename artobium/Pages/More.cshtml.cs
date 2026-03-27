using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace artobium.Pages;

public class MorePageModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;

    public MorePageModel(ILogger<PrivacyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}

