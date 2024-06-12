using System.Linq.Expressions;
using Domain.Entities;
using Domain.Enum;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Levels;

public class GetByIdLevelQuery
{
    public class Request : IRequest<OperationResponse<Response>>
    {
        public Guid Id { get; set; }
    }

    public class Response
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid LanguageId { get; set; }
        public List<LessonsRes> Lessons { get; set; }
        
        public class LessonsRes
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int Order { get; set; }
            public string Description { get; set; }
            public string? FileUrl { get; set; }
            public LessonType Type { get; set; }
        }
        
        public static Expression<Func<Level, Response>> Selector => l
            => new()
            {
                Id = l.Id,
                Name = l.Name.ToString(),
                Description = l.Description,
                LanguageId = l.LanguageId,
                Lessons = l.Lessons.Select(le => new LessonsRes()
                {
                        Id = le.Id,
                        Name = le.Name,
                        Description = le.Description,
                        Order = le.Order,
                        Type = le.Type,
                        FileUrl = le.FileUrl
                }).ToList()
            };
    }
}