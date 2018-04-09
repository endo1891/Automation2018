using System;

namespace EvernoteDesktop
{

    public class TestContext
    {
        public bool IsLoggedIn { get; set; }
        public string CurrentUsername { get; set; }
        public string CurrentPassword { get; set; }
        bool IsMenuOpen { get; set; }
        Uri PasswordReset { get; set; }

        public TestContext()
        {
            IsLoggedIn = false;
        }

        public void SetLoggedIn()
        {
            IsLoggedIn = true;
        }
    }
}