﻿using System.Diagnostics;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Jpc.Shared.Kernel;

/// <summary>
/// Adds logging for all requests in MediatR pipeline.
/// Configure by adding the service with a scoped lifetime
/// 
/// Example for Autofac:
/// builder
///   .RegisterType&lt;Mediator&gt;()
///   .As&lt;IMediator&gt;()
///   .InstancePerLifetimeScope();
///
/// builder
///   .RegisterGeneric(typeof(LoggingBehavior&lt;,&gt;))
///      .As(typeof(IPipelineBehavior&lt;,&gt;))
///   .InstancePerLifetimeScope();
///
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
  where TRequest : IRequest<TResponse>
{
    private readonly ILogger<Mediator> _logger;

    public LoggingBehavior(ILogger<Mediator> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var isCommand = request is ICommand<TResponse>;

        if ((_logger.IsEnabled(LogLevel.Information) && isCommand) || _logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogInformation("Handling {RequestName}", typeof(TRequest).Name);

            // Reflection! Could be a performance concern
            Type myType = request.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            foreach (PropertyInfo prop in props)
            {
                object? propValue = prop?.GetValue(request, null);
                _logger.LogInformation("Property {Property} : {@Value}", prop?.Name, propValue);
            }
        }

        var sw = Stopwatch.StartNew();

        var response = await next();

        sw.Stop();

        _logger.LogInformation("Handled {RequestName} with {Response} in {ms} ms", typeof(TRequest).Name, response, sw.ElapsedMilliseconds);

        return response;
    }
}

