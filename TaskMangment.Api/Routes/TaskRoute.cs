using System;

namespace TaskMangment.Api.Routes;

public static class TaskRoute
{
    public const string Base = "/api/task";
    public const string GetAll = Base + "/all";
    public const string Create = Base + "/create";
    public const string Update = Base + "/update";
    public const string Delete = Base + "/delete";
    public const string Reopen = Base + "/reopen";
}
