using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base;
using PokemonRules;
using BattleLib;
using BattleLibTest;

namespace ConsoleClient
{
    class Program
    {
        static PlayerClient pc1;
        static Player p1;
        static IBattleServer server;
        static void printInfo(ClientInfo info)
        {
            Console.WriteLine(info.ClientName);
            Console.WriteLine(info.CharName + ": HP:" + info.CurrentHP + " Status: ?");
            Console.WriteLine();
        }
        //static void actionEventHandler(object sender, ActionEventArgs args)
        //{
        //    var source = observer.getInfo(args.Source).First().CharName;
        //    var target = observer.getInfo(args.Target).First().CharName;

        //    Console.WriteLine("Some action from " + source + " hit " + target + ".\n");
        //}

        //static void exitEventHandler(object sender, ExitEventArgs args)
        //{
        //    var source = observer.getInfo(args.Id).First().ClientName;

        //    Console.WriteLine(source + " left.\n");
        //}

        //static void newTurnHandler(object sender, EventArgs args)
        //{
        //    Console.WriteLine("New turn!\n");
        //}

        //static void newCharEventHandler(object sender, NewCharEventArg args)
        //{
        //    var info = observer.getInfo(args.Id).First();

        //    Console.WriteLine(info.ClientName + " uses new pkm " + info.CharName + ".\n");
        //}

        static void handleAttack(WaitForInputArgs args)
        {
            int i = 1;
            Console.WriteLine("Chose a move:");
            foreach (var move in args.Current.Moves)
            {
                Console.WriteLine(i + ": " + move.Data.Name);
                i++;
            }
            int limit = i;
            int answer = 0;

            while (true)
            {
                Console.Write("Input: ");
                var str = Console.ReadLine();
                try
                {
                    answer = int.Parse(str);
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input");
                    continue;
                }

                if (answer > 0 && answer <= limit)
                    break;
                else
                    Console.WriteLine("Invalid input");
            }
            args.Client.Command = args.Client.MoveCommand(args.Current.Moves[answer], pc1.searchTarget());
        }

        static void handleChange(WaitForInputArgs args)
        {
            var newPkm =  (from pkm in p1._pkm
                    where !pkm.IsKO() && pkm != args.Current
                    select pkm).FirstOrDefault();

            if (newPkm == null)
                newPkm = args.Current;

            args.Client.Command = args.Client.ChangeCommand(newPkm);
        }


        static void waitForInputHandler(object sender, WaitForInputArgs args)
        {
            Console.WriteLine("Current status:\n");
            foreach (var info in server.GetCurrentState())
            {
                printInfo(info);
            }
            Console.WriteLine("What are you gonna do?");
            Console.WriteLine("1: Attack");
            Console.WriteLine("2: Change");
            Console.WriteLine("3: Escape");
            int answer = 0;
            while (true)
            {
                Console.Write("Input: ");
                var str = Console.ReadLine();
                try
                {
                    answer = int.Parse(str);
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input");
                    continue;
                }

                if (answer > 0 && answer < 4)
                    break;
                else
                    Console.WriteLine("Invalid input");
            }

            switch (answer)
            {
                case 1:
                    handleAttack(args);
                    break;
                case 2:
                    handleChange(args);
                    break;
                case 3:
                    args.Client.Command = args.Client.ExitCommand();
                    break;
            }

        }

        static void Main(string[] args)
        {
            if (args.Length != 1)
                return;

            var factory = new CharFactory(args[0], new Gen1CharRules(new MoveFactory("")));
            var ids = (from id in factory.getIds()
                       select id).Take(6);
            if (ids.Count() == 0)
                return;

            p1 = new Player();
            pc1 = new PlayerClient(p1);
            AIClient ac1 = new AIClient();
            foreach (var id in ids)
            {
                p1._pkm.Add(factory.getChar(id));
                ac1._chars.Add(factory.getChar(id));
            }

            server = new DefaultBattleServer(new TestScheduler(), new TestRules(), pc1, ac1);

            server.Start();
        }
    }
}
