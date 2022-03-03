using EmployeeManagement.Models.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class DisplayEmployeeBase : ComponentBase
    {
        [Parameter]
        public Employee Employee { get; set; }

        public bool ShowFooter { get; set; } = false;

        [Parameter]
        public EventCallback<bool> OnCheckboxChanged { get; set; }

        protected async Task OnCheckboxStatusChanged(ChangeEventArgs e)
        {
            ShowFooter = (bool)e.Value;
            await OnCheckboxChanged.InvokeAsync((bool)e.Value);
        }


    }
}
