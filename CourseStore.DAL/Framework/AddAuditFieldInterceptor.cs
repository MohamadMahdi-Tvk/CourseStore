﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CourseStore.DAL.Framework;

public class AddAuditFieldInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        SetShadowProperties(eventData);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        SetShadowProperties(eventData);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void SetShadowProperties(DbContextEventData eventData)
    {
        var changeTracker = eventData.Context.ChangeTracker;
        var addedEntities = changeTracker.Entries().Where(c => c.State == EntityState.Added);
        var modifiedEntities = changeTracker.Entries().Where(c => c.State == EntityState.Modified);

        DateTime now = DateTime.Now;

        foreach (var item in addedEntities)
        {
            item.Property("CreateBy").CurrentValue = "1";
            item.Property("UpdateBy").CurrentValue = "1";
            item.Property("CreateDate").CurrentValue = now;
            item.Property("UpdateDate").CurrentValue = now;
        }

        foreach (var item in modifiedEntities)
        {
            item.Property("UpdateBy").CurrentValue = "1";
            item.Property("UpdateDate").CurrentValue = now;
        }
    }
}
