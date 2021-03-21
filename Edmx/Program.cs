using System;

namespace Edmx
{
    internal class Program
    {
        private static async System.Threading.Tasks.Task Main(string[] args)
        {
            var generator = new XmlGenerator();
            await generator.GenerateColumnAsync();
            await generator.GenerateColumnAsync2();
            await generator.GenerateColumnAsync3();
            await generator.GenerateColumnAsync4();
            await generator.GenerateConstraintsAsync();
            await generator.GenerateConstraintsAsync2();
            await generator.GenerateConstraintsAsync3();

            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}