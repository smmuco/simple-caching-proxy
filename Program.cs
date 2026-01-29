using System.Net;
using System.Text;

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


StartServer(portNumber);

void StartServer(int port)
{
    var listener = new HttpListener();
    listener.Prefixes.Add($"http://localhost:{port}/");

    listener.Start();
    Console.WriteLine($"Listening on http://localhost:{port}/");

    while (true)
    {
        var context = listener.GetContext();
        var response = context.Response;

        string responseText = "hello";
        byte[] buffer = Encoding.UTF8.GetBytes(responseText);
        
        response.ContentLength64 = buffer.Length;
        response.OutputStream.Write(buffer, 0, buffer.Length);
        response.OutputStream.Close();
    }
}




