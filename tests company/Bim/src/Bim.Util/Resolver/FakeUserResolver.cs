using System;

namespace Bim.Util.Resolver
{
    public interface IFakeUserResolver
    {
        Guid CurrentUserId { get; }
    }

    public class FakeUserResolver : IFakeUserResolver
    {
        //because user can be a constant guid (simulating user are already logged on)
        Guid IFakeUserResolver.CurrentUserId => new Guid("{B22AD448-8D55-338F-98B6-25FE69582828}");
    }
}
