using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Ex7
{
    public class Repository
    {
        Worker[] Workers;
        private string file = "data.txt";

        public Worker[] GetAllWorkers()
        {
            string[] text = File.ReadAllLines(@file);

            Worker[] workers = new Worker[text.Length - 1];
            for (int i = 1; i < text.Length; i++)
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

            return workers;
        }

        public Worker GetWorkerByID(int id)
        {
            Workers = GetAllWorkers();
            return Workers[id];
        }

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

        public void AddWorker(Worker w)
        {
            Workers = GetAllWorkers();
            w.ID = Workers[Workers.Length - 1].ID + 1;

            using (StreamWriter sw = new StreamWriter(file, true, Encoding.UTF8))
                sw.WriteLine(WorkerToString(w));
        }

        public Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
        {
            Workers = GetAllWorkers();
            List<Worker> workers = new List<Worker>();
            foreach (Worker w in Workers)
            {
                if (w.NoteDate >= dateFrom & w.NoteDate < dateTo)
                    workers.Add(w);
            }

            return workers.ToArray();
        }

        public string WorkerToString(Worker w)
        {
            return $"{w.ID}#{w.NoteDate}#{w.FIO}#{w.Age}#{w.Height}#{w.Birthday}#{w.BirthPlace}";
        }
    }
}
