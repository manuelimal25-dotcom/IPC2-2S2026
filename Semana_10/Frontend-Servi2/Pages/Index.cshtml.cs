using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Frontend_Servi2.Pages;

public class IndexModel : PageModel
{
    public IActionResult OnGet() => RedirectToPage("/Contribuyentes");
}
