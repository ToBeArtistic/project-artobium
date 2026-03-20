using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

[HtmlTargetElement("gridcard")]
public class GridCardTagHelper : TagHelper
{
	public string? Title { get; set; }
    public string? Subtitle { get; set; }

	public string? Img { get; set; }

	private readonly IUrlHelperFactory _urlHelperFactory;

	public GridCardTagHelper(IUrlHelperFactory urlHelperFactory)
	{
		_urlHelperFactory = urlHelperFactory;
	}

	[ViewContext]
	[HtmlAttributeNotBound]
	public ViewContext ViewContext { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
		var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
		var resolvedImg = urlHelper.Content(Img ?? "");

        output.TagName = "div";
        output.Attributes.SetAttribute("class", "card m-2");
		output.Attributes.SetAttribute("style", "width: 18rem; max-height:100%;contain:strict;");

        output.Content.SetHtmlContent($@"
			<!--GridCard.cs-->
			<img class='card-img-top' src='{resolvedImg}' style=''/>
            <div class='card-body'>
				<h5 class='card-title'>This is the title</h5>
				<p class='card-text'>Card description</p>
            </div>
        ");
    }
}