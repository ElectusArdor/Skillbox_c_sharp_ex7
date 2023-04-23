using System;

namespace Ex7
{
    public struct Worker
    {
        public int ID { get; set; }
        public DateTime NoteDate { get; set; }
        public string FIO { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public DateTime Birthday { get; set; }
        public string BirthPlace { get; set; }

        public Worker(int ID, DateTime NoteDate, string FIO, int Age, int Height, DateTime Birthday, string BirthPlace)
        {
            this.ID = ID;
            this.NoteDate = NoteDate;
            this.FIO = FIO;
            this.Age = Age;
            this.Height = Height;
            this.Birthday = Birthday;
            this.BirthPlace = BirthPlace;
        }
    }
}