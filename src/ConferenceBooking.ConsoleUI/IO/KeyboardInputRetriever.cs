using ConferenceBooking.Core.Interfaces;

namespace ConferenceBooking.ConsoleUI.IO
{
    public class KeyboardInputRetriever : IInput
    {
        private readonly IOutput _output;

        public KeyboardInputRetriever(IOutput output)
        {
            _output = output;
        }

        public string GetStringInput(string prompt)
        {
            string userInput = string.Empty;
            while (userInput == string.Empty)
            {
                _output.PrintPrompt(prompt);
                userInput = Console.ReadLine() ?? string.Empty;
                userInput = userInput.Trim();
            }
            return userInput;
        }
    }
}
