using MediatR;

namespace BLL.Comments.Queries.GetCommentsList
{
    public class GetCommentsListQuery : IRequest<Result<List<CommentDto>>>
    {
        public Guid ActivityId { get; set; }
    }
}
