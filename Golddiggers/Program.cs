using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golddiggers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int x = 0;
            int y = 0;
            do
            {
                Console.Write("Въведете брой редове от 10 до 50: ");
                y = int.Parse(Console.ReadLine());
               
                if(y<10 || y>50)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Невалидни размери.");
                    Console.ResetColor();
                }
            } while (y<10 || y>50);
            do
            {
                Console.Write("Въведете брой колони от 10 до 50: ");
                x = int.Parse(Console.ReadLine());

                if (y<10 || y>50)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Невалидни размери.");
                    Console.ResetColor();
                }
            } while (x<10 || x>50);
            char[,] field=GetFieldBounds(x, y);
            field=CreateField(field,x,y);
            DrawField(field);
        }
        static char[,] GetFieldBounds(int x,int y)
        {
            if (x>10 || y>10 || x<50 || y<50)
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
            int diamondCount=(m*n)/10;
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
            r=rnd.Next(m);
            c = rnd.Next(n);
            while (field[r, c]!='♦')
            {
                field[r, c]='☺';
            }
            return field;
        }
        static void DrawField(char[,] field)
        {
            Console.Clear();
            for(int i = 0; i<field.GetLength(0); i++)
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
                        Console.ForegroundColor = ConsoleColor.Cyan; // Цвят по избор
                        Console.Write("☺");
                    }
                }
                Console.WriteLine();
            }
            
        }
    }
}
