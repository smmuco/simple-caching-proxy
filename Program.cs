
string? port = null;
string? origin = null;


for (int i = 0; i<args.Length; i++)
{
    if(args[i] == "--port" && i + 1 < args.Length)
    {
        port = args[i + 1];
    }
    else if(args[i] == "--origin" && i + 1 < args.Length)
    {
        origin = args[i + 1];
    }
}


if (!int.TryParse(port, out int portNumber))
{
    Console.WriteLine("Error: port must be a number");
    Environment.Exit(1);
}

if (portNumber<1 || portNumber>65535)
{
    Console.WriteLine("Error: port must be between 1 and 65535");
    Environment.Exit(1);
}

if (!Uri.TryCreate(origin,UriKind.Absolute, out Uri? originUri))
{
    Console.WriteLine("Error: origin must be a valid absolute URL");
    Environment.Exit(1);
}



Console.WriteLine($"Port: {port}");
Console.WriteLine($"Orgin: {origin}");



