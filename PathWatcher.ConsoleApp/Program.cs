using System;

namespace PathWatcher.ConsoleApp
{
    class Program
    {
        private static Entities.PathWatcher _pathWatcher;
        static void Main(string[] args)
        {
            Watcher();
            Console.ReadKey();
        }

        private static void Watcher()
        {
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //directory pode ser alterado esse é apenas para teste
            var directory = System.IO.Path.GetDirectoryName(path);

            _pathWatcher = new Entities.PathWatcher(directory, "txt", "Log", "Gravar", "PathWatcher.ConsoleApp");
        }
    }
}
