using Domain.Entities;
using Domain.Repositories;
using LingoLearn.Application.Dashboard.Core.Abstractions.Http;
using Microsoft.EntityFrameworkCore;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Levels;

public class GetByIdLevelHandler : IRequestHandler<GetByIdLevelQuery.Request,
    OperationResponse<GetByIdLevelQuery.Response>>
{
    private readonly ILingoLearnRepository _repository;
    private readonly IHttpService _httpService;

    public GetByIdLevelHandler(ILingoLearnRepository repository, IHttpService httpService)
    {
        _repository = repository;
        _httpService = httpService;
    }

    public async Task<OperationResponse<GetByIdLevelQuery.Response>> HandleAsync(GetByIdLevelQuery.Request request,
        CancellationToken cancellationToken = new())
    {
        var level = await _repository.Query<Level>()
            .Where(s => s.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (level is not { UtcDateDeleted: null })
            return OperationResponse.WithBadRequest("Level Not found").ToResponse<GetByIdLevelQuery.Response>();

        return await _repository.GetAsync(request.Id,
            GetByIdLevelQuery.Response.Selector(_httpService.CurrentUserId!.Value, request.Search),
            "Lessons");
    }
}