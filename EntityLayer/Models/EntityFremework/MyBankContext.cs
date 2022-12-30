using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EntityLayer.Models.EntityFremework;

public partial class MyBankContext : DbContext
{
    public MyBankContext()
    {
        base.Database.EnsureCreated();
    }

    public MyBankContext(DbContextOptions<MyBankContext> options)
        : base(options)
    {
        base.Database.EnsureCreated();
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountBalance> AccountBalances { get; set; }

    public virtual DbSet<AccountBalanceHistory> AccountBalanceHistories { get; set; }

    public virtual DbSet<AccountType> AccountTypes { get; set; }

    public virtual DbSet<AdditionalAccount> AdditionalAccounts { get; set; }

    public virtual DbSet<AdditionalAccountHistory> AdditionalAccountHistories { get; set; }

    public virtual DbSet<Adress> Adresses { get; set; }

    public virtual DbSet<AtmCard> AtmCards { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<CardBalance> CardBalances { get; set; }

    public virtual DbSet<CardBalanceHistory> CardBalanceHistories { get; set; }

    public virtual DbSet<CardPassword> CardPasswords { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<CreditCard> CreditCards { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerRole> CustomerRoles { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<EMail> EMails { get; set; }

    public virtual DbSet<InternetPassword> InternetPasswords { get; set; }

    public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Turkish_CI_AS");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.HasIndex(e => e.BranchId, "IX_Account_BranchId");

            entity.HasIndex(e => e.CurrencyUnitId, "IX_Account_CurrencyUnitId");

            entity.HasIndex(e => e.CustomerId, "IX_Account_CustomerId");

            entity.HasIndex(e => e.TypeId, "IX_Account_TypeId");

            entity.HasIndex(e => e.AccountNumber, "UK_Accounts_AccountNumber").IsUnique();

            entity.HasIndex(e => e.Iban, "UK_Accounts_Iban").IsUnique();

            entity.Property(e => e.AccountName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.AccountNumber)
                .HasMaxLength(7)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Iban)
                .HasMaxLength(26)
                .IsUnicode(false);

            entity.HasOne(d => d.Branch).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Accounts_Branches");

            entity.HasOne(d => d.CurrencyUnit).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.CurrencyUnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Accounts_Currencies");

            entity.HasOne(d => d.Customer).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Accounts_Customers");

            entity.HasOne(d => d.Type).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Accounts_AccountTypes");
        });

        modelBuilder.Entity<AccountBalance>(entity =>
        {
            entity.ToTable("AccountBalance");

            entity.HasIndex(e => e.AccountId, "IX_AccountBalance_AccountId");

            entity.Property(e => e.Balance).HasColumnType("money");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountBalances)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountBalance_Accounts");
        });

        modelBuilder.Entity<AccountBalanceHistory>(entity =>
        {
            entity.ToTable("AccountBalanceHistory");

            entity.HasIndex(e => e.AccountId, "IX_AccountBalanceHistory_AccountId");

            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.BeforeBalance).HasColumnType("money");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Explanation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NowBalance).HasColumnType("money");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountBalanceHistories)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountBalanceHistory_Accounts");
        });

        modelBuilder.Entity<AccountType>(entity =>
        {
            entity.ToTable("AccountType");

            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TypeName)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AdditionalAccount>(entity =>
        {
            entity.ToTable("AdditionalAccount");

            entity.HasIndex(e => e.AccountId, "IX_AdditionalAccount_AccountId");

            entity.Property(e => e.Balance).HasColumnType("money");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Limit).HasColumnType("money");

            entity.HasOne(d => d.Account).WithMany(p => p.AdditionalAccounts)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdditionalAccount_Accounts");
        });

        modelBuilder.Entity<AdditionalAccountHistory>(entity =>
        {
            entity.ToTable("AdditionalAccountHistory");

            entity.HasIndex(e => e.AdditionalAccountId, "IX_AdditionalAccountHistory_AdditionalAccountId");

            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.BeforeBalance).HasColumnType("money");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NowBalance).HasColumnType("money");

            entity.HasOne(d => d.AdditionalAccount).WithMany(p => p.AdditionalAccountHistories)
                .HasForeignKey(d => d.AdditionalAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdditionalAccountHistory_AdditionalAccount");
        });

        modelBuilder.Entity<Adress>(entity =>
        {
            entity.ToTable("Adress");

            entity.HasIndex(e => e.CityId, "IX_Adress_CityId");

            entity.HasIndex(e => e.CountryId, "IX_Adress_CountryId");

            entity.HasIndex(e => e.CustomerId, "IX_Adress_CustomerId");

            entity.HasIndex(e => e.DistrictId, "IX_Adress_DistrictId");

            entity.Property(e => e.AdressDetail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.AdressName)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DomicileStartDate).HasColumnType("date");

            entity.HasOne(d => d.City).WithMany(p => p.Adresses)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Adresses_Cities");

            entity.HasOne(d => d.Country).WithMany(p => p.Adresses)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Adresses_Countries");

            entity.HasOne(d => d.Customer).WithMany(p => p.Adresses)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Adresses_Customers");

            entity.HasOne(d => d.District).WithMany(p => p.Adresses)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Adresses_District");
        });

        modelBuilder.Entity<AtmCard>(entity =>
        {
            entity.ToTable("AtmCard");

            entity.Property(e => e.AtmCardId).ValueGeneratedNever();

            entity.HasOne(d => d.AtmCardNavigation).WithOne(p => p.AtmCard)
                .HasForeignKey<AtmCard>(d => d.AtmCardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AtmCards_Cards");
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.ToTable("Branch");

            entity.HasIndex(e => e.CityId, "IX_Branch_CityId");

            entity.HasIndex(e => e.DistrictId, "IX_Branch_DistrictId");

            entity.HasIndex(e => e.BranchName, "UK_Branches_BranchNumber").IsUnique();

            entity.Property(e => e.BranchAdress)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.BranchName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.BranchNumber)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Latitude)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Longitude)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.City).WithMany(p => p.Branches)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Branches_Cities");

            entity.HasOne(d => d.District).WithMany(p => p.Branches)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("FK_Branches_District");
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.ToTable("Card");

            entity.HasIndex(e => e.CustomerId, "IX_Card_CustomerId");

            entity.HasIndex(e => e.CardNumber, "UK_Cards_CardNumber").IsUnique();

            entity.Property(e => e.CardNumber)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Cards)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cards_Customers");
        });

        modelBuilder.Entity<CardBalance>(entity =>
        {
            entity.ToTable("CardBalance");

            entity.HasIndex(e => e.CreditCardId, "IX_CardBalance_CreditCardId");

            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.Balance).HasColumnType("money");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Debit).HasColumnType("money");

            entity.HasOne(d => d.CreditCard).WithMany(p => p.CardBalances)
                .HasForeignKey(d => d.CreditCardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CardBalance_CreditCards");
        });

        modelBuilder.Entity<CardBalanceHistory>(entity =>
        {
            entity.ToTable("CardBalanceHistory");

            entity.HasIndex(e => e.CreditCardId, "IX_CardBalanceHistory_CreditCardId");

            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.Balance).HasColumnType("money");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Debit).HasColumnType("money");

            entity.HasOne(d => d.CreditCard).WithMany(p => p.CardBalanceHistories)
                .HasForeignKey(d => d.CreditCardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CardBalanceHistory_CreditCards");
        });

        modelBuilder.Entity<CardPassword>(entity =>
        {
            entity.ToTable("CardPassword");

            entity.HasIndex(e => e.CardId, "IX_CardPassword_CardId");

            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Password).IsUnicode(false);

            entity.HasOne(d => d.Card).WithMany(p => p.CardPasswords)
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CardPasswords_Cards");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("City");

            entity.HasIndex(e => e.CountryId, "IX_City_CountryId");

            entity.Property(e => e.CityName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_Cities_Countries");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Country");

            entity.Property(e => e.CountryCode)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.CountryName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<CreditCard>(entity =>
        {
            entity.ToTable("CreditCard");

            entity.Property(e => e.CreditCardId).ValueGeneratedNever();
            entity.Property(e => e.CreditCardLimit).HasColumnType("money");
            entity.Property(e => e.CreditCardName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.CreditCardNavigation).WithOne(p => p.CreditCard)
                .HasForeignKey<CreditCard>(d => d.CreditCardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CreditCards_Cards");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK_Currencies");

            entity.ToTable("Currency");

            entity.Property(e => e.CountryId).ValueGeneratedNever();
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IconPath).IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ShortName)
                .HasMaxLength(8)
                .IsUnicode(false);

            entity.HasOne(d => d.Country).WithOne(p => p.Currency)
                .HasForeignKey<Currency>(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Currencies_Countries");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.HasIndex(e => e.IdentityNumber, "UK_Customer_IdentityNumber").IsUnique();

            entity.HasIndex(e => e.CustomerNumber, "UK_Customers_CustemerNumber").IsUnique();

            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerNumber)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.DateofBirth).HasColumnType("date");
            entity.Property(e => e.IdentityNumber)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Photo).IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CustomerRole>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.ToTable("CustomerRole");

            entity.HasIndex(e => e.CustomerId, "IX_CustomerRole_CustomerId");

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerRoles)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerRole_Customer");

            entity.HasOne(d => d.Role).WithOne(p => p.CustomerRole)
                .HasForeignKey<CustomerRole>(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerRole_Role");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.ToTable("District");

            entity.HasIndex(e => e.CityId, "IX_District_CityId");

            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DistrictName)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.City).WithMany(p => p.Districts)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_District_Cities");
        });

        modelBuilder.Entity<EMail>(entity =>
        {
            entity.ToTable("E-Mail");

            entity.HasIndex(e => e.CustomerId, "IX_E-Mail_CustomerId");

            entity.HasIndex(e => e.EMail1, "UK_E-Mails").IsUnique();

            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EMail1)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("E_Mail");

            entity.HasOne(d => d.Customer).WithMany(p => p.EMails)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_E-Mails_Customers");
        });

        modelBuilder.Entity<InternetPassword>(entity =>
        {
            entity.ToTable("InternetPassword");

            entity.HasIndex(e => e.CustomerId, "IX_InternetPassword_CustomerId");

            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Password).IsUnicode(false);

            entity.HasOne(d => d.Customer).WithMany(p => p.InternetPasswords)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InternetPasswords_Customers");
        });

        modelBuilder.Entity<PhoneNumber>(entity =>
        {
            entity.ToTable("PhoneNumber");

            entity.HasIndex(e => e.CustomerId, "IX_PhoneNumber_CustomerId");

            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NumberName)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber1)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PhoneNumber");

            entity.HasOne(d => d.Customer).WithMany(p => p.PhoneNumbers)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhoneNumbers_Customers");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
