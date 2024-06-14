using CourseStore.BLL.Framework;
using CourseStore.DAL.DbContexts;
using CourseStore.Model.Framework;
using CourseStore.Model.Tags.Commands;
using CourseStore.Model.Tags.Entities;
using MediatR;

namespace CourseStore.BLL.Tags.Commands;

public class CreateTagHandler : BaseApplicationServiceHandler<CreateTag, Tag>
{
    public CreateTagHandler(CourseStoreDbContext context) : base(context)
    {
    }

    protected override async Task HandleRequest(CreateTag request, CancellationToken cancellationToken)
    {
        Tag tag = new()
        {
            TagName = request.TagName
        };

        await _context.Tags.AddAsync(tag);
        await _context.SaveChangesAsync();

        AddResult(tag);
    }
}
