using Xunit;

namespace MockU.Tests.Linq;

public class MockRepositoryQuerying
{
    public class GivenAStrictFactory

    
    {
        private readonly MockRepository repository;

        public GivenAStrictFactory()
        {
            repository = new MockRepository(MockBehavior.Strict);
        }

        [Fact]
        public void WhenQueryingSingle_ThenItIsStrict()
        {
            var foo = repository.OneOf<IFoo>();

            Assert.Throws<MockException>(() => foo.Do());
        }

        [Fact]
        public void WhenQueryingMultiple_ThenItIsStrict()
        {
            var foo = repository.Of<IFoo>().First();

            Assert.Throws<MockException>(() => foo.Do());
        }

        [Fact]
        public void WhenQueryingSingleWithProperty_ThenItIsStrict()
        {
            var foo = repository.OneOf<IFoo>(x => x.Id == "1");

            Assert.Throws<MockException>(() => foo.Do());

            Mock.Get(foo).Verify();

            Assert.Equal("1", foo.Id);
        }

        [Fact]
        public void WhenQueryingMultipleWithProperty_ThenItIsStrict()
        {
            var foo = repository.Of<IFoo>(x => x.Id == "1").First();

            Assert.Throws<MockException>(() => foo.Do());

            Mock.Get(foo).Verify();

            Assert.Equal("1", foo.Id);
        }

        [Fact]
        public void Can_override_behavior_and_create_loose_mock()
        {
            var foo = repository.OneOf<IFoo>(MockBehavior.Loose);
            Assert.Equal(MockBehavior.Loose, Mock.Get(foo).Behavior);
            _ = foo.Id;
        }
    }

    public class Strict_mocks

    
    {
        private readonly MockRepository repository;

        public Strict_mocks()
        {
            repository = new MockRepository(MockBehavior.Default);
        }

        [Fact]
        public void Strict_Of_will_throw_for_non_setup_property()
        {
            var foo = repository.Of<IFoo>(MockBehavior.Strict).First();
            Assert.Throws<MockException>(() => _ = foo.Name);
        }

        [Fact]
        public void Strict_Of_with_expression_will_throw_for_non_setup_property()
        {
            var foo = repository.Of<IFoo>(f => f.Id == default, MockBehavior.Strict).First();
            _ = foo.Id;
            Assert.Throws<MockException>(() => _ = foo.Name);
        }

        [Fact]
        public void Strict_OneOf_will_throw_for_non_setup_property()
        {
            var foo = repository.OneOf<IFoo>(MockBehavior.Strict);
            Assert.Throws<MockException>(() => _ = foo.Name);
        }

        [Fact]
        public void Strict_OneOf_with_expression_will_throw_for_non_setup_property()
        {
            var foo = repository.OneOf<IFoo>(f => f.Id == default, MockBehavior.Strict);
            _ = foo.Id;
            Assert.Throws<MockException>(() => _ = foo.Name);
        }
    }

    public interface IFoo
    {
        string Id { get; set; }
        string Name { get; set; }
        bool Do();
    }
}
