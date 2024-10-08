using Microsoft.AspNetCore.Components;

namespace UofUCalGen.Pages
{
    /// <summary>
    /// This class is the base class for the CheckBoxListExt component.
    /// It is able to handle the selection of multiple items in a list.
    /// </summary>
    public class CheckBoxListExtBase : ComponentBase
    {
        public List<string> Tables { get; set; }
        public Dictionary<string, List<string>> EventList { get; set; }

        protected List<string> SelectedIds = new List<string>();
        public string SelectedEvents { get; set; }
        protected override void OnInitialized()
        {
            EventList = new();
            EventList.Add("t1", new List<string> { "e1", "e2", "e3" });
            EventList.Add("t2", new List<string> { "e4", "e5", "e6" });
            EventList.Add("t3", new List<string> { "e7", "e8", "e9" });
            SelectedEvents = string.Empty;
            Tables = ["t1", "t2", "t3"];
        }

        protected void ShowSelectedValues()
        {
            SelectedEvents = string.Join(",", SelectedIds.ToArray());
            StateHasChanged();
        }
    }
}
