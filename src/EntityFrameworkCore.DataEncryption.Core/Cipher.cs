using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EntityFrameworkCore.DataEncryption.Core;

/// <summary>
/// Base Cipher class
/// </summary>
public static class Cipher
{
    /// <summary>
    /// Encrypt a string.
    /// </summary>
    /// <param name="plainText">String to be encrypted</param>
    /// <param name="password">Password</param>
    public static string Encrypt(string plainText, string password)
    {
        // Get the bytes of the string
        var bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
        var passwordBytes = Encoding.UTF8.GetBytes(password);

        // Hash the password with SHA256
        passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

        var bytesEncrypted = Cipher.Encrypt(bytesToBeEncrypted, passwordBytes);

        return Convert.ToBase64String(bytesEncrypted);
    }

    /// <summary>
    /// Decrypt a string.
    /// </summary>
    /// <param name="encryptedText">String to be decrypted</param>
    /// <param name="password">Password used during encryption</param>
    /// <exception cref="FormatException"></exception>
    public static string Decrypt(string encryptedText, string password)
    {
        // Get the bytes of the string
        var bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
        var passwordBytes = Encoding.UTF8.GetBytes(password);

        passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

        var bytesDecrypted = Cipher.Decrypt(bytesToBeDecrypted, passwordBytes);

        return Encoding.UTF8.GetString(bytesDecrypted);
    }

    private static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
    {
        // Set your salt here, change it to meet your flavor:
        // The salt bytes must be at least 8 bytes.
        var saltBytes = new byte[] {1, 2, 3, 4, 5, 6, 7, 8};

        using var ms = new MemoryStream();
        using var aes = new RijndaelManaged();
        using var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
        aes.KeySize = 256;
        aes.BlockSize = 128;
        aes.Key = key.GetBytes(aes.KeySize / 8);
        aes.IV = key.GetBytes(aes.BlockSize / 8);

        aes.Mode = CipherMode.CBC;

        using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
        {
            cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
            cs.Close();
        }

        var encryptedBytes = ms.ToArray();

        return encryptedBytes;
    }

    private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
    {
        // Set your salt here, change it to meet your flavor:
        // The salt bytes must be at least 8 bytes.
        var saltBytes = new byte[] {1, 2, 3, 4, 5, 6, 7, 8};

        using var ms = new MemoryStream();
        using var aes = new RijndaelManaged();
        using var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
        aes.KeySize = 256;
        aes.BlockSize = 128;
        aes.Key = key.GetBytes(aes.KeySize / 8);
        aes.IV = key.GetBytes(aes.BlockSize / 8);
        aes.Mode = CipherMode.CBC;

        using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
        {
            cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
            cs.Close();
        }

        var decryptedBytes = ms.ToArray();

        return decryptedBytes;
    }
}