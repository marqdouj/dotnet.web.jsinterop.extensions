using Microsoft.JSInterop;

namespace Marqdouj.DotNet.Web.JsInterop.Extensions
{
    /// <summary>
    /// Provides extension methods for working with a lazily loaded module, translating JavaScript
    /// exceptions to <see cref="JS2CSharpException"/>.
    /// </summary>
    public static class ModuleTaskExtensions
    {
        /// <summary>
        /// Creates a lazily initialized task that imports a JavaScript module.
        /// </summary>
        /// <remarks>The import is invoked when the returned <see cref="Lazy{T}.Value"/> is first
        /// accessed.</remarks>
        /// <param name="jsRuntime">The JavaScript runtime used to invoke the module import.</param>
        /// <param name="modulePath">The path of the JavaScript module to import.</param>
        /// <param name="identifier">The JavaScript function identifier used to import the module. Defaults to "import".</param>
        /// <returns>A lazy task that completes with a reference to the imported JavaScript module.</returns>
        /// <exception cref="InvalidOperationException">The module import failed because the JavaScript invocation raised a <see cref="JSException"/>.</exception>
        public static Lazy<Task<IJSObjectReference>> CreateModuleTask(this IJSRuntime jsRuntime, string modulePath, string identifier = "import")
        {
            return new Lazy<Task<IJSObjectReference>>(async () =>
            {
                try
                {
                    return await jsRuntime.InvokeAsync<IJSObjectReference>(identifier, modulePath).AsTask();
                }
                catch (JSException ex)
                {
                    throw new InvalidOperationException($"Unable to '{identifier}' the JavaScript module.", ex);
                }
            });
        }

        /// <summary>
        /// Asynchronously disposes the JavaScript module if its task has been created.
        /// </summary>
        /// <param name="moduleTask">The lazy task that produces the JavaScript module reference.</param>
        public static async ValueTask DisposeAsync(this Lazy<Task<IJSObjectReference>> moduleTask)
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }

        /// <summary>
        /// Wraps calls to moduleTask.InvokeAsync with a try/catch.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="moduleTask"></param>
        /// <param name="identifier"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="JS2CSharpException"></exception>
        public static async ValueTask<T> InvokeAsyncTC<T>(this Lazy<Task<IJSObjectReference>> moduleTask, string identifier, params object?[]? args)
        {
            try
            {
                var module = await moduleTask.Value;
                return await module.InvokeAsync<T>(identifier, args);
            }
            catch (JSException ex)
            {
                throw new JS2CSharpException(ex);
            }
        }

        /// <summary>
        /// Wraps calls to moduleTask.InvokeVoidAsync with a try/catch.
        /// </summary>
        /// <param name="moduleTask"></param>
        /// <param name="identifier"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="JS2CSharpException"></exception>
        public static async ValueTask InvokeVoidAsyncTC(this Lazy<Task<IJSObjectReference>> moduleTask, string identifier, params object?[]? args)
        {
            try
            {
                var module = await moduleTask.Value;
                await module.InvokeVoidAsync(identifier, args);
            }
            catch (JSException ex)
            {
                throw new JS2CSharpException(ex);
            }
        }
    }
}
