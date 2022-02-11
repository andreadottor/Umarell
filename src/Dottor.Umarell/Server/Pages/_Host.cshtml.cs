namespace Dottor.Umarell.Server.Pages
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [Authorize]
    public class _HostModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
