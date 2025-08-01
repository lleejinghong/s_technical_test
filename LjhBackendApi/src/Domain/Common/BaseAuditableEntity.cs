﻿namespace LjhBackendApi.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
#pragma warning disable 8632

    public DateTimeOffset Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTimeOffset LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}
