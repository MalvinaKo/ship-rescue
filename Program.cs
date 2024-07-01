using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Models
{
    public class Program
    {
        static void Main()
        {
            var inputData = "AAA_Y: 0,-5,90\nBCA_C: 10,20,95\nSAC_F: 5,80,65\nARH_B: 100,45,60\nXXX_S: 150,70,180";
            var vessels = ParseInput(inputData);
            var x = CanCommunicate(new Vessel("AAA", 0, -5, 90, 'Y'), new Vessel("BCA", 10, 20, 95, 'C'));
            //AAA_Y: 0,-5,90
            //BCA_C: 10,20,95
            //SAC_F: 5,80,65
            //ARH_B: 100,45,60
            //XXX_S: 150,70,180
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

        private static bool CanCommunicate(Vessel v1, Vessel v2)
        {
            var compatibleTypes = new Dictionary<char, char[]>
            {
                { 'Y', new[] { 'Y', 'B', 'F' } },
                { 'C', new[] { 'B', 'F' } },
                { 'F', new[] { 'Y', 'C' } },
                { 'B', new[] { 'C', 'F', 'B', 'S' } },
                { 'S', new[] { 'B', 'Y' } }
            };
            var distance = Math.Sqrt(Math.Pow(v2.X - v1.X, 2) + Math.Pow(v2.Y - v1.Y, 2));
            return compatibleTypes[v1.Type].Contains(v2.Type) && distance <= v1.Range;
        }
    }
}