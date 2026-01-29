

string? port = null;
string? orgin = null;


for (int i = 0; i<args.Length; i++)
{
    if(args[i] == "--port" && i + 1 < args.Length)
    {
        port = args[i + 1];
    }
    else if(args[i] == "--origin" && i + 1 < args.Length)
    {
        orgin = args[i + 1];
    }
}
Console.WriteLine($"Port: {port}");
Console.WriteLine($"Orgin: {orgin}");



