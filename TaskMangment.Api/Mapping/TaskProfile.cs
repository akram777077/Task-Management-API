using System;
using AutoMapper;
using TaskMangment.Api.DTOs;
using TaskMangment.Buisness.Models;

namespace TaskMangment.Api.Mapping;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<TaskModel,ResponseDto>();
        CreateMap<CreateTaskRequest,TaskModel>()
            .ConstructUsing(x => 
                new TaskModel(
                    0, 
                    x.Title, 
                    x.Description, 
                    x.DueDate,
                    false
                )
            );
        CreateMap<UpdateTaskRequest,TaskModel>()
            .ConstructUsing((x, context) => 
                new TaskModel(
                    context.Items["Id"] as int? ?? 0,
                    x.Title, 
                    x.Description, 
                    x.DueDate,
                    x.IsCompleted
                )
            );
    }
}
