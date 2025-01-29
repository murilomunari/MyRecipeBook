﻿using System.Security.Cryptography;
using System.Text;

namespace MyRecipeBook.Application.Services.Criptography;

public class PasswordEncrypter
{
    public string Encrypt(string password)
    {

        var chaveAdcional = "ABC";

        var newPassword = $"{password} {chaveAdcional}";
        
        var bytes = Encoding.UTF8.GetBytes(newPassword);
        
        var hashBytes = SHA512.HashData(bytes);
        
        return StringBytes(hashBytes);
    }

    private static string StringBytes(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (byte b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }
        
        return sb.ToString();
    }
}