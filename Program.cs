using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Models
{
    public class Program
    {
        static void Main()
        {
            var inputFilePath = "input.txt";
            var inputData = File.ReadAllText(inputFilePath);
            var vessels = ParseInput(inputData);
            var startVessel = vessels.First();
            var x = CanCommunicate(new Vessel("AAA", 0, -5, 90, 'Y'), new Vessel("BCA", 10, 20, 95, 'C'));
            var graph = BuildGraph(vessels);
            var (pathToShore, distanceToShore) = BfsShortestPath(graph, vessels, startVessel.Id, 'S');
            var (pathBack, distanceBack) = BfsShortestPath(graph, vessels, pathToShore.Last(), startVessel.Type);
            var totalDistance = distanceToShore + distanceBack;
            Console.WriteLine($"Total distance: {Math.Round(totalDistance, 2)}");
            Console.WriteLine("Path to shore: " + string.Join(" -> ", pathToShore));
            Console.WriteLine("Path back: " + string.Join(" -> ", pathBack));
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

        private static double CalculateDistance(Vessel v1, Vessel v2)
        {
            return Math.Sqrt(Math.Pow(v1.X - v2.X, 2) + Math.Pow(v1.Y - v2.Y, 2));
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
            var distance = CalculateDistance(v1, v2);
            return compatibleTypes[v1.Type].Contains(v2.Type) && distance <= v1.Range;
        }

        private static Dictionary<string, List<string>> BuildGraph(List<Vessel> vessels)
        {
            var graph = new Dictionary<string, List<string>>();
            foreach (var vessel in vessels)
                {
                graph[vessel.Id] = new List<string>();
                foreach (var other in vessels)
                    {
                    if (vessel.Id != other.Id && CanCommunicate(vessel, other))
                        {
                        graph[vessel.Id].Add(other.Id);
                        }
                    }
                }

            return graph;
        }

        private static (List<string> Path, double Distance) BfsShortestPath(
            Dictionary<string, List<string>> graph, List<Vessel> vessels, string start, char goalType)
        {
            var queue = new Queue<(string, List<string>, double)>();
            var visited = new HashSet<string>();
            var vesselDict = vessels.ToDictionary(v => v.Id);

            queue.Enqueue((start, new List<string> { start }, 0.0));

            while (queue.Count > 0)
                {
                var (current, path, currentDistance) = queue.Dequeue();

                if (visited.Contains(current))
                    continue;

                visited.Add(current);

                foreach (var neighbor in graph[current])
                    {
                    var newPath = new List<string>(path) { neighbor };
                    var newDistance = currentDistance + CalculateDistance(vesselDict[current], vesselDict[neighbor]);

                    if (neighbor.Last() == goalType)
                        {
                        return (newPath, newDistance);
                        }
                    else
                        {
                        queue.Enqueue((neighbor, newPath, newDistance));
                        }
                    }
                }

            return (null, 0.0);
        }
    }
}