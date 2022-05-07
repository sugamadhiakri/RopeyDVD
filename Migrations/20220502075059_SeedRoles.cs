using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RopeyDVD.Migrations
{
    public partial class SeedRoles : Migration
    {
        private string AssistantRoleid = Guid.NewGuid().ToString();
        private string AdminRoleid = Guid.NewGuid().ToString();

        private string adminId = Guid.NewGuid().ToString();
        private string assistantId = Guid.NewGuid().ToString();

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            SeedRolesSQL(migrationBuilder);
            SeedUsers(migrationBuilder);
            SeedUserRoles(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }

        private void SeedRolesSQL(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"INSERT INTO [dbo].[AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp])
            VALUES ('{AdminRoleid}', 'Administrator', 'ADMINISTRATOR', null);");

            migrationBuilder.Sql($@"INSERT INTO [dbo].[AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp])
            VALUES ('{AssistantRoleid}', 'Assistant', 'ASSISTANT', null);");

        }

        private void SeedUsers(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @$"INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], 
                [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], 
                [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) 
                VALUES 
                (N'{adminId}', N'superadmin@ropey.com', N'SUPERADMIN@ROPEY.COM', 
                N'superadmin@ropey.com', N'SUPERADMIN@ROPEY.COM', 0, 
                N'AQAAAAEAACcQAAAAEJaa45NdwGyku3XTBJlyA66wV7AicXArNKUjjTZf8Lqj+QtWK6F2IuvQQGMOGxOoPA==', 
                N'LXBM5QQQKHI3DDIHWL2WLRB3FZINCE7B', N'a2f2c52b-45a3-4b97-b9bb-95e71799dc46', NULL, 0, 0, NULL, 1, 0)");

            migrationBuilder.Sql(
                @$"INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], 
                [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], 
                [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) 
                VALUES 
                (N'{assistantId}', N'assistant1@ropey.com', N'ASSISTANT1@ROPEY.COM', 
                N'assistant1@ropey.com', N'ASSISTANT1@ROPEY.COM', 0, 
                N'AQAAAAEAACcQAAAAEH0/VCLci1s6cMSJojjgQ3XRxbvBiFa1FWFng4MrYbGZ6p+WGtdAdkXzpXQ5fhHSJg==', 
                N'KZFGJHCXYMBAFKYS52WAJTPOFH4LAII2', N'd4d028b6-db04-42dd-96f8-b4c9989e28ae', NULL, 0, 0, NULL, 1, 0)");
        }

        private void SeedUserRoles(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId])
                VALUES ('{adminId}', '{AdminRoleid}');
                
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId])
                VALUES ('{assistantId}', '{AssistantRoleid}');
                ");

        }
    }
}
