namespace Online_Food_Corner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [firstName], [address], [cellNumber]) VALUES (N'b43668b1-31dc-4cb2-b108-3e7eb23fce5c', N'usman@gmail.com', 0, N'AOiDpn4x9dohIXV1L2R52o+1pLW4gWAzJCS1vTyxrQgBMxLLgaWVr3noYCWIvG2AWw==', N'efc2b8f2-25d6-45f3-86a5-866001172997', NULL, 0, 0, NULL, 1, 0, N'admin', N'Usman Ali', N'Uet', N'03001234567')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [firstName], [address], [cellNumber]) VALUES (N'b7f95c53-375c-434d-a641-fc331034f31d', N'adeel@gmail.com', 0, N'ABxRdY5MDcsqH2cKVCg8wlS1fMkfD2D2dvl5ftSlIy1mmp8NyfLSecJyTjgIlBxcVg==', N'674ad459-7afd-42fb-a86e-550552e6ee14', NULL, 0, 0, NULL, 1, 0, N'user', N'Muhammad Adeel', N'Uet', N'03007918427')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'44100a48-fd4a-4012-b939-449401185b1f', N'CanManageOFC')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b43668b1-31dc-4cb2-b108-3e7eb23fce5c', N'44100a48-fd4a-4012-b939-449401185b1f')

");
        }
        
        public override void Down()
        {
        }
    }
}
