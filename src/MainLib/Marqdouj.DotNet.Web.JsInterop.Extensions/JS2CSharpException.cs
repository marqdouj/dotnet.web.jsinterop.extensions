using Microsoft.JSInterop;

namespace Marqdouj.DotNet.Web.JsInterop.Extensions
{
    /// <summary>
    /// When a <see cref="JSException"/> is caught by a <see cref="ModuleTaskExtensions"/> method, 
    /// it converts it to this exception and throws it.
    /// </summary>
    /// <param name="ex"><see cref="JSException"/></param>
    public class JS2CSharpException(JSException ex) : Exception(ParseMessage(ex), ex)
    {
        private static string ParseMessage(JSException ex)
        {
            var message = ex.Message;
            var index = message.IndexOf("Error:", StringComparison.OrdinalIgnoreCase);

            if (index > -1)
                message = message[..index].Replace("\n", " ");

            return message;
        }
    }
}
