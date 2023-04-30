using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;

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
                    sw.Write("");
                NextTask();
            }
            else
                NextTask();

            Console.ReadKey();
        }

        /// <summary>
        /// Выводит и запрашивает информацию по следующему действию.
        /// </summary>
        static void NextTask()
        {
            Console.WriteLine("\n1 - вывести данные по всем работникам\n2 - вывести данные по одному работнику\n3 - ввести данные нового работника");
            Console.WriteLine("4 - удалить сотрудника по ID\n5 - вывести данные в интервале дат\n6 - сгенерировать новых сотрудников\n7 - сортировать список по");
            Control(Console.ReadLine());
        }

        /// <summary>
        /// Принимает номер команды и выполняет её.
        /// </summary>
        /// <param name="key">Номер команды.</param>
        static void Control(string key)
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
                case "6":
                    CreateWorkers();
                    NextTask();
                    break;
                case "7":
                    SortWorkersList();
                    NextTask();
                    break;
                default:
                    Console.WriteLine("Неверная команда");
                    Control(Console.ReadLine());
                    break;
            }
        }

        /// <summary>
        /// Генерирует заданное число новых работников.
        /// </summary>
        static void CreateWorkers()
        {
            Console.WriteLine("\nСколько сгенерировать? ");
            int n = int.Parse(Console.ReadLine());

            Random rnd = new Random();
            Worker[] newWorkers = new Worker[n];

            for (int i = 0; i < n; i++)
            {
                int age;
                DateTime birthDay = new DateTime(1960, 01, 01).AddDays(rnd.Next(16425));
                if (birthDay.Month < DateTime.Now.Month)
                    age = DateTime.Now.Year - birthDay.Year;
                else if (birthDay.Month > DateTime.Now.Month)
                    age = DateTime.Now.Year - birthDay.Year - 1;
                else
                {
                    if (birthDay.Day < DateTime.Now.Day)
                        age = DateTime.Now.Year - birthDay.Year;
                    else
                        age = DateTime.Now.Year - birthDay.Year - 1;
                }

                newWorkers[i] = new Worker(0, DateTime.Now, "Name" + i, age, rnd.Next(140, 220), birthDay, "BirthPlace" + i);
            }

            rep.AddWorker(newWorkers);
            Console.WriteLine("Готово");
        }

        /// <summary>
        /// Загружает и выводит список сотрудников.
        /// </summary>
        static void ShowAllWorkers()
        {
            Print(rep.GetAllWorkers());
        }

        /// <summary>
        /// Сортировка и вывод списка сотрудников.
        /// </summary>
        static void SortWorkersList()
        {
            Console.WriteLine("\n1 - по ID\n2 - по дате добавления\n3 - по ФИО\n4 - по возрасту\n5 - по росту\n6 - по дате рождения\n7 - по месту рождения");
            Worker[] workers;

            switch (Console.ReadLine())
            {
                case "1":
                    workers = rep.GetAllWorkers().OrderBy(w => w.ID).ToArray();
                    Print(workers);
                    break;
                case "2":
                    workers = rep.GetAllWorkers().OrderBy(w => w.NoteDate).ToArray();
                    Print(workers);
                    break;
                case "3":
                    workers = rep.GetAllWorkers().OrderBy(w => w.FIO).ToArray();
                    Print(workers);
                    break;
                case "4":
                    workers = rep.GetAllWorkers().OrderBy(w => w.Age).ToArray();
                    Print(workers);
                    break;
                case "5":
                    workers = rep.GetAllWorkers().OrderBy(w => w.Height).ToArray();
                    Print(workers);
                    break;
                case "6":
                    workers = rep.GetAllWorkers().OrderBy(w => w.Birthday).ToArray();
                    Print(workers);
                    break;
                case "7":
                    workers = rep.GetAllWorkers().OrderBy(w => w.BirthPlace).ToArray();
                    Print(workers);
                    break;
                default:
                    Console.WriteLine("Нет такого поля.");
                    SortWorkersList();
                    break;
            }
        }

        /// <summary>
        /// Отображает работниа по ID.
        /// </summary>
        static void ShowWorkerByID()
        {
            Console.WriteLine("ID: ");
            Nullable<Worker> worker = rep.GetWorkerByID(int.Parse(Console.ReadLine()));
            if (worker != null)
            {
                Worker[] w = new Worker[] { (Worker)worker };
                Print(w);
            }
            else
            {
                Console.WriteLine("\nНет такого работника");
            }
        }

        /// <summary>
        /// Удалить сотрудника по ID.
        /// </summary>
        static void DeleteWorker()
        {
            Console.WriteLine("ID: ");
            rep.DeleteWorker(int.Parse(Console.ReadLine()));
            Console.WriteLine("Готово");
        }

        /// <summary>
        /// Добавляет одного сотрудника с введёнными данными.
        /// </summary>
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

            rep.AddWorker(new Worker[] { new Worker(0, DateTime.Now, args[0], int.Parse(args[1]), int.Parse(args[2]), DateTime.Parse(args[3]), args[4]) });
            Console.WriteLine("Готово");
        }

        /// <summary>
        /// Выводит записи, сделанные в интервале указанных дат.
        /// </summary>
        static void ShowFromDate()
        {
            Console.WriteLine("Дата с ");
            DateTime date1 = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Дата по ");
            DateTime date2 = DateTime.Parse(Console.ReadLine());

            Print(rep.GetWorkersBetweenTwoDates(date1, date2));
        }

        /// <summary>
        /// Выводит в консоль полученный список сотрудников.
        /// </summary>
        /// <param name="workers"></param>
        static void Print(Worker[] workers)
        {
            Console.WriteLine($"\n{titlesArray[0], 7}{titlesArray[1], 24}{titlesArray[2], 34}{titlesArray[3], 8}{titlesArray[4], 6}{titlesArray[5], 15}{titlesArray[6], 24}");
            for (int i = 0; i < workers.Length; i++)
                workers[i].Print();
            //foreach (Worker w in workers) w.Print();
        }
    }
}