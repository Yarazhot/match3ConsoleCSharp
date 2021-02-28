using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter size of matrix:");
            int N = Convert.ToInt32(Console.ReadLine());
            int[,] MyMx = new int[N, N];
            Functions.CreateMx(ref MyMx);
            Functions.ShowMx(MyMx);
            Console.WriteLine();
            while (Functions.Check(ref MyMx))
            {
                Functions.ShowMx(MyMx);
                Console.WriteLine();
                Functions.Falling(ref MyMx);
                Functions.ShowMx(MyMx);
                Console.WriteLine();
                Functions.AddGems(ref MyMx);
                Functions.ShowMx(MyMx);
                Console.WriteLine();
            }
            Functions.Cell Cell1;
            Functions.Cell Cell2;
            bool IsEnd = false;
            while (!IsEnd)
            {
                Console.Write("Enter the coordinates: ");
                Cell1.Row = Convert.ToInt32(Console.ReadLine());
                Cell1.Col = Convert.ToInt32(Console.ReadLine());
                Cell2.Row = Convert.ToInt32(Console.ReadLine());
                Cell2.Col = Convert.ToInt32(Console.ReadLine());
                Functions.Swap(ref MyMx, Cell1, Cell2);
                Functions.ShowMx(MyMx);
                Console.WriteLine();
                while (Functions.Check(ref MyMx))
                {
                    Functions.ShowMx(MyMx);
                    Console.WriteLine();
                    Functions.Falling(ref MyMx);
                    Functions.AddGems(ref MyMx);
                    Functions.ShowMx(MyMx);
                    Console.WriteLine();
                }
                Console.ReadKey();
            }
        }
    }
    class Functions
    {
        public static void CreateMx(ref int[,] FormMx)
        {
            Random Rnd = new Random();
            for (int i = 0; i <= FormMx.GetUpperBound(0); i++)
                for (int j = 0; j <= FormMx.GetUpperBound(1); j++)
                    FormMx[i, j] = Rnd.Next(1, 5);
        }
        public static void ShowMx(int[,] FormMx)
        {
            for (int i = 0; i <= FormMx.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= FormMx.GetUpperBound(1); j++)
                {
                    Console.Write($"{FormMx[i, j]} ");
                }
                Console.WriteLine();
            }
        }
        public struct Cell
        {
            public int Row;
            public int Col;
        }
        public static void Swap(ref int[,] SwapMx, Cell Cell1, Cell Cell2)
        {
            int buff = SwapMx[Cell1.Row, Cell1.Col];
            SwapMx[Cell1.Row, Cell1.Col] = SwapMx[Cell2.Row, Cell2.Col];
            SwapMx[Cell2.Row, Cell2.Col] = buff;
        }
        public static bool Check(ref int[,] FormMx)
        {
            Cell MyCell;
            Cell[] LocArr;
            bool Founded = false;
            for (MyCell.Row = 0; MyCell.Row <= FormMx.GetUpperBound(0); MyCell.Row++)
            {
                for (MyCell.Col = 0; MyCell.Col <= FormMx.GetUpperBound(1); MyCell.Col++)
                {
                    LocArr = RecVert(ref FormMx, MyCell);
                    if (LocArr.Length > 2)
                    {
                        DestroyGems(ref FormMx, LocArr);
                        Founded = true;
                    }
                    LocArr = RecHor(ref FormMx, MyCell);
                    if (LocArr.Length > 2)
                    {
                        DestroyGems(ref FormMx, LocArr);
                        Founded = true;
                    }
                }
            }
            return Founded;
        }
        public static Cell[] RecVert(ref int[,] FormMx, Cell MyCell)
        {
            if (MyCell.Row < FormMx.GetUpperBound(0))
            {
                if (Math.Abs(FormMx[MyCell.Row, MyCell.Col]) == Math.Abs(FormMx[MyCell.Row + 1, MyCell.Col]))
                {
                    MyCell.Row++;
                    Cell[] LocArr = RecVert(ref FormMx, MyCell);
                    Array.Resize(ref LocArr, LocArr.Length + 1);
                    MyCell.Row--;
                    LocArr[LocArr.GetUpperBound(0)] = MyCell;
                    return LocArr;
                }
                else
                    return new Cell[] { MyCell };
            }
            else
            {
                return new Cell[] { MyCell };
            }
        }

        public static Cell[] RecHor(ref int[,] FormMx, Cell MyCell)
        {
            if (MyCell.Col < FormMx.GetUpperBound(1))
            {
                if (Math.Abs(FormMx[MyCell.Row, MyCell.Col]) == Math.Abs(FormMx[MyCell.Row, MyCell.Col + 1]))
                {
                    MyCell.Col++;
                    Cell[] LocArr = RecHor(ref FormMx, MyCell);
                    Array.Resize(ref LocArr, LocArr.Length + 1);
                    MyCell.Col--;
                    LocArr[LocArr.GetUpperBound(0)] = MyCell;
                    return LocArr;
                }
                else
                    return new Cell[] { MyCell };
            }
            else
                return new Cell[] { MyCell };
        }

        public static void DestroyGems(ref int[,] FormMx, Cell[] FormArr)
        {
            for (int i = 0; i < FormArr.Length; i++)
                FormMx[FormArr[i].Row, FormArr[i].Col] = -1 * Math.Abs(FormMx[FormArr[i].Row, FormArr[i].Col]);
        }

        public static void Falling(ref int[,] FormMx)
        {
            int Buff;
            for (int j = 0; j <= FormMx.GetUpperBound(1); j++)
            {
                int n = 0;
                for (int k = FormMx.GetUpperBound(0); k > n; k--)
                    if (FormMx[k, j] < 0)
                    {
                        for (int i = k; i > 0; i--)
                        {
                            Buff = FormMx[i, j];
                            FormMx[i, j] = FormMx[i - 1, j];
                            FormMx[i - 1, j] = Buff;
                        }
                        k++;
                        n++;
                    }
            }
        }

        public static void AddGems(ref int[,] FormMx)
        {
            Random Rnd = new Random();
            for (int i = 0; i <= FormMx.GetUpperBound(0); i++)
                for (int j = 0; j <= FormMx.GetUpperBound(0); j++)
                    if (FormMx[i, j] < 0)
                        FormMx[i, j] = Rnd.Next(1, 5);
        }
    }
}