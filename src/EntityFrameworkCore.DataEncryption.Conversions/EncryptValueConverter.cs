using System;
using System.Linq.Expressions;
using EntityFrameworkCore.DataEncryption.Core;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EntityFrameworkCore.DataEncryption.Conversions;

/// <summary>
/// Encrypt Value Converter  
/// </summary>
public class EncryptValueConverter : ValueConverter<string, string>
{
    private static string _password;

    public EncryptValueConverter(string key, ConverterMappingHints mappingHints = default)
        : base(EncryptExpr, DecryptExpr, mappingHints)
    {
        _password = key;
    }

    // from
    /// <summary>
    /// From Expression
    /// </summary>
    private static Expression<Func<string, string>> DecryptExpr => prop => Cipher.Decrypt(prop, _password);

    // to
    /// <summary>
    /// To Expression
    /// </summary>
    private static Expression<Func<string, string>> EncryptExpr => prop => Cipher.Encrypt(prop, _password);
}