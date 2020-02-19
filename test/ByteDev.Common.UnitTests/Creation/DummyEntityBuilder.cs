using ByteDev.Common.Creation;

namespace ByteDev.Common.UnitTests.Creation
{
    internal class DummyEntityBuilder : Builder<DummyEntityBuilder, DummyEntity>
    {
        public static DummyEntityBuilder InMemory => new DummyEntityBuilder();

        public DummyEntityBuilder WithId(int id)
        {
            return With(entity => entity.Id = id);
        }

        protected override DummyEntity CreateEntity()
        {
            return new DummyEntity();
        }

        public void Clear()
        {
            ClearMutations();
        }
    }
}