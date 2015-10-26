//
//  BattleServerTest.cs
//
//  Author:
//       Nexxuz0 <>
//
//  Copyright (c) 2015 Nexxuz0
//
//  This program is free software; you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation; either version 2 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
//
using Base;
using BattleLib;
using BattleLib.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BattleLibTest
{
    delegate void ApplyDelegate(Pokemon charakter);

	public class TestScheduler : ICommandScheduler {
        readonly List<IClientCommand> _commands = new List<IClientCommand>();

        public void AppendCommand(BattleLib.Interfaces.IClientCommand command)
        {
            _commands.Add(command);
        }

        public void AppendCommand(IEnumerable<BattleLib.Interfaces.IClientCommand> commands)
        {
            _commands.AddRange(commands);
        }

        public IEnumerable<BattleLib.Interfaces.IClientCommand> ScheduleCommands()
        {
            return _commands;
        }


        public void ClearCommands()
        {
            _commands.Clear();
        }
    }

    public class TestRules : IBattleRules
    {

        public bool CanEscape()
        {
            return true;
        }

        public bool CanChange()
        {
            return true;
        }

        public bool ExecMove(Pokemon source, Move move, Pokemon target)
        {
            return true;
        }
    }

    class TestClient1 : TestClient
    {
        public override IClientCommand RequestAction()
        {
            if (RoundCnt == 0)
                Command =MoveCommand(new Move(new MoveData()), 1);
            else
                Command = ExitCommand();

            return base.RequestAction();
        }
    }
    class TestClient2 : TestClient
    {
        public override IClientCommand RequestAction()
        {
            Command =  MoveCommand(new Move(new MoveData()), 1);
            return base.RequestAction();
        }
    }
	[TestFixture]
	public class BattleServerTest
	{
        Mock<AbstractClient> clientMock1 = new Mock<AbstractClient>();
        Mock<AbstractClient> clientMock2 = new Mock<AbstractClient>();
        Mock<ICommandScheduler> schedulerMock = new Mock<ICommandScheduler>();
        Mock<IBattleRules> rulesMock = new Mock<IBattleRules>();
        Mock<Pokemon> characterMock = new Mock<Pokemon>();
        Mock<IClientCommand> commandMock = new Mock<IClientCommand>();

        IBattleServer _server;
		[SetUp]
		public void init() {
            _server = new DefaultBattleServer(schedulerMock.Object, rulesMock.Object, clientMock1.Object, clientMock2.Object);
            commandMock.Setup(command => command.Execute(It.IsAny<ICommandReceiver>()));
            
            //Setup action
            clientMock1.Setup(client => client.RequestAction()).Returns(commandMock.Object);
            clientMock2.Setup(client => client.RequestAction()).Returns(commandMock.Object);

            //Setup character
            clientMock1.Setup(client => client.RequestCharacter()).Returns(characterMock.Object);
            clientMock2.Setup(client => client.RequestCharacter()).Returns(characterMock.Object);

            rulesMock.Setup(r => r.CanEscape()).Returns(true);
            rulesMock.Setup(r => r.CanChange()).Returns(true);
            rulesMock.Setup(r => r.ExecMove(It.IsAny<Pokemon>(), It.IsAny<Move>(), It.IsAny<Pokemon>())).Returns(true);
            
            clientMock1.ResetCalls();
            clientMock2.ResetCalls();
		}

        [Test, Timeout(2000)]
		public void NullCharakterTest() {

            clientMock1.Setup(client => client.RequestCharacter()).Returns(characterMock.Object);
            clientMock2.Setup(client => client.RequestCharacter()).Returns(() =>  null );

			_server.Start ();

            clientMock1.Verify(c => c.RequestAction(), Times.Never());
            clientMock2.Verify(c => c.RequestAction(), Times.Never());
            clientMock1.Verify(c => c.RequestCharacter(), Times.AtMostOnce());
            clientMock2.Verify(c => c.RequestCharacter(), Times.Once());
		}

		[Test]
        public void ConstructorExceptionTest()
        {
            var rules = rulesMock.Object;
            var scheduler = schedulerMock.Object;
            var client = clientMock1.Object;

            Assert.Throws<ArgumentNullException>(() => new DefaultBattleServer(scheduler, rules, null));
            Assert.Throws<ArgumentNullException>(() => new DefaultBattleServer(null, rules, client));
            Assert.Throws<ArgumentNullException>(() => new DefaultBattleServer(scheduler, null, client));
            Assert.Throws<ArgumentNullException>(() => new DefaultBattleServer(null, rules, null));
            Assert.Throws<ArgumentNullException>(() => new DefaultBattleServer(null, null, null));
            Assert.Throws<ArgumentNullException>(() => new DefaultBattleServer(scheduler, rules, null, null));
            Assert.Throws<ArgumentNullException>(() => new DefaultBattleServer(scheduler, rules, client, null));
            Assert.Throws<ArgumentNullException>(() => new DefaultBattleServer(scheduler, rules, null, client));
        }

        [Test, Timeout(2000)]
        public void InvalidCommandTest()
        {
            int roundCnt = 0;
            var invalidClient = (new Mock<AbstractClient>()).Object;
            List<IClientCommand> commands = new List<IClientCommand>();

            schedulerMock.Setup(s => s.AppendCommand(It.IsAny<IClientCommand>()))
                .Callback<IClientCommand>(command => commands.Add(command));
            schedulerMock.Setup(s => s.AppendCommand(It.IsAny<IEnumerable<IClientCommand>>()))
                .Callback<IEnumerable<IClientCommand>>(list => commands.AddRange(list));
            schedulerMock.Setup(s => s.ScheduleCommands()).Returns(commands);

            clientMock1.Setup(c => c.RequestAction()).Returns(() =>
            {
                switch (roundCnt)
                {
                    case 0:
                        roundCnt++;
                        return new MoveCommand(null, new Move(new MoveData()), 0);
                    case 1:
                        roundCnt++;
                        return new MoveCommand(invalidClient, new Move(new MoveData()), 0);
                    case 2:
                        roundCnt++;
                        return new ChangeCommand(null, characterMock.Object);
                    case 3:
                        roundCnt++;
                        return new ChangeCommand(invalidClient, characterMock.Object);
                    case 4:
                        roundCnt++;
                        return new ExitCommand(null);
                    case 5:
                        roundCnt++;
                        return new ExitCommand(invalidClient);
                }
                return null;
            }
            );

            while (roundCnt < 6)
            {
                commands.Clear();
                Assert.Throws<ArgumentException>(() => _server.Start());
            }
        }


        [Test, Timeout(2000)]
        public void NullCommandTest()
        {
            clientMock1.Setup(c => c.RequestAction()).Returns(() => null);

            Assert.Throws<InvalidOperationException>(() => _server.Start());

            clientMock1.Verify(c => c.RequestAction(), Times.Once());
            clientMock2.Verify(c => c.RequestAction(), Times.AtMostOnce());
        }

        [Test, Timeout(2000)]
        public void StartExceptionTest()
        {
            var rules = rulesMock.Object;
            var scheduler = schedulerMock.Object;
            var client = clientMock1.Object;

            var testServer = new DefaultBattleServer(scheduler, rules);
			Assert.Throws<InvalidOperationException> (testServer.Start);

            testServer = new DefaultBattleServer(scheduler, rules, client);
			Assert.Throws<InvalidOperationException> (testServer.Start);
        }

        [Test, Timeout(2000)]
        public void fullBattleTest()
        {
            List<IClientCommand> commands1 = new List<IClientCommand>();
            List<IClientCommand> commands2 = new List<IClientCommand>();
            int roundCnt = 0;

            commands1.Add(commandMock.Object);
            commands1.Add(commandMock.Object);

            commands2.Add(commandMock.Object);
            commands2.Add(new ExitCommand(clientMock1.Object));

            schedulerMock.Setup(s => s.AppendCommand(It.IsAny<IClientCommand>()));
            schedulerMock.Setup(s => s.AppendCommand(It.IsAny<IEnumerable<IClientCommand>>()));
            schedulerMock.Setup(s => s.ScheduleCommands()).Returns(() =>
            {
                if (roundCnt == 1)
                    return commands2;
                roundCnt++;
                return commands1;
            });
            _server.Start();


            clientMock1.Verify(c => c.RequestAction(), Times.Exactly(2));
            clientMock2.Verify(c => c.RequestAction(), Times.Exactly(2));
            clientMock1.Verify(c => c.RequestCharacter(), Times.Once());
            clientMock2.Verify(c => c.RequestCharacter(), Times.Once());
            
        }
	}
}

