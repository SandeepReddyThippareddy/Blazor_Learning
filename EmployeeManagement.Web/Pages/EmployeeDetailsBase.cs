using EmployeeManagement.Models.Models;
using EmployeeManagement.Web.Repositories.EmployeeRepo.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class EmployeeDetailsBase : ComponentBase
    {

        [Inject]
        public IEmployeeRepositoryService EmployeeService { get; set; }


        public Employee Employee { get; set; } = new Employee();


        [Parameter]
        public string Id { get; set; }

        public string Coordinates { get; set; }
        public string FooterButtonText { get; set; } = "Hide Actions";
        public string FooterCSSClass { get; set; } = null;


        protected override async Task OnInitializedAsync()
        {
            Employee = await EmployeeService.GetEmployeeAsync(Id);
        }

        protected void Mouse_Move(MouseEventArgs e)
        {
            Coordinates = $"X = {e.ClientX} Y = {e.ClientY}";
        }

        protected void Hide_Footer()
        {
            if (FooterButtonText.ToLower().Contains("show"))
            {
                FooterCSSClass = null;
                FooterButtonText = "Hide Actions";
            }
            else
            {
                FooterCSSClass = "hideFooter";
                FooterButtonText = "Show Actions";
            }
        }
    }
}