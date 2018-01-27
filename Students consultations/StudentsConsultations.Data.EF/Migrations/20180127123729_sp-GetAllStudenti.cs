using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StudentsConsultations.Data.EF.Migrations
{
    public partial class spGetAllStudenti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[GetAllStudenti]
                AS
                BEGIN
                    select * from [dbo].[Student]
                END";

            migrationBuilder.Sql(sp);
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }

    }
}
