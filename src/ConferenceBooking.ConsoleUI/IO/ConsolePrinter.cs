using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Interfaces;

namespace ConferenceBooking.ConsoleUI.IO
{
    public class ConsolePrinter : IOutput
    {
        private const ConsoleColor DefaultColor = ConsoleColor.Gray;
        private const ConsoleColor ErrorColor = ConsoleColor.Red;
        private const ConsoleColor WarningColor = ConsoleColor.Yellow;
        private const ConsoleColor InfoColor = ConsoleColor.White;
        private const ConsoleColor SuccessColor = ConsoleColor.Green;
        private const ConsoleColor MenuColor = ConsoleColor.Gray;
        private const ConsoleColor PromptColor = ConsoleColor.Cyan;
        private const ConsoleColor SubtleColor = ConsoleColor.DarkGray;

        public void PrintPageTitle(string text)
        {
            Console.Clear();
            Console.ForegroundColor = DefaultColor;
            Console.WriteLine(text.ToUpper());
        }

        public void PrintListTitle(string text)
        {
            Console.ForegroundColor = MenuColor;
            Console.WriteLine();
            Console.WriteLine(text);
        }

        public void PrintListItem(string text)
        {
            Console.ForegroundColor = MenuColor;
            Console.WriteLine(text);
        }

        public void PrintSuccess(string text)
        {
            Console.ForegroundColor = SuccessColor;
            Console.WriteLine();
            Console.WriteLine(text);
        }

        public void PrintInfo(string text)
        {
            Console.ForegroundColor = InfoColor;
            Console.WriteLine();
            Console.WriteLine(text);
        }

        public void PrintError(string text)
        {
            Console.ForegroundColor = ErrorColor;
            Console.WriteLine();
            Console.WriteLine(text);
        }

        public void PrintWarning(string text)
        {
            Console.ForegroundColor = WarningColor;
            Console.WriteLine();
            Console.WriteLine(text);
        }

        public void PrintPrompt(string text)
        {
            Console.ForegroundColor = PromptColor;
            Console.WriteLine();
            Console.Write(text + " ");
        }

        public void PrintSubtle(string text)
        {
            Console.ForegroundColor = SubtleColor;
            Console.WriteLine();
            Console.WriteLine(text);
        }

        public void PrintLoggedInStatus(ApplicationUserDto? currentUserDto)
        {
            if (currentUserDto == null)
                PrintSubtle("Not logged in");
            else
                PrintSubtle($"Logged in as {currentUserDto.Username}");
        }

        public void ConfirmContinue()
        {
            Console.ForegroundColor = SubtleColor;
            Console.WriteLine();
            Console.WriteLine("Press ENTER to continue.");
            Console.ReadLine();
        }
    }
}
