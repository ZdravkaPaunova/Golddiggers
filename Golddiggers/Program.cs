using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golddiggers
{
    internal class Program
    {
        static char contentUnderPlayer = '▓';
        static int diamondCount=0;
        static int collectedDiamonds = 0;
        static int maxX = Console.WindowHeight;
        static int maxY = Console.WindowWidth;
        static void Main(string[] args)
        {
            int x = 0;
            int y = 0;
            
            Console.SetWindowSize(maxX, maxY);
            Console.SetBufferSize(maxX, maxY);
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            do
            {
                Console.Write("Въведете брой редове от 10 до 50: ");
                x = int.Parse(Console.ReadLine());
               
                if(x<10 || x>maxX)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Невалидни размери.");
                    Console.ResetColor();
                }
            } while (x<10 || x>maxX);
            do
            {
                Console.Write("Въведете брой колони от 10 до 50: ");
                y = int.Parse(Console.ReadLine());

                if (y<10 || y>maxY)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Невалидни размери.");
                    Console.ResetColor();
                }
            } while (y<10 || y>maxY);
            Console.Clear();
            char[,] field=GetFieldBounds(x, y);
            field=CreateField(field,x,y);

            int playerRow = 0;
            int playerCol = 0;


            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j] == '☺')
                    {
                        playerRow = i;
                        playerCol = j;
                    }
                }
            }
                while (true)
            {
                DrawField(field);

                if (collectedDiamonds == diamondCount)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Поздравления!");
                    Console.WriteLine("Събра всички диаманти!");
                    Console.ResetColor();
                    break;
                }


                (playerRow, playerCol) = MoveOurGuy(field, playerRow, playerCol);
            }


        }
        static char[,] GetFieldBounds(int x,int y)
        {
            if (x>=10 && y>=10 && x<=maxX && y<=maxY)
            {
                char[,] field = new char[x, y];
                return field;
            }
            else
            {
                Console.WriteLine("Невалиден размер.");
                return null;
            }
            
        }
        static char[,] CreateField(char[,] field, int m, int n)
        {
            Random rnd = new Random();
            diamondCount=(m*n)/10;
            for(int i = 0; i<m; i++)
            {
                for (int j = 0; j<n; j++)
                {
                    int chance = rnd.Next(1, 101);
                    if (chance<=40) field[i, j]= '▒';
                    else if (chance<=70) field[i, j]= '▓';
                    else if (chance<=90) field[i, j]= '♣';
                    else field[i, j]='٥';
                }
            }
            int r = 0;
            int c = 0;
            int postaveniDiamonds = 0;
            while (postaveniDiamonds<diamondCount)
            {
                r = rnd.Next(m);
                c = rnd.Next(n);
                if (field[r, c] != '♦')
                {
                    field[r, c]='♦';
                    postaveniDiamonds++;
                }
            }
            do
            {
                r = rnd.Next(m);
                c = rnd.Next(n);
            } while (field[r, c] == '♦');

          
            field[r, c] = '☺';

            return field;
        }
        static void DrawField(char[,] field)
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            for (int i = 0; i<field.GetLength(0); i++)
            {
                for(int j = 0; j<field.GetLength(1); j++)
                {
                    if (field[i, j] == '▒')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("▒");
                    }
                    else if (field[i, j] == '▓')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("▓");
                    }
                    else if (field[i, j] == '♣')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("♣");
                    }
                    else if (field[i, j] == '٥')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("٥");
                    }
                    else if (field[i, j] == '♦')
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("♦");
                    }
                    else if (field[i, j] == '☺')
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan; 
                        Console.Write("☺");
                    }
                }
                Console.WriteLine();
                

            }
           
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Диаманти: {collectedDiamonds}/{diamondCount}");

        }
        static (int, int) MoveOurGuy(char[,] field, int playerRow, int playerCol)
        {
            int rows = field.GetLength(0);
            int cols = field.GetLength(1);

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            int nextRow = playerRow;
            int nextCol = playerCol;

            if (keyInfo.Key == ConsoleKey.W || keyInfo.Key == ConsoleKey.UpArrow) nextRow--;
            if (keyInfo.Key == ConsoleKey.S || keyInfo.Key == ConsoleKey.DownArrow) nextRow++;
            if (keyInfo.Key == ConsoleKey.A || keyInfo.Key == ConsoleKey.LeftArrow) nextCol--;
            if (keyInfo.Key == ConsoleKey.D || keyInfo.Key == ConsoleKey.RightArrow) nextCol++;

            if (nextRow >= 0 && nextRow < rows && nextCol >= 0 && nextCol < cols)
            {
                field[playerRow, playerCol] = contentUnderPlayer;

                
                contentUnderPlayer = field[nextRow, nextCol];


                if (contentUnderPlayer == '♦')
                {
                    collectedDiamonds++;
                    contentUnderPlayer = '▓';
                }

                
                field[nextRow, nextCol] = '☺';
                return (nextRow, nextCol);
            }

            return (playerRow, playerCol);
        }

    }
}
