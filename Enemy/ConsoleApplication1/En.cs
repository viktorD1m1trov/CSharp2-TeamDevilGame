using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class En
    {
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
                            {' ',' ',' ',' ',' ','*',' ','*',' ',' ',' '},
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

        //Character Drawer
        public void Draw(int x, int y, int direction, ConsoleColor color = ConsoleColor.Yellow)
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
        public void Image(int n,int x, int direction)
        {
            string image;
            Draw(n, x, direction);
        }
    }
}
