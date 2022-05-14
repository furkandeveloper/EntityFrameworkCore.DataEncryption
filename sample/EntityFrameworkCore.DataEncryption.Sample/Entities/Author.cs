namespace EntityFrameworkCore.DataEncryption.Sample.Entities;

/// <summary>
/// Author Entity
/// </summary>
public class Author
{
    /// <summary>
    /// PK
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of Author
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Surname of Author
    /// </summary>
    public string Surname { get; set; }

    /// <summary>
    /// Phone of Author
    /// </summary>
    public string Phone { get; set; }
}