using ConferenceBooking.Core.Dtos;

namespace ConferenceBooking.ConsoleUI
{
    public class ConsolePrinter
    {
        private const ConsoleColor DefaultColor = ConsoleColor.Gray;
        private const ConsoleColor ErrorColor = ConsoleColor.Red;
        private const ConsoleColor WarningColor = ConsoleColor.Yellow;
        private const ConsoleColor InfoColor = ConsoleColor.White;
        private const ConsoleColor SuccessColor = ConsoleColor.Green;
        private const ConsoleColor MenuColor = ConsoleColor.Gray;
        private const ConsoleColor PromptColor = ConsoleColor.Cyan;
        private const ConsoleColor SubtleColor = ConsoleColor.DarkGray;

        public static void PrintPageTitle(string text)
        {
            Console.Clear();
            Console.ForegroundColor = DefaultColor;
            Console.WriteLine(text.ToUpper());
        }

        public static void PrintMenuTitle(string text)
        {
            Console.ForegroundColor = MenuColor;
            Console.WriteLine();
            Console.WriteLine(text);
        }

        public static void PrintMenuItem(string text)
        {
            Console.ForegroundColor = MenuColor;
            Console.WriteLine(text);
        }

        public static void PrintSuccess(string text)
        {
            Console.ForegroundColor = SuccessColor;
            Console.WriteLine();
            Console.WriteLine(text);
        }

        public static void PrintInfo(string text)
        {
            Console.ForegroundColor = InfoColor;
            Console.WriteLine();
            Console.WriteLine(text);
        }

        public static void PrintError(string text)
        {
            Console.ForegroundColor = ErrorColor;
            Console.WriteLine();
            Console.WriteLine(text);
        }

        public static void PrintWarning(string text)
        {
            Console.ForegroundColor = WarningColor;
            Console.WriteLine();
            Console.WriteLine(text);
        }

        public static void PrintPrompt(string text)
        {
            Console.ForegroundColor = PromptColor;
            Console.WriteLine();
            Console.Write(text + " ");
        }

        public static void PrintSubtle(string text)
        {
            Console.ForegroundColor = SubtleColor;
            Console.WriteLine();
            Console.WriteLine(text);
        }

        public static void PrintLoggedInStatus(ApplicationUserDto? currentUserDto)
        {
            if (currentUserDto == null)
                PrintSubtle("Not logged in");
            else
                PrintSubtle($"Logged in as {currentUserDto.Username}");
        }

        public static void ConfirmContinue()
        {
            Console.ForegroundColor = SubtleColor;
            Console.WriteLine();
            Console.WriteLine("Press ENTER to continue.");
            Console.ReadLine();
        }
    }
}
