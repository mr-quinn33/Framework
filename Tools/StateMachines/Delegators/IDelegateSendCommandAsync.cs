﻿using System.Threading.Tasks;
using Framework.Commands;

namespace Framework.Tools.StateMachines.Delegators
{
    public interface IDelegateSendCommandAsync
    {
        Task SendCommandAsync<T>(T command) where T : ICommandAsync;

        Task SendCommandAsync<T>() where T : ICommandAsync, new();
    }
}