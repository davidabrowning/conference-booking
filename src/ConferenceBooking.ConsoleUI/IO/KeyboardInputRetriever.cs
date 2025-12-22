using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Interfaces;
using ConferenceBooking.Core.Models;

namespace ConferenceBooking.ConsoleUI.IO
{
    public class KeyboardInputRetriever : IInput
    {
        private readonly IOutput _output;

        public KeyboardInputRetriever(IOutput output)
        {
            _output = output;
        }

        public T GetSelectionFromList<T>(string listTitle, IEnumerable<T> list)
        {
            int entityCounter = 0;
            _output.PrintListTitle(listTitle);
            foreach (T entity in list)
                _output.PrintListItem($"{++entityCounter}. {entity.ToString()}");
            int selection = GetIntInput(1, entityCounter, "Your choice:");
            return list.ElementAt(selection - 1);
        }

        public int GetIntInput(int min, int max, string prompt)
        {
            int inputInteger = min - 1;
            while (inputInteger < min || inputInteger > max)
            {
                string inputString = GetStringInput(prompt);
                inputInteger = Convert.ToInt32(inputString);
            }
            return inputInteger;
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
