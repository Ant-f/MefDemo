namespace Sdk
{
    public interface IDataProcessor
    {
        string ProcessorId { get; }

        void ProcessData(string data);
    }
}
