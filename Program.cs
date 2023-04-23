using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace Ex7
{
    class Program
    {
        static string titles = "ID#Дата записи#Ф.И.О.#Возраст#Рост#Дата рождения#Место рождения";
        static string[] titlesArray = titles.Split('#');
        static Repository rep = new Repository();

        static void Main(string[] args)
        {
            if (!File.Exists(@"data.txt"))
            {
                using (StreamWriter sw = new StreamWriter("data.txt", true, Encoding.UTF8))
                    sw.WriteLine(titles);
                NextTask();
            }
            else
                NextTask();

            Console.ReadKey();
        }

        static void NextTask()
        {
            Console.WriteLine("1 - вывести данные по всем работникам\n2 - вывести данные по одному работнику");
            Console.WriteLine("3 - ввести данные нового работника\n4 - удалить сотрудника по ID\n5 - вывести данные в интервале дат");
            CheckKey(Console.ReadLine());
        }

        static void CheckKey(string key)
        {
            switch (key)
            {
                case "1":
                    ShowAllWorkers();
                    NextTask();
                    break;
                case "2":
                    ShowWorkerByID();
                    NextTask();
                    break;
                case "3":
                    AddWorker();
                    NextTask();
                    break;
                case "4":
                    DeleteWorker();
                    NextTask();
                    break;
                case "5":
                    ShowFromDate();
                    NextTask();
                    break;
                default:
                    Console.WriteLine("Неверная команда");
                    CheckKey(Console.ReadLine());
                    break;
            }
        }

        static void ShowAllWorkers()
        {
            Print(rep.GetAllWorkers());
            Console.WriteLine("\n");
        }

        static void ShowWorkerByID()
        {
            Console.WriteLine("ID: ");
            Worker[] w = new Worker[] { rep.GetWorkerByID(int.Parse(Console.ReadLine())) };
            Print(w);
        }

        static void DeleteWorker()
        {
            Console.WriteLine("ID: ");
            rep.DeleteWorker(int.Parse(Console.ReadLine()));
            Console.WriteLine("Готово");
        }

        static void AddWorker()
        {
            List<string> args = new List<string>();

            foreach (string el in titles.Split('#'))
            {
                if (el != "ID" & el != "Дата записи")
                {
                    Console.Write($"{el}: ");
                    args.Add(Console.ReadLine());
                }
            }

            rep.AddWorker(new Worker(0, DateTime.Now, args[0], int.Parse(args[1]), int.Parse(args[2]), DateTime.Parse(args[3]), args[4]));
            Console.WriteLine("Готово");
        }

        static void ShowFromDate()
        {
            Console.WriteLine("Дата с ");
            DateTime date1 = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Дата по ");
            DateTime date2 = DateTime.Parse(Console.ReadLine());

            Print(rep.GetWorkersBetweenTwoDates(date1, date2));
        }

        static void Print(Worker[] workers)
        {
            Console.WriteLine($"{titlesArray[0], 3}{titlesArray[1], 17}{titlesArray[2], 32}{titlesArray[3], 8}{titlesArray[4], 5}{titlesArray[5], 14}{titlesArray[6], 21}");
            foreach (Worker w in workers)
                Console.WriteLine($"{w.ID,3}{w.NoteDate,17}{w.FIO,32}{w.Age,8}{w.Height,5}{w.Birthday,14}{w.BirthPlace,21}");
        }
    }
}