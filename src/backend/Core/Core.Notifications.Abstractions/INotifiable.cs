using FluentValidation.Results;
using System.Threading.Tasks;

namespace Hcqn.Core.Notifications.Abstractions
{
    public interface INotifiable
    {
        virtual async Task NotifyValidationErrors(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                await Notify(error.PropertyName, error.ErrorMessage);
            }
        }

        virtual Task Notify(string key, string value)
        {
            return Notify(new DomainNotification(key, value));
        }

        Task Notify(DomainNotification notification);

        public bool IsValid();
    }
}
