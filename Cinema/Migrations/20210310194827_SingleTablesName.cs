using Microsoft.EntityFrameworkCore.Migrations;

namespace Cinema.Migrations
{
    public partial class SingleTablesName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Seats_SeatId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Sessions_SessionId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Halls_HallId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Movies_MovieId",
                table: "Sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seats",
                table: "Seats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movies",
                table: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Halls",
                table: "Halls");

            migrationBuilder.RenameTable(
                name: "Sessions",
                newName: "Session");

            migrationBuilder.RenameTable(
                name: "Seats",
                newName: "Seat");

            migrationBuilder.RenameTable(
                name: "Reservations",
                newName: "Reservation");

            migrationBuilder.RenameTable(
                name: "Movies",
                newName: "Movie");

            migrationBuilder.RenameTable(
                name: "Halls",
                newName: "Hall");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_MovieId",
                table: "Session",
                newName: "IX_Session_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_HallId",
                table: "Session",
                newName: "IX_Session_HallId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_SessionId",
                table: "Reservation",
                newName: "IX_Reservation_SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_SeatId",
                table: "Reservation",
                newName: "IX_Reservation_SeatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Session",
                table: "Session",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seat",
                table: "Seat",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movie",
                table: "Movie",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hall",
                table: "Hall",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Seat_SeatId",
                table: "Reservation",
                column: "SeatId",
                principalTable: "Seat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Session_SessionId",
                table: "Reservation",
                column: "SessionId",
                principalTable: "Session",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Hall_HallId",
                table: "Session",
                column: "HallId",
                principalTable: "Hall",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Movie_MovieId",
                table: "Session",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Seat_SeatId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Session_SessionId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Session_Hall_HallId",
                table: "Session");

            migrationBuilder.DropForeignKey(
                name: "FK_Session_Movie_MovieId",
                table: "Session");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Session",
                table: "Session");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seat",
                table: "Seat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movie",
                table: "Movie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hall",
                table: "Hall");

            migrationBuilder.RenameTable(
                name: "Session",
                newName: "Sessions");

            migrationBuilder.RenameTable(
                name: "Seat",
                newName: "Seats");

            migrationBuilder.RenameTable(
                name: "Reservation",
                newName: "Reservations");

            migrationBuilder.RenameTable(
                name: "Movie",
                newName: "Movies");

            migrationBuilder.RenameTable(
                name: "Hall",
                newName: "Halls");

            migrationBuilder.RenameIndex(
                name: "IX_Session_MovieId",
                table: "Sessions",
                newName: "IX_Sessions_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_Session_HallId",
                table: "Sessions",
                newName: "IX_Sessions_HallId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_SessionId",
                table: "Reservations",
                newName: "IX_Reservations_SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_SeatId",
                table: "Reservations",
                newName: "IX_Reservations_SeatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seats",
                table: "Seats",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movies",
                table: "Movies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Halls",
                table: "Halls",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Seats_SeatId",
                table: "Reservations",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Sessions_SessionId",
                table: "Reservations",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Halls_HallId",
                table: "Sessions",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Movies_MovieId",
                table: "Sessions",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
