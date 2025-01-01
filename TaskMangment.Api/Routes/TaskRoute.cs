using System;

namespace TaskMangment.Api.Routes;

public static class TaskRoute
{
    public const string Base = "/api/tasks";
    public const string GetAll = Base;
    public const string Get = Base + "/{id:int}";
    public const string Create = Base;
    public const string Update = Base + "/{id:int}";
    public const string Delete = Base + "/{id:int}";
    public const string Complete = Base + "/{id:int}/Complete";
    public const string Reopen = Base + "/{id:int}/Reopen";
}
