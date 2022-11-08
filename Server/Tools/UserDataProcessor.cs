using System;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Markup;
using Microsoft.AspNet.Cryptography.KeyDerivation;
using Server.Classes;

namespace Server.Tools;

public static class UserDataProcessor
{
    public static bool RegisterUser(string login, string password, string email)
    {
        try
        {
            string normalizedLogin = NormalizeLogin(login);
            foreach (var u in AppSettings.DataBase!.DataReader($"Select * from Users where Users.UserLogin='{normalizedLogin}'"))
            {
                return false;
            }

            var hashedPassword = GenHashedPassword(password);
            if (hashedPassword.Length == 0) return false;
            
            string userPath = "";
            var command = $"insert into Users(UserID, UserLogin, HashedPassword, Salt, UserPath) values (" +
                          $"'{(Guid.NewGuid().ToString("N").Remove(16, 16))}'," +
                          $"'{normalizedLogin}'," +
                          $"'{hashedPassword[0]}'," +
                          $"'{hashedPassword[1]}'," +
                          $"'{userPath}'" +
                          $")";
            AppSettings.DataBase.CommandExecuter(command);
            return true;
        }
        catch(Exception ex)
        {
            Methods.AddTextToOutBox(ex.Message);
            return false;
        }

        return false;
    }
    public static bool CheckUserLogin(string login, string password)
    {
        try
        {
            string normalizedLogin = NormalizeLogin(login);
            foreach (var u in AppSettings.DataBase!.DataReader($"Select * from Users where Users.UserLogin='{normalizedLogin}'"))
            {
                /*
             * 0 ID
             * 1 Login
             * 2 HashedPassword
             * 3 Salt
             * 4 UserPath
             */
                return PasswordCompare(password, "" + u[2], "" + u[3]);
            }

            return false;
        }
        catch (Exception e)
        {
            Methods.AddTextToOutBox(e.Message);
            return false;
        }
    }
    
    public static string[] GenHashedPassword(string password)
    {
        string[] data = new string[2];
        // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
        byte[] salt = new byte[128 / 8];
        using (var rngCsp = new RNGCryptoServiceProvider())
        {
            rngCsp.GetNonZeroBytes(salt);
        }
        data[1] = Convert.ToBase64String(salt);

        // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
        data[0] = hashed;
        return data;
    }

    public static bool PasswordCompare(string password, string hashedPassword, string salt)
    {
        // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
        byte[] byteSalt = Convert.FromBase64String(salt);

        // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: byteSalt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

        return hashed == hashedPassword;
    }

    public static string NormalizeLogin(string login)
    {
        string normalizedLogin = login.ToLower();
        return normalizedLogin[0].ToString().ToUpper() + normalizedLogin.Remove(0, 1);
    }
}