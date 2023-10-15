using MediatR;

namespace BLL.Attendees.Commands.UpdateAttendees
{
    public class UpdateAttendanceCommand : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }
}
