using CSM.Core.Features.Views;
using Microsoft.AspNetCore.Mvc;

namespace CSM.Web.Attributes;

public sealed class BinaryAuthorizeAttribute(ViewCode ViewCode, ActionType ActionType) : TypeFilterAttribute(typeof(BinaryAuthorizeAttribute))
{
}