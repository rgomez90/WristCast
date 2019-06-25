namespace WristCast.Core.Services
{
    public interface IStorageProvider
    {
        string MediaFolderPath { get; }

        string AppDataDirectory { get; }

        string SharedAppDataDirectory { get; }

        string DatabasePath { get; }
    }
}