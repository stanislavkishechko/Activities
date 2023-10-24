using MediatR;

namespace BLL.Comments.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest<Result<CommentDto>>
    {
        public string Body { get; set; }
        public Guid ActivityId { get; set; }
    }
}
