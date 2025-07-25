using CSM.Core.Common;
using CSM.UseCases.Dtos.Views;
using CSM.UseCases.Features.Countries.GetCitiesByCountryId;
using MediatR;

namespace CSM.UseCases.Features.Views.GetViews;

public sealed record GetViewsQuery() : IRequest<Result<List<ViewDto>>>;
