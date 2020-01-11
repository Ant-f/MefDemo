using Sdk;
using System;
using System.ComponentModel.Composition;

namespace MefDemo
{
    [Export(typeof(IDataProcessor))]
    public class DataProcessor : IDataProcessor
    {
        public string ProcessorId { get; } = "data";

        public void ProcessData(string data)
        {
            Console.WriteLine($"DataProcessor: {data}");
        }
    }
}
