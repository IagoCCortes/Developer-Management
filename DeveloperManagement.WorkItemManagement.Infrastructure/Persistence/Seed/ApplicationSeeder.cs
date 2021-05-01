using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.CommentAggregate;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Interfaces;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Seed
{
    public class ApplicationSeeder
    {
        private static readonly Dictionary<string, Type> _enumTables = new Dictionary<string, Type>
        {
            {nameof(Activity), typeof(Activity)},
            {nameof(LinkType), typeof(LinkType)},
            {nameof(Priority), typeof(Priority)},
            {nameof(StateReason), typeof(StateReason)},
            {nameof(ValueArea), typeof(ValueArea)},
            {nameof(WorkItemState), typeof(WorkItemState)},
            {nameof(WorkItemType), typeof(WorkItemType)},
            {nameof(Reactiontype), typeof(Reactiontype)},
        };

        private readonly IDapperConnectionFactory _factory;

        public ApplicationSeeder(IDapperConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task SeedDatabaseAsync()
        {
            using var connection = await _factory.CreateConnectionAsync();
            var tables = await connection.QueryAsync<string>("SHOW TABLES");
            if (tables.Any()) return;

            using var transaction = connection.BeginTransaction();

            await CreateTables(connection, transaction);
            await SeedEnumTables(connection, transaction);

            transaction.Commit();
        }

        private async Task CreateTables(IDbConnection connection, IDbTransaction transaction)
        {
            foreach (var table in _enumTables)
            {
                await connection.ExecuteAsync(
                    $"CREATE TABLE `{table.Key}` (`Id` int NOT NULL,`Description` varchar(45) NOT NULL,PRIMARY KEY (`id`)) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci",
                    transaction: transaction);
            }
            
            await connection.ExecuteAsync(
                "CREATE TABLE `WorkItem` (" +
                "`Id` char(36) NOT NULL," +
                "`Title` varchar(150) NOT NULL," +
                "`AssignedTo` char(36) DEFAULT NULL," +
                "`StateId` int NOT NULL," +
                "`TeamId` char(36) NOT NULL," +
                "`SprintId` char(36) DEFAULT NULL," +
                "`Description` varchar(4000) DEFAULT NULL," +
                "`PriorityId` int NOT NULL," +
                "`RepoLink` varchar(1000) DEFAULT NULL," +
                "`StateReasonId` int NOT NULL," +
                "`Created` datetime NOT NULL," +
                "`CreatedBy` varchar(45) NOT NULL," +
                "`LastModified` datetime DEFAULT NULL," +
                "`LastModifiedBy` varchar(45) DEFAULT NULL," +
                "PRIMARY KEY (`Id`)" +
                // "KEY `natureza_fk_idx` (`id_natureza`)," + index
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci", transaction: transaction);
            
            await connection.ExecuteAsync(
                "ALTER TABLE `WorkItem` " +
                "ADD CONSTRAINT `fk_WorkItem_WorkItemState_Id` FOREIGN KEY(`" +
                "StateId`) REFERENCES `WorkItemState` (`Id`);", transaction: transaction);
            await connection.ExecuteAsync(
                "ALTER TABLE `WorkItem` " +
                "ADD CONSTRAINT `fk_WorkItem_Priority_Id` FOREIGN KEY(`" +
                "PriorityId`) REFERENCES `Priority` (`Id`);", transaction: transaction);
            await connection.ExecuteAsync(
                "ALTER TABLE `WorkItem` " +
                "ADD CONSTRAINT `fk_WorkItem_StateReason_Id` FOREIGN KEY(`" +
                "StateReasonId`) REFERENCES `StateReason` (`Id`);", transaction: transaction);
            
            await connection.ExecuteAsync(
                "CREATE TABLE `Bug` (" +
                "`Id` char(36) NOT NULL," +
                "`EffortOriginalEstimate` int DEFAULT NULL," +
                "`EffortRemaining` int DEFAULT NULL," +
                "`EffortCompleted` int DEFAULT NULL," +
                "`IntegratedInBuild` varchar(400) DEFAULT NULL," +
                "`StoryPoints` int DEFAULT NULL," +
                "`SeverityId` int NOT NULL," +
                "`ActivityId` int DEFAULT NULL," +
                "`SystemInfo` varchar(1000) DEFAULT NULL," +
                "`FoundInBuild` varchar(400) DEFAULT NULL," +
                "`Created` datetime NOT NULL," +
                "`CreatedBy` varchar(45) NOT NULL," +
                "`LastModified` datetime DEFAULT NULL," +
                "`LastModifiedBy` varchar(45) DEFAULT NULL," +
                "PRIMARY KEY (`Id`)" +
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci", transaction: transaction);
                
            await connection.ExecuteAsync(
                "ALTER TABLE `Bug` " +
                "ADD CONSTRAINT `fk_Bug_WorkItem_Id` FOREIGN KEY(`" +
                "Id`) REFERENCES `WorkItem` (`Id`);", transaction: transaction);
            await connection.ExecuteAsync(
                "ALTER TABLE `Bug` " +
                "ADD CONSTRAINT `fk_Bug_Activity_Id` FOREIGN KEY(`" +
                "ActivityId`) REFERENCES `Activity` (`Id`);", transaction: transaction);
            await connection.ExecuteAsync(
                "ALTER TABLE `Bug` " +
                "ADD CONSTRAINT `fk_Bug_Priority_Id` FOREIGN KEY(`" +
                "SeverityId`) REFERENCES `Priority` (`Id`);", transaction: transaction);
            
            await connection.ExecuteAsync(
                "CREATE TABLE `Attachment` (" +
                "`Id` int NOT NULL AUTO_INCREMENT," +
                "`Path` varchar(1000) Not NULL," +
                "`FileName` varchar(250) Not NULL," +
                "`MimeType` varchar(200) Not NULL," +
                "`WorkItemId` char(36) NOT NULL," +
                "`Created` datetime NOT NULL," +
                "`CreatedBy` varchar(45) NOT NULL," +
                "`LastModified` datetime DEFAULT NULL," +
                "`LastModifiedBy` varchar(45) DEFAULT NULL," +
                "PRIMARY KEY (`Id`)" +
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci", transaction: transaction);
            
            await connection.ExecuteAsync(
                "ALTER TABLE `Attachment` " +
                "ADD CONSTRAINT `fk_Attachment_WorkItem_Id` FOREIGN KEY(`" +
                "WorkItemId`) REFERENCES `WorkItem` (`Id`);", transaction: transaction);
            
            await connection.ExecuteAsync(
                "CREATE TABLE `Comment` (" +
                "`Id` char(36) NOT NULL," +
                "`Text` varchar(1000) Not NULL," +
                "`WorkItemId` char(36) NOT NULL," +
                "`Created` datetime NOT NULL," +
                "`CreatedBy` varchar(45) NOT NULL," +
                "`LastModified` datetime DEFAULT NULL," +
                "`LastModifiedBy` varchar(45) DEFAULT NULL," +
                "PRIMARY KEY (`Id`)" +
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci", transaction: transaction);
            
            await connection.ExecuteAsync(
                "ALTER TABLE `Comment` " +
                "ADD CONSTRAINT `fk_Comment_WorkItem_Id` FOREIGN KEY(`" +
                "WorkItemId`) REFERENCES `WorkItem` (`Id`);", transaction: transaction);
            
            await connection.ExecuteAsync(
                "CREATE TABLE `Reaction` (" +
                "`Id` int NOT NULL AUTO_INCREMENT," +
                "`ReactionTypeId` int NOT NULL," +
                "`CommentId` char(36) NOT NULL," +
                "`Created` datetime NOT NULL," +
                "`CreatedBy` varchar(45) NOT NULL," +
                "`LastModified` datetime DEFAULT NULL," +
                "`LastModifiedBy` varchar(45) DEFAULT NULL," +
                "PRIMARY KEY (`Id`)" +
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci", transaction: transaction);
            
            await connection.ExecuteAsync(
                "ALTER TABLE `Reaction` " +
                "ADD CONSTRAINT `fk_Reaction_Comment_Id` FOREIGN KEY(`" +
                "CommentId`) REFERENCES `Comment` (`Id`);", transaction: transaction);
            await connection.ExecuteAsync(
                "ALTER TABLE `Reaction` " +
                "ADD CONSTRAINT `fk_Reaction_ReactionType_Id` FOREIGN KEY(`" +
                "ReactionTypeId`) REFERENCES `ReactionType` (`Id`);", transaction: transaction);
            
            await connection.ExecuteAsync(
                "CREATE TABLE `RelatedWork` (" +
                "`Id` char(36) NOT NULL," +
                "`LinkTypeId` int Not NULL," +
                "`Comment` varchar(1000) DEFAULT NULL," +
                "`Url` varchar(400) DEFAULT NULL," +
                "`ReferencedWorkItemId` char(36) DEFAULT NULL," +
                "`WorkItemId` char(36) NOT NULL," +
                "`Created` datetime NOT NULL," +
                "`CreatedBy` varchar(45) NOT NULL," +
                "`LastModified` datetime DEFAULT NULL," +
                "`LastModifiedBy` varchar(45) DEFAULT NULL," +
                "PRIMARY KEY (`Id`)" +
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci", transaction: transaction);
            
            await connection.ExecuteAsync(
                "ALTER TABLE `RelatedWork` " +
                "ADD CONSTRAINT `fk_RelatedWork_WorkItem_Id` FOREIGN KEY(`" +
                "WorkItemId`) REFERENCES `WorkItem` (`Id`);", transaction: transaction);
            await connection.ExecuteAsync(
                "ALTER TABLE `RelatedWork` " +
                "ADD CONSTRAINT `fk_RelatedWork_WorkItem_Id_ref` FOREIGN KEY(`" +
                "ReferencedWorkItemId`) REFERENCES `WorkItem` (`Id`);", transaction: transaction);
            
            await connection.ExecuteAsync(
                "CREATE TABLE `Tag` (" +
                "`Id` int NOT NULL AUTO_INCREMENT," +
                "`Text` varchar(150) Not NULL," +
                "`WorkItemId` char(36) NOT NULL," +
                "`Created` datetime NOT NULL," +
                "`CreatedBy` varchar(45) NOT NULL," +
                "`LastModified` datetime DEFAULT NULL," +
                "`LastModifiedBy` varchar(45) DEFAULT NULL," +
                "PRIMARY KEY (`Id`)" +
                ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci", transaction: transaction);
            
            await connection.ExecuteAsync(
                "ALTER TABLE `Tag` " +
                "ADD CONSTRAINT `fk_Tag_WorkItem_Id` FOREIGN KEY(`" +
                "WorkItemId`) REFERENCES `WorkItem` (`Id`);", transaction: transaction);
        }

        private async Task SeedEnumTables(IDbConnection connection, IDbTransaction transaction)
        {
            foreach (var table in _enumTables)
            {
                var sb = new StringBuilder($"INSERT INTO {table.Key} (Id, Description) VALUES ");
                foreach (var value in Enum.GetValues(table.Value))
                    sb.Append($"({(int) value},'{value.ToString()}'),");
                sb.Length--;

                await connection.ExecuteAsync(sb.ToString(), transaction: transaction);
            }
        }
    }
}