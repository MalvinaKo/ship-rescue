using System;
using System.Collections.Generic;

namespace Models
{
    public class Program
    {
        static void Main()
        {
            var inputData = "AAA_Y: 0,-5,90\nBCA_C: 10,20,95\nSAC_F: 5,80,65\nARH_B: 100,45,60\nXXX_S: 150,70,180";
            var vessels = ParseInput(inputData);

            foreach (var vessel in vessels)
                {
                Console.WriteLine(vessel);
                }
        }

        private static List<Vessel> ParseInput(string inputData)
        {
            var vessels = new List<Vessel>();
            var lines = inputData.Replace(" ", "").Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
                {
                var parts = line.Split(':');
                var id = parts[0];
                var type = id[^1];
                var details = parts[1].Split(',');
                var x = int.Parse(details[0]);
                var y = int.Parse(details[1]);
                var range = int.Parse(details[2]);
                vessels.Add(new Vessel(id, x, y, range, type));
                }

            return vessels;
        }
    }
}