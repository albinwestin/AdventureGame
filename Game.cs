using Alcatraz.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Dynamic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

using static Alcatraz.Model.Player;

namespace Alcatraz
{
    public class Game
    {

        
        public int StartGame { get; set; }
        public Player Player { get; set; } = new Player();

        public List<Level> Levels { get; set; } = new List<Level>();
       
        public void debugSeed()
        {



            Player.Name = "Albin";
            Player.CurrentLevelId = 5;
            Player.CurrentLocationId = 1;

            Player.PlayerItems = new List<Item>();





            var watch = new Item();
            watch.Id = 1;
            watch.ItemName = "Watch";
            Player.PlayerItems.Add(watch);


            var sharpner = new Item();
            sharpner.Id = 3;
            sharpner.ItemName = "Sharpner";
            Player.PlayerItems.Add(sharpner);

            var sharpshiftkey = new Item();
            sharpshiftkey.Id = 4;
            sharpshiftkey.ItemName = "Sharp shiftkey";
            Player.PlayerItems.Add(sharpshiftkey);

            //var rock = new Item();
            //rock.Id = 5;
            //rock.ItemName = "Rock";
            //Player.PlayerItems.Add(rock);


            //Sharp shiftkey to open the door

            FourthEncounter();


        }

        public Game()
        {

            Level.CreateLevels(Levels);

            Introduction();

           

        }


        public string PlayerInputs(string input)
        {
            if (Player != null && Player.PlayerItems != null)
            {


                foreach (var item in Player.PlayerItems)
                {
                    if (item.ItemName.ToLower() == input.ToLower())
                    {


                        if (input.ToLower() == "watch")
                        {
                            var x = Player.GetItem(input);
                            Console.WriteLine(x);

                            return input;
                        }

                    }

                }
                if (input.ToLower() == "inventory")
                {
                    while (true)
                    {
                        Console.WriteLine("-----------------");

                        foreach (var item in Player.PlayerItems)
                        {

                            Console.WriteLine($"{item.Id}      {item.ItemName}");

                        }
                        Console.WriteLine("To combine items enter their respective ids together ex. 23");
                        Console.WriteLine("To drop items type 'drop' and specific item id");
                        Console.WriteLine("To leave inventory type exit");
                        Console.WriteLine("-----------------");
                        var invInput = Console.ReadLine();

                        int combineNum = 0;
                        var invInputNumbers = int.TryParse(invInput, out combineNum);

                        if (invInput.ToLower() == "exit")
                        {
                            return "exit";

                        }
                        else if (invInputNumbers == true)
                        {

                            CombineItem(invInput);
                            continue;
    
                        }else if (invInput.ToLower().Contains("drop")){
                            Drop(invInput);
                            continue;
                        }
                        else
                        {
                            continue;
                        }




                    }


                }
            }
            else
            {
                return input;
            }

            return input;


        }
        public string CombineItem(string input)
        {
            //kombinationer

            foreach (var item in Player.PlayerItems.ToList())
            {


                if (input == "23")
                {


                    var sharpkey = new Item();

                    sharpkey.ItemName = "Sharp shiftkey";
                    sharpkey.Id = 4;
                    Console.WriteLine("Sharp shiftkey created");


                    var olditem = Player.PlayerItems.SingleOrDefault(x => x.Id == 2);

                    if (olditem != null)
                    {
                        Player.PlayerItems.Remove(olditem);
                        Player.PlayerItems.Add(sharpkey);
                        Console.WriteLine("New item has been added to you're inventory");

                        break;


                    }
                    else
                    {

                        Console.WriteLine("combination might be wrong");



                    }


                }

            }

           


            return input;

        }


        public void Drop (string input)
        {
            while (true)
            {

                var dropComand = Regex.Replace(input, @"\s+", "");


                if (dropComand == "drop1")
                {
                    var resultString = Regex.Match(input, @"\d+").Value;
                    var num = int.Parse(resultString);
                    var itemDrop = Player.PlayerItems.Where(x => x.Id == num).SingleOrDefault();
                    Player.DropItem(num);
                    Console.WriteLine("You dropped you're watch");

                    break;
                }
                if (dropComand == "drop3")
                {
                    var resultString = Regex.Match(input, @"\d+").Value;
                    var num = int.Parse(resultString);
                    var itemDrop = Player.PlayerItems.Where(x => x.Id == num).SingleOrDefault();
                    Player.DropItem(num);
                    Console.WriteLine("you dropped sharpener");
                    break;
                }
                if (dropComand == "drop4")
                {
                    var resultString = Regex.Match(input, @"\d+").Value;
                    var num = int.Parse(resultString);
                    var itemDrop = Player.PlayerItems.Where(x => x.Id == num).SingleOrDefault();
                    Player.DropItem(num);
                    Console.WriteLine("You dropped sharp shiftkey");
                    break;
                }
                else
                {
                    Console.WriteLine("cant drop something you dont have");
                    break;
                }
            }
        }



        public void Introduction()
        {

            Player.CurrentLevelId = 1;
            Player.CurrentLocationId = 1;
            Player.PlayerItems = new List<Item>();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Escape from Alcatraz");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Create you're character here, enter a name:");
            var namn = PlayerInputs(Console.ReadLine());
            Player.Name = namn;

            Console.WriteLine("Now the game begins..." + Player.Name);

            Console.WriteLine("To get to the first Inspection you need to 'Walk', type walk");
            while (true)
            {
                var input = PlayerInputs(Console.ReadLine());
                if (input.ToLower() == "walk")
                {
                    Console.WriteLine("You're now walking into the most feared prison in the world called Alcatraz ");
                    Console.WriteLine("Not a single person has ever escaped from here, will you be the first one");
                    Thread.Sleep(3500);
                    FirstEncounter();
                    break;

                }
                else
                {
                    Console.WriteLine("You're pherhaps not ready yet, You need to type walk");

                }

            }


        }
        public void FirstEncounter()
        {



            //
            var level = Levels.Where(row => row.Id == Player.CurrentLevelId).FirstOrDefault();
            var location = level.Locations.Where(row => row.Id == Player.CurrentLocationId).FirstOrDefault();
            Thread.Sleep(2500);
            Console.WriteLine("Walking to the inspection...");
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(level.LevelName);
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("You made it into the inspection room, From here you're all by yourself ");
            Console.WriteLine(location.Description);

            Console.WriteLine("The head guard looks at you and demands that you strip of all of you're clothes");
            Console.WriteLine("So he can let you enter the prison the choice is yours");

            while (true)
            {
                Console.WriteLine("You can either 'Accept' or 'Spit' at him");
                var sinput = PlayerInputs(Console.ReadLine());
                if (sinput.ToLower() == "spit")
                {
                    Console.WriteLine("The guard yells ''WHAT are you doing you pig''");
                    Console.WriteLine("Meanwhile the other prisoners laughs");
                    continue;
                }
                else if (sinput.ToLower() == "accept")
                {

                    string response = "";
                    while (true)
                    {

                        if (response.ToLower() == "watch")
                            break;

                        var haveWatch = Player.PlayerItems.SingleOrDefault(x => x.Id == 1);
                        if (haveWatch == null)
                        {
                            Console.WriteLine("The guards does a fullbody search and lets you have you're watch");


                            var watch = new Item();
                            watch.Id = 1;
                            watch.ItemName = "Watch";
                            Player.PlayerItems.Add(watch);
                        }


                        Console.WriteLine("To use the watch type Watch");

                        response = PlayerInputs(Console.ReadLine());
                        if (response.ToLower() != "watch")
                        {
                            Console.WriteLine("type watch to see the time");
                            continue;
                        }
                        Console.WriteLine("You now have the ability to always check time");
                        Console.WriteLine("Press any key to contiue");
                        PlayerInputs(Console.ReadLine());
                    }
                    Console.Clear();
                    //TODO gå vidare 
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("You and the other prisoners are taken to the prison cells");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("You enter you're cell and a voice from the cell beside asks what time is it, mabey use the watch... ");
                    string watchused = "";
                    while (true)
                    {


                        watchused = PlayerInputs(Console.ReadLine());
                        if (watchused.ToLower() == "watch")
                        {
                            Console.WriteLine($"Thanks {Player.Name } i am you're roommate John! ");
                            Console.WriteLine("Lets talk in the morning im going to bed ");
                            Thread.Sleep(2000);
                            Console.WriteLine("You're also tired and drift away in deep slumber...");
                            Player.CurrentLevelId = 2;
                            Player.CurrentLocationId = 1;
                            Thread.Sleep(3000);
                            SecondEncounter();
                        }
                        else
                        {
                            Console.WriteLine("use the watch to help you're you the person from the cell beisde you");
                        }

                    }



                }
                else
                {
                    Console.WriteLine("You need to type accept to continue");
                }



            }





        }
        public void SecondEncounter()
        {

            var level = Levels.Where(row => row.Id == Player.CurrentLevelId).FirstOrDefault();
            var location1 = level.Locations.Where(row => row.Id == Player.CurrentLocationId).FirstOrDefault();
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(level.LevelName);
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(location1.Description);

            Console.WriteLine("Do you want to walk to the tables ?");
            while (true)
            {
                var input = PlayerInputs(Console.ReadLine());
                if (input.ToLower() == "walk")
                {

                    Console.WriteLine("Walking to the tables...");
                    break;

                }
                else
                {
                    Console.WriteLine("Stand here all day if you want, but you wont get out of here, typ walk to get out of here");
                    continue;
                }
            }

            Console.WriteLine("One inmate bangs his plate on you're side of the table and demands a fight");
            Console.WriteLine("Here you can either 'Fight the inmate' or 'Call on the guards'");
            Console.WriteLine("Type Punch to start the fight");

            while (true)
            {

                var input = PlayerInputs(Console.ReadLine());
                if (input.ToLower() == "punch")
                {

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You punched the other inmate");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("He drops to the floor and an he drops to floor losing consucionsess");
                    Thread.Sleep(2500);
                    Console.WriteLine("You see a shift key in his right pocket pick it up");
                    Console.WriteLine("Type pick to steal the item");

                    while (true)
                    {
                        var input2 = PlayerInputs(Console.ReadLine());
                        if (input2.ToLower() == "pick")
                        {



                            var shiftkey = new Item();
                            shiftkey.Id = 2;
                            shiftkey.ItemName = "Shiftkey";
                            Player.PlayerItems.Add(shiftkey);
                            Console.WriteLine("Nicely done you now have an " + shiftkey.ItemName);
                            Player.CurrentLevelId = 3;
                            Player.CurrentLocationId = 1;
                            Thread.Sleep(3000);
                            ThirdEncounter();
                            break;

                        }
                        else
                        {
                            Console.WriteLine("Type pick");
                            continue;
                        }
                    }
                


                }
                else
                {
                    Console.WriteLine("Here you need to take action calling on guards will only set you back");
                    continue;
                }
            }

        }
        public void ThirdEncounter()
        {
            Console.Clear();
            var level = Levels.Where(row => row.Id == Player.CurrentLevelId).FirstOrDefault();
            var location2 = level.Locations.Where(row => row.Id == Player.CurrentLocationId).FirstOrDefault();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(level.LevelName);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(location2.Description);


            Console.WriteLine("Inside the isolation you search to the room for a way out ");
            Console.WriteLine("You see a locked ventilation system at the top of the room");
            Console.WriteLine("try using the shiftkey to open the ventilation");
            Console.WriteLine("To use the shiftkey type open");


            while (true)
            {
                var open = PlayerInputs(Console.ReadLine());

                if (open == "Open".ToLower())
                {
                    Console.WriteLine("The door is locked you need to sharp the shiftkey");

                    break;
                }
                else
                {
                    Console.WriteLine("To open the ventialtion shaft, type Open");


                    continue;
                }

            }
            Console.WriteLine("You look around the floor and you trip on a sharpener");
            var sharpener = new Item();
            sharpener.Id = 3;
            sharpener.ItemName = "Sharpener";
            Player.PlayerItems.Add(sharpener);

            Console.WriteLine("You pick up the sharpener and add it to you're inventory");
            Console.WriteLine("To create items type 'inventory' to see what items you've gathered so far");
            while (true)
            {

                var input = PlayerInputs(Console.ReadLine());
                if (input.ToLower() == "exit")
                {

                    var sharpKeyExist = Player.PlayerItems.SingleOrDefault(row => row.Id == 4);
                    if (sharpKeyExist == null)
                    {
                        Console.WriteLine("You must combine the items");
                        PlayerInputs("inventory");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("use the sharp shiftkey to open the ventilation shaft, type open");
                        var open = PlayerInputs(Console.ReadLine());
                        while (true)
                        {
                            if (open.ToLower() == "open")
                            {
                                Console.WriteLine("The vent is now open");

                                Player.CurrentLevelId = 3;
                                Player.CurrentLocationId = 1;
                                Thread.Sleep(3000);
                                FourthEncounter();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Type open to continue");
                            }
                        }


                    }


                }
                else
                {
                  
                    continue;
                }
            }




            // I denna level ska man kunna slå ihop item + item för att få ett finished item som går att använda på 
            // t.e.x En lucka för att fly isloering....
            // någon typ av level där man måste droppa ett föremål kanske en utgång som kräver att man släpper
            //en bla för att komma igenom

        }
        public void FourthEncounter()
        {
            Console.Clear();

            var level = Levels.Where(row => row.Id == Player.CurrentLevelId).FirstOrDefault();
            var location = level.Locations.Where(row => row.Id == Player.CurrentLocationId).FirstOrDefault();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(level.LevelName);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(location.Description);


            Console.WriteLine("Your mission is to crawl to the roof of the prison to escape");
            Console.WriteLine("As you're crawling you notice that the shaft gets smaller and smaller");
            Console.WriteLine("You notice a ladder that leads to the roof");

            Console.WriteLine("To pass through you might have to drop an item check in your inventory for somehting too drop");


            while (true)
            {
                var input = PlayerInputs(Console.ReadLine());
                if (input.ToLower() == "exit")
                {
                    if (Player.PlayerItems.Count() == 2)
                    {
                        Console.WriteLine("You can now climb the ladder to the roof");
                        while (true)
                        {
                            Console.WriteLine("Type in climb to get to the roof");
                            var input2 = PlayerInputs(Console.ReadLine());
                            if (input2.ToLower() == "climb")
                            {
                                Console.WriteLine("You're now climbing to the roof and all that is left is to open the door to freedom");
                                Thread.Sleep(3000);
                                Player.CurrentLevelId = 5;
                                Player.CurrentLocationId = 1;

                                EndPoint();
                                break;
                            }
                            else
                            {
                                continue;

                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("you must drop an item type inventory to drop ");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("type inventory to check what items you can drop");
                }


            }


        }

        public void EndPoint()
        {
            Console.Clear();
            var level = Levels.Where(row => row.Id == Player.CurrentLevelId).FirstOrDefault();
            var location = level.Locations.Where(row => row.Id == Player.CurrentLocationId).FirstOrDefault();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(level.LevelName);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(location.Description);

            

            while (true)
            {
                Console.WriteLine("To escape type 'escape' and finish the game");
                var input = PlayerInputs(Console.ReadLine());
                if (input.ToLower() == "escape")
                {
                    Console.WriteLine("Game Over");
                    Thread.Sleep(4000);
                    Environment.Exit(0);

                    break;
                    
                }
                else
                {
                    continue;
                }
            }

            Console.ReadLine();



        }
    }
}


