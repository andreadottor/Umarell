namespace Dottor.Umarell.UI.Services
{
    using System;
    using System.Threading.Tasks;

    public delegate void MessageBoxShowHandler(string caption, string text, MessageBoxType messageBoxType, Func<bool, Task> returnCallback);

    public interface IMessageBoxService
    {
        event MessageBoxShowHandler? MessageBoxShowInvoked;
        Task ShowAlertAsync(string title, string text);
        Task<bool> ShowConfirmAsync(string title, string text);
    }

    public class MessageBoxService : IMessageBoxService
    {
        public event MessageBoxShowHandler? MessageBoxShowInvoked;

        public Task ShowAlertAsync(string caption, string text)
        {
            var tcs = new TaskCompletionSource();
            MessageBoxShowInvoked?.Invoke(caption, text, MessageBoxType.Alert, res =>
            {
                try
                {
                    tcs.SetResult();
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
                return Task.CompletedTask;
            });

            return tcs.Task;
        }

        public Task<bool> ShowConfirmAsync(string title, string text)
        {
            var tcs = new TaskCompletionSource<bool>();
            MessageBoxShowInvoked?.Invoke(title, text, MessageBoxType.Confirm, res =>
            {
                try
                {
                    tcs.SetResult(res);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
                return Task.CompletedTask;
            });

            return tcs.Task;
        }
    }

    public enum MessageBoxType
    {
        Alert,
        Confirm
    }

}
