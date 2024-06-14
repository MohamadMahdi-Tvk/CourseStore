using CourseStore.BLL.Framework;
using CourseStore.DAL.DbContexts;
using CourseStore.DAL.Tags;
using CourseStore.Model.Tags.Dtos;
using CourseStore.Model.Tags.Queries;

namespace CourseStore.BLL.Tags.Queries;

public class FilterByNameHandler : BaseApplicationServiceHandler<FilterByName, List<TagQueryResult>>
{
    public FilterByNameHandler(CourseStoreDbContext context) : base(context)
    {
    }

    protected override async Task HandleRequest(FilterByName request, CancellationToken cancellationToken)
    {
        var result = await _context.Tags.WhereOver(request.TagName).ToTagQueryResultAsync();

        AddResult(result);
    }
}
