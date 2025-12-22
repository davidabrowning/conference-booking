using ConferenceBooking.ConsoleUI.Menus;
using ConferenceBooking.Core.Dtos;
using ConferenceBooking.Core.Interfaces;

namespace ConferenceBooking.ConsoleUI
{
    public class MenuManager : IUserMenu
    {
        private ApplicationUserDto? _selectedUser;
        private UserSelectionMenu _userSelectionMenu;
        private BookingMenu _bookingMenu;

        public MenuManager(UserSelectionMenu userSelectionMenu, BookingMenu bookingMenu)
        {
            _selectedUser = null;
            _userSelectionMenu = userSelectionMenu;
            _bookingMenu = bookingMenu;
        }

        public async Task RunAsync()
        {
            while (UserWantsToContinue())
            {
                if (NoUserHasBeenSelected())
                    _selectedUser = await _userSelectionMenu.LoginAsync();
                else
                    await _bookingMenu.RunAsync(_selectedUser);
            }
        }

        private bool UserWantsToContinue()
        {
            return _userSelectionMenu.UserWantsToContinue && _bookingMenu.UserWantsToContinue;
        }

        private bool NoUserHasBeenSelected()
        {
            return _selectedUser == null;
        }
    }
}
