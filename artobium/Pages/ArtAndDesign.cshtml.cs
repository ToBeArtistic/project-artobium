using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace artobium.Pages;

public class ArtAndDesignPageModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;

    public ArtAndDesignPageModel(ILogger<PrivacyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}

