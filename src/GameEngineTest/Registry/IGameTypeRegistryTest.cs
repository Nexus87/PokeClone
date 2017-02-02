using System.Reflection;
using GameEngine.TypeRegistry;
using NUnit.Framework;

namespace GameEngineTest.Registry
{
    public abstract class IGameTypeRegistryTest
    {
        protected abstract IGameTypeRegistry CreateRegistry();

        [SetUp]
        public void Setup()
        {
            GameEntityClass.Instances = 0;
            GameService.Instances = 0;
        }

        [GameType]
        public class GameType { }
        [Test]
        public void ScanAssembly_GameTypeAttribute_TypeCanBeResolved()
        {
            var registry = CreateRegistry();
            registry.ScanAssembly(Assembly.GetExecutingAssembly());

            var type = registry.ResolveType<GameType>();

            Assert.NotNull(type);
        }

        [GameType]
        public class GenericType<T> { }

        [Test]
        public void ScanAssembly_GameTypeAttributeGenericType_TypeCanBeResolved()
        {
            var registry = CreateRegistry();
            registry.ScanAssembly(Assembly.GetExecutingAssembly());

            var type = registry.ResolveType<GenericType<object>>();

            Assert.NotNull(type);
        }

        [Test]
        public void ScanAssembly_GameServiceAttribute_ServiceCanBeResolved()
        {
            var registry = CreateRegistry();
            registry.ScanAssembly(Assembly.GetExecutingAssembly());

            var service = registry.ResolveType<IGameService>();

            Assert.NotNull(service);
        }

        [Test]
        public void ResolveType_GameServiceAttribute_OnlyOneInstanceCreated()
        {
            var registry = CreateRegistry();
            registry.ScanAssembly(Assembly.GetExecutingAssembly());

            registry.ResolveType<IGameService>();
            registry.ResolveType<IGameService>();

            Assert.AreEqual(1, GameService.Instances);
        }

        public class ClassWithArgument
        {
            public string Argument;
            public ClassWithArgument(string argument)
            {
                Argument = argument;
            }
        }
    }
}
