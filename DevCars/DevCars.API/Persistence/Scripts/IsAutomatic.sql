BEGIN TRANSACTION;
GO

ALTER TABLE [Cars] ADD [IsAutomatic] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210403160414_IsAutomatic', N'5.0.4');
GO

COMMIT;
GO

