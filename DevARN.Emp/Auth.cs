using System;
using System.IO;
using PWDTK_DOTNET451;
namespace DevARN.Emp
{
    public class Auth
    {
        public delegate void AuthanticationError(string error);
        public event AuthanticationError ErrorOccured;
        private static readonly string AppPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\DevARN.Auth\\";
        public void Authanticate(string username, string password)
        {
            if (username == "" || password == "")
            {
                if (Directory.Exists(AppPath))
                {
                    string[] userCreds = File.ReadAllLines(AppPath + "user.info");

                }
            }
        }
    }
}
