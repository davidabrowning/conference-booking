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
                try
                {
                    inputInteger = Convert.ToInt32(inputString);
                }
                catch (Exception ex)
                {
                    _output.PrintError(ex.Message);
                }
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

        public DateTime GetDateTimeInput(string prompt)
        {
            bool validInput = false;
            int year, month, day, hour, minute, second = 0;
            DateTime dateTime = DateTime.MinValue;
            while (!validInput)
            {
                string inputString = GetStringInput(prompt);
                try
                {
                    string[] inputStringParts = inputString.Split(" ");
                    string dateString = inputStringParts[0];
                    string timeString = inputStringParts[1];

                    string[] dateStringParts = dateString.Split("-");
                    year = Convert.ToInt32(dateStringParts[0]);
                    month = Convert.ToInt32(dateStringParts[1]);
                    day = Convert.ToInt32(dateStringParts[2]);

                    string[] timeStringParts = timeString.Split(":");
                    hour = Convert.ToInt32(timeStringParts[0]);
                    minute = Convert.ToInt32(timeStringParts[1]);

                    dateTime = new DateTime(year, month, day, hour, minute, second);
                    validInput = true;
                }
                catch (Exception ex)
                {
                    _output.PrintError(ex.Message);
                }
            }
            return dateTime;
        }
    }
}
