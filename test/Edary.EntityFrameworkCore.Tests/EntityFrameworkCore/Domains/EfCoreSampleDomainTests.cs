using Edary.Samples;
using Xunit;

namespace Edary.EntityFrameworkCore.Domains;

[Collection(EdaryTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<EdaryEntityFrameworkCoreTestModule>
{

}
