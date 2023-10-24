using AutoMapper;
using AutoMapper.QueryableExtensions;
using DAL.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BLL.Comments.Queries.GetCommentsList
{
    public class GetCommentsListQueryHandler : IRequestHandler<GetCommentsListQuery, Result<List<CommentDto>>>
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;

        public GetCommentsListQueryHandler(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<Result<List<CommentDto>>> Handle(GetCommentsListQuery request, CancellationToken cancellationToken)
        {
            var comments = await _dbContext.Comments
                .Where(x => x.Activity.Id == request.ActivityId)
                .OrderByDescending(x => x.CreatedAt)
                .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Result<List<CommentDto>>.Success(comments);
        }
    }
}
