namespace WristCast.Core.Services
{
    public interface IStorageProvider
    {
        string MediaFolderPath { get; }

        string AppDataDirectory { get; }

        string DatabasePath { get; }
    }
}