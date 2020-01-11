namespace MefDemo
{
    class Program
    {
        public const string PluginDirectory = ".\\Plugins";

        static void Main(string[] args)
        {
            var service = new DataProcessingService();
            service.Start();
        }
    }
}
