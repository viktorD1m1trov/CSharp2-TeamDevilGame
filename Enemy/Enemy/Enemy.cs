﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;

namespace Enemy
{
    class Enemy
    {

        //Character details for left, right , up, down(and soon diagonal)
        static public char[,] Details(int i)
        {
            switch (i)
            {
                case 1:
                    {
                        char[,] bigEnemeyUp = new char[8, 5]
                        {
                            {'*','_','^','_','*'},
                            {' ','*','*','*',' '},
                            {'-','|','*','|','-'},
                            {'-','|','*','|','-'},
                            {'-','|','*','|','-'},
                            {' ',' ','|',' ',' '},
                            {' ',' ','|',' ',' '},
                            {' ',' ','V',' ',' '},
                        };
                        return bigEnemeyUp;
                    }
                case 2:
                    {
                        char[,] bigEnemeyDown = new char[8, 5]
                        {
                            {' ',' ','^',' ',' '},
                            {' ',' ','|',' ',' '},
                            {' ',' ','|',' ',' '},
                            {'-','|','*','|','-'},
                            {'-','|','*','|','-'},
                            {'-','|','*','|','-'},
                            {' ','*','*','*',' '},
                            {'*','-','v','-','*'},
                        };
                        return bigEnemeyDown;
                    }
                case 3:
                    {
                        char[,] bigEnemeyRight = new char[5, 12]
                        {
                            {' ',' ',' ',' ','|',' ','|',' ','|',' ',' ','*'},
                            {' ',' ',' ',' ',' ',' ','-','-','-',' ','*','|'},
                            {'<','-','-','-','*','*','*','*','*',' ','*','>'},
                            {' ',' ',' ',' ',' ',' ','-','-','-',' ','*','|'},
                            {' ',' ',' ',' ','|',' ','|',' ','|',' ',' ','*'},
                        };
                        return bigEnemeyRight;
                    }
                case 4:
                    {
                        char[,] bigEnemeyLeft = new char[5, 12]
                        {
                            {'*',' ',' ','|',' ','|',' ','|',' ',' ',' ',' '},
                            {'|','*',' ','-','-','-','-','-',' ',' ',' ',' '},
                            {'<','*',' ','*','*','*','*','*','-','-','-','>'},
                            {'|','*',' ','-','-','-','-','-',' ',' ',' ',' '},
                            {'*',' ',' ','|',' ','|',' ','|',' ',' ',' ',' '},

                        };
                        return bigEnemeyLeft;
                    }
                case 5:
                    {
                        char[,] bigEnemeySide = new char[10, 11]
                        {
                            {' ',' ',' ',' ',' ',' ','*',' ',' ',' ',' '},
                            {' ',' ',' ',' ',' ','*',' ',' ',' ',' ',' '},
                            {' ',' ',' ',' ','-','/','*',' ','*',' ',' '},
                            {' ',' ',' ','-','/',' ','*','*',' ','*',' '},
                            {' ',' ','-','/',' ','*','/','-','*',' ','*'},
                            {' ',' ','/',' ','*','/','-',' ',' ',' ',' '},
                            {' ',' ',' ','*','/','-',' ',' ',' ',' ',' '},
                            {' ',' ','/','/',' ',' ',' ',' ',' ',' ',' '},
                            {'<','/',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                            {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '},
                        };
                        return bigEnemeySide;
                    }
                default:
                    {
                        return null;
                    }
            }
        }
        public Point coordiantes = new Point();
        public Color color = new Color();
        //Character Drawer
        public static void Draw(int x, int y, int direction, ConsoleColor color = ConsoleColor.Yellow)
        {
            char[,] test = Details(direction);

            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            for (int i = 0; i < test.GetLength(0); i++)
            {
                for (int j = 0; j < test.GetLength(1); j++)
                {
                    Console.Write(test[i, j]);
                }
                y++;
                Console.SetCursorPosition(x, y);
            }
        }
        //Testing ground
        static void Main(string[] args)
        {     
            Random ran = new Random();
            Console.OutputEncoding = Encoding.UTF8;
            Console.BufferWidth = Console.WindowWidth = 30;
            Console.BufferHeight = Console.WindowHeight = 50;

            int x = 20;
            int y = 40;
            int direction = 1;
            while (true)
            {
                Draw(x, y, direction);
                x++;
                y--;
                if (y <= 0)
                {
                    y = Console.BufferHeight - 10;
                    Console.SetCursorPosition(x, y);
                }
                else if (y > Console.BufferHeight - 10)
                {
                    y = 0;
                    Console.SetCursorPosition(x, y);
                }
                else if (x <= 0)
                {
                    y = Console.BufferHeight - 9;
                    x = 9;
                    Console.SetCursorPosition(x, y);
                }
                else if (x > Console.BufferWidth - 10)
                {
                    x = 0;
                    Console.SetCursorPosition(x, y);
                }
                Thread.Sleep(250);
                Console.Clear();
            }
        }
        //Direction (under construction)
    }
}


