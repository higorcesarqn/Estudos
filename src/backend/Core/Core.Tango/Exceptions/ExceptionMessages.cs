using Hcqn.Core.Tango.Types.Extensions;
using System.ComponentModel;

namespace Hcqn.Core.Tango.Exceptions
{
    internal enum ExceptionMessages
    {
        [Description("The input \"{0}\" must be positive")]
        MustBePositive
    }

    internal static class ExceptionMessagesMethods
    {
        internal static string GetMessage(this ExceptionMessages message, params string[] args)
            => string.Format(message.GetDescription(), args);
        
    }
}