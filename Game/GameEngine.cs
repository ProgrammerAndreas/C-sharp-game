using PigeonCarrier.UI;

namespace PigeonCarrier.Game
{
    internal static class GameEngine
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            StoryManager.LoadProgress();

            Application.Run(new MenuForm());
        }
    }
}