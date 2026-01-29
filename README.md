# SimpleCachingProxy

A lightweight asynchronous HTTP reverse proxy implemented in C# and .NET.
The proxy forwards incoming requests to a configured origin server and caches responses in memory to reduce repeated upstream calls.

## Features

- Asynchronous request handling using async/await
- Reverse proxy behavior for GET requests
- In-memory response caching
- Thread-safe cache implementation
- X-Cache: HIT / X-Cache: MISS response headers
- CLI-based configuration

## How It Works

- The proxy listens on a local port
- Incoming GET requests are forwarded to the origin server
- Responses are cached in memory using the following cache key:

    HTTP method + request path + query string

- Cached responses are returned directly on subsequent requests without calling the origin server

## Usage

Clone the repository:

    git clone https://github.com/yourusername/SimpleCachingProxy.git
    cd SimpleCachingProxy

Run the proxy:

    dotnet run -- --port 3000 --origin https://example.com

Send requests to the proxy:

    http://localhost:3000

The proxy will forward requests to the origin server and cache responses automatically.

## Limitations

- Only GET requests are supported
- Cache is in-memory only (no persistence)
- No cache expiration or eviction strategy
- Request and response headers are not fully forwarded
- No HTTPS termination
- Not intended for production use

## License

This project is provided for educational purposes.
