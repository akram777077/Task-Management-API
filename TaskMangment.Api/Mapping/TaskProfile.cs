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
        CreateMap<CreateTaskWithUserDto,TaskModel>()
            .ConstructUsing(x => 
                new TaskModel(
                    x.Username,
                    0, 
                    x.Title, 
                    x.Description, 
                    x.DueDate,
                    false
                )
            );
        CreateMap<UpdateTaskWithUser,TaskModel>()
            .ConstructUsing((x, context) => 
                new TaskModel(
                    x.Username,
                    context.Items["id"] as int? ?? 0,
                    x.Title, 
                    x.Description, 
                    x.DueDate,
                    x.IsCompleted
                )
            );
        CreateMap<CreateTaskRequest,CreateTaskWithUserDto>()
        .ConstructUsing((x,context) => new CreateTaskWithUserDto(
            context.Items["username"] as string ?? string.Empty,
            x.Title,
            x.Description,
            x.DueDate
        ));
        CreateMap<UpdateTaskRequest,UpdateTaskWithUser>()
        .ConstructUsing((x,context) => new UpdateTaskWithUser(
            context.Items["username"] as string ?? string.Empty,
            x.Title,
            x.Description,
            x.DueDate,
            x.IsCompleted
        ));
    }
}
