namespace Registration.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using AutoMapper;
    using ENode.Eventing;
    using Infrastructure.BlobStorage;
    using Infrastructure.Serialization;
    using Registration.Events;
    using Registration.ReadModel;

    public class SeatAssignmentsViewModelGenerator :
        IEventHandler<SeatAssignmentsCreated>,
        IEventHandler<SeatAssigned>,
        IEventHandler<SeatUnassigned>,
        IEventHandler<SeatAssignmentUpdated>
    {
        private readonly IBlobStorage storage;
        private readonly ITextSerializer serializer;
        private readonly IConferenceDao conferenceDao;

        public SeatAssignmentsViewModelGenerator(
            IConferenceDao conferenceDao,
            IBlobStorage storage,
            ITextSerializer serializer)
        {
            this.conferenceDao = conferenceDao;
            this.storage = storage;
            this.serializer = serializer;
        }

        static SeatAssignmentsViewModelGenerator()
        {
            Mapper.CreateMap<SeatAssigned, OrderSeat>();
            Mapper.CreateMap<SeatAssignmentUpdated, OrderSeat>();
        }

        public static string GetSeatAssignmentsBlobId(Guid sourceId)
        {
            return "SeatAssignments-" + sourceId.ToString();
        }

        public void Handle(IEventContext eventContext, SeatAssignmentsCreated @event)
        {
            var seatTypes = this.conferenceDao.GetSeatTypeNames(@event.Seats.Select(x => x.SeatType))
                .ToDictionary(x => x.Id, x => x.Name);

            var dto = new OrderSeats(@event.AggregateRootId, @event.OrderId, @event.Seats.Select(i =>
                new OrderSeat(i.Position, seatTypes.TryGetValue(i.SeatType))));

            Save(dto);
        }

        public void Handle(IEventContext eventContext, SeatAssigned @event)
        {
            var dto = Find(@event.AggregateRootId);
            var seat = dto.Seats.First(x => x.Position == @event.Position);
            Mapper.Map(@event, seat);
            Save(dto);
        }

        public void Handle(IEventContext eventContext, SeatUnassigned @event)
        {
            var dto = Find(@event.AggregateRootId);
            var seat = dto.Seats.First(x => x.Position == @event.Position);
            seat.Attendee.Email = seat.Attendee.FirstName = seat.Attendee.LastName = null;
            Save(dto);
        }

        public void Handle(IEventContext eventContext, SeatAssignmentUpdated @event)
        {
            var dto = Find(@event.AggregateRootId);
            var seat = dto.Seats.First(x => x.Position == @event.Position);
            Mapper.Map(@event, seat);
            Save(dto);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "By design")]
        private OrderSeats Find(Guid id)
        {
            var dto = this.storage.Find(GetSeatAssignmentsBlobId(id));
            if (dto == null)
                return null;

            using (var stream = new MemoryStream(dto))
            using (var reader = new StreamReader(stream))
            {
                return (OrderSeats)this.serializer.Deserialize(reader);
            }
        }

        private void Save(OrderSeats dto)
        {
            using (var writer = new StringWriter())
            {
                this.serializer.Serialize(writer, dto);
                this.storage.Save(GetSeatAssignmentsBlobId(dto.AssignmentsId), "text/plain", Encoding.UTF8.GetBytes(writer.ToString()));
            }
        }
    }
}
