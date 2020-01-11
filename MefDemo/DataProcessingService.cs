using System;
using System.IO;

namespace MefDemo
{
    public class DataProcessingService
    {
        private readonly DataProcessorManager _dataProcessorManager;

        public DataProcessingService()
        {
            _dataProcessorManager = new DataProcessorManager();
        }

        public void Start()
        {
            using (InitialisePluginFolder())
            {
                while (true)
                {
                    Console.Write("Input command: ");

                    var input = Console.ReadLine();
                    var split = input.Split(' ');

                    try
                    {
                        _dataProcessorManager.FindProcessorAndProcessInput(split[0], split[1]);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(
                            "Commands should be in the format \"[processor ID] [data]\", e.g. \"data 123\"");
                        Console.WriteLine(e);
                    }
                    finally
                    {
                        Console.WriteLine();
                    }
                }
            }
        }

        private FileSystemWatcher InitialisePluginFolder()
        {
            Directory.CreateDirectory(Program.PluginDirectory);
            var watcher = new FileSystemWatcher(Program.PluginDirectory, "*.dll");
            
            watcher.Created += RescanDataProcessors;
            watcher.Changed += RescanDataProcessors;
            watcher.Deleted += RescanDataProcessors;
            watcher.Renamed += RescanDataProcessors;
            
            watcher.EnableRaisingEvents = true;
            return watcher;
        }

        private void RescanDataProcessors(object sender, FileSystemEventArgs e)
        {
            _dataProcessorManager.RefreshProcessors();
        }
    }
}
