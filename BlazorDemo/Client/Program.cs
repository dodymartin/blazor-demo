using BlazorDemo.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorDemo.Client.Services;
using Polly;
using Polly.Contrib.WaitAndRetry;
using System.Threading.Tasks;
using Polly.Extensions.Http;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");



//Need to create a typed HttpClient class.
builder.Services.AddHttpClient<TodoService>(
    client =>
    {
        client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
        client.Timeout = TimeSpan.FromSeconds(15);
    })
    .AddPolicyHandler(HttpPolicyExtensions.HandleTransientHttpError()
        .Or<TaskCanceledException>()
        .OrInner<TimeoutException>()
        .WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5)));

await builder.Build().RunAsync();
