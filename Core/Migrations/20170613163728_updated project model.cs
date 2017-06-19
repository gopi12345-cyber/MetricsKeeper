using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class updatedprojectmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PortfolioId",
                table: "Project",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Project_PortfolioId",
                table: "Project",
                column: "PortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Portfolio_PortfolioId",
                table: "Project",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Portfolio_PortfolioId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_PortfolioId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "PortfolioId",
                table: "Project");
        }
    }
}
