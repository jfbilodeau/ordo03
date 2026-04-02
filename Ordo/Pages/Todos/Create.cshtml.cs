using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ordo.Services;

namespace Ordo.Pages.Todos;

public class CreateModel(ITodoService todoService) : PageModel
{
    private readonly ITodoService _todoService = todoService;

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _todoService.Create(Input.Name, Input.DueDate);
        return RedirectToPage("/Todos/Index");
    }

    public class InputModel
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateOnly? DueDate { get; set; }
    }
}
