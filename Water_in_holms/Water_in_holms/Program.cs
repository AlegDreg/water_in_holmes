using System;

namespace Water_in_holms
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите высоты через запятую (пример: 0,3,5,0,2,0): ");

            string[] line = Delete_non_correct_items(Console.ReadLine().Replace(" ", "")).Split(',');
            Console.WriteLine();
            int[] holmes = Convert_mas_to_int(line);

            if(holmes.Length > 3*104)
            {
                Console.WriteLine("Слишком большая длина!");
                Main(args);
                return;
            }

            Start_search(holmes);

            Console.ReadKey();
        }

        static string Delete_non_correct_items(string line)//удаление некорректных значений
        {
            string[] k = line.Split(new char[] { ',' });

            string lines = "";
            string un_corrects = "";

            for (int s = 0; s < k.Length; s++)
            {
                int it;

                bool z = Int32.TryParse(k[s], out it);

                if (z && k[s] != "" && Convert.ToInt32(k[s]) >= 0 && Convert.ToInt32(k[s]) < 106)
                { 
                    lines += k[s] + ",";
                }
                else
                {
                    un_corrects += k[s] + " ";
                }
            }

            if (lines[lines.Length - 1] == ',')
            {
                lines = lines.Remove(lines.Length - 1);
            }

            if (un_corrects != "")
            {
                Console.WriteLine("\nУдалённые значения - " + un_corrects + "\n");
                Console.WriteLine("Полученная строка - " + lines +"\n");
            }

            return lines;
        }

        static int[] Convert_mas_to_int(string[] mas)//перевод в инт массив
        {
            int[] ar = new int[mas.Length];

            for(int i = 0; i<ar.Length;i++)
            {
                ar[i] = Convert.ToInt32(mas[i]);
            }

            return ar;
        }

        static void Start_search(int[] holms)
        {
            int max_height = 0;

            for(int i = 0; i < holms.Length; i++)
            {
                if (holms[i] > max_height)
                    max_height = holms[i];
            }

            int[,] arr = Create_area(holms, max_height);

            Find_sim_max(arr);

            int count = Find_count(arr);

            Print(arr);
            Console.WriteLine("\nКоличество клеток с водой - "+count);
        }

        static int Find_count(int[,] ar)//нахождение суммы воды 
        {
            int cou = 0;
            for (int i = ar.GetLength(0) - 1; i > -1; i--)////высота с конца
            {
                for (int j = 0; j < ar.GetLength(1); j++)//ширина с начала
                {
                    if (ar[i, j] == 2)
                    {
                        cou++;
                    }
                }
            }
            return cou;
        }

        static int[,] Create_area(int[] holms,int max_heig)//создание массива с заданными вершинами
        {
            int[,] area = new int[max_heig, holms.Length];

            for (int i = 0; i < area.GetLength(1); i++)
            {
                for (int j = 0; j < area.GetLength(0); j++)
                {
                    area[j, i] = 0;
                }

                if(holms[i] != 0)
                {
                    for(int j = 0; j < holms[i]; j++)
                    {
                        area[j,i] = 1;
                    }
                }
            }

            return area;
        }

        static void Print(int[,] holms)
        {
            for (int i = holms.GetLength(0)-1; i > -1; i--)
            {
                for (int j = 0; j < holms.GetLength(1); j++)
                {
                    if (holms[i,j] == 0)//пусто
                        Console.ForegroundColor = ConsoleColor.White;
                    if (holms[i, j] == 1)//земля
                        Console.ForegroundColor = ConsoleColor.Red;
                    if (holms[i, j] == 2)//вода
                        Console.ForegroundColor = ConsoleColor.Blue;

                    Console.Write("0");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        static void Find_sim_max(int[,] area)//заполнение водой
        {
            int first_point = -1;
            int second_point = -1;
            bool isFind_first_point = false;

            for (int i = area.GetLength(0) - 1; i > -1; i--)////высота с конца
            {
                for (int j = 0; j < area.GetLength(1); j++)//ширина с начала
                {
                    if(area[i,j] == 1)
                    {
                        if (!isFind_first_point)
                        {
                            first_point = j;
                            isFind_first_point = true;
                        }
                        else
                        {
                            second_point = j;

                            for (int z = first_point + 1; z < second_point; z++)
                                area[i, z] = 2;

                            first_point = j;
                            
                        }
                    }
                }

                first_point = -1;
                second_point = -1;
                isFind_first_point = false;
            }
        }
    }
}
