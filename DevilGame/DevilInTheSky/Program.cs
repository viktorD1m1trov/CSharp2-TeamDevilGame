using System;
using System.Threading;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Media;

namespace DevilInTheSky
{
    class Program
    {

        // game settings
       static public int difficultyLevel = 10;
       static public int gameSpeed = 100;
       static public int score = 0;
      static  public int lifes = 6;

      static public string currentName = string.Empty;
      static SoundPlayer devilDie = new SoundPlayer(@"DevilDeath.wav");
      static public int direction = 0;

        static void Main()
        {



            StartScreen menu = new StartScreen();
            menu.PlaySound();
            while (!menu.StartScreenMenu())
            {
                Thread.Sleep(100);
            }
           
                
          

              //  Set Console settings  
       
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BufferHeight= Console.WindowHeight = 100;   
            Console.BufferWidth = Console.WindowWidth = 140;


            // define all objects

            GameFrame frame = new GameFrame();
            List<Bullet> bullets = new List<Bullet>();
            List<Monsters> monsters = new List<Monsters>();
            Devil k = new Devil(new Point(30, 20), 2, 0); 
            List<Object> allObjects = new List<Object>();


            // define all random variables
            Random insertRandomMonster = new Random();
            Random monsterTypeSpeed = new Random();         
            Random monsterColor = new Random();



            frame.PrintFrame();                                                                  


            while (true)
            {
                frame.UpdateData(score,lifes);                                                   
    
                // add devil to render
                allObjects.Add(k);

                // set difficult
                string difficulty = frame.elapsedTime.ToString("mm\\:ss");
                if (difficulty == "01:00" || difficulty == "02:00" || difficulty == "03:00")
                {
                    difficultyLevel += 2;
                   
                }
  
                if(insertRandomMonster.Next(1,101)<=difficultyLevel)//% probability to insert new asteroid;
                {
                    ConsoleColor monColor = new ConsoleColor();
                    switch(monsterColor.Next(1,4))
                    {
                        case 1:{
                            monColor = ConsoleColor.Yellow;
                            break;
                        }
                        case 2:
                            {
                                monColor = ConsoleColor.Green;
                                break;
                            }
                        case 3:
                            {
                                monColor = ConsoleColor.White;
                                break;
                            }

                    }

                    monsters.Add(new Monsters(monColor, monsterTypeSpeed.Next(1, 4), Console.BufferHeight - 5, Console.WindowWidth - 30, monsterTypeSpeed.Next(1, 4)));
                    
                }

                    for (int i = 0; i < monsters.Count;i++ )
                    {
                        monsters[i].moveMonster();
                        if (!(CheckInFrameMonster(monsters[i], Console.WindowWidth - 30, Console.BufferHeight - 5)))
                          {
                            monsters.Remove(monsters[i]);
                            if (monsters.Count == 0)
                                break;
                        }
                        
                    }

                    allObjects.Add(monsters);
                      
                   
                   
                    // change the direction if it's necessary
                ConsoleKeyInfo secondPressedKey = new ConsoleKeyInfo();
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo firstPressedKey = Console.ReadKey(true);

                    // pause the game;
                    if(firstPressedKey.KeyChar=='p')
                    {
                        bool wait = true;
                        while(wait)
                        {
                            if (Console.KeyAvailable)
                                if (Console.ReadKey(true).KeyChar == 'p')
                                    break;
                            frame.watch.Stop(); 
                            Thread.Sleep(2000);
                        }
                    }

                    if(firstPressedKey.Key == ConsoleKey.Spacebar)
                    {
                        AddBullet(bullets,k, direction);
                    }

                    while (Console.KeyAvailable)
                    {
                        secondPressedKey = Console.ReadKey(true);
                        if(secondPressedKey.Key==ConsoleKey.Spacebar)
                        {
                            AddBullet(bullets, k, direction);

                        }
                    }

                    for( int i=0;i< bullets.Count;i++ )
                    {
                        bullets[i].moveBullet();

                            if(bullets.Count == 0)
                                break;
                        
                    }

                    direction = ChangeDirection(k,firstPressedKey, secondPressedKey);
                    k.moveDevil(direction);
                    k.position = CheckInFrameDevil(k, Console.WindowWidth - 30, Console.BufferHeight - 5);
                    

                }
                else
                {
                    for (int i = 0; i < bullets.Count; i++)
                    {
                        bullets[i].moveBullet();

                       
                            if (bullets.Count == 0)
                                break;

                    }

                    k.moveDevil(direction);
                    k.position = CheckInFrameDevil(k, Console.WindowWidth - 30, Console.BufferHeight - 5);
                }

                allObjects.Add(bullets);

                PrintRender(allObjects,k,monsters,bullets);
                if (DevilCollision(k, monsters))
                {
                    lifes--;
                    if (lifes == 0)
                    {
                        devilDie.Play();
                        PrintOnScreen(40, 36, "GAME OVER!", ConsoleColor.Yellow);
                
                        break;
                    }

                }
               
                Thread.Sleep(gameSpeed);
                allObjects.Clear();

                for (int i = 1; i < 95; i++)        //95 refresh rate
                {
                    Console.SetCursorPosition(1, i);
                    Console.Write(new String(' ', 109));
                }
               
            }
          
           
        }


        static bool DevilCollision(Devil dev, List<Monsters> monster)
        {
            int touch = 0;


            if (dev.direction >= 0 && dev.direction <= 3)
            {
                if (dev.direction == 2 || dev.direction == 3)
                    touch = 1;

                bool left = false;
                bool right = false;
                bool top = false;
                bool bottom = false;
                bool upleft = false;

                foreach (Monsters a in monster)
                {
                    top = ((a.currentPoint.X > dev.position.X && a.currentPoint.X < dev.position.X + 7) &&
                       ((a.currentPoint.Y + (a.radius * 2) > dev.position.Y + touch) && (a.currentPoint.Y + (2 * a.radius) < dev.position.Y + 7 - touch)));

                    bottom = ((a.currentPoint.X > dev.position.X && a.currentPoint.X < dev.position.X + 7) &&
                       ((a.currentPoint.Y > dev.position.Y + touch) && (a.currentPoint.Y < dev.position.Y + 7 - touch)));

                    left = ((a.currentPoint.Y > dev.position.Y && a.currentPoint.Y < dev.position.Y + 7) &&
                       ((a.currentPoint.X + (a.radius * 2) > dev.position.X) && (a.currentPoint.X + (a.radius * 2) < dev.position.X + 7)));

                    right = ((a.currentPoint.Y > dev.position.Y && a.currentPoint.Y < dev.position.Y + 7) &&
                       ((a.currentPoint.X > dev.position.X) && (a.currentPoint.X < dev.position.X + 7)));

                    upleft = (a.currentPoint.X + (2 * a.radius) > dev.position.X && a.currentPoint.X + (2 * a.radius) < dev.position.X + 7 && a.currentPoint.Y + (2 * a.radius) < dev.position.Y + 7 && a.currentPoint.Y + (2 * a.radius) > dev.position.Y);


                    //(a.currentPoint.X+ (2*a.radius)>=dev.position.X && a.currentPoint.X+(2*a.radius)<=dev.position.X+7 && a.currentPoint.Y <=dev.position.Y+7 && a.currentPoint.Y>=dev.position.Y)||
                    //(a.currentPoint.X>=dev.position.X && a.currentPoint.X<=dev.position.X+7 && a.currentPoint.Y <=dev.position.Y+7 && a.currentPoint.Y>=dev.position.Y)||
                    //(a.currentPoint.X>=dev.position.X && a.currentPoint.X<=dev.position.X+7 && a.currentPoint.Y+(2*a.radius) <=dev.position.Y+7 && a.currentPoint.Y+(2*a.radius)>=dev.position.Y))



                    if (top || left || right || bottom || upleft)
                    {
                        monster.Remove(a);
                        return true;
                    }

                }
            }
            return false;
        }

        static void PrintRender(List<Object> lst, Devil k, List<Monsters> monster, List<Bullet> bul)
        { //print


            foreach(Object obj in lst)
            {
                if(obj.Equals(k))
                {
                    string l = "";
                     for (int i = 0; i < 8; i++)
                    {

                        for (int j = 0; j < 8; j++)
                        {
                            l += k.imageDevil[i, j].ToString();
                        }
                        PrintOnScreen(k.position.X, k.position.Y + i, l, k.color);

                        

                        l = "";

                    }

                }
                else
                    if(obj.Equals(monster))
                    {
                        foreach(Monsters a in (List<Monsters>)obj)
                        {
                            a.printMonster();
                        }
                    }
                    else
                    {
                        foreach(Bullet b in (List<Bullet>)obj)
                        {
                            b.printBulet();
                        }
                    }
            }

        }

        static void AddBullet(List<Bullet> bulets,Devil k, int direction )
        {

            if (direction == 0)
            {
                bulets.Add(new Bullet(new Point(k.position.X + 4, k.position.Y), ConsoleColor.Magenta, direction));
            }
            else
                if (direction == 1)
                {
                    bulets.Add(new Bullet(new Point(k.position.X + 4, k.position.Y + 7), ConsoleColor.Magenta, direction));
                }
                else
                    if (direction == 2)
                    {
                        bulets.Add(new Bullet(new Point(k.position.X + 7, k.position.Y + 4), ConsoleColor.Magenta, direction));
                    }
                    else
                        if (direction == 3)
                        {
                            bulets.Add(new Bullet(new Point(k.position.X, k.position.Y + 4), ConsoleColor.Magenta, direction));
                        }
        }

        static int ChangeDirection(Devil k,ConsoleKeyInfo pressedKey, ConsoleKeyInfo pressedSecondKey)
        {
            if (pressedKey.Key == ConsoleKey.LeftArrow && pressedSecondKey.Key == 0)
            {
                return 3;
            }
            else
                if (pressedKey.Key == ConsoleKey.LeftArrow && pressedSecondKey.Key == ConsoleKey.UpArrow)
                {
                    return 5;
                }
                else
                    if (pressedKey.Key == ConsoleKey.LeftArrow && pressedSecondKey.Key == ConsoleKey.DownArrow)
                    {
                        return 7;
                    }


            if (pressedKey.Key == ConsoleKey.RightArrow && pressedSecondKey.Key == 0)
            {
                return 2;
            }
            else
                if (pressedKey.Key == ConsoleKey.RightArrow && pressedSecondKey.Key == ConsoleKey.UpArrow)
                {
                    return 4;
                }
                else
                    if (pressedKey.Key == ConsoleKey.RightArrow && pressedSecondKey.Key == ConsoleKey.DownArrow)
                    {
                        return 6;
                    }
            if (pressedKey.Key == ConsoleKey.UpArrow && pressedSecondKey.Key == 0)
            {
                return 0;
            }
            else
                if (pressedKey.Key == ConsoleKey.UpArrow && pressedSecondKey.Key == ConsoleKey.LeftArrow)
                {
                    return 5;
                }
                else
                    if (pressedKey.Key == ConsoleKey.UpArrow && pressedSecondKey.Key == ConsoleKey.RightArrow)
                    {
                        return 4;
                    }
            if (pressedKey.Key == ConsoleKey.DownArrow && pressedSecondKey.Key == 0)
            {
                return 1;
            }
            else
                if (pressedKey.Key == ConsoleKey.DownArrow && pressedSecondKey.Key == ConsoleKey.RightArrow)
                {
                    return 6;
                }
                else
                    if (pressedKey.Key == ConsoleKey.DownArrow && pressedSecondKey.Key == ConsoleKey.LeftArrow)
                    {
                        return 7;
                    }
            return k.direction;
        }

        static void PrintOnScreen(int x, int y, string str, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(str);
        }

        static bool CheckInFrameMonster(Monsters monster, int frameWidth, int frameHeight)
        {

            if (monster.currentPoint.X + (2 * monster.radius) >= frameWidth ||
                monster.currentPoint.X <= 0 ||
                (monster.currentPoint.Y + (2 * monster.radius)) >= frameHeight ||
                monster.currentPoint.Y <= 0)
            {

                return false;
            }
            else
                return true;

        } // functon that check if the  monster is inside the  frame 

        static Point CheckInFrameDevil(Devil dev, int frameWidth, int frameHeight)
        {
            Point teleport = new Point();
            if (dev.position.X + 7 >= frameWidth ||
                dev.position.X <= 0 ||
                (dev.position.Y + 7) >= frameHeight ||
                dev.position.Y <= 0)
            {


                if (dev.position.X + 8 >= frameWidth)
                {
                    teleport.X = 1;
                    teleport.Y = dev.position.Y;
                }
                else
                    if ((dev.position.Y + 8) >= frameHeight)
                    {
                        teleport.Y = 1;
                        teleport.X = dev.position.X;
                    }
                    else
                        if (dev.position.X <= 0)
                        {
                            teleport.X = frameWidth - 1 - 8;
                            teleport.Y = dev.position.Y;
                        }
                        else
                            if (dev.position.Y <= 0)
                            {
                                teleport.Y = frameHeight - 1 - 8;
                                teleport.X = dev.position.X;
                            }
                            else
                            {
                                teleport.Y = dev.teleportStartPoint.Y;
                                teleport.X = dev.teleportStartPoint.X;
                            }



                return teleport;
            }
            else
                return dev.position;

        }

 
      }

   }


