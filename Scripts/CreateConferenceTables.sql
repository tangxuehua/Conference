CREATE TABLE [dbo].[Conferences] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [AccessCode]    NVARCHAR (6)     NULL,
    [OwnerName]     NVARCHAR (MAX)   NOT NULL,
    [OwnerEmail]    NVARCHAR (MAX)   NOT NULL,
    [Slug]          NVARCHAR (MAX)   NOT NULL,
    [Name]          NVARCHAR (MAX)   NOT NULL,
    [Description]   NVARCHAR (MAX)   NOT NULL,
    [Location]      NVARCHAR (MAX)   NOT NULL,
    [Tagline]       NVARCHAR (MAX)   NULL,
    [TwitterSearch] NVARCHAR (MAX)   NULL,
    [StartDate]     DATETIME         NOT NULL,
    [EndDate]       DATETIME         NOT NULL,
    [IsPublished]   BIT              NOT NULL,
    [Version]       BIGINT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO
CREATE TABLE [dbo].[ConferenceSlugs] (
    [IndexId]      NVARCHAR (32)    NOT NULL,
    [ConferenceId] UNIQUEIDENTIFIER NOT NULL,
    [Slug]         NVARCHAR (MAX)   NOT NULL,
    PRIMARY KEY CLUSTERED ([IndexId] ASC)
)
GO
CREATE TABLE [dbo].[ConferenceSeatTypes] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [ConferenceId]      UNIQUEIDENTIFIER NOT NULL,
    [Name]              NVARCHAR (70)    NOT NULL,
    [Description]       NVARCHAR (250)   NOT NULL,
    [Price]             DECIMAL (18, 2)  NOT NULL,
    [Quantity]          INT              NOT NULL,
    [AvailableQuantity] INT              NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO
CREATE TABLE [dbo].[ReservationItems] (
    [ConferenceId]  UNIQUEIDENTIFIER NOT NULL,
    [ReservationId] UNIQUEIDENTIFIER NOT NULL,
    [SeatTypeId]    UNIQUEIDENTIFIER NOT NULL,
    [Quantity]      INT              NOT NULL,
    PRIMARY KEY CLUSTERED ([ConferenceId] ASC, [ReservationId] ASC, [SeatTypeId] ASC)
)
GO
CREATE TABLE [dbo].[Orders] (
    [OrderId]                   UNIQUEIDENTIFIER NOT NULL,
    [ConferenceId]              UNIQUEIDENTIFIER NOT NULL,
    [Status]                    INT              NOT NULL,
    [AccessCode]                NVARCHAR (MAX)   NULL,
    [RegistrantFirstName]       NVARCHAR (MAX)   NULL,
    [RegistrantLastName]        NVARCHAR (MAX)   NULL,
    [RegistrantEmail]           NVARCHAR (MAX)   NULL,
    [TotalAmount]               DECIMAL (18, 2)  NOT NULL,
    [ReservationExpirationDate] DATETIME         NULL,
    [Version]                   BIGINT           NOT NULL,
    PRIMARY KEY CLUSTERED ([OrderId] ASC)
)
GO
CREATE TABLE [dbo].[OrderLines] (
    [OrderId]      UNIQUEIDENTIFIER NOT NULL,
    [SeatTypeId]   UNIQUEIDENTIFIER NOT NULL,
    [SeatTypeName] NVARCHAR (MAX)   NULL,
    [UnitPrice]    DECIMAL (18, 2)  NOT NULL,
    [Quantity]     INT              NOT NULL,
    [LineTotal]    DECIMAL (18, 2)  NOT NULL,
    PRIMARY KEY CLUSTERED ([OrderId] ASC, [SeatTypeId] ASC)
)
GO
CREATE TABLE [dbo].[OrderSeatAssignments] (
    [AssignmentsId]     UNIQUEIDENTIFIER NOT NULL,
    [OrderId]           UNIQUEIDENTIFIER NOT NULL,
    [Position]          INT              NOT NULL,
    [SeatTypeId]        UNIQUEIDENTIFIER NOT NULL,
    [SeatTypeName]      NVARCHAR (MAX)   NULL,
    [AttendeeFirstName] NVARCHAR (MAX)   NULL,
    [AttendeeLastName]  NVARCHAR (MAX)   NULL,
    [AttendeeEmail]     NVARCHAR (MAX)   NULL,
    PRIMARY KEY CLUSTERED ([AssignmentsId] ASC, [Position] ASC)
)
GO
CREATE TABLE [dbo].[Payments] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [State]       INT              NOT NULL,
    [OrderId]     UNIQUEIDENTIFIER NOT NULL,
    [Description] NVARCHAR (MAX)   NULL,
    [TotalAmount] DECIMAL (18, 2)  NOT NULL,
    [Version]     BIGINT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO
CREATE TABLE [dbo].[PaymentItems] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Description] NVARCHAR (MAX)   NULL,
    [Amount]      DECIMAL (18, 2)  NOT NULL,
    [PaymentId]   UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO