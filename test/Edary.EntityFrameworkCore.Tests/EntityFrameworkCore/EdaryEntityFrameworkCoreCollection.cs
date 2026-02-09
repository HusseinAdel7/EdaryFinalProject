using Xunit;

namespace Edary.EntityFrameworkCore;

[CollectionDefinition(EdaryTestConsts.CollectionDefinitionName)]
public class EdaryEntityFrameworkCoreCollection : ICollectionFixture<EdaryEntityFrameworkCoreFixture>
{

}
