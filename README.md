<p align="center">
  <img src="https://user-images.githubusercontent.com/47147484/168492480-206b432f-0d3f-4ee7-9ecf-19df9f7973b6.png" style="max-width:100%;" height="140" />
</p>

<p align="center">
  <a href="https://gitmoji.carloscuesta.me">
    <img src="https://img.shields.io/badge/gitmoji-%20😜%20😍-FFDD67.svg?style=flat-square" alt="Gitmoji">
  </a> 
</p>

***

## Give a Star 🌟
If you liked the project or if **EntityFrameworkCore.DataEncryption** helped you, please give a star.

***

### Purpose
**EntityFrameworkCore.DataEncryption** with the entity framework core value converter feature, it allows you to store your data by encrypting it.

***

### Summary

```
Install-Package EntityFrameworkCore.DataEncryption.Conversions
```

<p align="center">
  <img src="https://user-images.githubusercontent.com/47147484/169529159-8f400ad7-922b-43f3-867f-4eeb93aa724b.png" style="max-width:100%;" height="40" />
</p>

Then mark the columns to be encrypted in DbContext.

<p align="center">
  <img src="https://user-images.githubusercontent.com/47147484/169529159-8f400ad7-922b-43f3-867f-4eeb93aa724b.png" style="max-width:100%;" height="40" />
</p>

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Author>(entity =>
    {
        entity
            .Property(p => p.Id)
            .UseIdentityColumn();
        entity
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(2048);
        
        entity
            .Property(p => p.Surname)
            .IsRequired()
            .HasMaxLength(2048);
        entity
            .Property(p => p.Phone)
            .IsRequired()
            .HasConversion(new EncryptValueConverter(key:"89acMXSBpuEBDWHZ"));
    });
    base.OnModelCreating(modelBuilder);
}
```

***

### Documentation
Visit wiki documentation. [Wiki](https://github.com/furkandeveloper/EntityFrameworkCore.DataEncryption/wiki)

***

### Sample Project

See for more information visit [sample project](https://github.com/furkandeveloper/EntityFrameworkCore.DataEncryption/tree/master/sample/EntityFrameworkCore.DataEncryption.Sample)
