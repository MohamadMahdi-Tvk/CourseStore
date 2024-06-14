using CourseStore.BLL.Framework;
using CourseStore.DAL.DbContexts;
using CourseStore.Model.Framework;
using CourseStore.Model.Tags.Commands;
using CourseStore.Model.Tags.Entities;
using MediatR;

namespace CourseStore.BLL.Tags.Commands;

public class UpdateTagHandler : BaseApplicationServiceHandler<UpdateTag, Tag>
{
    public UpdateTagHandler(CourseStoreDbContext context) : base(context)
    {
    }

    protected override async Task HandleRequest(UpdateTag request, CancellationToken cancellationToken)
    {
        Tag tag = _context.Tags.SingleOrDefault(c => c.Id == request.Id);

        if (tag == null)
        {
            AddError($"تگ با شناسه {request.Id} یافت نشد");
        }
        else
        {
            tag.TagName = request.TagName;

            await _context.SaveChangesAsync();

            AddResult(tag);
        }
    }

}


//Without using BaseApplicationServiceHandler

//public class UpdateTagHandler : IRequestHandler<UpdateTag, ApplicationServiceResponse<Tag>>
//{
//    private readonly CourseStoreDbContext _context;
//    public UpdateTagHandler(CourseStoreDbContext context)
//    {
//        _context = context;
//    }

//    public async Task<ApplicationServiceResponse<Tag>> Handle(UpdateTag request, CancellationToken cancellationToken)
//    {
//        ApplicationServiceResponse<Tag> response = new();

//        Tag tag = _context.Tags.SingleOrDefault(c => c.Id == request.Id);

//        if (tag == null)
//        {
//            response.AddError($"تگ با شناسه {request.Id} یافت نشد");
//        }
//        else
//        {
//            tag.TagName = request.TagName;

//            await _context.SaveChangesAsync();

//            response.Result = tag;
//        }

//        return response;
//    }
//}
