-- Create the database if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'RecordStore')
BEGIN
    CREATE DATABASE RecordStore;
END
GO

USE RecordStore;
GO

-- Create Artists table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Artists]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Artists] (
        [ArtistID] INT IDENTITY(1,1) PRIMARY KEY,
        [Name] NVARCHAR(100) NOT NULL
    );
END
GO

-- Create Genres table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Genres]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Genres] (
        [GenreID] INT IDENTITY(1,1) PRIMARY KEY,
        [Name] NVARCHAR(50) NOT NULL
    );
END
GO

-- Create Records table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Records]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Records] (
        [RecordID] INT IDENTITY(1,1) PRIMARY KEY,
        [Title] NVARCHAR(200) NOT NULL,
        [ArtistID] INT NOT NULL,
        [GenreID] INT NOT NULL,
        [Year] INT NOT NULL,
        [Price] DECIMAL(18,2) NOT NULL,
        [CoverImageURL] NVARCHAR(500) NULL,
        CONSTRAINT [FK_Records_Artists] FOREIGN KEY ([ArtistID]) REFERENCES [Artists]([ArtistID]),
        CONSTRAINT [FK_Records_Genres] FOREIGN KEY ([GenreID]) REFERENCES [Genres]([GenreID])
    );
END
GO

-- Insert sample data if tables are empty
IF NOT EXISTS (SELECT * FROM [dbo].[Genres])
BEGIN
    INSERT INTO [dbo].[Genres] ([Name]) VALUES 
    ('Rock'),
    ('Jazz'),
    ('Classical'),
    ('Pop'),
    ('Hip Hop');
END
GO

IF NOT EXISTS (SELECT * FROM [dbo].[Artists])
BEGIN
    INSERT INTO [dbo].[Artists] ([Name]) VALUES 
    ('Pink Floyd'),
    ('Miles Davis'),
    ('Ludwig van Beethoven'),
    ('The Beatles'),
    ('Michael Jackson');
END
GO

IF NOT EXISTS (SELECT * FROM [dbo].[Records])
BEGIN
    INSERT INTO [dbo].[Records] ([Title], [ArtistID], [GenreID], [Year], [Price], [CoverImageURL]) VALUES 
    ('The Dark Side of the Moon', 1, 1, 1973, 29.99, '/images/dark-side-of-the-moon.jpg'),
    ('Kind of Blue', 2, 2, 1959, 24.99, '/images/kind-of-blue.jpg'),
    ('Symphony No. 9', 3, 3, 1824, 19.99, '/images/symphony-9.jpg'),
    ('Abbey Road', 4, 1, 1969, 27.99, '/images/abbey-road.jpg'),
    ('Thriller', 5, 4, 1982, 25.99, '/images/thriller.jpg');
END
GO 