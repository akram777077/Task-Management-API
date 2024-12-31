using System;

namespace TaskMangment.Api.Middlewares.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public class SkipValidateIdAttribute : Attribute
{
}
