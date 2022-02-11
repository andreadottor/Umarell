﻿namespace Dottor.Umarell.UI.Services
{
    using System;
    using System.Threading.Tasks;

    public record MessageBoxShowEventArgs(string Title, string Text, MessageBoxType MessageBoxType, Func<bool, Task> ReturnCallback);

    public interface IMessageBoxService
    {
        event EventHandler<MessageBoxShowEventArgs>? MessageBoxShow;
        Task ShowAlertAsync(string title, string text);
        Task<bool> ShowConfirmAsync(string title, string text);
    }

    public class MessageBoxService : IMessageBoxService
    {
        //public event MessageBoxShowHandler? MessageBoxShowInvoked;
        public event EventHandler<MessageBoxShowEventArgs>? MessageBoxShow;

        public Task ShowAlertAsync(string title, string text)
        {
            var tcs = new TaskCompletionSource();

            MessageBoxShow?.Invoke(
                this,
                new(title, text, MessageBoxType.Alert, res =>
                {
                    tcs?.SetResult();
                    return Task.CompletedTask;
                }));

            return tcs.Task;
        }

        public Task<bool> ShowConfirmAsync(string title, string text)
        {
            var tcs = new TaskCompletionSource<bool>();

            MessageBoxShow?.Invoke(
                this,
                new(title, text, MessageBoxType.Confirm, res =>
                {
                    tcs?.SetResult(res);
                    return Task.CompletedTask;
                }));

            return tcs.Task;
        }
    }

    public enum MessageBoxType
    {
        Alert,
        Confirm
    }

}
