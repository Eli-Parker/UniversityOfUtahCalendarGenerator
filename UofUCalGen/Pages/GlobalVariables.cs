// Ignore Spelling: Uof

using MudBlazor;

namespace UofUCalGen.Pages
{
    /// <summary>
    /// Holds all variables used by all pages.
    /// </summary>
    public static class GlobalVariables
    {
        /// <summary>
        /// Theme to use for the application.
        /// </summary>
        public static MudTheme SchoolSpiritTheme = new()
        {
            PaletteLight = new PaletteLight(),
            PaletteDark = new PaletteDark()
            {
                Primary = Colors.Red.Darken3,
                Secondary = Colors.Gray.Darken1,
                Tertiary = Colors.Red.Darken4,
                Background = Colors.Gray.Darken4,
                AppbarBackground = Colors.Red.Darken4,
            },

            LayoutProperties = new LayoutProperties()
            {
                DrawerWidthLeft = "0px",
                DrawerWidthRight = "0px"
            }
        };

        /// <summary>
        /// Controls whether the first section of the site is disabled
        /// </summary>
        public static bool FirstSectionDisabled;

        /// <summary>
        /// Controls whether the drop-down for the second section is expanded
        /// </summary>
        public static bool secondSectionExpanded;

        /// <summary>
        /// Gives the second page the path to the chosen file.
        /// </summary>
        public static string? chosenPath;
    }
}
