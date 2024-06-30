using System;
using System.Linq;
using System.Reflection;
using Focus.Business.Users;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Focus.Persistence.Extensions
{
    public static class ModelBuilder
    {
        public static void ShadowProperties(this Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            foreach (var tp in modelBuilder.Model.GetEntityTypes())
            {
                var t = tp.ClrType;

                // set auditing properties
                if (typeof(IAuditedEntityBase).IsAssignableFrom(t))
                {
                    var method = SetAuditingShadowPropertiesMethodInfo.MakeGenericMethod(t);
                    method.Invoke(modelBuilder, new object[] { modelBuilder });
                }

                // set tenant properties
                if (typeof(ITenant).IsAssignableFrom(t))
                {
                    var method = SetTenantShadowPropertyMethodInfo.MakeGenericMethod(t);
                    method.Invoke(modelBuilder, new object[] { modelBuilder });
                }

                // set soft delete property
                if (typeof(ISoftDelete).IsAssignableFrom(t))
                {
                    var method = SetIsDeletedShadowPropertyMethodInfo.MakeGenericMethod(t);
                    method.Invoke(modelBuilder, new object[] { modelBuilder });
                }

                // set concurrency token property
                if (typeof(IConcurrencyCheckEntity).IsAssignableFrom(t))
                {
                    var method = SetConcurrencyStampShadowPropertyMethodInfo.MakeGenericMethod(t);
                    method.Invoke(modelBuilder, new object[] { modelBuilder });
                }

                // set concurrency token property
                if (typeof(IRecordDailyEntry).IsAssignableFrom(t))
                {
                    var method = SetDailyEntryShadowPropertiesMethodInfo.MakeGenericMethod(t);
                    method.Invoke(modelBuilder, new object[] { modelBuilder });
                }
            }
        }

        private static readonly MethodInfo SetIsDeletedShadowPropertyMethodInfo = typeof(ModelBuilder)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == "SetIsDeletedShadowProperty");

        private static readonly MethodInfo SetTenantShadowPropertyMethodInfo = typeof(ModelBuilder)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == "SetTenantShadowProperty");

        private static readonly MethodInfo SetAuditingShadowPropertiesMethodInfo = typeof(ModelBuilder)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == "SetAuditingShadowProperties");

        private static readonly MethodInfo SetConcurrencyStampShadowPropertyMethodInfo = typeof(ModelBuilder)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == "SetConcurrencyShadowProperty");

        private static readonly MethodInfo SetDailyEntryShadowPropertiesMethodInfo = typeof(ModelBuilder)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == "SetDailyEntryShadowProperties");

        public static void SetIsDeletedShadowProperty<T>(Microsoft.EntityFrameworkCore.ModelBuilder builder) where T : class, ISoftDelete
        {
            // define shadow property
            builder.Entity<T>().Property<bool>("IsDeleted");
        }

        public static void SetTenantShadowProperty<T>(Microsoft.EntityFrameworkCore.ModelBuilder builder) where T : class, ITenant
        {
            // define shadow property
            builder.Entity<T>().Property<Guid>("CompanyId");
            // define FK to Company
            builder.Entity<T>().HasOne<Company>().WithMany().HasForeignKey("CompanyId").OnDelete(DeleteBehavior.Restrict);
        }

        public static void SetAuditingShadowProperties<T>(Microsoft.EntityFrameworkCore.ModelBuilder builder) where T : class, IAuditedEntityBase
        {
            // define shadow properties
            builder.Entity<T>().Property<DateTime>("CreatedOn").HasDefaultValueSql("GetUtcDate()");
            builder.Entity<T>().Property<DateTime>("ModifiedOn").HasDefaultValueSql("GetUtcDate()");
            builder.Entity<T>().Property<string>("CreatedById");
            builder.Entity<T>().Property<string>("ModifiedById");

            // define FKs to User
            //builder.Entity<T>().HasOne<User>().WithMany().HasForeignKey("CreatedById").OnDelete(DeleteBehavior.Restrict);
            //builder.Entity<T>().HasOne<User>().WithMany().HasForeignKey("ModifiedById").OnDelete(DeleteBehavior.Restrict);
        }

        public static void SetConcurrencyShadowProperty<T>(Microsoft.EntityFrameworkCore.ModelBuilder builder) where T : class, IConcurrencyCheckEntity
        {
            builder.Entity<T>().Property<byte[]>("ConcurrencyStamp").IsRowVersion();
        }

        public static void SetDailyEntryShadowProperties<T>(Microsoft.EntityFrameworkCore.ModelBuilder builder) where T : class, IRecordDailyEntry
        {
            // define shadow properties
            builder.Entity<T>().Property<Guid>("ClientId");
            builder.Entity<T>().Property<Guid>("BusinessId");
            builder.Entity<T>().Property<Guid>("UserId");
            builder.Entity<T>().Property<Guid?>("CounterId");
            builder.Entity<T>().Property<Guid?>("DayId");
        }

        public static void Seed(this Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasData(new Company
            {
                Id = Guid.Parse("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                NameEnglish = "noble@gmail.com",
                Blocked = false,
                CompanyRegNo = "56ty60",
                CreatedDate = DateTime.UtcNow
            });
            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = "5f8d5614-2c7e-4ec0-868c-d254e6516b4d",
                UserName = "noble@gmail.com",
                Email = "noble@gmail.com",
                NormalizedEmail = "noble@gmail.com",
                EmailConfirmed = true,
                NormalizedUserName = "NOBLE@GMAIL.COM",
                PasswordHash = "AQAAAAEAACcQAAAAEEMJll7GgXWk1z2fatxJWFPeucssBaOZ+EXMY5MYkhRNow+oaTxB0nH+sWvTX6wKWA==",
                SecurityStamp = "HDM6TKME4T5DISZCHW3MHD6YLQFNSWC2",
                ConcurrencyStamp = "117c7248-5202-44d5-a7eb-8f2717eba7e9",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                CompanyId = Guid.Parse("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                FirstName = "Noble App",
                LastName = "",
            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole

            {
                Id = "C1448F88-49B4-476C-B07D-33514A0F9C1E",
                Name = "Noble Admin",
                NormalizedName = "NOBLE ADMIN",
                ConcurrencyStamp = "0590a33c-cd2d-4c93-9e17-fce19bc2bd9d",

            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole

            {
                Id = "C1448F88-49CS-476C-B07D-33514A0F9C1E",
                Name = "Supervisor",
                NormalizedName = "SUPERVISOR",
                ConcurrencyStamp = "4590A33c-cd2d-4c93-9e17-fcE19bc2bd9d",

            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole

            {
                Id = "C1448F88-49B4-476C-B07D-33514A0F3C1F",
                Name = "Mobile Customer",
                NormalizedName = "MOBILE CUSTOMER",
                ConcurrencyStamp = "0C90a33c-cd2d-4c93-9e17-fce14bc2bd9d",

            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole

            {
                Id = "S1448F88-49B4-476C-B07D-33514A0F3C1F",
                Name = "Order Tracker",
                NormalizedName = "ORDER TRACKER",
                ConcurrencyStamp = "0C90a33c-dd2d-5c93-9e17-fce14bc2bd9d",

            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole

            {
                Id = "S1448F55-49B4-476C-B07D-33514A0F3C1F",
                Name = "Sale Man",
                NormalizedName = "SALE MAN",
                ConcurrencyStamp = "0C90a33c-dd2d-5c93-9e17-FCS14bc2bd9d",

            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole

            {
                Id = "52E0A954-7039-483B-9224-99990743636D",
                Name = "Super Admin",
                NormalizedName = "SUPER ADMIN",
                ConcurrencyStamp = "0590a33c-cd2d-4c93-9e17-fce12bc2bd9d",

            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole

            {
                Id = "CEA36602-E3BD-4CF6-B186-5D8A3E558A04",
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "0590a33c-cd2d-4d93-9e17-fce19bc2bd9d",

            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole

            {
                Id = "E5480E8E-A150-4C80-82AB-62B5A8EBFD1B",
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = "1590a33c-cd2d-4c93-9e17-fce19bc2bd9d",

            });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "C1448F88-49B4-476C-B07D-33514A0F9C1E",
                UserId = "5f8d5614-2c7e-4ec0-868c-d254e6516b4d"
            });
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(new IdentityUserClaim<string>
            {
                Id = 1,
                UserId = "5f8d5614-2c7e-4ec0-868c-d254e6516b4d",
                ClaimType = "Email",
                ClaimValue = "noble@gmail.com"
            },
            new IdentityUserClaim<string>
            {
                Id = 2,
                UserId = "5f8d5614-2c7e-4ec0-868c-d254e6516b4d",
                ClaimType = "CompanyId",
                ClaimValue = "5f8d5614-2c7e-4ec0-868c-d254e6516b4d"

            },
            new IdentityUserClaim<string>
            {
                Id = 3,
                UserId = "5f8d5614-2c7e-4ec0-868c-d254e6516b4d",
                ClaimType = "Organization",
                ClaimValue = "Noble POS"

            },
            new IdentityUserClaim<string>
            {
                Id = 4,
                UserId = "5f8d5614-2c7e-4ec0-868c-d254e6516b4d",
                ClaimType = "FullName",
                ClaimValue = "Noble App"

            }
            );
            modelBuilder.Entity<AccountTemplate>().HasData(new AccountTemplate
            {
                Id = Guid.Parse("ecfe29c8-c6af-4a3d-9c24-f87b30bf831c"),
                Name = "SmallBusinessCOA",
                Description = "SmallBusinessCOA",
                Type = "Business",
                JsonTemplate = "{\"AccountsType\":[{\"Name\":\"Assets\",\"NameArabic\":\"\u0627\u0644\u0627\u0633\u0627\u0633\",\"IsActive\":true,\"CostCenters\":[{\"Name\":\"Cash in Hand\",\"NameArabic\":\"\u0635\u0646\u062F\u0648\u0642\",\"Description\":\"Cash in Hand\",\"IsActive\":true,\"Code\":\"101000\",\"Accounts\":[{\"Name\":\"Cash in Hand\",\"NameArabic\":\"\u0635\u0646\u062F\u0648\u0642\",\"Description\":\"Cash in Hand\",\"IsActive\":true,\"Code\":\"10100001\"}]},{\"Name\":\"Cash in Hand - Store\",\"NameArabic\":\"\u0635\u0646\u062F\u0648\u0642 \u0627\u0644\u0645\u0633\u062A\u0648\u062F\u0639\",\"Description\":\"Cash in Hand - Store\",\"IsActive\":true,\"Code\":\"101001\",\"Accounts\":[{\"Name\":\"Accounts receivable\",\"NameArabic\":\"\u0627\u0644\u062D\u0633\u0628\u0627\u062A \u0627\u0644\u0645\u0633\u062A\u062D\u0642\u0647\",\"Description\":\"Accounts receivable\",\"IsActive\":true,\"Code\":\"10100101\"}]},{\"Name\":\"Inventory\",\"NameArabic\":\"\u0627\u0644\u0645\u062E\u0632\u0648\u0646\",\"Description\":\"Inventory\",\"IsActive\":true,\"Code\":\"111000\",\"Accounts\":[{\"Name\":\"Inventory\",\"NameArabic\":\"\u0627\u0644\u0645\u062E\u0632\u0648\u0646\",\"Description\":\"Inventory\",\"IsActive\":true,\"Code\":\"11100001\"}]},{\"Name\":\"Customer Reciveables\",\"NameArabic\":\"\u0645\u0633\u062A\u062D\u0642\u0627\u062A \u0627\u0644\u0639\u0645\u0644\u0627\u0621\",\"Description\":\"Customer Reciveables\",\"IsActive\":true,\"Code\":\"120000\",\"Accounts\":[{\"Name\":\"Customer Reciveables\",\"NameArabic\":\"\u0645\u0633\u062A\u062D\u0642\u0627\u062A \u0627\u0644\u0639\u0645\u0644\u0627\u0621\",\"Description\":\"Customer Reciveables\",\"IsActive\":true,\"Code\":\"1200001\"}]},{\"Name\":\"VAT Paid\",\"NameArabic\":\"\u0645\u062F\u0641\u0648\u0639\u0627\u062A \u0627\u0644\u0642\u064A\u0645\u0647 \u0627\u0644\u0645\u0636\u0627\u0641\u0647\",\"Description\":\"VAT Paid\",\"IsActive\":true,\"Code\":\"130000\",\"Accounts\":[{\"Name\":\"VAT Paid on Purchases\",\"NameArabic\":\"\",\"Description\":\"VAT Paid on Purchases\",\"IsActive\":true,\"Code\":\"1300001\"}]},{\"Name\":\"Banks\",\"NameArabic\":\"\u0627\u0644\u0628\u0646\u0648\u0643\",\"Description\":\"Banks\",\"IsActive\":true,\"Code\":\"105000\",\"Accounts\":[{\"Name\":\"Banks\",\"NameArabic\":\"\u0627\u0644\u0628\u0646\u0648\u0643\",\"Description\":\"Banks\",\"IsActive\":true,\"Code\":\"10500001\"}]},{\"Name\":\"Accumulated Depreciation\",\"NameArabic\":\"\u0627\u0644\u0627\u0633\u062A\u0647\u0644\u0627\u0643 \u0627\u0644\u062A\u0631\u0627\u0643\u0645\u064A\",\"Description\":\"Accumulated Depreciation\",\"IsActive\":true,\"Code\":\"170000\",\"Accounts\":[{\"Name\":\"Accumulated Depreciation\",\"NameArabic\":\"\u0627\u0644\u0627\u0633\u062A\u0647\u0644\u0627\u0643 \u0627\u0644\u062A\u0631\u0627\u0643\u0645\u064A\",\"Description\":\"Accumulated Depreciation\",\"IsActive\":true,\"Code\":\"17000001\"}]},{\"Name\":\"Fixed Assets\",\"NameArabic\":\"\u0627\u0644\u0627\u0633\u0627\u0633 \u0627\u0644\u062B\u0627\u0628\u062A\",\"Description\":\"Fixed Assets\",\"IsActive\":true,\"Code\":\"150000\",\"Accounts\":[{\"Name\":\"Fixed Assets\",\"NameArabic\":\"\u0627\u0644\u0627\u0633\u0627\u0633 \u0627\u0644\u062B\u0627\u0628\u062A\",\"Description\":\"Fixed Assets\",\"IsActive\":true,\"Code\":\"1500001\"}]},{\"Name\":\"Due from Employee\",\"NameArabic\":\"\u0645\u062F\u064A\u0648\u0646\u0627\u062A \u0627\u0644\u0645\u0648\u0638\u0641\",\"Description\":\"Due from Employee\",\"IsActive\":true,\"Code\":\"126000\",\"Accounts\":[{\"Name\":\"Due from Employee\",\"NameArabic\":\"\u0645\u062F\u064A\u0648\u0646\u0627\u062A \u0627\u0644\u0645\u0648\u0638\u0641\",\"Description\":\"Due from Employee\",\"IsActive\":true,\"Code\":\"12600001\"}]}]},{\"Name\":\"Liabilities\",\"NameArabic\":\"\u0627\u0644\u062A\u0632\u0627\u0645\u0627\u062A\",\"IsActive\":true,\"CostCenters\":[{\"Name\":\"Supplier Payable\",\"NameArabic\":\"\u0645\u062F\u0641\u0648\u0639\u0627\u062A \u0627\u0644\u0645\u0648\u0631\u062F\u064A\u0646\",\"Description\":\"Supplier Payable\",\"IsActive\":true,\"Code\":\"200000\",\"Accounts\":[{\"Name\":\"Supplier Payable\",\"NameArabic\":\"\u0645\u062F\u0641\u0648\u0639\u0627\u062A \u0627\u0644\u0645\u0648\u0631\u062F\u064A\u0646\",\"Description\":\"Supplier Payable\",\"IsActive\":true,\"Code\":\"20000001\"}]},{\"Name\":\"Payroll Liabilities\",\"NameArabic\":\"\u0645\u062F\u0641\u0648\u0639\u0627\u062A \u0627\u0644\u0631\u0648\u0627\u062A\u0628\",\"Description\":\"Payroll Liabilities\",\"IsActive\":true,\"Code\":\"240000\",\"Accounts\":[{\"Name\":\"Payroll Liabilities\",\"NameArabic\":\"\u0645\u062F\u0641\u0648\u0639\u0627\u062A \u0627\u0644\u0631\u0648\u0627\u062A\u0628\",\"Description\":\"Payroll Liabilities\",\"IsActive\":true,\"Code\":\"24000001\"}]},{\"Name\":\"VAT Payable\",\"NameArabic\":\"\u0645\u062F\u0641\u0648\u0639\u0627\u062A \u0627\u0644\u0636\u0631\u064A\u0628\u0647\",\"Description\":\"VAT Payable\",\"IsActive\":true,\"Code\":\"250000\",\"Accounts\":[{\"Name\":\"VAT Payable on Sale\",\"NameArabic\":\"\u0645\u062F\u0641\u0648\u0639\u0627\u062A \u0627\u0644\u0636\u0631\u064A\u0628\u0647\",\"Description\":\"VAT Payable on Sale\",\"IsActive\":true,\"Code\":\"25000001\"}]},{\"Name\":\"Loan Payable\",\"NameArabic\":\"\u0642\u0631\u0636 \u0645\u0633\u062A\u062D\u0642 \u0627\u0644\u062F\u0641\u0639\",\"Description\":\"Loan Payable\",\"IsActive\":true,\"Code\":\"253001\",\"Accounts\":[{\"Name\":\"Loan Payable\",\"NameArabic\":\"\u0642\u0631\u0636 \u0645\u0633\u062A\u062D\u0642 \u0627\u0644\u062F\u0641\u0639\",\"Description\":\"Loan Payable\",\"IsActive\":true,\"Code\":\"2530101\"}]}]},{\"Name\":\"Equity\",\"NameArabic\":\"\u0631\u0623\u0633 \u0627\u0644\u0645\u0627\u0644\",\"IsActive\":true,\"CostCenters\":[{\"Name\":\"Opening Balance Equity\",\"NameArabic\":\"\u0627\u0644\u0631\u0635\u064A\u062F \u0627\u0644\u0627\u0641\u062A\u062A\u0627\u062D\u064A\",\"Description\":\"Opening Balance Equity\",\"IsActive\":true,\"Code\":\"300000\",\"Accounts\":[{\"Name\":\"Opening Balance Equity\",\"NameArabic\":\"\u0627\u0644\u0631\u0635\u064A\u062F \u0627\u0644\u0627\u0641\u062A\u062A\u0627\u062D\u064A\",\"Description\":\"Opening Balance Equity\",\"IsActive\":true,\"Code\":\"30000001\"}]},{\"Name\":\"Owner Investment\",\"NameArabic\":\"\u0627\u0633\u062A\u062B\u0645\u0627\u0631 \u0627\u0644\u0645\u0627\u0644\u0643 \",\"Description\":\"Owner Investment\",\"IsActive\":true,\"Code\":\"301001\",\"Accounts\":[{\"Name\":\"Owner Investment\",\"NameArabic\":\"\u0627\u0633\u062A\u062B\u0645\u0627\u0631 \u0627\u0644\u0645\u0627\u0644\u0643 \",\"Description\":\"Owner Investment\",\"IsActive\":true,\"Code\":\"30100101\"}]},{\"Name\":\"Owner Withdrawals\",\"NameArabic\":\"\u0627\u0646\u0633\u062D\u0627\u0628\u0627\u062A \u0627\u0644\u0645\u0627\u0644\u0643 \",\"Description\":\"Owner Withdrawals\",\"IsActive\":true,\"Code\":\"302001\",\"Accounts\":[{\"Name\":\"Owner Withdrawals\",\"NameArabic\":\"\u0627\u0646\u0633\u062D\u0627\u0628\u0627\u062A \u0627\u0644\u0645\u0627\u0644\u0643 \",\"Description\":\"Owner Withdrawals\",\"IsActive\":true,\"Code\":\"30200101\"}]},{\"Name\":\"Retained Earnings\",\"NameArabic\":\"\u0627\u0644\u0627\u0631\u0628\u0627\u062D\",\"Description\":\"Retained Earnings\",\"IsActive\":true,\"Code\":\"320000\",\"Accounts\":[{\"Name\":\"Retained Earnings\",\"NameArabic\":\"\u0627\u0644\u0627\u0631\u0628\u0627\u062D\",\"Description\":\"Retained Earnings\",\"IsActive\":true,\"Code\":\"32000001\"}]},{\"Name\":\"Net Profit for the period\",\"NameArabic\":\"\u0635\u0627\u0641\u064A \u0627\u0644\u0631\u0628\u062D \u0644\u0644\u0641\u062A\u0631\u0647\",\"Description\":\"Net Profit for the period\",\"IsActive\":true,\"Code\":\"321002\",\"Accounts\":[{\"Name\":\"Net Profit for the period\",\"NameArabic\":\"\u0635\u0627\u0641\u064A \u0627\u0644\u0631\u0628\u062D \u0644\u0644\u0641\u062A\u0631\u0647\",\"Description\":\"Net Profit for the period\",\"IsActive\":true,\"Code\":\"32100201\"}]}]},{\"Name\":\"Income\",\"NameArabic\":\"\u0627\u064A\u0631\u0627\u062F\u0627\u062A\",\"IsActive\":true,\"CostCenters\":[{\"Name\":\"Sale\",\"NameArabic\":\"\u0645\u0628\u064A\u0639\u0627\u062A\",\"Description\":\"Sale\",\"IsActive\":true,\"Code\":\"420000\",\"Accounts\":[{\"Name\":\"Sale\",\"NameArabic\":\"\u0645\u0628\u064A\u0639\u0627\u062A\",\"Description\":\"Sale\",\"IsActive\":true,\"Code\":\"42000001\"}]},{\"Name\":\"Teller\",\"NameArabic\":\"\u0627\u0644\u0635\u0646\u062F\u0648\u0642\",\"Description\":\"Teller\",\"IsActive\":true,\"Code\":\"421000\",\"Accounts\":[{\"Name\":\"Teller\",\"NameArabic\":\"\u0627\u0644\u0635\u0646\u062F\u0648\u0642\",\"Description\":\"Teller\",\"IsActive\":true,\"Code\":\"42100001\"}]},{\"Name\":\"POS-Terminal\",\"NameArabic\":\"\u0646\u0642\u0627\u0637 \u0627\u0644\u0628\u064A\u0639 - \u0628\u0646\u0643\",\"Description\":\"POS-Terminal\",\"IsActive\":true,\"Code\":\"425000\",\"Accounts\":[{\"Name\":\"POS-Terminal\",\"NameArabic\":\"\u0646\u0642\u0627\u0637 \u0627\u0644\u0628\u064A\u0639 - \u0628\u0646\u0643\",\"Description\":\"POS-Terminal\",\"IsActive\":true,\"Code\":\"42500001\"}]},{\"Name\":\"Discount Taken\",\"NameArabic\":\"\u0627\u0644\u062E\u0635\u0645 \u0627\u0644\u0645\u0623\u062E\u0648\u0630\",\"Description\":\"Discount Taken\",\"IsActive\":true,\"Code\":\"426000\",\"Accounts\":[{\"Name\":\"Discount Taken\",\"NameArabic\":\"\u0627\u0644\u062E\u0635\u0645 \u0627\u0644\u0645\u0623\u062E\u0648\u0630\",\"Description\":\"Discount Taken\",\"IsActive\":true,\"Code\":\"42600001\"}]}]},{\"Name\":\"Expenses\",\"NameArabic\":\"\u0627\u0644\u0645\u0635\u0627\u0631\u064A\u0641\",\"IsActive\":true,\"CostCenters\":[{\"Name\":\"Cost of Goods Sold\",\"NameArabic\":\"\u062A\u0643\u0644\u0641\u0647 \u0627\u0644\u0628\u0636\u0627\u0639\u0647 \u0627\u0644\u0645\u0628\u0627\u0639\u0647\",\"Description\":\"Cost of Goods Sold\",\"IsActive\":true,\"Code\":\"600001\",\"Accounts\":[{\"Name\":\"Cost of Goods Sold\",\"NameArabic\":\"\u062A\u0643\u0644\u0641\u0647 \u0627\u0644\u0628\u0636\u0627\u0639\u0647 \u0627\u0644\u0645\u0628\u0627\u0639\u0647\",\"Description\":\"Cost of Goods Sold\",\"IsActive\":true,\"Code\":\"60000101\"}]},{\"Name\":\"Freight Paid\",\"NameArabic\":\"\u0645\u062F\u0641\u0648\u0639\u0627\u062A \u0627\u0644\u0634\u062D\u0646\",\"Description\":\"Freight Paid\",\"IsActive\":true,\"Code\":\"608001\",\"Accounts\":[{\"Name\":\"Freight Paid\",\"NameArabic\":\"\u0645\u062F\u0641\u0648\u0639\u0627\u062A \u0627\u0644\u0634\u062D\u0646\",\"Description\":\"Freight Paid\",\"IsActive\":true,\"Code\":\"60800101\"}]},{\"Name\":\"Discount Given\",\"NameArabic\":\"\u0627\u0644\u062E\u0635\u0645 \u0627\u0644\u0645\u0642\u062F\u0645\",\"Description\":\"Discount Given\",\"IsActive\":true,\"Code\":\"607001\",\"Accounts\":[{\"Name\":\"Discount Given\",\"NameArabic\":\"\u0627\u0644\u062E\u0635\u0645 \u0627\u0644\u0645\u0642\u062F\u0645\",\"Description\":\"Discount Given\",\"IsActive\":true,\"Code\":\"60700101\"}]},{\"Name\":\"Depreciation Expense\",\"NameArabic\":\"\u0645\u0635\u0627\u0631\u064A\u0641 \u0627\u0644\u0627\u0633\u062A\u0642\u062F\u0627\u0645\",\"Description\":\"Depreciation Expense\",\"IsActive\":true,\"Code\":\"606001\",\"Accounts\":[{\"Name\":\"Depreciation Expense\",\"NameArabic\":\"\u0645\u0635\u0627\u0631\u064A\u0641 \u0627\u0644\u0627\u0633\u062A\u0642\u062F\u0627\u0645\",\"Description\":\"Depreciation Expense\",\"IsActive\":true,\"Code\":\"60600101\"}]},{\"Name\":\"General Expenses\",\"NameArabic\":\"\u0627\u0644\u0645\u0635\u0627\u0631\u064A\u0641 \u0627\u0644\u0639\u0627\u0645\u0629\",\"Description\":\"General Expenses\",\"IsActive\":true,\"Code\":\"605050\",\"Accounts\":[{\"Name\":\"General Expenses\",\"NameArabic\":\"\u0627\u0644\u0645\u0635\u0627\u0631\u064A\u0641 \u0627\u0644\u0639\u0627\u0645\u0629\",\"Description\":\"General Expenses\",\"IsActive\":true,\"Code\":\"60505001\"}]},{\"Name\":\"Payroll\",\"NameArabic\":\"\u0627\u0644\u0631\u0648\u0627\u062A\u0628\",\"Description\":\"Payroll\",\"IsActive\":true,\"Code\":\"603001\",\"Accounts\":[{\"Name\":\"Payroll\",\"NameArabic\":\"\u0627\u0644\u0631\u0648\u0627\u062A\u0628\",\"Description\":\"Payroll\",\"IsActive\":true,\"Code\":\"60300101\"}]},{\"Name\":\"Utilities\",\"NameArabic\":\"\u0627\u0644\u062E\u062F\u0645\u0627\u062A\",\"Description\":\"Utilities\",\"IsActive\":true,\"Code\":\"604001\",\"Accounts\":[{\"Name\":\"Utilities\",\"NameArabic\":\"\u0627\u0644\u062E\u062F\u0645\u0627\u062A\",\"Description\":\"Utilities\",\"IsActive\":true,\"Code\":\"60400101\"}]},{\"Name\":\"Rent\",\"NameArabic\":\"\u0627\u064A\u062C\u0627\u0631\u0627\u062A\",\"Description\":\"Rent\",\"IsActive\":true,\"Code\":\"604050\",\"Accounts\":[{\"Name\":\"Rent\",\"NameArabic\":\"\u0627\u064A\u062C\u0627\u0631\u0627\u062A\",\"Description\":\"Rent\",\"IsActive\":true,\"Code\":\"60405001\"}]},{\"Name\":\"Legal Expenses\",\"NameArabic\":\"\u0627\u0644\u0645\u0635\u0627\u0631\u064A\u0641 \u0627\u0644\u0642\u0627\u0646\u0648\u0646\u064A\u0647\",\"Description\":\"Legal Expenses\",\"IsActive\":true,\"Code\":\"605001\",\"Accounts\":[{\"Name\":\"Legal Expenses\",\"NameArabic\":\"\u0627\u0644\u0645\u0635\u0627\u0631\u064A\u0641 \u0627\u0644\u0642\u0627\u0646\u0648\u0646\u064A\u0647\",\"Description\":\"Legal Expenses\",\"IsActive\":true,\"Code\":\"60500101\"}]}]}]}"

            });

            //#region For Module Names / User Rights

            //#region For Setup Form
            //modelBuilder.Entity<NobleModule>().HasData(new NobleModule
            //{
            //    Id = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F"),
            //    ModuleName = "Setup Form",
            //    Description = "Setup Form",

            //});

            //#region Brand




            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("91B7B87B-9C39-49C4-89B9-19C52CFCC592"),
            //    Description = "Can Save Brand",
            //    Category = "Brand",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("AE1A7C13-DAA5-4466-A8A8-544DCD2B76D1"),
            //    Description = "Can Edit Brand",
            //    Category = "Brand",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("ED83E71C-4949-485C-84FB-FE8C01CA279A"),
            //    Description = "Can Delete Brand",
            //    Category = "Brand",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("082DE882-9836-4869-ABB1-698E264EF6F4"),
            //    Description = "Can View Brand",
            //    Category = "Brand",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F"),
            //});
            //#endregion
            //#region For BarCode Printing

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("5E2A63E0-2CD3-43E5-825A-0CE263BE0765"),
            //    Description = "Can Save BarCode Printing",
            //    Category = "BarCode Printing",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("6E045010-76D2-4261-8C0B-378D5252AF49"),
            //    Description = "Can Edit BarCode Printing",
            //    Category = "BarCode Printing",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("F1E667A4-B7D4-4105-A232-EAC63C804CCD"),
            //    Description = "Can Delete BarCode Printing",
            //    Category = "BarCode Printing",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("F161D9BD-7DB7-467E-A86D-7EB812BEAD04"),
            //    Description = "Can View BarCode Printing",
            //    Category = "BarCode Printing",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F"),
            //});
            //#endregion

            //#region For Category

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("079F4F75-7B77-4339-BC73-FC3E40E36C8D"),
            //    Description = "Can Save Category",
            //    Category = "Category",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("06BFF7FC-ECCA-4305-886E-3D7AA5246BEA"),
            //    Description = "Can Edit Category",
            //    Category = "Category",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("4AD20ABC-4E2D-4CDF-A70D-863AE4EE5588"),
            //    Description = "Can Delete Category",
            //    Category = "Category",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("669ED81D-96BC-4ED6-A3E5-0F3F01954114"),
            //    Description = "Can View Category",
            //    Category = "Category",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F"),
            //});
            //#endregion
            //#region For Color

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("DF168358-99A7-41CF-BC2D-32DCD6B62D03"),
            //    Description = "Can Save Color",
            //    Category = "Color",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("B11A0C0C-934C-4E0B-B652-932C8C507C1F"),
            //    Description = "Can Edit Color",
            //    Category = "Color",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("53805CD0-480D-49E3-A792-040611C2776F"),
            //    Description = "Can Delete Color",
            //    Category = "Color",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("2C57F8DE-23F6-4FDF-A99E-A87CD6A48083"),
            //    Description = "Can View Color",
            //    Category = "Color",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F"),
            //});
            //#endregion





            //#region For Origin

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("452C8428-FA7C-421D-89D6-CD12E6FC8439"),
            //    Description = "Can Save Origin",
            //    Category = "Origin",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("D79AFA05-039B-4F3B-8038-ED835E7123ED"),
            //    Description = "Can Edit Origin",
            //    Category = "Origin",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("E14BC4E1-8D09-4A3E-8550-04BCC331AF1C"),
            //    Description = "Can Delete Origin",
            //    Category = "Origin",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("1F093EA3-814D-4338-8AA5-6B25013D5878"),
            //    Description = "Can View Origin",
            //    Category = "Origin",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F"),
            //});
            //#endregion
            //#region For SubCategories

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("E72BE317-8C29-4487-9382-A0CCA1AED819"),
            //    Description = "Can Save SubCategories",
            //    Category = "SubCategories",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("977102EF-9524-4AB8-A163-1FE4672348A8"),
            //    Description = "Can Edit SubCategories",
            //    Category = "SubCategories",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("7E04D43E-AA36-404C-9633-288DA5AE072E"),
            //    Description = "Can Delete SubCategories",
            //    Category = "SubCategories",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("C6C23136-17B8-4787-8F96-563389A204AB"),
            //    Description = "Can View SubCategories",
            //    Category = "SubCategories",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //#endregion


            //#region For TaxRates

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("3AECC007-62CC-4657-9CFF-F26E6D91D3F4"),
            //    Description = "Can Save Tax Rate",
            //    Category = "Tax Rate",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("2A2A3400-D8E4-4D85-83E8-ADFA8D3E6428"),
            //    Description = "Can Edit Tax Rate",
            //    Category = "Tax Rate",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("6C5A8310-4508-48DA-9228-C81DA9B4F84A"),
            //    Description = "Can Delete Tax Rate",
            //    Category = "Tax Rate",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("63B07E2C-EAB5-4CCE-B838-D592553F8D06"),
            //    Description = "Can View Tax Rate",
            //    Category = "Tax Rate",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //#endregion

            //#region For Units

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("38C57C94-31A9-48C5-AB03-B20B6FE24EF5"),
            //    Description = "Can Save Unit",
            //    Category = "Unit",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("F70377CA-2E6C-435F-9025-23C1361579B6"),
            //    Description = "Can Edit Unit",
            //    Category = "Unit",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("F09F60E6-8AC3-444F-86D8-B4489A40EC7B"),
            //    Description = "Can Delete Unit",
            //    Category = "Unit",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("7E17FF28-B433-4D27-BF29-51AFD7709DD0"),
            //    Description = "Can View Unit",
            //    Category = "Unit",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //#endregion

            //#region For Warehouses

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("75EF2940-38D4-41F2-8DE0-89CB8C216592"),
            //    Description = "Can Save Ware House",
            //    Category = "Ware House",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("804B8823-C980-4B11-9D7D-3936469DFDE6"),
            //    Description = "Can Edit Ware House",
            //    Category = "Ware House",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("E2092C9B-7051-485E-9002-E4BDFB987005"),
            //    Description = "Can Delete Ware House",
            //    Category = "Ware House",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("63EDF9D4-FEB8-44F3-8FEE-12022AFF1E54"),
            //    Description = "Can View Ware House",
            //    Category = "Ware House",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //#endregion

            //#region For Company Setup

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("D0F5705E-9BEF-405B-A925-56B833D5C029"),
            //    Description = "Can Save Company Setup",
            //    Category = "Company Setup",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("0F3CAA9A-07BA-4C5F-B4C5-E8CCB88EA421"),
            //    Description = "Can Edit Company Setup",
            //    Category = "Company Setup",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("01471545-B55B-4820-9FA5-37A13DA008B0"),
            //    Description = "Can Delete Company Setup",
            //    Category = "Company Setup",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("3C707A3B-16EF-4D69-8B8D-2906CD97C1A8"),
            //    Description = "Can View Company Setup",
            //    Category = "Company Setup",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //#endregion
            //#region For Payment Option

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("48A7E968-C394-48B3-97F3-CA170D69BF07"),
            //    Description = "Can Save Payment Option",
            //    Category = "Payment Option",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("589A7313-8B15-40D1-9863-8B23BCF5F387"),
            //    Description = "Can Edit Payment Option",
            //    Category = "Payment Option",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("F0F8183B-6E0A-4A47-8BA3-A49200D5C564"),
            //    Description = "Can Delete Payment Option",
            //    Category = "Payment Option",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("9BA25565-C988-499A-8C7C-604CD3D0B5FC"),
            //    Description = "Can View Payment Option",
            //    Category = "Payment Option",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //#endregion
            //#region For Terminals

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("01B9E41D-C0DC-4065-9C04-FFF087468616"),
            //    Description = "Can Save Terminals",
            //    Category = "Terminals",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("AB6C2CAA-FE02-4B01-8AE3-639318A4F09A"),
            //    Description = "Can Edit Terminals",
            //    Category = "Terminals",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("52B993B9-65EE-45ED-B044-9F488F05EA9E"),
            //    Description = "Can Delete Terminals",
            //    Category = "Terminals",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("40E034FD-00DD-4EA8-8FF2-F5AE34E7CB7B"),
            //    Description = "Can View Terminals",
            //    Category = "Terminals",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //#endregion
            //#region For Currency

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("57282478-E239-45E6-BF8C-3B11A84F5A8C"),
            //    Description = "Can Save Currency",
            //    Category = "Currency",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("F466E424-E5EC-4A8F-9893-675A5A96E526"),
            //    Description = "Can Edit Currency",
            //    Category = "Currency",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("6D11941F-4EFC-4184-891B-95D1BB486D98"),
            //    Description = "Can Delete Currency",
            //    Category = "Currency",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("0232B199-7C83-4199-BDC1-1E057D3678D3"),
            //    Description = "Can View Currency",
            //    Category = "Currency",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //#endregion
            //#region For Monthly Cost

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("38DF78A2-2E02-448E-8458-70AE509D2A5E"),
            //    Description = "Can Save Monthly Cost",
            //    Category = "Monthly Cost",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("B4FD3F41-684D-413B-98D8-BBBBDB8EB0A8"),
            //    Description = "Can Edit Monthly Cost",
            //    Category = "Monthly Cost",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("998E1AE2-071C-4687-BA77-4DB8B7786888"),
            //    Description = "Can Delete Monthly Cost",
            //    Category = "Monthly Cost",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("F75ADBD1-53EA-4713-873B-A21D4EB2C658"),
            //    Description = "Can View Monthly Cost",
            //    Category = "Monthly Cost",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //#endregion
            //#region For Size

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("99AF4D7B-731F-4456-8D03-37FC76682625"),
            //    Description = "Can Save Size",
            //    Category = "Size",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("E14FD3BB-1D4A-4878-90DB-5A29B56E6500"),
            //    Description = "Can Edit Size",
            //    Category = "Size",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("0BF63044-8C7A-42C5-8764-BF795BB37447"),
            //    Description = "Can Delete Size",
            //    Category = "Size",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("62A3710D-F16F-4CE5-AB72-04B717E48890"),
            //    Description = "Can View Size",
            //    Category = "Size",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //#endregion
            //#region For User Role

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("AA96F625-5526-4CB8-B0D7-01FBCAED6F45"),
            //    Description = "Can Save Role",
            //    Category = "Role",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("604FF746-2BD5-4558-A6B0-EF5F49CCD9E8"),
            //    Description = "Can View Role",
            //    Category = "Role",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //#endregion

            //#region For Permission

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("7441B264-1D06-4C56-A80A-B0272110273F"),
            //    Description = "Can View Permission",
            //    Category = "Permission",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("C19E11D3-977F-4A77-8B6E-B87B5769BB5E"),
            //    Description = "Can Assign User",
            //    Category = "Permission",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("1438F661-3DD7-42D4-A9E9-81FC1D8A0C54"),
            //    Description = "Can Update Permission",
            //    Category = "Permission",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //#endregion
            //#region For Change Profile

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("949AD470-96E6-4A46-BBFD-5384457BC2DC"),
            //    Description = "Can Change Profile",
            //    Category = "Permission",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});


            //#endregion

            //#region For View Dashboard

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("CFF3D9A2-B4ED-46F9-B0C1-080F0C6E0D4E"),
            //    Description = "Can View Dashboard",
            //    Category = "View Dashboard",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});


            //#endregion
            //#region For Location Attachment

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("98558D0D-8032-4B8D-B91B-D97936B9FDB1"),
            //    Description = "Can View Location Attachment",
            //    Category = "Can View Location Attachment",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});


            //#endregion

            //#region Denomination Setup

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("5441B964-256B-4722-B761-3844FBF33532"),
            //    Description = "Can Save Denomination Setup",
            //    Category = "Denomination Setup",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("D65035C8-8F9D-463F-B528-8160F1A02F38"),
            //    Description = "Can Edit Denomination Setup",
            //    Category = "Denomination Setup",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("6F4CC69E-937E-416A-819F-591F83867984"),
            //    Description = "Can Delete Denomination Setup",
            //    Category = "Denomination Setup",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("A3AD316C-7D58-4466-9920-5D421699D9EF"),
            //    Description = "Can View Denomination Setup",
            //    Category = "Denomination Setup",
            //    NobleModuleId = Guid.Parse("F172D244-FEF6-4CB0-826E-55778F3E196F")
            //});
            //#endregion

            //#endregion

            //#region Accounting
            //modelBuilder.Entity<NobleModule>().HasData(new NobleModule
            //{
            //    Id = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //    ModuleName = "Accounting",
            //    Description = "Accounting",

            //});

            //#region For Bank

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("1966796C-CB72-4F4C-BBA4-1D10D8A1DC5F"),
            //    Description = "Can Save  Bank",
            //    Category = "Bank",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("8872D4DB-6554-441E-9513-AF35E4963433"),
            //    Description = "Can Edit Bank",
            //    Category = "Bank",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("D5565381-58F5-4C97-975E-3D7CD44E7C0B"),
            //    Description = "Can Delete Bank",
            //    Category = "Bank",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("7C1327B2-AB3B-4462-8A32-5F202D6B6886"),
            //    Description = "Can View Bank",
            //    Category = "Bank",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //#endregion

            //#region For Cash Receipt Draft
            //modelBuilder.Entity<NobleModule>().HasData(new NobleModule
            //{
            //    Id = Guid.Parse("27F9AA66-5FDE-49B3-A9D8-0A16E65D3F7A"),
            //    ModuleName = "Cash Receipt",
            //    Description = "Cash Receipt",

            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("C16C04D9-72E1-4BD6-8AEC-F630D7B4A1F4"),
            //    Description = "Can Save Cash Receipt as Draft",
            //    Category = "Cash Receipt",
            //    NobleModuleId = Guid.Parse("27F9AA66-5FDE-49B3-A9D8-0A16E65D3F7A"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("155A03AF-EBBD-4260-B010-2ED20D7CFD63"),
            //    Description = "Can Edit Cash Receipt as Draft",
            //    Category = "Cash Receipt",
            //    NobleModuleId = Guid.Parse("27F9AA66-5FDE-49B3-A9D8-0A16E65D3F7A"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("053493A2-5755-4E07-BC3B-30942F506B92"),
            //    Description = "Can Delete Cash Receipt as Draft",
            //    Category = "Cash Receipt",
            //    NobleModuleId = Guid.Parse("27F9AA66-5FDE-49B3-A9D8-0A16E65D3F7A"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("073FAF36-CAAA-4D70-876A-AFF78E5A6530"),
            //    Description = "Can View Cash Receipt as Draft",
            //    Category = "Cash Receipt",
            //    NobleModuleId = Guid.Parse("27F9AA66-5FDE-49B3-A9D8-0A16E65D3F7A"),
            //});
            //#endregion
            //#region For Cash Receipt Post

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("ADDE6B14-A937-4B3B-998D-350D01FD198B"),
            //    Description = "Can Save Cash Receipt as Post",
            //    Category = "Cash Receipt",
            //    NobleModuleId = Guid.Parse("27F9AA66-5FDE-49B3-A9D8-0A16E65D3F7A"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("3F145B00-BB89-4A5F-9CB8-BABB540803DB"),
            //    Description = "Can Edit Cash Receipt as Post",
            //    Category = "Cash Receipt",
            //    NobleModuleId = Guid.Parse("27F9AA66-5FDE-49B3-A9D8-0A16E65D3F7A"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("93449156-D2FC-4402-9E83-A168BA0FB4C3"),
            //    Description = "Can Delete Cash Receipt as Post",
            //    Category = "Cash Receipt",
            //    NobleModuleId = Guid.Parse("27F9AA66-5FDE-49B3-A9D8-0A16E65D3F7A"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("E09194B3-E355-4975-8247-C0170438E849"),
            //    Description = "Can View Cash Receipt as Post",
            //    Category = "Cash Receipt",
            //    NobleModuleId = Guid.Parse("27F9AA66-5FDE-49B3-A9D8-0A16E65D3F7A"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("65798E08-466B-4385-8E9F-667B97FD2550"),
            //    Description = "Can Reject Cash Receipt",
            //    Category = "Cash Receipt",
            //    NobleModuleId = Guid.Parse("27F9AA66-5FDE-49B3-A9D8-0A16E65D3F7A"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("4C70E79D-8F4D-429B-82FA-3593E90F1F8B"),
            //    Description = "Can Bulk Reject Cash Receipt",
            //    Category = "Cash Receipt",
            //    NobleModuleId = Guid.Parse("27F9AA66-5FDE-49B3-A9D8-0A16E65D3F7A"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("3D295FD3-6E71-47A1-8BA9-AF0AD840A4CA"),
            //    Description = "Can Bulk Post Cash Receipt",
            //    Category = "Cash Receipt",
            //    NobleModuleId = Guid.Parse("27F9AA66-5FDE-49B3-A9D8-0A16E65D3F7A"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("39E11111-D914-407F-B5C6-3FAABEC8E049"),
            //    Description = "Can Void Cash Receipt",
            //    Category = "Cash Receipt",
            //    NobleModuleId = Guid.Parse("27F9AA66-5FDE-49B3-A9D8-0A16E65D3F7A"),
            //});
            //#endregion

            //#region For Cash Pay Draft

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("324744B6-54AE-4A7A-9C47-8C36A8383F3A"),
            //    Description = "Can Save Cash Pay  as Draft",
            //    Category = "Cash Pay",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("93FD99D1-458A-4446-954C-8DDFA631DEF5"),
            //    Description = "Can Edit  Cash Pay as Draft",
            //    Category = "Cash Pay",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("4AEE9684-139C-44D8-B7E6-EFE076995BA2"),
            //    Description = "Can Delete Cash Pay as Draft",
            //    Category = "Cash Pay",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("DE921DB2-438C-4E82-A956-6674DBF27860"),
            //    Description = "Can View Cash Pay as Draft",
            //    Category = "Cash Pay",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //#endregion
            //#region For Cash Pay Post

            //modelBuilder.Entity<NobleModule>().HasData(new NobleModule
            //{
            //    Id = Guid.Parse("134A06EA-9447-4EA1-982E-2B688CA243D0"),
            //    ModuleName = "Cash Pay",
            //    Description = "Cash Pay",

            //});

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("D0A55259-37FF-4987-9A17-EC17B56107DA"),
            //    Description = "Can Save Cash Pay  as Post",
            //    Category = "Cash Pay",
            //    NobleModuleId = Guid.Parse("134A06EA-9447-4EA1-982E-2B688CA243D0"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("FAFA3DEE-7560-4713-A664-9877470E485B"),
            //    Description = "Can Edit  Cash Pay as Post",
            //    Category = "Cash Pay",
            //    NobleModuleId = Guid.Parse("134A06EA-9447-4EA1-982E-2B688CA243D0"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("31BC4A88-10A6-493A-8FCB-5C2C6100D7EF"),
            //    Description = "Can Delete Cash Pay as Post",
            //    Category = "Cash Pay",
            //    NobleModuleId = Guid.Parse("134A06EA-9447-4EA1-982E-2B688CA243D0"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("5F290A32-02F2-4893-947D-DFA81B83F8CD"),
            //    Description = "Can View Cash Pay as Post",
            //    Category = "Cash Pay",
            //    NobleModuleId = Guid.Parse("134A06EA-9447-4EA1-982E-2B688CA243D0"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("3EAA85DC-DB63-45FC-98C8-1DEE0BCB4D5E"),
            //    Description = "Can Reject Cash Pay",
            //    Category = "Cash Pay",
            //    NobleModuleId = Guid.Parse("134A06EA-9447-4EA1-982E-2B688CA243D0"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("13993ED5-8ADB-4253-BC48-932B4C9C02B3"),
            //    Description = "Can Bulk Reject Cash Pay",
            //    Category = "Cash Pay",
            //    NobleModuleId = Guid.Parse("134A06EA-9447-4EA1-982E-2B688CA243D0"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("060956A5-C1D7-4A18-95D0-27B25880F2D8"),
            //    Description = "Can Bulk Post Cash Pay",
            //    Category = "Cash Pay",
            //    NobleModuleId = Guid.Parse("134A06EA-9447-4EA1-982E-2B688CA243D0"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("F9A309A0-7EC5-4180-9BFE-87C4E6D03B9C"),
            //    Description = "Can Void Cash Pay",
            //    Category = "Cash Pay",
            //    NobleModuleId = Guid.Parse("134A06EA-9447-4EA1-982E-2B688CA243D0"),
            //});
            //#endregion

            //#region For Bank Pay Draft

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("B2DE6DAD-5F4A-4E16-9247-EFB55626CE9A"),
            //    Description = "Can Save Bank Pay  as Draft",
            //    Category = "Bank Pay",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("5FB823EA-615E-455C-8C85-D43F1653D3BF"),
            //    Description = "Can Edit Bank Pay as Draft",
            //    Category = "Bank Pay",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("5E489851-97B6-4C42-BE7D-4AFF035E17AA"),
            //    Description = "Can Delete Bank Pay as Draft",
            //    Category = "Bank Pay",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("D15D5014-2312-4B71-911A-BEDDC3BD9065"),
            //    Description = "Can View Bank Pay as Draft",
            //    Category = "Bank Pay",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //#endregion
            //#region For Bank Pay Post

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("272E9227-35F4-47BC-9566-C2EEC585E66B"),
            //    Description = "Can Save Bank Pay  as Post",
            //    Category = "Bank Pay",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("1C051982-F3D2-469D-BE5E-A9D89B780152"),
            //    Description = "Can Edit Bank Pay as Post",
            //    Category = "Bank Pay",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("689BCDAD-0197-41C6-97B0-628FCF9F4456"),
            //    Description = "Can Delete Bank Pay as Post",
            //    Category = "Bank Pay",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("D7A683A8-7E98-4CE3-806E-3A849546D84B"),
            //    Description = "Can View Bank Pay as Post",
            //    Category = "Bank Pay",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("00A03A14-4CC3-4A50-90F9-2D128C054241"),
            //    Description = "Can Reject Bank Pay",
            //    Category = "Bank Pay",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("A7CDEE78-22CC-4764-8B4F-33B849423114"),
            //    Description = "Can Bulk Reject Bank Pay",
            //    Category = "Bank Pay",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("6F9DE553-985F-49C8-A12D-6304757B9FD4"),
            //    Description = "Can Bulk Post Bank Pay",
            //    Category = "Bank Pay",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("C4E89441-577F-4820-955D-1805B7333C16"),
            //    Description = "Can Void Bank Pay",
            //    Category = "Bank Pay",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //#endregion

            //#region For Bank Receipt as Draft
            //modelBuilder.Entity<NobleModule>().HasData(new NobleModule
            //{
            //    Id = Guid.Parse("8D800A27-2C0D-4D8C-B530-44CF83A8B7C8"),
            //    ModuleName = "Bank Receipt",
            //    Description = "Bank Receipt",

            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("E9DD831F-02EC-4B41-BAF8-5A5D96A3450A"),
            //    Description = "Can Save Bank Receipt as Draft",
            //    Category = "Bank Receipt",
            //    NobleModuleId = Guid.Parse("8D800A27-2C0D-4D8C-B530-44CF83A8B7C8"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("0E469F66-B4FD-4627-8590-0761AD978259"),
            //    Description = "Can Edit Bank Receipt as Draft",
            //    Category = "Bank Receipt",
            //    NobleModuleId = Guid.Parse("8D800A27-2C0D-4D8C-B530-44CF83A8B7C8"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("DCB93D01-A317-446F-A51B-80CC065C9415"),
            //    Description = "Can Delete Bank Receipt as Draft",
            //    Category = "Bank Receipt",
            //    NobleModuleId = Guid.Parse("8D800A27-2C0D-4D8C-B530-44CF83A8B7C8"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("DADD482E-D6EA-466A-A32D-4A836070480E"),
            //    Description = "Can View Bank Receipt as Draft",
            //    Category = "Bank Receipt",
            //    NobleModuleId = Guid.Parse("8D800A27-2C0D-4D8C-B530-44CF83A8B7C8"),
            //});
            //#endregion
            //#region For Bank Receipt as Post

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("53A5207A-2E57-4636-80B6-E1E0B9390B6F"),
            //    Description = "Can Save Bank Receipt as Post",
            //    Category = "Bank Receipt",
            //    NobleModuleId = Guid.Parse("8D800A27-2C0D-4D8C-B530-44CF83A8B7C8"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("40624F69-1C4E-4C43-9EA3-F8F31B74AB34"),
            //    Description = "Can Edit Bank Receipt as Post",
            //    Category = "Bank Receipt",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("6143BB33-1034-448E-8433-E50F39F83360"),
            //    Description = "Can Delete Bank Receipt as Post",
            //    Category = "Bank Receipt",
            //    NobleModuleId = Guid.Parse("8D800A27-2C0D-4D8C-B530-44CF83A8B7C8"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("D0C71D2A-06CC-45E3-8D63-6302BD85019A"),
            //    Description = "Can View Bank Receipt as Post",
            //    Category = "Bank Receipt",
            //    NobleModuleId = Guid.Parse("8D800A27-2C0D-4D8C-B530-44CF83A8B7C8"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("121DFEA0-D3DC-4F6C-92EB-942A1961929C"),
            //    Description = "Can Reject Bank Receipt",
            //    Category = "Bank Receipt",
            //    NobleModuleId = Guid.Parse("8D800A27-2C0D-4D8C-B530-44CF83A8B7C8"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("E63597AE-2C66-4378-A110-627D444A721B"),
            //    Description = "Can Bulk Reject Bank Receipt",
            //    Category = "Bank Receipt",
            //    NobleModuleId = Guid.Parse("8D800A27-2C0D-4D8C-B530-44CF83A8B7C8"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("38B88D18-D6E6-4E97-9E79-045EF2E971FD"),
            //    Description = "Can Bulk Post Bank Receipt",
            //    Category = "Bank Receipt",
            //    NobleModuleId = Guid.Parse("8D800A27-2C0D-4D8C-B530-44CF83A8B7C8"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("D6D6C184-762E-42C9-B11B-395245D599E4"),
            //    Description = "Can Void Bank Receipt",
            //    Category = "Bank Receipt",
            //    NobleModuleId = Guid.Parse("8D800A27-2C0D-4D8C-B530-44CF83A8B7C8"),
            //});
            //#endregion

            //#region For COA

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("6E935BC8-6BB3-428A-93C8-DFED39F62AA5"),
            //    Description = "Can Save Chart Of Account",
            //    Category = "Chart Of Account",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("5062204B-6BD7-40D5-8B1A-DD4F3BE10095"),
            //    Description = "Can Edit Chart Of Account",
            //    Category = "Chart Of Account",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("27559DBA-5FC7-42E7-82AA-38863113F5D0"),
            //    Description = "Can Delete Chart Of Account",
            //    Category = "Chart Of Account",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("15813B93-E6B9-47E0-AF83-01ED14DBA606"),
            //    Description = "Can View Chart Of Account",
            //    Category = "Chart Of Account",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //#endregion
            //#region For Journal Voucher as Draft

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("611753D3-0399-4C52-B335-C3EFAB8AFE03"),
            //    Description = "Can Save Journal Voucher as Draft",
            //    Category = "Journal Voucher",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("232517A9-F32D-48B2-B570-8CD830AE1ABF"),
            //    Description = "Can Edit  Journal Voucher as Draft",
            //    Category = "Journal Voucher",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("2DEA3C02-5ECE-4F77-A4E2-51BD228B1B26"),
            //    Description = "Can Delete  Journal Voucher as Draft",
            //    Category = "Journal Voucher",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("FE7234E7-1B45-4147-836C-DF0538E36446"),
            //    Description = "Can View Journal Voucher as Draft",
            //    Category = "Journal Voucher",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //#endregion
            //#region For Journal Voucher as Post

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("ECB25B27-0AFD-4888-9B96-DCBB930B0C89"),
            //    Description = "Can Save Journal Voucher as Post",
            //    Category = "Journal Voucher",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("61705C54-0304-4A43-BC5F-9D4C46A0583C"),
            //    Description = "Can Edit  Journal Voucher as Post",
            //    Category = "Journal Voucher",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("5F7B17FD-6DC4-4B8D-B772-7C80EC0BF148"),
            //    Description = "Can Delete  Journal Voucher as Post",
            //    Category = "Journal Voucher",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("C628AB0C-5109-46FA-97B5-4D3954D1F566"),
            //    Description = "Can View Journal Voucher as Post",
            //    Category = "Journal Voucher",
            //    NobleModuleId = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //});
            //#endregion


            //#region Opening Cash as Draft
            //modelBuilder.Entity<NobleModule>().HasData(new NobleModule
            //{
            //    Id = Guid.Parse("BC9C5967-19B3-460E-ACE7-77229907E1E3"),
            //    ModuleName = "Opening Cash",
            //    Description = "Opening Cash",

            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("B72DE9F0-2571-4378-B855-B4C8218CCCBB"),
            //    Description = "Can Save Opening Cash as Draft",
            //    Category = "Opening Cash",
            //    NobleModuleId = Guid.Parse("BC9C5967-19B3-460E-ACE7-77229907E1E3"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("3B7FD3BD-43FF-40F3-AF08-F71CEA6942F3"),
            //    Description = "Can Edit  Opening Cash as Draft",
            //    Category = "Opening Cash",
            //    NobleModuleId = Guid.Parse("BC9C5967-19B3-460E-ACE7-77229907E1E3"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("AFEA8EE7-482B-408C-9820-0E9E8D39B3DE"),
            //    Description = "Can Delete  Opening Cash as Draft",
            //    Category = "Opening Cash",
            //    NobleModuleId = Guid.Parse("BC9C5967-19B3-460E-ACE7-77229907E1E3"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("479F1176-3194-4DC5-B167-D08C78AE0A59"),
            //    Description = "Can View Opening Cash as Draft",
            //    Category = "Opening Cash",
            //    NobleModuleId = Guid.Parse("BC9C5967-19B3-460E-ACE7-77229907E1E3"),
            //});
            //#endregion
            //#region For Opening Cash as Post

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("CB5C4ED6-889C-4C45-BAB0-976367852964"),
            //    Description = "Can Save Opening Cash as Post",
            //    Category = "Opening Cash",
            //    NobleModuleId = Guid.Parse("BC9C5967-19B3-460E-ACE7-77229907E1E3"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("6D1BBE01-C642-47AD-9ADB-2745A8E377F4"),
            //    Description = "Can Edit  Opening Cash as Post",
            //    Category = "Opening Cash",
            //    NobleModuleId = Guid.Parse("BC9C5967-19B3-460E-ACE7-77229907E1E3"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("FD94FD5C-AB4C-4904-8507-CDCE421A8717"),
            //    Description = "Can Delete  Opening Cash as Post",
            //    Category = "Opening Cash",
            //    NobleModuleId = Guid.Parse("BC9C5967-19B3-460E-ACE7-77229907E1E3"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("D7E02612-0A24-4055-85F5-2870449B5376"),
            //    Description = "Can View Opening Cash as Post",
            //    Category = "Opening Cash",
            //    NobleModuleId = Guid.Parse("BC9C5967-19B3-460E-ACE7-77229907E1E3"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("D333DD22-BD80-48CF-B5E1-D477B26A2C39"),
            //    Description = "Can Void Opening Cash",
            //    Category = "Opening Cash",
            //    NobleModuleId = Guid.Parse("BC9C5967-19B3-460E-ACE7-77229907E1E3"),
            //});
            //#endregion

            //#region Petty Cash as Draft
            //modelBuilder.Entity<NobleModule>().HasData(new NobleModule
            //{
            //    Id = Guid.Parse("F9CEAB2B-9EB3-4DA1-BD35-49FE51375FAF"),
            //    ModuleName = "Petty Cash",
            //    Description = "Petty Cash",

            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("0D45F8F5-EEF4-477C-B218-5F8A50C53F23"),
            //    Description = "Can Save Petty Cash as Draft",
            //    Category = "Petty Cash",
            //    NobleModuleId = Guid.Parse("F9CEAB2B-9EB3-4DA1-BD35-49FE51375FAF"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("0B67DB30-940C-426F-B836-06BDEAA6BF3B"),
            //    Description = "Can Edit  Petty Cash as Draft",
            //    Category = "Petty Cash",
            //    NobleModuleId = Guid.Parse("F9CEAB2B-9EB3-4DA1-BD35-49FE51375FAF"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("60D0F8FA-CEF0-4B4D-8A31-088A3592767F"),
            //    Description = "Can Delete  Petty Cash as Draft",
            //    Category = "Petty Cash",
            //    NobleModuleId = Guid.Parse("F9CEAB2B-9EB3-4DA1-BD35-49FE51375FAF"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("CE5B3803-8506-4D67-8E1E-7B40308DB7B3"),
            //    Description = "Can View Petty Cash as Draft",
            //    Category = "Petty Cash",
            //    NobleModuleId = Guid.Parse("F9CEAB2B-9EB3-4DA1-BD35-49FE51375FAF"),
            //});
            //#endregion
            //#region For Opening Cash as Post

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("EC36441F-0F6A-4A3D-9363-88B8167A2E7A"),
            //    Description = "Can Save Petty Cash as Post",
            //    Category = "Petty Cash",
            //    NobleModuleId = Guid.Parse("F9CEAB2B-9EB3-4DA1-BD35-49FE51375FAF"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("EF57786C-FB62-419B-ABEE-17476CB98384"),
            //    Description = "Can Edit  Petty Cash as Post",
            //    Category = "Petty Cash",
            //    NobleModuleId = Guid.Parse("F9CEAB2B-9EB3-4DA1-BD35-49FE51375FAF"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("2A42D57C-5B2D-4672-B97F-2D8A1431ACA4"),
            //    Description = "Can Delete  Petty Cash as Post",
            //    Category = "Petty Cash",
            //    NobleModuleId = Guid.Parse("F9CEAB2B-9EB3-4DA1-BD35-49FE51375FAF"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("EAEF4F9A-F788-44A5-893F-07650026DE50"),
            //    Description = "Can View Petty Cash as Post",
            //    Category = "Petty Cash",
            //    NobleModuleId = Guid.Parse("F9CEAB2B-9EB3-4DA1-BD35-49FE51375FAF"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("7631BAD7-72E7-44D4-A67C-735DFA462BA8"),
            //    Description = "Can Void Petty Cash",
            //    Category = "Petty Cash",
            //    NobleModuleId = Guid.Parse("F9CEAB2B-9EB3-4DA1-BD35-49FE51375FAF"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("020DCAC7-B35A-4B72-8298-4F191D8F9A61"),
            //    Description = "Can Reject Petty Cash",
            //    Category = "Petty Cash",
            //    NobleModuleId = Guid.Parse("F9CEAB2B-9EB3-4DA1-BD35-49FE51375FAF"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("F35E55AE-5122-4C0A-B792-40D1794DF0AD"),
            //    Description = "Can Bulk Reject Petty Cash",
            //    Category = "Petty Cash",
            //    NobleModuleId = Guid.Parse("F9CEAB2B-9EB3-4DA1-BD35-49FE51375FAF"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("D1A81B53-64AF-485A-8D81-442D7C447436"),
            //    Description = "Can Bulk Post Petty Cash",
            //    Category = "Petty Cash",
            //    NobleModuleId = Guid.Parse("F9CEAB2B-9EB3-4DA1-BD35-49FE51375FAF"),
            //});
            //#endregion

            //#endregion

            //#region Purchase Order
            //modelBuilder.Entity<NobleModule>().HasData(new NobleModule
            //{
            //    Id = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //    ModuleName = "Purchase Order",
            //    Description = "Purchase Order",

            //});

            //#region For Purchase Order Draft

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("61AFAF5A-6EAF-4387-B81B-A96A7165A44E"),
            //    Description = "Can Save  Purchase Order as Draft",
            //    Category = "Purchase Order as Draft",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("F8A3751E-D8F9-49B3-95C9-ABC3116C11FC"),
            //    Description = "Can Edit Purchase Order as Draft",
            //    Category = "Purchase Order as Draft",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("F2B3751E-D8F9-49B3-95C9-ABC3116C11FC"),
            //    Description = "Can Delete Purchase Order as Draft",
            //    Category = "Purchase Order as Draft",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("C7D6F1AF-395F-4EB2-9A24-8FDE4B775074"),
            //    Description = "Can View Purchase Order as Draft",
            //    Category = "Purchase Order as Draft",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //#endregion
            //#region For Purchase Order as Post

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("BC24546B-0ABA-4DA8-B2BB-2DB326A70783"),
            //    Description = "Can Save Purchase Order as Post",
            //    Category = "Purchase Order as Post",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("238F7D1B-243B-4854-ABCA-CB06309A6822"),
            //    Description = "Can Edit Purchase Order as Post",
            //    Category = "Purchase Order as Post",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("FDB7CEA6-6BCC-4CB6-94A9-C7981C6EEA7C"),
            //    Description = "Can Delete Purchase Order as Post",
            //    Category = "Purchase Order as Post",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("55B674DA-88ED-4A38-A056-8A88E72CE7A6"),
            //    Description = "Can View  Purchase Order as Post",
            //    Category = "Purchase Order as Post",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //#endregion

            //#region For Purchase Invoice Draft

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("3D175F3F-EFD6-42B0-9013-0E4BC148BCD0"),
            //    Description = "Can Save  Purchase Invoice as Draft",
            //    Category = "Purchase Invoice as Draft",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("853F028D-2A1D-419F-BE3C-81D7619C2E96"),
            //    Description = "Can Edit Purchase Invoice as Draft",
            //    Category = "Purchase Invoice as Draft",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("4F37E638-1793-48AA-B5A6-25A5CF215B0D"),
            //    Description = "Can Delete Purchase Invoice as Draft",
            //    Category = "Purchase Invoice as Draft",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("F5E47E34-E531-4E91-B288-FB09B7F950E7"),
            //    Description = "Can View Purchase Invoice as Draft",
            //    Category = "Purchase Invoice as Draft",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //#endregion
            //#region For Purchase Invoice as Post

            //modelBuilder.Entity<NobleModule>().HasData(new NobleModule
            //{
            //    Id = Guid.Parse("E328E825-8D13-4CCF-83BA-3ED0B2560E42"),
            //    ModuleName = "Purchase Invoice as Post",
            //    Description = "Purchase Invoice as Post",

            //});

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("02651EB3-3F41-4C78-8D3B-92C8E401C37C"),
            //    Description = "Can Save Purchase Invoice as Post",
            //    Category = "Purchase Invoice as Post",
            //    NobleModuleId = Guid.Parse("E328E825-8D13-4CCF-83BA-3ED0B2560E42"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("F2A3751E-D8F9-49B3-95C9-0BC3116C11FC"),
            //    Description = "Can Edit Purchase Invoice as Post",
            //    Category = "Purchase Invoice as Post",
            //    NobleModuleId = Guid.Parse("E328E825-8D13-4CCF-83BA-3ED0B2560E42"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("DF4C05F4-AFEA-48CB-9B94-06AA7D04F1DF"),
            //    Description = "Can Delete Purchase Invoice as Post",
            //    Category = "Purchase Invoice as Post",
            //    NobleModuleId = Guid.Parse("E328E825-8D13-4CCF-83BA-3ED0B2560E42"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("36BDC1CD-DE83-4443-871A-07782E048499"),
            //    Description = "Can View  Purchase Invoice as Post",
            //    Category = "Purchase Invoice as Post",
            //    NobleModuleId = Guid.Parse("E328E825-8D13-4CCF-83BA-3ED0B2560E42"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("E66D99A3-92D3-4E85-A435-D427E024E057"),
            //    Description = "Can Void Purchase Invoice",
            //    Category = "Purchase Invoice as Post",
            //    NobleModuleId = Guid.Parse("E328E825-8D13-4CCF-83BA-3ED0B2560E42"),
            //});
            //#endregion

            //#region For Purchase Return

            //modelBuilder.Entity<NobleModule>().HasData(new NobleModule
            //{
            //    Id = Guid.Parse("FE448D69-E152-4D03-A68D-7E7D21420A74"),
            //    ModuleName = "Purchase Return",
            //    Description = "Purchase Return",

            //});

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("EE288FCE-C3B4-45C6-9A98-68068EC3F5DB"),
            //    Description = "Can Save Purchase Return",
            //    Category = "Purchase Return",
            //    NobleModuleId = Guid.Parse("FE448D69-E152-4D03-A68D-7E7D21420A74"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("2D116856-8144-43A8-8297-633C6E1FA69A"),
            //    Description = "Can Edit Purchase Return",
            //    Category = "Purchase Return",
            //    NobleModuleId = Guid.Parse("FE448D69-E152-4D03-A68D-7E7D21420A74"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("66B335A9-85CB-45F8-8A40-8DE42A214D48"),
            //    Description = "Can Delete Purchase Return",
            //    Category = "Purchase Return",
            //    NobleModuleId = Guid.Parse("FE448D69-E152-4D03-A68D-7E7D21420A74"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("F8DCBDF3-FE8B-4251-9672-9DFD2535E6E8"),
            //    Description = "Can View  Purchase Return",
            //    Category = "Purchase Return",
            //    NobleModuleId = Guid.Parse("FE448D69-E152-4D03-A68D-7E7D21420A74"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("655F9534-7685-40D1-A7FA-BD4A817F7317"),
            //    Description = "Can Void  Purchase Return",
            //    Category = "Purchase Return",
            //    NobleModuleId = Guid.Parse("FE448D69-E152-4D03-A68D-7E7D21420A74"),
            //});
            //#endregion
            //#region For Stock In as Draft
            //modelBuilder.Entity<NobleModule>().HasData(new NobleModule
            //{
            //    Id = Guid.Parse("5930455B-F54D-4C62-AAE4-510CF844E3C9"),
            //    ModuleName = "Stock In",
            //    Description = "Stock In",

            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("AF665BA5-4C02-45AA-AB00-CF533C037DF2"),
            //    Description = "Can Save Stock In as Draft",
            //    Category = "Stock In",
            //    NobleModuleId = Guid.Parse("5930455B-F54D-4C62-AAE4-510CF844E3C9"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("9593897E-63B0-4D35-BD8C-D5F6AD49ED73"),
            //    Description = "Can Edit Stock In as Draft",
            //    Category = "Stock In",
            //    NobleModuleId = Guid.Parse("5930455B-F54D-4C62-AAE4-510CF844E3C9"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("11B73791-0E85-4EDC-8089-260D79579A8D"),
            //    Description = "Can Delete Stock In as Draft",
            //    Category = "Stock In",
            //    NobleModuleId = Guid.Parse("5930455B-F54D-4C62-AAE4-510CF844E3C9"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("508D3CB2-A1F7-4940-91EA-55FA3CA60567"),
            //    Description = "Can View  Stock In as Draft",
            //    Category = "Stock In",
            //    NobleModuleId = Guid.Parse("5930455B-F54D-4C62-AAE4-510CF844E3C9"),
            //});
            //#endregion
            //#region For Stock In as Post

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("08339BAA-2B00-4508-9AFA-0176C095B655"),
            //    Description = "Can Save Stock In as Post",
            //    Category = "Stock In",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("E999CBEA-CB6C-492F-BBD2-085B94123526"),
            //    Description = "Can Edit Stock In as Post",
            //    Category = "Stock In",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("4A8EA498-5B32-467C-8598-0B56F4895856"),
            //    Description = "Can Delete Stock In as Post",
            //    Category = "Stock In",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("73DA0510-B625-488D-B5E1-8C51BE6852E1"),
            //    Description = "Can View  Stock In as Post",
            //    Category = "Stock In",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("1B106F55-3E86-4C18-B146-285C9207BD88"),
            //    Description = "Can import Stock In Product",
            //    Category = "Stock In",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("21505CB9-7791-4583-8CF4-E574F051BC4B"),
            //    Description = "Can Void Stock In ",
            //    Category = "Stock In",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //#endregion


            //#region For Stock Out as Draft
            //modelBuilder.Entity<NobleModule>().HasData(new NobleModule
            //{
            //    Id = Guid.Parse("A1C6AE96-A6B5-4A2B-B803-E688A7ECAA2C"),
            //    ModuleName = "Stock Out",
            //    Description = "Stock Out",

            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("ABB53FBB-7C75-4C1C-B5E5-B9B148C81218"),
            //    Description = "Can Save Stock Out as Draft",
            //    Category = "Stock Out",
            //    NobleModuleId = Guid.Parse("A1C6AE96-A6B5-4A2B-B803-E688A7ECAA2C"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("8DF1D3D9-358B-4062-9CD6-EFF5EFC7C5D3"),
            //    Description = "Can Edit Stock Out as Draft",
            //    Category = "Stock Out",
            //    NobleModuleId = Guid.Parse("A1C6AE96-A6B5-4A2B-B803-E688A7ECAA2C"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("091991AA-37CE-467A-9EF4-E9358A201D2A"),
            //    Description = "Can Delete Stock Out as Draft",
            //    Category = "Stock Out",
            //    NobleModuleId = Guid.Parse("A1C6AE96-A6B5-4A2B-B803-E688A7ECAA2C"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("C546EE15-506D-444C-AEFD-1C86231BC0C9"),
            //    Description = "Can View  Stock Out as Draft",
            //    Category = "Stock Out",
            //    NobleModuleId = Guid.Parse("A1C6AE96-A6B5-4A2B-B803-E688A7ECAA2C"),
            //});
            //#endregion
            //#region For Stock Out as Post

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("2164EC38-DE68-4B78-A5CD-5CDAE140E09F"),
            //    Description = "Can Save Stock Out as Post",
            //    Category = "Stock Out",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("02D93298-0F97-4D8B-82F0-3DA7359018AE"),
            //    Description = "Can Edit Stock Out as Post",
            //    Category = "Stock Out",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("6E575B38-55D2-4925-AAFB-5654ACF48116"),
            //    Description = "Can Delete Stock Out as Post",
            //    Category = "Stock Out",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("2646A8C9-2002-403A-BED1-26B6A90A381A"),
            //    Description = "Can View  Stock Out as Post",
            //    Category = "Stock Out",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("9B7912BB-390F-4993-A35E-FDFD8AF9903F"),
            //    Description = "Can Void Stock Out ",
            //    Category = "Stock Out",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //#endregion


            //#region For Stock Return
            //modelBuilder.Entity<NobleModule>().HasData(new NobleModule
            //{
            //    Id = Guid.Parse("DAA49EF1-B2B3-4098-9A31-B5C0E501B1D0"),
            //    ModuleName = "Stock Return",
            //    Description = "Stock Return",

            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("F10732D2-0F61-4676-B86C-3530BE9EFF17"),
            //    Description = "Can Save Stock Return",
            //    Category = "Stock Return",
            //    NobleModuleId = Guid.Parse("DAA49EF1-B2B3-4098-9A31-B5C0E501B1D0"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("E303975E-5FB0-49DD-89DD-6550D9F796E4"),
            //    Description = "Can Edit Stock Return",
            //    Category = "Stock Return",
            //    NobleModuleId = Guid.Parse("DAA49EF1-B2B3-4098-9A31-B5C0E501B1D0"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("855377C8-A74F-4542-ACD0-8AA52AB7E7F5"),
            //    Description = "Can Delete Stock Return",
            //    Category = "Stock Return",
            //    NobleModuleId = Guid.Parse("DAA49EF1-B2B3-4098-9A31-B5C0E501B1D0"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("A9008A7A-5C19-4CCD-97C7-42F9937529E1"),
            //    Description = "Can View  Stock Return",
            //    Category = "Stock Return",
            //    NobleModuleId = Guid.Parse("DAA49EF1-B2B3-4098-9A31-B5C0E501B1D0"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("B70F468B-B540-4638-B05B-513EA6C31414"),
            //    Description = "Can Void Stock Return",
            //    Category = "Stock Return",
            //    NobleModuleId = Guid.Parse("DAA49EF1-B2B3-4098-9A31-B5C0E501B1D0"),
            //});
            //#endregion

            //#region For Stock Transfer as Draft
            //modelBuilder.Entity<NobleModule>().HasData(new NobleModule
            //{
            //    Id = Guid.Parse("59D41CDA-8B68-46D5-B46F-AA0AE6592FAB"),
            //    ModuleName = "Stock Transfer",
            //    Description = "Stock Transfer",

            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("CB9D0F1E-4B2E-4CDB-8D66-8DCB05FAB5B9"),
            //    Description = "Can Save Stock Transfer as Draft",
            //    Category = "Stock Transfer",
            //    NobleModuleId = Guid.Parse("59D41CDA-8B68-46D5-B46F-AA0AE6592FAB"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("E1B98CFA-D450-4C36-818D-296E16082A3A"),
            //    Description = "Can Edit Stock Transfer as Draft",
            //    Category = "Stock Transfer",
            //    NobleModuleId = Guid.Parse("59D41CDA-8B68-46D5-B46F-AA0AE6592FAB"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("C9849A14-1B2C-4993-B50E-A6BC64F69095"),
            //    Description = "Can Delete Stock Transfer as Draft",
            //    Category = "Stock Transfer",
            //    NobleModuleId = Guid.Parse("59D41CDA-8B68-46D5-B46F-AA0AE6592FAB"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("379354E8-36AB-47D7-B023-4CEA93A133EB"),
            //    Description = "Can View  Stock Transfer as Draft",
            //    Category = "Stock Transfer",
            //    NobleModuleId = Guid.Parse("59D41CDA-8B68-46D5-B46F-AA0AE6592FAB"),
            //});
            //#endregion

            //#region For Stock Transfer as Post

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("FCF03061-8BC8-4B53-9866-74844F730DB5"),
            //    Description = "Can Save Stock Transfer as Post",
            //    Category = "Stock Transfer",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("7EE087C4-981F-42E2-AF43-F52761D96F83"),
            //    Description = "Can Edit Stock Transfer as Post",
            //    Category = "Stock Transfer",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("52779803-F0E9-4DA1-B228-8AFD3EA8D88A"),
            //    Description = "Can Delete Stock Transfer as Post",
            //    Category = "Stock Transfer",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("8E82686E-C68D-4D91-8681-C8EECD2A6DE3"),
            //    Description = "Can View  Stock Transfer as Post",
            //    Category = "Stock Transfer",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("7E45912D-F341-4466-B00E-7AE1EBA45593"),
            //    Description = "Can Void  Stock Transfer",
            //    Category = "Stock Transfer",
            //    NobleModuleId = Guid.Parse("7600596A-7CAD-4514-91F3-21FE30628593"),
            //});
            //#endregion



            //#endregion

            //#region HR

            //modelBuilder.Entity<NobleModule>().HasData(new NobleModule
            //{
            //    Id = Guid.Parse("EB578E43-58EF-4E59-B50C-9F4C9C5B129E"),
            //    ModuleName = "HR",
            //    Description = "HR",

            //});
            //#region For Employee

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("6A496FB2-7E61-4BF7-843C-9B92888FE4EC"),
            //    Description = "Can Save Employee",
            //    Category = "Employee",
            //    NobleModuleId = Guid.Parse("EB578E43-58EF-4E59-B50C-9F4C9C5B129E"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("E3609735-21CB-447C-ACEA-2F7B10053C72"),
            //    Description = "Can Edit Employee",
            //    Category = "Employee",
            //    NobleModuleId = Guid.Parse("EB578E43-58EF-4E59-B50C-9F4C9C5B129E")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("F443FBDB-86F0-4DCB-B411-BC709D2A0361"),
            //    Description = "Can Delete Employee",
            //    Category = "Employee",
            //    NobleModuleId = Guid.Parse("EB578E43-58EF-4E59-B50C-9F4C9C5B129E"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("6A096FB2-7E61-4BF7-843C-9B92888FE4EC"),
            //    Description = "Can View  Employee",
            //    Category = "Employee",
            //    NobleModuleId = Guid.Parse("EB578E43-58EF-4E59-B50C-9F4C9C5B129E"),
            //});
            //#endregion

            //#region For Signup User

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("29AAC720-971B-43EF-9F02-9C60CD4E168B"),
            //    Description = "Can Save Sign up User",
            //    Category = "Sign up User",
            //    NobleModuleId = Guid.Parse("EB578E43-58EF-4E59-B50C-9F4C9C5B129E"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("9D857030-EDE1-4A36-BBE6-3DDF252E3B86"),
            //    Description = "Can Edit Sign up User",
            //    Category = "Sign up User",
            //    NobleModuleId = Guid.Parse("EB578E43-58EF-4E59-B50C-9F4C9C5B129E"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("C7519C17-A863-4654-B5CE-3640600F9EB7"),
            //    Description = "Can Delete Signup User",
            //    Category = "Sign up User",
            //    NobleModuleId = Guid.Parse("EB578E43-58EF-4E59-B50C-9F4C9C5B129E"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("EE737EB1-7AEE-4806-9982-6E410928D1C8"),
            //    Description = "Can View  Sign up User",
            //    Category = "Sign up User",
            //    NobleModuleId = Guid.Parse("EB578E43-58EF-4E59-B50C-9F4C9C5B129E"),
            //});
            //#endregion
            //#endregion
            //#region Sale 
            //modelBuilder.Entity<NobleModule>().HasData(new NobleModule
            //{
            //    Id = Guid.Parse("FDD1A2EE-6792-4DB2-9A05-F9377175470C"),
            //    ModuleName = "Sale Invoice",
            //    Description = "Sale Invoice",

            //});



            //#region For Sale Invoice as Hold

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("EC2BB155-7D9E-4746-A7DA-EE8B9FAB59D0"),
            //    Description = "Can Save Sale Invoice as Hold",
            //    Category = "Sale Invoice",
            //    NobleModuleId = Guid.Parse("FDD1A2EE-6792-4DB2-9A05-F9377175470C")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("738DFF20-0E10-44AE-821E-A67DEABB765F"),
            //    Description = "Can Edit Sale Invoice as Hold",
            //    Category = "Sale Invoice",
            //    NobleModuleId = Guid.Parse("FDD1A2EE-6792-4DB2-9A05-F9377175470C")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("F862B14C-9CF5-4514-B86C-D3BF64EB6B25"),
            //    Description = "Can Delete Sale Invoice as Hold",
            //    Category = "Sale Invoice",
            //    NobleModuleId = Guid.Parse("FDD1A2EE-6792-4DB2-9A05-F9377175470C")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("C8A96A66-87D6-46FD-86F8-86377EBE5A9C"),
            //    Description = "Can View  Sale Invoice as Hold",
            //    Category = "Sale Invoice",
            //    NobleModuleId = Guid.Parse("FDD1A2EE-6792-4DB2-9A05-F9377175470C")
            //});
            //#endregion

            //#region For Sale Invoice as Paid

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("81CE93CC-9FBE-4A99-8449-A4D1FDB6A935"),
            //    Description = "Can Save Sale Invoice as Paid",
            //    Category = "Sale Invoice",
            //    NobleModuleId = Guid.Parse("FDD1A2EE-6792-4DB2-9A05-F9377175470C")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("604A7045-C03F-4BD7-B23D-8D21CD649864"),
            //    Description = "Can Edit Sale Invoice as Paid",
            //    Category = "Sale Invoice",
            //    NobleModuleId = Guid.Parse("FDD1A2EE-6792-4DB2-9A05-F9377175470C")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("924A6F86-FB2B-4AFB-B8AA-5632AC9EEDDE"),
            //    Description = "Can Delete Sale Invoice as Paid",
            //    Category = "Sale Invoice",
            //    NobleModuleId = Guid.Parse("FDD1A2EE-6792-4DB2-9A05-F9377175470C")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("D5758EE0-699A-410B-8D11-67667E284B8E"),
            //    Description = "Can View  Sale Invoice as Paid",
            //    Category = "Sale Invoice",
            //    NobleModuleId = Guid.Parse("FDD1A2EE-6792-4DB2-9A05-F9377175470C")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("DD8A4F4E-6B6C-49E3-9C98-A6A05616F531"),
            //    Description = "Can Void  Sale Invoice",
            //    Category = "Sale Invoice",
            //    NobleModuleId = Guid.Parse("FDD1A2EE-6792-4DB2-9A05-F9377175470C")
            //});
            //#endregion

            //#region For Mobile Order

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("D576E3F9-B5D6-43A6-9799-171D8D8D470A"),
            //    Description = "Can Save  Mobile Order",
            //    Category = "Mobile Order",
            //    NobleModuleId = Guid.Parse("FDD1A2EE-6792-4DB2-9A05-F9377175470C")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("28EA4238-E4D0-4F6F-8C85-B5E51083B423"),
            //    Description = "Can Edit  Mobile Order",
            //    Category = "Mobile Order",
            //    NobleModuleId = Guid.Parse("FDD1A2EE-6792-4DB2-9A05-F9377175470C")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("9045F6DE-FAC6-44F0-B90F-A6D61361CA66"),
            //    Description = "Can Delete  Mobile Order",
            //    Category = "Mobile Order",
            //    NobleModuleId = Guid.Parse("FDD1A2EE-6792-4DB2-9A05-F9377175470C")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("C4F75F26-32B3-4BD8-8F20-328BE7D82197"),
            //    Description = "Can View   Mobile Order",
            //    Category = "Mobile Order",
            //    NobleModuleId = Guid.Parse("FDD1A2EE-6792-4DB2-9A05-F9377175470C")
            //});
            //#endregion
            //#region For Sale Return
            //modelBuilder.Entity<NobleModule>().HasData(new NobleModule
            //{
            //    Id = Guid.Parse("EB023060-25B5-4CF3-9850-B9A27DD82B8F"),
            //    ModuleName = "Sale Return",
            //    Description = "Sale Return",

            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("8821831A-F344-4455-8EE0-BE85389E20DC"),
            //    Description = "Can Save Sale Return",
            //    Category = "Sale Return",
            //    NobleModuleId = Guid.Parse("EB023060-25B5-4CF3-9850-B9A27DD82B8F"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("637A753D-5F9F-490F-8F26-CF2060972F2B"),
            //    Description = "Can Edit  Sale Return",
            //    Category = "Sale Return",
            //    NobleModuleId = Guid.Parse("EB023060-25B5-4CF3-9850-B9A27DD82B8F"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("5127C542-4BF8-4029-80C4-2B2149864152"),
            //    Description = "Can Delete Sale Return",
            //    Category = "Sale Return",
            //    NobleModuleId = Guid.Parse("EB023060-25B5-4CF3-9850-B9A27DD82B8F"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("161B2ED5-2702-4C34-BBF5-9F49DBDAFAB4"),
            //    Description = "Can View  Sale Return",
            //    Category = "Sale Return",
            //    NobleModuleId = Guid.Parse("EB023060-25B5-4CF3-9850-B9A27DD82B8F"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("9992EE2A-4686-473D-BA2F-E7A8CBCD1DCC"),
            //    Description = "Can Void  Sale Return",
            //    Category = "Sale Return",
            //    NobleModuleId = Guid.Parse("EB023060-25B5-4CF3-9850-B9A27DD82B8F"),
            //});
            //#endregion,
            //#region For Day Start

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("B721B192-A6C2-405D-B083-1D0AD699EA60"),
            //    Description = "Day Start",
            //    Category = "Day Start",
            //    NobleModuleId = Guid.Parse("FDD1A2EE-6792-4DB2-9A05-F9377175470C")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("70F3E79C-D703-48DD-8AA7-1565998C47BE"),
            //    Description = "Day Register",
            //    Category = "Day Start",
            //    NobleModuleId = Guid.Parse("FDD1A2EE-6792-4DB2-9A05-F9377175470C")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("952F4FC4-7C5F-4F25-89A5-F3BF3A5ABF12"),
            //    Description = "Day Close",
            //    Category = "Day Start",
            //    NobleModuleId = Guid.Parse("FDD1A2EE-6792-4DB2-9A05-F9377175470C")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("621A8253-B3D8-4943-82BB-27BE56AC7031"),
            //    Description = "Day Counter",
            //    Category = "Day Start",
            //    NobleModuleId = Guid.Parse("FDD1A2EE-6792-4DB2-9A05-F9377175470C")
            //});
            //#endregion
            //#endregion

            //#region Touch Invoice

            //modelBuilder.Entity<NobleModule>().HasData(new NobleModule
            //{
            //    Id = Guid.Parse("C82F8189-5419-4268-A5B8-8A527E62E3A8"),
            //    ModuleName = "Touch Invoice",
            //    Description = "Touch Invoice",

            //});

            //#region For Touch Invoice

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("7C1F24C2-7AE3-4D4E-B32E-F8A9D2109436"),
            //    Description = "Can Save Touch Invoice",
            //    Category = "Touch Invoice",
            //    NobleModuleId = Guid.Parse("C82F8189-5419-4268-A5B8-8A527E62E3A8"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("9AF9F141-28FB-4BC6-AEFF-1880841F4BC9"),
            //    Description = "Can Edit Touch Invoice",
            //    Category = "Touch Invoice",
            //    NobleModuleId = Guid.Parse("C82F8189-5419-4268-A5B8-8A527E62E3A8"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("98BC72AA-D005-4E27-88DB-657A778C99F7"),
            //    Description = "Can Delete Touch Invoice",
            //    Category = "Touch Invoice",
            //    NobleModuleId = Guid.Parse("C82F8189-5419-4268-A5B8-8A527E62E3A8"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("8631C097-F267-4B0D-9E3A-A32DA30402C5"),
            //    Description = "Can View  Touch Invoice",
            //    Category = "Touch Invoice",
            //    NobleModuleId = Guid.Parse("C82F8189-5419-4268-A5B8-8A527E62E3A8"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("DAEEB948-FC4F-44EE-9747-23EDC3918B9C"),
            //    Description = "Can Void  Touch Invoice",
            //    Category = "Touch Invoice",
            //    NobleModuleId = Guid.Parse("C82F8189-5419-4268-A5B8-8A527E62E3A8"),
            //});
            //#endregion

            //#endregion

            //#region Product

            //modelBuilder.Entity<NobleModule>().HasData(new NobleModule
            //{
            //    Id = Guid.Parse("CA3978F8-9AA3-49A3-95DA-7BB35BE89289"),
            //    ModuleName = "Product & Bundles",
            //    Description = "Product & Bundles",

            //});
            //#region For Product

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("32C5FBD7-642B-4F65-A875-919025AE6D18"),
            //    Description = "Can Save Product",
            //    Category = "Product",
            //    NobleModuleId = Guid.Parse("CA3978F8-9AA3-49A3-95DA-7BB35BE89289"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("5D78148C-057A-46B5-A9C3-63245C323C6C"),
            //    Description = "Can Edit Product",
            //    Category = "Product",
            //    NobleModuleId = Guid.Parse("CA3978F8-9AA3-49A3-95DA-7BB35BE89289"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("7F9EDEC7-09D8-4E25-8F61-B8C4876A9068"),
            //    Description = "Can Delete Product",
            //    Category = "Product",
            //    NobleModuleId = Guid.Parse("CA3978F8-9AA3-49A3-95DA-7BB35BE89289"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("D652D884-8B21-42D6-BF48-1C0337BE1475"),
            //    Description = "Can View  Product",
            //    Category = "Product",
            //    NobleModuleId = Guid.Parse("CA3978F8-9AA3-49A3-95DA-7BB35BE89289"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("16DF871A-5635-451D-A9BD-8756C3A2EC50"),
            //    Description = "Can Import Products",
            //    Category = "Product",
            //    NobleModuleId = Guid.Parse("CA3978F8-9AA3-49A3-95DA-7BB35BE89289"),
            //});

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("77392A41-FB96-409C-8852-0A27B76F3FB3"),
            //    Description = "Can View Inventory Count List",
            //    Category = "Product",
            //    NobleModuleId = Guid.Parse("CA3978F8-9AA3-49A3-95DA-7BB35BE89289"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("67D24ED6-9003-44EC-B2F4-787585AFD4E8"),
            //    Description = "Add Inventory Count",
            //    Category = "Product",
            //    NobleModuleId = Guid.Parse("CA3978F8-9AA3-49A3-95DA-7BB35BE89289"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("DA645710-8DAD-47F6-B602-B7BE96103033"),
            //    Description = "Can View Updated Inventory Count List",
            //    Category = "Product",
            //    NobleModuleId = Guid.Parse("CA3978F8-9AA3-49A3-95DA-7BB35BE89289"),
            //});

            //#endregion

            //#region For Bundles

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("E646C347-D3D1-41D5-88DB-2CD5F672B6A5"),
            //    Description = "Can Save Bundles",
            //    Category = "Bundles",
            //    NobleModuleId = Guid.Parse("CA3978F8-9AA3-49A3-95DA-7BB35BE89289"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("CC65A663-483F-4BD3-855F-65EA1D7C8F75"),
            //    Description = "Can Edit Bundles",
            //    Category = "Bundles",
            //    NobleModuleId = Guid.Parse("CA3978F8-9AA3-49A3-95DA-7BB35BE89289"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("07902830-3480-440E-BBF6-5DD1AA924949"),
            //    Description = "Can Delete Bundles",
            //    Category = "Bundles",
            //    NobleModuleId = Guid.Parse("CA3978F8-9AA3-49A3-95DA-7BB35BE89289"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("5BC6E4EF-A2B2-458B-BC62-93C0B7103579"),
            //    Description = "Can View  Bundles",
            //    Category = "Bundles",
            //    NobleModuleId = Guid.Parse("CA3978F8-9AA3-49A3-95DA-7BB35BE89289"),
            //});
            //#endregion
            //#region For Promotion

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("95C0FC68-3DC2-4E8F-9577-0E8DA47ED580"),
            //    Description = "Can Save Promotion",
            //    Category = "Promotion",
            //    NobleModuleId = Guid.Parse("CA3978F8-9AA3-49A3-95DA-7BB35BE89289"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("64A7FBA3-AADF-47F0-A8D8-DF53888A3863"),
            //    Description = "Can Edit Promotion",
            //    Category = "Promotion",
            //    NobleModuleId = Guid.Parse("CA3978F8-9AA3-49A3-95DA-7BB35BE89289"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("72D8BD35-702C-4EA2-81E1-10F72EF8F58C"),
            //    Description = "Can Delete Promotion",
            //    Category = "Promotion",
            //    NobleModuleId = Guid.Parse("CA3978F8-9AA3-49A3-95DA-7BB35BE89289"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("0F77F740-823F-4643-8DC7-30D6BB124144"),
            //    Description = "Can View  Promotion",
            //    Category = "Promotion",
            //    NobleModuleId = Guid.Parse("CA3978F8-9AA3-49A3-95DA-7BB35BE89289"),
            //});
            //#endregion
            //#region For Product Master
            //modelBuilder.Entity<NobleModule>().HasData(new NobleModule
            //{
            //    Id = Guid.Parse("E8217E77-786C-4E37-8FC1-D800A55D8FA9"),
            //    ModuleName = "Super Product",
            //    Description = "Super Product",

            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("5B5446BA-2274-4C23-BF1A-CEC5BD014557"),
            //    Description = "Can Save Super Product",
            //    Category = "Super Product",
            //    NobleModuleId = Guid.Parse("E8217E77-786C-4E37-8FC1-D800A55D8FA9")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("446231D8-667B-483F-8CEB-9B00B13B58AD"),
            //    Description = "Can Edit Super Product",
            //    Category = "Super Product",
            //    NobleModuleId = Guid.Parse("E8217E77-786C-4E37-8FC1-D800A55D8FA9")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("AF16373F-E5D3-429F-9780-8935D9961846"),
            //    Description = "Can Delete Super Product",
            //    Category = "Super Product",
            //    NobleModuleId = Guid.Parse("E8217E77-786C-4E37-8FC1-D800A55D8FA9")
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("4634CE35-DB20-44A9-A2A0-23B2BA6C80E2"),
            //    Description = "Can View  Super Product",
            //    Category = "Super Product",
            //    NobleModuleId = Guid.Parse("E8217E77-786C-4E37-8FC1-D800A55D8FA9")
            //});
            //#endregion





            //#endregion
            //#endregion

            //#region Contact

            //modelBuilder.Entity<NobleModule>().HasData(new NobleModule
            //{
            //    Id = Guid.Parse("80346A2F-7934-45F0-B669-060A3924D382"),
            //    ModuleName = "Contact",
            //    Description = "Contact",

            //});
            //#region For Customer

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("175F38FB-F20F-4DAF-80C8-FFC105469703"),
            //    Description = "Can Save Customer",
            //    Category = "Customer",
            //    NobleModuleId = Guid.Parse("80346A2F-7934-45F0-B669-060A3924D382"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("487EE180-F4F3-4846-94CC-DF404FDA8EF1"),
            //    Description = "Can Edit Customer",
            //    Category = "Customer",
            //    NobleModuleId = Guid.Parse("80346A2F-7934-45F0-B669-060A3924D382"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("B16AB14A-E25F-4807-A69B-07A2CF930F17"),
            //    Description = "Can Delete Customer",
            //    Category = "Customer",
            //    NobleModuleId = Guid.Parse("80346A2F-7934-45F0-B669-060A3924D382"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("8ADB14B7-3549-4E2E-8214-04D7E1348207"),
            //    Description = "Can View  Customer",
            //    Category = "Customer",
            //    NobleModuleId = Guid.Parse("80346A2F-7934-45F0-B669-060A3924D382"),
            //});
            //#endregion

            //#region For Supplier

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("C4121042-4D8C-4FC2-95D2-CFC4C4F8FA12"),
            //    Description = "Can Save Supplier",
            //    Category = "Supplier",
            //    NobleModuleId = Guid.Parse("80346A2F-7934-45F0-B669-060A3924D382"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("5215425A-5226-4B80-93BC-4C81E4555144"),
            //    Description = "Can Edit Supplier",
            //    Category = "Supplier",
            //    NobleModuleId = Guid.Parse("80346A2F-7934-45F0-B669-060A3924D382"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("00B0C128-0698-4EC2-AB02-FCC7150F614D"),
            //    Description = "Can Delete Supplier",
            //    Category = "Supplier",
            //    NobleModuleId = Guid.Parse("80346A2F-7934-45F0-B669-060A3924D382"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("9FE5B3D0-1D1F-4D3D-BB0A-968188E70345"),
            //    Description = "Can View  Supplier",
            //    Category = "Supplier",
            //    NobleModuleId = Guid.Parse("80346A2F-7934-45F0-B669-060A3924D382"),
            //});
            //#endregion
            //#endregion


            //#region Expense 
            //modelBuilder.Entity<NobleModule>().HasData(new NobleModule
            //{
            //    Id = Guid.Parse("CFCF2315-0697-401C-A5BB-38C8AACED1E1"),
            //    ModuleName = "Expense",
            //    Description = "Expense",

            //});



            //#region For Expense for Draft

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("22122094-19D8-42C1-9BA3-D67087ED5D88"),
            //    Description = "Can Save Expense as Draft",
            //    Category = "Expense",
            //    NobleModuleId = Guid.Parse("CFCF2315-0697-401C-A5BB-38C8AACED1E1"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("216A8F48-BE1E-44F7-85D2-7AC90BF17B12"),
            //    Description = "Can Edit Expense as Draft",
            //    Category = "Expense",
            //    NobleModuleId = Guid.Parse("CFCF2315-0697-401C-A5BB-38C8AACED1E1"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("89491549-353B-4FED-B397-77770F3D713B"),
            //    Description = "Can Delete Expense as Draft",
            //    Category = "Expense",
            //    NobleModuleId = Guid.Parse("CFCF2315-0697-401C-A5BB-38C8AACED1E1"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("68A0B70C-02AE-4D29-89EE-0A550A805259"),
            //    Description = "Can View  Expense as Draft",
            //    Category = "Expense",
            //    NobleModuleId = Guid.Parse("CFCF2315-0697-401C-A5BB-38C8AACED1E1"),
            //});
            //#endregion
            //#region For Expense for Post

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("3A87487D-2950-4366-B3B4-3FCAFC47651E"),
            //    Description = "Can Save Expense as Post",
            //    Category = "Expense",
            //    NobleModuleId = Guid.Parse("CFCF2315-0697-401C-A5BB-38C8AACED1E1"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("264F10A3-7006-428D-938B-716101E3B113"),
            //    Description = "Can Edit Expense as Post",
            //    Category = "Expense",
            //    NobleModuleId = Guid.Parse("CFCF2315-0697-401C-A5BB-38C8AACED1E1"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("04CDCDDD-475C-4E66-8390-A7E15E133B3F"),
            //    Description = "Can Delete Expense as Post",
            //    Category = "Expense",
            //    NobleModuleId = Guid.Parse("CFCF2315-0697-401C-A5BB-38C8AACED1E1"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("2F314227-8B9F-44FC-985D-4A9ECA30E1DB"),
            //    Description = "Can View  Expense as Post",
            //    Category = "Expense",
            //    NobleModuleId = Guid.Parse("CFCF2315-0697-401C-A5BB-38C8AACED1E1"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("21DE1004-5A7B-4D6C-B21A-8775D023EF88"),
            //    Description = "Can Void Daily Expense",
            //    Category = "Expense",
            //    NobleModuleId = Guid.Parse("CFCF2315-0697-401C-A5BB-38C8AACED1E1"),
            //});
            //#endregion
            //#region For Expense for Rejected

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("633DE319-F00D-4AC4-BBC7-A77EB44FA672"),
            //    Description = "Can Save Expense as Reject",
            //    Category = "Expense",
            //    NobleModuleId = Guid.Parse("CFCF2315-0697-401C-A5BB-38C8AACED1E1"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("725374F9-867E-40BB-A8A2-EB3661C2223D"),
            //    Description = "Can Edit Expense as Reject",
            //    Category = "Expense",
            //    NobleModuleId = Guid.Parse("CFCF2315-0697-401C-A5BB-38C8AACED1E1"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("73FDEA15-53DF-46C8-99F8-FE911A2765A7"),
            //    Description = "Can Delete Expense as Reject",
            //    Category = "Expense",
            //    NobleModuleId = Guid.Parse("CFCF2315-0697-401C-A5BB-38C8AACED1E1"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("1CEEFEEB-9B95-4CAA-97F4-0D78E5C718EA"),
            //    Description = "Can View  Expense as Reject",
            //    Category = "Expense",
            //    NobleModuleId = Guid.Parse("CFCF2315-0697-401C-A5BB-38C8AACED1E1"),
            //});
            //#endregion
            //#region For Bulk Expense  Rejected

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("A973D44C-684D-4473-9B48-81E8CCF3CBC3"),
            //    Description = "Can Bulk Expense Rejected",
            //    Category = "Expense",
            //    NobleModuleId = Guid.Parse("CFCF2315-0697-401C-A5BB-38C8AACED1E1"),
            //});

            //#endregion
            //#region For Bulk Expense  Approved

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("1A1F8A7B-C256-435F-90EE-9FF5E2ED03DA"),
            //    Description = "Can Bulk Expense Approved",
            //    Category = "Expense",
            //    NobleModuleId = Guid.Parse("CFCF2315-0697-401C-A5BB-38C8AACED1E1"),
            //});
            //#endregion
            //#endregion

            //#region Report 
            //modelBuilder.Entity<NobleModule>().HasData(new NobleModule
            //{
            //    Id = Guid.Parse("CAC77350-BEFB-4063-969F-461F188D063E"),
            //    ModuleName = "Report",
            //    Description = "Report",

            //});
            //#region For Inventory Report

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("04B567B5-B5DB-47E1-9B48-3D10F14A36A2"),
            //    Description = "Can View Report",
            //    Category = "Inventory Report",
            //    NobleModuleId = Guid.Parse("CAC77350-BEFB-4063-969F-461F188D063E"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("1CCCA4AD-C148-45A6-BFDB-6E875F3C70F4"),
            //    Description = "Can Print Report",
            //    Category = "Inventory Report",
            //    NobleModuleId = Guid.Parse("CAC77350-BEFB-4063-969F-461F188D063E"),
            //});

            //#endregion
            //#region For Stock Value Report

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("8CC04D98-F1C4-4FB3-A3DA-735AE2E0B8A3"),
            //    Description = "Can View Stock Value Report",
            //    Category = "Stock Value Report",
            //    NobleModuleId = Guid.Parse("CAC77350-BEFB-4063-969F-461F188D063E"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("51DCA813-BB8B-473A-AEC8-47E32493FE01"),
            //    Description = "Can Stock Value Report",
            //    Category = "Stock Value Report",
            //    NobleModuleId = Guid.Parse("CAC77350-BEFB-4063-969F-461F188D063E"),
            //});

            //#endregion
            //#region For Stock Average Value Report

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("1B8C15D1-C29B-48E5-A117-D961A53AEFA5"),
            //    Description = "Can View Stock Average  Value Report",
            //    Category = "Stock Averag Value Report",
            //    NobleModuleId = Guid.Parse("CAC77350-BEFB-4063-969F-461F188D063E"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("FCEE2E1F-3BDF-40AC-8983-07A9DA81255C"),
            //    Description = "Can Print Stock Average Value Report",
            //    Category = "Stock Averag Value Report",
            //    NobleModuleId = Guid.Parse("CAC77350-BEFB-4063-969F-461F188D063E"),
            //});

            //#endregion
            //#region For Transaction Type Stock Value Report

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("030FD7A5-8BEC-4CCE-A4BF-E3BBD49FF3B7"),
            //    Description = "Can View Transaction Type Stock Value Report",
            //    Category = "Transaction Type Stock Value Report",
            //    NobleModuleId = Guid.Parse("CAC77350-BEFB-4063-969F-461F188D063E"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("3C3FEBD7-54DF-4A2A-895B-731E8FE72128"),
            //    Description = "Can Print Transaction Type Stock Value Report",
            //    Category = "Transaction Type Stock Value Report",
            //    NobleModuleId = Guid.Parse("CAC77350-BEFB-4063-969F-461F188D063E"),
            //});

            //#endregion

            //#region For Customer Wise Product Sale Report

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("7447AB3C-DFBA-4644-9B3F-80E8B031C5A7"),
            //    Description = "Can View Customer Wise Product Sale Report",
            //    Category = "Customer Wise Product Sale",
            //    NobleModuleId = Guid.Parse("CAC77350-BEFB-4063-969F-461F188D063E"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("4047F716-FC4E-498A-950A-04F3B4038880"),
            //    Description = "Can Print Customer Wise Product Sale Report",
            //    Category = "Customer Wise Product Sale",
            //    NobleModuleId = Guid.Parse("CAC77350-BEFB-4063-969F-461F188D063E"),
            //});

            //#endregion

            //#region For Supplier Wise Product Purchase Report

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("A5CC534A-12FA-4A90-827C-0600FE148B3F"),
            //    Description = "Can View Supplier Wise Product Purchase Report",
            //    Category = "Supplier Wise Product Purchase",
            //    NobleModuleId = Guid.Parse("CAC77350-BEFB-4063-969F-461F188D063E"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("EA8B0004-0327-4A06-A01F-7E0BC278CCC0"),
            //    Description = "Can Print Supplier Wise Product Purchase Report",
            //    Category = "Supplier Wise Product Purchase",
            //    NobleModuleId = Guid.Parse("CAC77350-BEFB-4063-969F-461F188D063E"),
            //});

            //#endregion


            //#region For Customer Discount Product Report

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("306F33C5-42DD-48DA-B6AF-922182D8596A"),
            //    Description = "Can View Customer Discount Product Report",
            //    Category = "Customer Discount Product",
            //    NobleModuleId = Guid.Parse("CAC77350-BEFB-4063-969F-461F188D063E"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("FF863483-15DE-44ED-97D4-F1BA2D9161C3"),
            //    Description = "Can Print Customer Discount Product Report",
            //    Category = "Customer Discount Product",
            //    NobleModuleId = Guid.Parse("CAC77350-BEFB-4063-969F-461F188D063E"),
            //});

            //#endregion
            //#region For Supplier Discount Product Report

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("0C261749-97A6-41D9-9705-A9106DFB76D3"),
            //    Description = "Can View Supplier Discount Product Report",
            //    Category = "Supplier Discount Product",
            //    NobleModuleId = Guid.Parse("CAC77350-BEFB-4063-969F-461F188D063E"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("036C6C73-FA06-4995-AA2A-770DB17157D3"),
            //    Description = "Can Print Supplier Discount Product Report",
            //    Category = "Supplier Discount Product",
            //    NobleModuleId = Guid.Parse("CAC77350-BEFB-4063-969F-461F188D063E"),
            //});

            //#endregion

            //#region For   Product Discount Customer Report

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("71DC039F-5B2E-43AF-9BD3-592632D3FFE8"),
            //    Description = "Can View Product Discount Customer Report",
            //    Category = "Product Discount Customer",
            //    NobleModuleId = Guid.Parse("CAC77350-BEFB-4063-969F-461F188D063E"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("56E15D9C-3DB6-46A0-A830-6BA8078A54CA"),
            //    Description = "Can Print Product Discount Customer Report",
            //    Category = "Product Discount Customer",
            //    NobleModuleId = Guid.Parse("CAC77350-BEFB-4063-969F-461F188D063E"),
            //});

            //#endregion
            //#region For   Product Discount Supplier Report

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("8DF9D4F8-9ACE-4B10-B96F-758442EAA164"),
            //    Description = "Can View Product Discount Supplier Report",
            //    Category = "Product Discount Supplier",
            //    NobleModuleId = Guid.Parse("CAC77350-BEFB-4063-969F-461F188D063E"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("006272B6-1197-422C-BFEC-95367D7FBE04"),
            //    Description = "Can Print Product Discount Supplier Report",
            //    Category = "Product Discount Supplier",
            //    NobleModuleId = Guid.Parse("CAC77350-BEFB-4063-969F-461F188D063E"),
            //});

            //#endregion
            //#region Free of Cost Sale

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("6687BE37-B66A-421B-8E98-61D2B6DD630C"),
            //    Description = "Can View Free of Cost Sale Report",
            //    Category = "Free of Cost Sale",
            //    NobleModuleId = Guid.Parse("CAC77350-BEFB-4063-969F-461F188D063E"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("42D8BA7D-8A08-4897-A17C-ED83FA815A59"),
            //    Description = "Can Print Free of Cost Sale Report",
            //    Category = "Free of Cost Sale",
            //    NobleModuleId = Guid.Parse("CAC77350-BEFB-4063-969F-461F188D063E"),
            //});

            //#endregion
            //#region Account Ledger Report

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("62B7BB8E-DE4C-4201-B3CC-C247C0E0F620"),
            //    Description = "Can View Account Ledger Report",
            //    Category = "Account Ledger",
            //    NobleModuleId = Guid.Parse("CAC77350-BEFB-4063-969F-461F188D063E"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("E1E28E4B-7C6F-4106-AD84-43637284A975"),
            //    Description = "Can Print Account Ledger Report",
            //    Category = "Account Ledger",
            //    NobleModuleId = Guid.Parse("CAC77350-BEFB-4063-969F-461F188D063E"),
            //});

            //#endregion
            //#endregion

            //#region Production 
            //modelBuilder.Entity<NobleModule>().HasData(new NobleModule
            //{
            //    Id = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //    ModuleName = "Production",
            //    Description = "Production",

            //});



            //#region For Recipe as Draft

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("277ED0CF-C965-4EBB-BDE3-2B8EA94F4735"),
            //    Description = "Can Save Recipe as Draft",
            //    Category = "Recipe",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("FBBB462C-5501-4CE9-85D2-5EAA3C6F256A"),
            //    Description = "Can Edit Recipe as Draft",
            //    Category = "Recipe",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("91EA3770-C0DA-49D3-BE8D-8E0CD4AB24FD"),
            //    Description = "Can Delete Recipe as Draft",
            //    Category = "Recipe",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("528C1062-D45B-4524-BCBF-05F3CD24AB31"),
            //    Description = "Can View  Recipe as Draft",
            //    Category = "Recipe",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //#endregion
            //#region For Recipe as Post

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("17997ED1-7510-41E0-B84E-0338F2439B0E"),
            //    Description = "Can Save Recipe as Post",
            //    Category = "Recipe",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("0E6E2786-7EB9-4E45-9B5D-AFC67DEF4980"),
            //    Description = "Can Edit Recipe as Post",
            //    Category = "Recipe",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("8909BB3C-4784-456C-B306-58FA20FC49E9"),
            //    Description = "Can Delete Recipe as Post",
            //    Category = "Recipe",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("D8E6BC84-774D-462A-B5D2-18DE27F90C8C"),
            //    Description = "Can View  Recipe as Post",
            //    Category = "Recipe",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //#endregion

            //#region For Production Batch as Draft

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("53140B33-89E1-4FC2-90CE-B2B2E577F66F"),
            //    Description = "Can Save Production Batch as Draft",
            //    Category = "Production Batch",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("DE8F915C-843E-4548-8C8F-2D7DC3CD573E"),
            //    Description = "Can Edit Production Batch as Draft",
            //    Category = "Production Batch",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("148F1F0D-AB0F-49D3-9D02-FEC1D9BD10BF"),
            //    Description = "Can Delete Production Batch as Draft",
            //    Category = "Production Batch",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("5D045860-B3A1-42FF-8DD3-82661FD1D939"),
            //    Description = "Can View Production Batch as Draft",
            //    Category = "Production Batch",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //#endregion

            //#region For Production Batch as Post

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("BAA059E1-32A6-4843-A1E6-5EF6F14A93E2"),
            //    Description = "Can Save Production Batch as Post",
            //    Category = "Production Batch",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("D7935A0C-7985-439C-B75F-52A1B9FC5D2E"),
            //    Description = "Can Edit Production Batch as Post",
            //    Category = "Production Batch",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("2A7710C0-EADA-406F-9548-671E3E9BEC80"),
            //    Description = "Can Delete Production Batch as Post",
            //    Category = "Production Batch",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("DDD70AE5-83AD-4F25-884D-51989DDBEAAD"),
            //    Description = "Can View Production Batch as Post",
            //    Category = "Production Batch",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //#endregion

            //#region For Despatch Note as Draft

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("D1D2FC80-55D4-408E-9E1E-7F59369F6B7E"),
            //    Description = "Can Save Despatch Note as Draft",
            //    Category = "Despatch Note",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("1CFACDF4-4D05-43FB-85BB-1570F7DFC42B"),
            //    Description = "Can Edit Despatch Note as Draft",
            //    Category = "Despatch Note",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("C3C79CF2-9624-4AD4-8BCE-2A3C514ABB8A"),
            //    Description = "Can Delete Despatch Note as Draft",
            //    Category = "Despatch Note",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("E8B25346-FD48-4249-BF38-7450F0B71EB4"),
            //    Description = "Can View Despatch Note as Draft",
            //    Category = "Despatch Note",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //#endregion

            //#region For Despatch Note as Post

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("6D7F02BD-058F-4F2C-8E02-6AACBAAF66DC"),
            //    Description = "Can Save Despatch Note as Post",
            //    Category = "Despatch Note",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("1EC28E57-E9CC-48F6-AB2B-D4EA025BE4FE"),
            //    Description = "Can Edit Despatch Note as Post",
            //    Category = "Despatch Note",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("D42760B7-96C0-476D-BB9D-D5587EE7466C"),
            //    Description = "Can Delete Despatch Note as Post",
            //    Category = "Despatch Note",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("B93E7E1B-9041-43A5-974D-7B980DFDB750"),
            //    Description = "Can View Despatch Note as Post",
            //    Category = "Despatch Note",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //#endregion

            //#region For Production Stock as Draft

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("90A74990-2CC1-4224-B90A-1BE6D53191CE"),
            //    Description = "Can Save Production Stock as Draft",
            //    Category = "Production Stock",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("CDBC8E2C-70F3-46A7-9AAB-0666A6B49309"),
            //    Description = "Can Edit Production Stock as Draft",
            //    Category = "Production Stock",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("BEC3DD0C-FC1F-4743-AC34-C06D3770B7E0"),
            //    Description = "Can Delete Production Stock as Draft",
            //    Category = "Production Stock",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("5C060004-7C7A-4539-B097-C3A8CEAD671C"),
            //    Description = "Can View Production Stock as Draft",
            //    Category = "Production Stock",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //#endregion

            //#region For Production Stock as Post

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("57069004-7C7A-4539-B097-C3A8CEAD671C"),
            //    Description = "Can Save Production Stock as Post",
            //    Category = "Production Stock",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("E272BCA6-7A9F-44CD-943A-27D55F4EF932"),
            //    Description = "Can Edit Production Stock as Post",
            //    Category = "Production Stock",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("0BE0D795-2652-4884-B939-58D154043BA1"),
            //    Description = "Can Delete Production Stock as Post",
            //    Category = "Production Stock",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("08CA50EE-442E-4E68-8065-850CCCF751ED"),
            //    Description = "Can View Production Stock as Post",
            //    Category = "Production Stock",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //#endregion

            //#region For Sale Order as Draft

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("1F9797F1-4639-443F-B687-6B64A4A83C05"),
            //    Description = "Can Save Sale Order as Draft",
            //    Category = "Sale Order",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("6A02310E-BE2D-4DD1-9EB9-9887A8EFE5E5"),
            //    Description = "Can Edit Sale Order as Draft",
            //    Category = "Sale Order",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("C1E20C47-08D2-4628-B283-8BAC062E956E"),
            //    Description = "Can Delete Sale Order as Draft",
            //    Category = "Sale Order",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("5F0CA00F-D9F3-4620-8E1B-D89A563AF1FA"),
            //    Description = "Can View Sale Order as Draft",
            //    Category = "Sale Order",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //#endregion
            //#region For Sale Order as Post

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("1F844C71-C41E-4068-8731-E86F76873FCE"),
            //    Description = "Can Save Sale Order as Post",
            //    Category = "Sale Order",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("B77E401C-80E9-4302-A9E5-1D6FCDB69815"),
            //    Description = "Can Edit Sale Order as Post",
            //    Category = "Sale Order",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("24E9D982-8FEA-4C96-823A-BAD0669D2634"),
            //    Description = "Can Delete Sale Order as Post",
            //    Category = "Sale Order",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("326661B9-3293-478B-A5A2-1C646C43D1AD"),
            //    Description = "Can View Sale Order as Post",
            //    Category = "Sale Order",
            //    NobleModuleId = Guid.Parse("B04179A6-DEAF-4B50-B3B8-7D5CB3BD0296"),
            //});
            //#endregion
            //#endregion

            //#region Systems 
            //modelBuilder.Entity<NobleModule>().HasData(new NobleModule
            //{
            //    Id = Guid.Parse("0A00C448-C2BA-4EE4-972D-9352BC6175B8"),
            //    ModuleName = "System",
            //    Description = "System",

            //});



            //#region For System

            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("482E0412-62DE-46E7-ADD5-2FB443596F11"),
            //    Description = "Can Change Company Profile",
            //    Category = "System",
            //    NobleModuleId = Guid.Parse("0A00C448-C2BA-4EE4-972D-9352BC6175B8"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("D04864B9-C51C-4F40-9D01-639F5BE114CA"),
            //    Description = "Database Backup",
            //    Category = "System",
            //    NobleModuleId = Guid.Parse("0A00C448-C2BA-4EE4-972D-9352BC6175B8"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("6EE0462D-4BF1-4074-8A42-6709E2375267"),
            //    Description = "Restore Database",
            //    Category = "System",
            //    NobleModuleId = Guid.Parse("0A00C448-C2BA-4EE4-972D-9352BC6175B8"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("69624997-4C9C-4A23-B178-93233C1028A0"),
            //    Description = "Flush Database",
            //    Category = "System",
            //    NobleModuleId = Guid.Parse("0A00C448-C2BA-4EE4-972D-9352BC6175B8"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("3F674DE2-D92A-4633-9D76-B48D6DFC4612"),
            //    Description = "Can Change Invoice Setting",
            //    Category = "System",
            //    NobleModuleId = Guid.Parse("0A00C448-C2BA-4EE4-972D-9352BC6175B8"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("85A3C9B8-7E86-4A26-8A7C-1910C186144B"),
            //    Description = "Pull Record of Database",
            //    Category = "System",
            //    NobleModuleId = Guid.Parse("0A00C448-C2BA-4EE4-972D-9352BC6175B8"),
            //});
            //modelBuilder.Entity<NoblePermission>().HasData(new NoblePermission
            //{
            //    Id = Guid.Parse("6C5E1875-1B3F-410C-825C-9349D07A85A5"),
            //    Description = "Push Record of Database",
            //    Category = "System",
            //    NobleModuleId = Guid.Parse("0A00C448-C2BA-4EE4-972D-9352BC6175B8"),
            //});
            //#endregion
            //#endregion

        }
    }
}

