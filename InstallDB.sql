USE [Conference]
GO

/****** Object:  Table [dbo].[Conferences]    Script Date: 11/10/2014 09:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Conferences](
    [Id] [uniqueidentifier] NOT NULL,
    [AccessCode] [nvarchar](6) NULL,
    [OwnerName] [nvarchar](max) NOT NULL,
    [OwnerEmail] [nvarchar](max) NOT NULL,
    [Slug] [nvarchar](max) NOT NULL,
    [Name] [nvarchar](max) NOT NULL,
    [Description] [nvarchar](max) NOT NULL,
    [Location] [nvarchar](max) NOT NULL,
    [Tagline] [nvarchar](max) NULL,
    [TwitterSearch] [nvarchar](max) NULL,
    [StartDate] [datetime] NOT NULL,
    [EndDate] [datetime] NOT NULL,
    [IsPublished] [bit] NOT NULL,
	[Version] [bigint] NOT NULL,
 CONSTRAINT [PK_ConferenceManagement.Conferences] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[ConferenceSlugs](
    [IndexId] [nvarchar](32) NOT NULL,
    [ConferenceId] [uniqueidentifier] NOT NULL,
    [Slug] [nvarchar](max) NOT NULL
 CONSTRAINT [PK_ConferenceManagement.ConferenceSlugs] PRIMARY KEY CLUSTERED 
(
    [IndexId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [Conference]
GO

/****** Object:  Table [dbo].[Orders]    Script Date: 11/10/2014 09:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Orders](
    [OrderId] [uniqueidentifier] NOT NULL,
    [ConferenceId] [uniqueidentifier] NOT NULL,
    [Status] [int] NOT NULL,
    [AccessCode] [nvarchar](max) NULL,
    [RegistrantFirstName] [nvarchar](max) NULL,
    [RegistrantLastName] [nvarchar](max) NULL,
    [RegistrantEmail] [nvarchar](max) NULL,
    [TotalAmount] [decimal](18, 2) NOT NULL,
    [ReservationExpirationDate] [datetime] NULL,
    [Version] [bigint] NOT NULL,
 CONSTRAINT [PK_ConferenceManagement.Orders] PRIMARY KEY CLUSTERED 
(
    [OrderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [Conference]
GO

/****** Object:  Table [dbo].[OrderItemsViewV3]    Script Date: 11/10/2014 09:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OrderLines](
    [OrderId] [uniqueidentifier] NOT NULL,
    [SeatTypeId] [uniqueidentifier] NOT NULL,
    [SeatTypeName] [nvarchar](max) NULL,
    [UnitPrice] [decimal](18, 2) NOT NULL,
    [Quantity] [int] NOT NULL,
    [LineTotal] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
    [OrderId] ASC,
    [SeatTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [Conference]
GO

/****** Object:  Table [dbo].[OrderSeats]    Script Date: 11/10/2014 09:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OrderSeats](
    [OrderId] [uniqueidentifier] NOT NULL,
    [Position] [int] NOT NULL,
    [Attendee_FirstName] [nvarchar](max) NULL,
    [Attendee_LastName] [nvarchar](max) NULL,
    [Attendee_Email] [nvarchar](max) NULL,
    [SeatInfoId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ConferenceManagement.OrderSeats] PRIMARY KEY CLUSTERED 
(
    [OrderId] ASC,
    [Position] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [Conference]
GO

/****** Object:  Table [dbo].[SeatTypes]    Script Date: 11/10/2014 09:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConferenceSeatTypes](
    [Id] [uniqueidentifier] NOT NULL,
    [ConferenceId] [uniqueidentifier] NOT NULL,
    [Name] [nvarchar](70) NOT NULL,
    [Description] [nvarchar](250) NOT NULL,
    [Price] [decimal](18, 2) NOT NULL,
    [Quantity] [int] NOT NULL,
    [AvailableQuantity] [int] NOT NULL,
 CONSTRAINT [PK_ConferenceManagement.SeatTypes] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [Conference]
GO

/****** Object:  Table [dbo].[ThidPartyProcessorPaymentItems]    Script Date: 11/10/2014 09:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ThidPartyProcessorPaymentItems](
    [Id] [uniqueidentifier] NOT NULL,
    [Description] [nvarchar](max) NULL,
    [Amount] [decimal](18, 2) NOT NULL,
    [PaymentId] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [Conference]
GO

/****** Object:  Table [dbo].[ThirdPartyProcessorPayments]    Script Date: 11/10/2014 09:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ThirdPartyProcessorPayments](
    [Id] [uniqueidentifier] NOT NULL,
    [State] [int] NOT NULL,
    [OrderId] [uniqueidentifier] NOT NULL,
    [Description] [nvarchar](max) NULL,
    [TotalAmount] [decimal](18, 2) NOT NULL,
    [Version] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [Conference]
GO

/****** Object:  Table [dbo].[ConferenceSeatTypesView]    Script Date: 11/10/2014 09:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConferenceSeatTypesView](
    [Id] [uniqueidentifier] NOT NULL,
    [ConferenceId] [uniqueidentifier] NOT NULL,
    [Name] [nvarchar](max) NULL,
    [Description] [nvarchar](max) NULL,
    [Price] [decimal](18, 2) NOT NULL,
    [Quantity] [int] NOT NULL,
    [AvailableQuantity] [int] NOT NULL,
    [ConferenceVersion] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [Conference]
GO

/****** Object:  Table [dbo].[ConferencesView]    Script Date: 11/10/2014 09:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConferencesView](
    [Id] [uniqueidentifier] NOT NULL,
    [Code] [nvarchar](max) NULL,
    [Name] [nvarchar](max) NULL,
    [Description] [nvarchar](max) NULL,
    [Location] [nvarchar](max) NULL,
    [Tagline] [nvarchar](max) NULL,
    [TwitterSearch] [nvarchar](max) NULL,
    [StartDate] [datetimeoffset](7) NOT NULL,
    [IsPublished] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [Conference]
GO

/****** Object:  Table [dbo].[ConferenceReservationItems]    Script Date: 11/10/2014 09:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ReservationItems](
    [ConferenceId] [uniqueidentifier] NOT NULL,
    [ReservationId] [uniqueidentifier] NOT NULL,
    [SeatTypeId] [uniqueidentifier] NOT NULL,
    [Quantity] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
    [ConferenceId] ASC,
    [ReservationId] ASC,
    [SeatTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


USE [Conference]
GO

/****** Object:  Table [dbo].[OrderItemsViewV3]    Script Date: 11/10/2014 09:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OrderItemsViewV3](
    [OrderId] [uniqueidentifier] NOT NULL,
    [SeatType] [uniqueidentifier] NOT NULL,
    [RequestedSeats] [int] NOT NULL,
    [ReservedSeats] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
    [OrderId] ASC,
    [SeatType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [Conference]
GO

/****** Object:  Table [dbo].[OrdersViewV3]    Script Date: 11/10/2014 09:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OrdersViewV3](
    [OrderId] [uniqueidentifier] NOT NULL,
    [ConferenceId] [uniqueidentifier] NOT NULL,
    [ReservationExpirationDate] [datetime] NULL,
    [StateValue] [int] NOT NULL,
    [OrderVersion] [int] NOT NULL,
    [RegistrantEmail] [nvarchar](max) NULL,
    [AccessCode] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
    [OrderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [Conference]
GO

/****** Object:  Table [dbo].[PricedOrderLineSeatTypeDescriptionsV3]    Script Date: 11/10/2014 09:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PricedOrderLineSeatTypeDescriptionsV3](
    [SeatTypeId] [uniqueidentifier] NOT NULL,
    [Name] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
    [SeatTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [Conference]
GO

/****** Object:  Table [dbo].[PricedOrderLinesV3]    Script Date: 11/10/2014 09:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PricedOrderLinesV3](
    [OrderId] [uniqueidentifier] NOT NULL,
    [Position] [int] NOT NULL,
    [Description] [nvarchar](max) NULL,
    [UnitPrice] [decimal](18, 2) NOT NULL,
    [Quantity] [int] NOT NULL,
    [LineTotal] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
    [OrderId] ASC,
    [Position] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [Conference]
GO

/****** Object:  Table [dbo].[PricedOrdersV3]    Script Date: 11/10/2014 09:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PricedOrdersV3](
    [OrderId] [uniqueidentifier] NOT NULL,
    [AssignmentsId] [uniqueidentifier] NULL,
    [Total] [decimal](18, 2) NOT NULL,
    [OrderVersion] [int] NOT NULL,
    [IsFreeOfCharge] [bit] NOT NULL,
    [ReservationExpirationDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
    [OrderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO