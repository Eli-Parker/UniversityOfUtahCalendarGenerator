using Microsoft.AspNetCore.Components;

namespace UofUCalGen.Pages
{
    /// <summary>
    /// This class is the base class for the CheckBoxListExt component.
    /// It is able to handle the selection of multiple items in a list.
    /// </summary>
    public class CheckBoxListExtBase : ComponentBase
    {
        public List<string> EventList { get; set; }

        protected List<string> SelectedIds = new List<string>();
        public string SelectedEvents { get; set; }
        protected override void OnInitialized()
        {
            EventList = ["Fall break", "Spring break", "Christmas"];
            SelectedEvents = string.Empty;
        }

        protected void ShowSelectedValues()
        {
            SelectedEvents = string.Join(",", SelectedIds.ToArray());
            StateHasChanged();
        }
    }
}
