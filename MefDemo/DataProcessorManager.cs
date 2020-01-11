using Sdk;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace MefDemo
{
    public class DataProcessorManager
    {
        [ImportMany(typeof(IDataProcessor), AllowRecomposition = true)]
        [SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "Imported using MEF")]
        private IEnumerable<IDataProcessor> _dataProcessors;

        private readonly DirectoryCatalog _directoryCatalog =
            new DirectoryCatalog(Program.PluginDirectory);

        public DataProcessorManager()
        {
            var catalog = new AggregateCatalog(
                new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly()),
                _directoryCatalog);

            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        public void FindProcessorAndProcessInput(string processorId, string input)
        {
            var processor = _dataProcessors.SingleOrDefault(p =>
                p.ProcessorId == processorId);

            if (processor == null)
            {
                Console.WriteLine($"Cannot find processor with ID: {processorId}");
            }
            else
            {
                processor.ProcessData(input);
            }
        }

        public void RefreshProcessors()
        {
            _directoryCatalog.Refresh();
            Console.WriteLine("Data processor catalog rebuilt!");
        }
    }
}
