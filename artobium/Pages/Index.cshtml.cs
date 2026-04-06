using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace artobium.Pages;


public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public readonly List<WorkItem> workItems;

    public List<Product> Products { get; private set; } = new();
    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        workItems = new List<WorkItem>()
            {
              new WorkItem
              {
                view = "_WorkItemPartialAntinomia",
                image = "antinomia-logo.png",
                header = "Work-Antinomia-Header",
                subtitle = "Work-Antinomia-Subtitle",
                teaser = "Work-Antinomia-Teaser",
                fulltext = "Work-Antinomia-Fulltext"
              },
              new WorkItem
              {
                view = "_WorkItemPartialEcoOnline",
                image = "ecoonline-logo.webp",
                header = "Work-EcoOnline-Header",
                subtitle = "Work-EcoOnline-Subtitle",
                teaser = "Work-EcoOnline-Teaser",
                fulltext = "Work-EcoOnline-Fulltext"
              },
              new WorkItem
              {
                view = "_WorkItemPartialNetcompany",
                image = "netcompany-logo.png",
                header = "Work-Netcompany-Header",
                subtitle = "Work-Netcompany-Subtitle",
                teaser = "Work-Netcompany-Teaser",
                fulltext = "Work-Netcompany-Fulltext"
              },
              new WorkItem
              {
                view = "_WorkItemPartialDTU",
                image = "DTU-logo.png",
                header = "Work-DTU-Header",
                subtitle = "Work-DTU-Subtitle",
                teaser = "Work-DTU-Teaser",
                fulltext = "Work-DTU-Fulltext"
              },
            };
    }

    public async Task OnGetAsync(string lang = "en")
    {

    }

    public IActionResult OnGetWorkItemPartial(string header)
    {
      WorkItem workItem = workItems.FirstOrDefault(x => x.header == header) ?? new WorkItem();

      return Partial(workItem.view, workItem);
    }
    public class WorkItem
    {
        public string view {get;set;} = "_WorkItemPartial";
        public string image {get;set;} = string.Empty;
        public string header { get; set; } = string.Empty;

        public string subtitle { get; set; } = string.Empty;
        public string teaser { get; set; } = string.Empty;

        public string fulltext { get; set; } = string.Empty;
    }

    public class Localizer
    {
        private readonly Dictionary<string, string> _translations;

        public Localizer(IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env)
        {
            List<string> supportedCultures = ["en"];
            var lang = httpContextAccessor.HttpContext?.Request.Query["lang"].ToString() ?? "en";
            lang = supportedCultures.Contains(lang) ? lang : "en";
            var path = Path.Combine(
                env.WebRootPath,
                "content",
                $"translations.{lang}.json"
            );
            var json = System.IO.File.ReadAllText(path);
            var jsonTranslations = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            _translations = jsonTranslations != null ? jsonTranslations : new Dictionary<string, string>();
        }

        public HtmlString this[string key] =>
            new HtmlString(_translations.TryGetValue(key, out var value) ? value : key);
    }
}
