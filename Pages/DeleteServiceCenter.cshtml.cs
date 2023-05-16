using Car_Store.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Car_Store.Pages
{
    public class DeleteServiceCenterModel : PageModel
    {
        [BindProperty]
        public int ID { get; set; }
        public DB db { get; set; }
        public ServicesCenters myCenter { get; set; }


        public IActionResult OnGet(int id)
        {

            if (HttpContext.Session.GetInt32("User_Type") != 1)
            {
                return RedirectToPage("/index");
            }

            ID = id;

            return Page();
        }

        public IActionResult OnPostYes(int id)
        {
            myCenter = new ServicesCenters();
            myCenter.delete(id);
            return RedirectToPage("/ServiceCenters");
        }
        public IActionResult OnPostNo()
        {
            return RedirectToPage("/index");
        }

    }
}
