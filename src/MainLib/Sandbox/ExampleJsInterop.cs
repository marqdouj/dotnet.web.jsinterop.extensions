using Microsoft.JSInterop;
using Marqdouj.DotNet.Web.JsInterop.Extensions;

namespace Sandbox
{
    internal class ExampleJsInterop(IJSRuntime jsRuntime) : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = jsRuntime.CreateModuleTask("./exampleJsInterop.js");

        public async ValueTask<string> Prompt(string message)
        {
            return await moduleTask.InvokeAsyncTC<string>("prompt", message);
        }

        public async ValueTask ThrowError()
        {
            await moduleTask.InvokeVoidAsyncTC("throwError");
        }

        public async ValueTask DisposeAsync()
        {
            await moduleTask.DisposeAsync();
        }
    }
}
