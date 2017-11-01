using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using GameEngine.Core.ECS;
using NUnit.Framework;

namespace GameEngine.Core.Tests.ECS
{
    public class MessageBusTests
    {
        private const string StringAction = "String";
        private const int IntAction = 10;

        public interface IHandler
        {
            void StringHandler(string action, IEntityManager entityManager);
            void ObjectHandler(object action, IEntityManager entityManager);
        }

        private IHandler _handlerFake;
        private readonly object _objectAction = new object();
        private IMessageBus _messageBus;

        [SetUp]
        public void SetUp()
        {
            _handlerFake = A.Fake<IHandler>();
            _messageBus = GetMessageBus();
        }

        private void WaitForActionProcessed(int numberOfActionsToWaitFor = -1)
        {
            numberOfActionsToWaitFor = numberOfActionsToWaitFor == -1 ? 0 : numberOfActionsToWaitFor;

            while (_messageBus.ActionCount > numberOfActionsToWaitFor)
            {
                
            }
            Thread.Sleep(500);
        }
        [Test]
        public void Registred_handler_is_called_when_action_is_dispatched()
        {
            RegisterHandlers();

            _messageBus.SendAction(StringAction);

            _messageBus.StartProcess();

            A.CallTo(() => _handlerFake.StringHandler(StringAction, A<IEntityManager>._))
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void Handlers_are_not_called_when_a_different_action_is_dispatched()
        {
            var messageBus = GetMessageBus();
            RegisterHandlers();

            messageBus.SendAction(IntAction);
            messageBus.StartProcess();

            A.CallTo(() => _handlerFake.StringHandler(A<string>._, A<IEntityManager>._))
                .MustNotHaveHappened();
            A.CallTo(() => _handlerFake.ObjectHandler(A<object>._, A<IEntityManager>._))
                .MustNotHaveHappened();
        }

        [Test]
        public void Handleres_can_be_unregistered_and_are_no_longer_called()
        {
            RegisterHandlers();
            _messageBus.UnregisterHandler<string>(_handlerFake.StringHandler);

            _messageBus.SendAction(StringAction);
            _messageBus.StartProcess();

            A.CallTo(() => _handlerFake.StringHandler(StringAction, A<IEntityManager>._))
                .MustNotHaveHappened();
        }

        [Test]
        public void Handlers_are_called_in_the_order_the_actions_are_dispatched()
        {
            RegisterHandlers();

            _messageBus.SendAction(StringAction);
            _messageBus.SendAction(_objectAction);
            _messageBus.StartProcess();


            A.CallTo(() => _handlerFake.StringHandler(A<string>._, A<IEntityManager>._))
                .MustHaveHappened()
                .Then(
                    A.CallTo(() => _handlerFake.ObjectHandler(A<object>._, A<IEntityManager>._)).MustHaveHappened()
                );
        }

        public class BlockingHandler : IHandler
        {
            public static BlockingHandler Instance()
            {
                var blockingHandler = A.Fake<BlockingHandler>();
                A.CallTo(() => blockingHandler.StringHandler(A<string>._, A<IEntityManager>._)).CallsBaseMethod();
                return blockingHandler;
            }

            private bool _block = true;

            public virtual void StringHandler(string action, IEntityManager entityManager)
            {
                while (_block)
                {
                }
            }

            public virtual void ObjectHandler(object action, IEntityManager entityManager)
            {
            }

            public void StopBlocking()
            {
                _block = false;
            }
        }

        [Test]
        public async Task Handlers_are_called_in_a_sync_way()
        {
            var blockingHandler = BlockingHandler.Instance();

            RegisterHandlers(blockingHandler);

            _messageBus.SendAction(StringAction);
            _messageBus.SendAction(_objectAction);

            var processTask = Task.Run(() => _messageBus.StartProcess());

            WaitForActionProcessed(1);

            A.CallTo(() => blockingHandler.ObjectHandler(_objectAction, A<IEntityManager>._))
                .MustNotHaveHappened();

            blockingHandler.StopBlocking();

            await processTask;

            A.CallTo(() => blockingHandler.ObjectHandler(_objectAction, A<IEntityManager>._))
                .MustHaveHappened();
        }

        private void RegisterHandlers(IHandler blockingHandler = null)
        {
            _messageBus.RegisterForAction<string>((blockingHandler ?? _handlerFake).StringHandler);
            _messageBus.RegisterForAction<object>((blockingHandler ?? _handlerFake).ObjectHandler);
        }

        private static IMessageBus GetMessageBus()
        {
            return new MessageBus(A.Fake<IEntityManager>());
        }
    }
}