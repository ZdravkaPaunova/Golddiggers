using System;
using System.Text;

namespace Golddiggers
{
    enum CellType
    {
        OurGuy,
        Diamond,
        Ground,
        Grass,
        Tree,
        Stone
    }

    internal class Program
    {
        static int diamondCount = 0;
        static int collectedDiamonds = 0;
        static CellType contentUnderPlayer = CellType.Ground;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

            int maxX = Console.WindowHeight - 5;
            int maxY = Console.WindowWidth - 5;

            Console.Clear();

            CellType[,] field = GetFieldBounds(maxX, maxY);
            if (field == null) return;

            int x = field.GetLength(0);
            int y = field.GetLength(1);

            diamondCount = CreateField(field, x, y);

            int playerRow = 0, playerCol = 0;
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (field[i, j] == CellType.OurGuy)
                    {
                        playerRow = i;
                        playerCol = j;
                    }
                }
            }
            Console.Clear();
            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
            DrawField(field);

            while (collectedDiamonds < diamondCount && keyInfo.Key != ConsoleKey.Escape)
            {
                keyInfo = Console.ReadKey(true);

                string direction = "";
                if (keyInfo.Key == ConsoleKey.W || keyInfo.Key == ConsoleKey.UpArrow) direction = "UP";
                if (keyInfo.Key == ConsoleKey.S || keyInfo.Key == ConsoleKey.DownArrow) direction = "DOWN";
                if (keyInfo.Key == ConsoleKey.A || keyInfo.Key == ConsoleKey.LeftArrow) direction = "LEFT";
                if (keyInfo.Key == ConsoleKey.D || keyInfo.Key == ConsoleKey.RightArrow) direction = "RIGHT";

                if (direction != "")
                {
                    (playerRow, playerCol) = MoveOurGuy(field, direction, playerRow, playerCol);
                }

                if (collectedDiamonds < diamondCount && keyInfo.Key != ConsoleKey.Escape)
                {
                    DrawField(field);
                }
            }

            Console.Clear();
            double percent = ((double)collectedDiamonds / diamondCount) * 100;

            Console.ForegroundColor = ConsoleColor.Yellow;
            if (collectedDiamonds == diamondCount)
            {
                Console.WriteLine("ПОЗДРАВЛЕНИЯ! Събрахте всички диаманти!");
            }
            else
            {
                Console.WriteLine("Играта приключи.");
            }
            Console.WriteLine($"Успешно събрани диаманти: {percent:F1}% ({collectedDiamonds} от {diamondCount})");
            Console.ResetColor();

            
        }

        static CellType[,] GetFieldBounds(int maxX, int maxY)
        {
            int x = 0, y = 0;

            do
            {
                Console.Write($"Въведете брой редове (между 11 и {maxX}): ");
                if (!int.TryParse(Console.ReadLine(), out x) || x <= 10 || x >= maxX)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Невалидно число или размер извън позволения диапазон.");
                    Console.ResetColor();
                    x = 0;
                }
            } while (x == 0);

            do
            {
                Console.Write($"Въведете брой колони (между 11 и {maxY}): ");
                if (!int.TryParse(Console.ReadLine(), out y) || y <= 10 || y >= maxY)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Невалидно число или размер извън позволения диапазон.");
                    Console.ResetColor();
                    y = 0;
                }
            } while (y == 0);

            return new CellType[x, y];
        }

        static int CreateField(CellType[,] field, int m, int n)
        {
            Random rnd = new Random();
            int totalDiamonds = (m * n) / 10;
            if (totalDiamonds == 0) totalDiamonds = 1;

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int chance = rnd.Next(1, 101);
                    if (chance <= 40) field[i, j] = CellType.Ground;
                    else if (chance <= 70) field[i, j] = CellType.Grass;
                    else if (chance <= 90) field[i, j] = CellType.Tree;
                    else field[i, j] = CellType.Stone;
                }
            }

            int postaveniDiamonds = 0;
            while (postaveniDiamonds < totalDiamonds)
            {
                int r = rnd.Next(m);
                int c = rnd.Next(n);
                if (field[r, c] != CellType.Diamond)
                {
                    field[r, c] = CellType.Diamond;
                    postaveniDiamonds++;
                }
            }

            int pr, pc;
            do
            {
                pr = rnd.Next(m);
                pc = rnd.Next(n);
            } while (field[pr, pc] == CellType.Diamond);

            contentUnderPlayer = field[pr, pc];
            field[pr, pc] = CellType.OurGuy;

            return totalDiamonds;
        }

        static void DrawField(CellType[,] field)
        {
            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    CellType cell = field[i, j];

                    if (cell == CellType.Ground)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("▒");
                    }
                    else if (cell == CellType.Grass)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("▓");
                    }
                    else if (cell == CellType.Tree)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("♣");
                    }
                    else if (cell == CellType.Stone)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("٥");
                    }
                    else if (cell == CellType.Diamond)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("♦");
                    }
                    else if (cell == CellType.OurGuy)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("☺");
                    }
                }
                Console.WriteLine();
            }

            Console.ResetColor();
            Console.WriteLine($"\nСъбрани диаманти: {collectedDiamonds} / {diamondCount}   ");
            Console.WriteLine("Натиснете [ESC] за отказване от играта.");
        }

        static (int, int) MoveOurGuy(CellType[,] field, string direction, int playerRow, int playerCol)
        {
            int rows = field.GetLength(0);
            int cols = field.GetLength(1);

            int nextRow = playerRow;
            int nextCol = playerCol;

            if (direction == "UP") nextRow--;
            if (direction == "DOWN") nextRow++;
            if (direction == "LEFT") nextCol--;
            if (direction == "RIGHT") nextCol++;

            if (nextRow >= 0 && nextRow < rows && nextCol >= 0 && nextCol < cols)
            {
                field[playerRow, playerCol] = contentUnderPlayer;
                contentUnderPlayer = field[nextRow, nextCol];

                if (contentUnderPlayer == CellType.Diamond)
                {
                    collectedDiamonds++;
                    contentUnderPlayer = CellType.Ground;
                }

                field[nextRow, nextCol] = CellType.OurGuy;
                return (nextRow, nextCol);
            }

            return (playerRow, playerCol);
        }
    }
}
