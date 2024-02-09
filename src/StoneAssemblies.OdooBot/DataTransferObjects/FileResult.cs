using Microsoft.AspNetCore.Mvc;

namespace StoneAssemblies.OdooBot.DataTransferObjects;

public class FileResult
{
    public string FileName { get; set; }

    public byte[] Content { get; set; }
}