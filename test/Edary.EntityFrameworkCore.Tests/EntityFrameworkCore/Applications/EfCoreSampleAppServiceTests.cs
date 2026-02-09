using Edary.Samples;
using Xunit;

namespace Edary.EntityFrameworkCore.Applications;

[Collection(EdaryTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<EdaryEntityFrameworkCoreTestModule>
{

}
