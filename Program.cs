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


await StartServerAsync(portNumber, originUri);

async Task StartServerAsync(int port, Uri origin)
{
    var listener = new HttpListener();
    listener.Prefixes.Add($"http://localhost:{port}/");
    listener.Start();

    Console.WriteLine($"Listening on http://localhost:{port}/");
    Console.WriteLine($"Forwarding to {origin}");

    var HttpClient = new HttpClient();

    while (true)
    {
        var context = await listener.GetContextAsync();
        _ = HandleRequestAsync(context ,HttpClient ,origin );
    }
}
async Task HandleRequestAsync(HttpListenerContext context, HttpClient htppClient, Uri origin)
{
    var request = context.Request;
    var response = context.Response;

    if (request.HttpMethod != "GET")
    {
        response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
        response.Close();
        return;
    }

    var targetUrl = new Uri(origin, request.RawUrl);

    HttpResponseMessage originResponse;
    try
    {
        originResponse = await htppClient.GetAsync(targetUrl);
    }
    catch
    {
        response.StatusCode = (int)HttpStatusCode.BadGateway;
        response.Close();
        return;
    }

    var responseBody = await originResponse.Content.ReadAsByteArrayAsync();

    response.StatusCode = (int)originResponse.StatusCode;
    response.ContentLength64 = responseBody.Length;

    await response.OutputStream.WriteAsync(responseBody, 0, responseBody.Length);
    response.OutputStream.Close();
}

