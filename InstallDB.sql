USE [conference]
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
	[WasEverPublished] [bit] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Location] [nvarchar](max) NOT NULL,
	[Tagline] [nvarchar](max) NULL,
	[TwitterSearch] [nvarchar](max) NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[IsPublished] [bit] NOT NULL,
 CONSTRAINT [PK_ConferenceManagement.Conferences] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [conference]
GO

/****** Object:  Table [dbo].[Orders]    Script Date: 11/10/2014 09:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Orders](
	[Id] [uniqueidentifier] NOT NULL,
	[ConferenceId] [uniqueidentifier] NOT NULL,
	[AssignmentsId] [uniqueidentifier] NULL,
	[AccessCode] [nvarchar](max) NULL,
	[RegistrantName] [nvarchar](max) NULL,
	[RegistrantEmail] [nvarchar](max) NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[StatusValue] [int] NOT NULL,
 CONSTRAINT [PK_ConferenceManagement.Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [conference]
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

USE [conference]
GO

/****** Object:  Table [dbo].[SeatTypes]    Script Date: 11/10/2014 09:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SeatTypes](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](70) NOT NULL,
	[Description] [nvarchar](250) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[ConferenceInfo_Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ConferenceManagement.SeatTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [conference]
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
	[ThirdPartyProcessorPayment_Id] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [conference]
GO

/****** Object:  Table [dbo].[ThirdPartyProcessorPayments]    Script Date: 11/10/2014 09:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ThirdPartyProcessorPayments](
	[Id] [uniqueidentifier] NOT NULL,
	[StateValue] [int] NOT NULL,
	[PaymentSourceId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [conference]
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
	[SeatsAvailabilityVersion] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [conference]
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

USE [conference]
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

USE [conference]
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

USE [conference]
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

USE [conference]
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

USE [conference]
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

USE [conference]
GO

/****** Object:  Table [dbo].[Command]    Script Date: 11/10/2014 09:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Command](
	[Sequence] [bigint] IDENTITY(1,1) NOT NULL,
	[CommandId] [nvarchar](64) NOT NULL,
	[CommandTypeCode] [int] NOT NULL,
	[AggregateRootTypeCode] [int] NOT NULL,
	[AggregateRootId] [nvarchar](32) NULL,
	[SourceEventId] [nvarchar](32) NULL,
	[SourceExceptionId] [nvarchar](32) NULL,
	[Timestamp] [datetime] NOT NULL,
	[Payload] [varbinary](max) NOT NULL,
	[Events] [varbinary](max) NULL,
	[Items] [varbinary](max) NULL,
 CONSTRAINT [PK_Command] PRIMARY KEY CLUSTERED 
(
	[CommandId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [conference]
GO

/****** Object:  Table [dbo].[EventHandleInfo]    Script Date: 11/10/2014 09:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EventHandleInfo](
	[EventId] [nvarchar](32) NOT NULL,
	[EventHandlerTypeCode] [int] NOT NULL,
	[EventTypeCode] [int] NOT NULL,
	[AggregateRootId] [nvarchar](32) NULL,
	[AggregateRootVersion] [int] NULL,
 CONSTRAINT [PK_EventHandleInfo] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC,
	[EventHandlerTypeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [conference]
GO

/****** Object:  Table [dbo].[EventPublishInfo]    Script Date: 11/10/2014 09:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EventPublishInfo](
	[EventProcessorName] [nvarchar](64) NOT NULL,
	[AggregateRootId] [nvarchar](32) NOT NULL,
	[PublishedVersion] [int] NOT NULL,
 CONSTRAINT [PK_EventPublishInfo] PRIMARY KEY CLUSTERED 
(
	[EventProcessorName] ASC,
	[AggregateRootId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [conference]
GO

/****** Object:  Table [dbo].[EventStream]    Script Date: 11/10/2014 09:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EventStream](
	[Sequence] [bigint] IDENTITY(1,1) NOT NULL,
	[AggregateRootTypeCode] [int] NOT NULL,
	[AggregateRootId] [nvarchar](32) NOT NULL,
	[Version] [int] NOT NULL,
	[CommandId] [nvarchar](64) NOT NULL,
	[Timestamp] [datetime] NOT NULL,
	[Events] [varbinary](max) NOT NULL,
	[Items] [varbinary](max) NULL,
 CONSTRAINT [PK_EventStream] PRIMARY KEY CLUSTERED 
(
	[AggregateRootId] ASC,
	[Version] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [conference]
GO

/****** Object:  Table [dbo].[Lock]    Script Date: 11/10/2014 09:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Lock](
	[LockKey] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_Lock] PRIMARY KEY CLUSTERED 
(
	[LockKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [conference]
GO

/****** Object:  Table [dbo].[Message]    Script Date: 11/10/2014 09:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Message](
	[MessageOffset] [bigint] NOT NULL,
	[Topic] [varchar](128) NOT NULL,
	[QueueId] [int] NOT NULL,
	[QueueOffset] [bigint] NOT NULL,
	[Code] [int] NOT NULL,
	[Body] [varbinary](max) NOT NULL,
	[StoredTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[MessageOffset] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [conference]
GO

/****** Object:  Table [dbo].[QueueOffset]    Script Date: 11/10/2014 09:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[QueueOffset](
	[Version] [bigint] NOT NULL,
	[ConsumerGroup] [nvarchar](128) NOT NULL,
	[Topic] [nvarchar](128) NOT NULL,
	[QueueId] [int] NOT NULL,
	[QueueOffset] [bigint] NOT NULL,
	[Timestamp] [datetime] NOT NULL,
 CONSTRAINT [PK_QueueOffset] PRIMARY KEY CLUSTERED 
(
	[ConsumerGroup] ASC,
	[Topic] ASC,
	[QueueId] ASC,
	[Version] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [conference]
GO

/****** Object:  Table [dbo].[Snapshot]    Script Date: 11/10/2014 09:07:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Snapshot](
	[AggregateRootId] [nvarchar](32) NOT NULL,
	[Version] [int] NOT NULL,
	[AggregateRootTypeCode] [int] NOT NULL,
	[Payload] [varbinary](max) NOT NULL,
	[Timestamp] [datetime] NOT NULL,
 CONSTRAINT [PK_Snapshot] PRIMARY KEY CLUSTERED 
(
	[AggregateRootId] ASC,
	[Version] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO