using System.Linq.Expressions;
using Domain.Entities;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Certificates;

public class GetAllCertificatesQuery
{
    public class Request : IRequest<OperationResponse<List<Response>>>
    {

    }

    public class Response
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileUrl { get; set; }
        public Guid LevelId { get; set; }

        public static Expression<Func<Certificate, Response>> Selector => l
            => new()
            {
                Id = l.Id,
                Title = l.Title,
                Description = l.Description,
                FileUrl = l.FileUrl,
                LevelId = l.LevelId
            };
    }
}