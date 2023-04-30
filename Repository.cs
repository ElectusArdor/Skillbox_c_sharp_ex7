using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Ex7
{
    public class Repository
    {
        Worker[] Workers;
        private string file = "data.txt";

        /// <summary>
        /// Загружает весь список сотрудников.
        /// </summary>
        /// <returns></returns>
        public Worker[] GetAllWorkers()
        {
            string[] text = File.ReadAllLines(@file);
            Worker[] workers = new Worker[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] != "")
                {
                    string[] dataInLine = text[i].Split('#');

                    workers[i] = new Worker(
                        int.Parse(dataInLine[0]),
                        DateTime.Parse(dataInLine[1]),
                        dataInLine[2],
                        int.Parse(dataInLine[3]),
                        int.Parse(dataInLine[4]),
                        DateTime.Parse(dataInLine[5]),
                        dataInLine[6]);
                }
            }
            return workers;
        }

        /// <summary>
        /// Возвращает одного работника с заданным ID, если таковой имеется.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Nullable<Worker> GetWorkerByID(int id)
        {
            Workers = GetAllWorkers();
            if (Workers.Length > 0)
            {
                foreach (Worker w in Workers)
                {
                    if (w.ID == id)
                        return w;
                }
                return null;
            }
            else
                return null;
        }

        /// <summary>
        /// Удаляет одного работника по ID.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteWorker(int id)
        {
            Workers = GetAllWorkers();
            List<string> workersList = new List<string>();

            foreach (Worker w in Workers)
            {
                if (w.ID != id)
                    workersList.Add(WorkerToString(w));
            }

            File.WriteAllLines(file, workersList);
        }

        /// <summary>
        /// Дописывает в конец файла указанных работников.
        /// </summary>
        /// <param name="w"></param>
        public void AddWorker(Worker[] w)
        {
            Workers = GetAllWorkers();
            int id;

            if (Workers.Length > 0)
                id = Workers[Workers.Length - 1].ID + 1;
            else
                id = 1;

            for (int i = 0; i < w.Length; i++)
            {
                w[i].ID = id++;
                using (StreamWriter sw = new StreamWriter(file, true, Encoding.UTF8))
                    sw.WriteLine(WorkerToString(w[i]));
            }
        }

        /// <summary>
        /// Загружает массив работников и возвращает массив между указанными датами.
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
        {
            Workers = GetAllWorkers();
            List<Worker> workers = new List<Worker>();
            foreach (Worker w in Workers)
            {
                if (w.NoteDate >= dateFrom & w.NoteDate <= dateTo)
                    workers.Add(w);
            }

            return workers.ToArray();
        }

        /// <summary>
        /// Преобразует объект типа Worker в строку для хранения в файле.
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        public string WorkerToString(Worker w)
        {
            return $"{w.ID}#{w.NoteDate}#{w.FIO}#{w.Age}#{w.Height}#{w.Birthday}#{w.BirthPlace}";
        }
    }
}
