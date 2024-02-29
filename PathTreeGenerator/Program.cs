using DataStructures;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace PathTreeGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the path to generate the tree: ");
            string? path = Console.ReadLine();

            if (string.IsNullOrEmpty(path))
            {
                Console.WriteLine("Loading tree from file...");
                Console.WriteLine("Do you want to display the hash?: ");
                bool isDisplayHash = false;
                string? displayHashInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(displayHashInput))
                {
                    isDisplayHash = true;
                }
                var tree = PathTree.LoadTree("PathTree");
                Console.WriteLine("Tree loaded");
                Console.WriteLine(tree?.Display(isDisplayHash));
                return;
            }


            var stopWatch = new System.Diagnostics.Stopwatch();

            stopWatch.Start();
            var pathTree = new PathTree(path);
            stopWatch.Stop();
            //log time taken to generate the tree
            Console.WriteLine("Tree generated in: " + stopWatch.Elapsed);

            pathTree.SaveTree("PathTree");
        }
    }
}