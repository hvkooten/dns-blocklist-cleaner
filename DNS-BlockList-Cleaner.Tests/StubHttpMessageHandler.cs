using System.Net;

namespace DNS_BlockList_Cleaner.Tests;

/// <summary>
/// A test double for <see cref="HttpMessageHandler"/> that returns a canned
/// response and records the requests it received.
/// </summary>
internal sealed class StubHttpMessageHandler : HttpMessageHandler
{
    private readonly Func<HttpRequestMessage, HttpResponseMessage> _responder;

    public StubHttpMessageHandler(HttpResponseMessage response)
        => _responder = _ => response;

    public StubHttpMessageHandler(Func<HttpRequestMessage, HttpResponseMessage> responder)
        => _responder = responder;

    public List<Uri?> Requests { get; } = [];

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Requests.Add(request.RequestUri);
        return Task.FromResult(_responder(request));
    }

    public static HttpResponseMessage Json(string body, HttpStatusCode status = HttpStatusCode.OK)
        => new(status)
        {
            Content = new StringContent(body, System.Text.Encoding.UTF8, "application/json")
        };
}
