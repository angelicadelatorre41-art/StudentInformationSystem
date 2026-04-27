using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
namespace StudentInfoSystem.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
