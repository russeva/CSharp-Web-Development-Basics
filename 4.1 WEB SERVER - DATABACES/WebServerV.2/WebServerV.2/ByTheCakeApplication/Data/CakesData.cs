namespace WebServerV._2.ByTheCakeApplication.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ByTheCakeApplication.Models;

    public  class CakesData
    {
        private const string DatabaseFile = @"..\\..\\..\\ByTheCakeApplication\\Data\\database.csv";

        public void Add(string name, string price)
        {
            var streamReader = new StreamReader(DatabaseFile); 
            var id = streamReader.ReadToEnd().Split(Environment.NewLine).Length;
            streamReader.Dispose();

            using (var streamWriter = new StreamWriter(DatabaseFile, true))
            {
                streamWriter.WriteLine($"{id},{name},{price}");
            }

        }

        public  IEnumerable<Cake> All()
        {   
            return  File
                .ReadAllLines(DatabaseFile)
                    .Where(line => line.Contains(','))
                    .Select(line => line.Split(','))
                    .Select(line => new Cake
                    {
                        Id = int.Parse(line[0]),
                        Name = line[1],
                        Price = decimal.Parse(line[2])
                    });
        }

        public Cake Find(int id)
        {
            return this.All().FirstOrDefault(c => c.Id == id);
        }

      
    }
}
