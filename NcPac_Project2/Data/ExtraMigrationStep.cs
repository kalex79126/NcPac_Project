using Microsoft.EntityFrameworkCore.Migrations;

namespace NcPac_Project2.Data
{
    public static class ExtraMigrationStep
    {
        public static void Steps(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetMemberTimestampOnUpdate
                    AFTER UPDATE ON Members
                    BEGIN
                        UPDATE Members
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetMemberTimestampOnInsert
                    AFTER INSERT ON Members
                    BEGIN
                        UPDATE Members
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");

            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetMeetingTimestampOnUpdate
                    AFTER UPDATE ON Meetings
                    BEGIN
                        UPDATE Meetings
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetMeetingTimestampOnInsert
                    AFTER INSERT ON Meetings
                    BEGIN
                        UPDATE Meetings
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");

            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetActionItemTimestampOnUpdate
                    AFTER UPDATE ON ActionItems
                    BEGIN
                        UPDATE ActionItems
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");
            migrationBuilder.Sql(
                @"
                    CREATE TRIGGER SetActionItemTimestampOnInsert
                    AFTER INSERT ON ActionItems
                    BEGIN
                        UPDATE ActionItems
                        SET RowVersion = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");

            //Alternative way to create report table - NMK
            //migrationBuilder.Sql(
            //    @"
            //        CREATE View MemberSummaries as
            //        Select m.ID, m.FirstName, m.LastName, m.City, m.Address, m.Email, m.Phone, m.JobTitle, m.Company, m.NcGrad,
            //        m.RenewedDate, m.RegisteredAt, COUNT(m.ID) as NumberOfChairs
            //        From Members m left join Enrrolls e
            //        on m.ID = e.MemberID
            //        left join m
            //        on m.ID = e.MemberID
            //        GROUP BY m.ID, m.FirstName, m.LastName
            //    ");
        }
    }
}
