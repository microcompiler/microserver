﻿using System;

using Bytewizer.TinyCLR.Logging;
using Bytewizer.TinyCLR.Pipeline;
using Bytewizer.TinyCLR.Sockets.Channel;

namespace Bytewizer.TinyCLR.Sockets
{
    /// <summary>
    /// Represents an implementation of the <see cref="SocketServer"/> for creating network servers.
    /// </summary>
    public class SocketServer : SocketService, IServer
    {
        private readonly ILogger _logger;
        private readonly ContextPool _contextPool;

        /// <summary>
        /// Initializes a new instance of the <see cref="SocketServer"/> class.
        /// </summary>
        /// <param name="configure">The configuration options of <see cref="SocketServer"/> specific features.</param>
        public SocketServer(ServerOptionsDelegate configure)
            : this(NullLoggerFactory.Instance, new ServerOptions())
        {
            configure(_options);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SocketServer"/> class.
        /// </summary>
        /// <param name="loggerFactory">The factory used to create loggers.</param>
        /// <param name="configure">The configuration options of <see cref="SocketServer"/> specific features.</param>
        public SocketServer(ILoggerFactory loggerFactory, ServerOptionsDelegate configure)
            : this(loggerFactory, new ServerOptions())
        {
            configure(_options);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SocketServer"/> class.
        /// </summary>
        /// <param name="loggerFactory">The factory used to create loggers.</param>
        /// <param name="options">The configuration options of <see cref="SocketServer"/> specific features.</param>
        public SocketServer(ILoggerFactory loggerFactory, ServerOptions options)
            : base(loggerFactory, options)
        {
            _contextPool = new ContextPool();
            _logger = loggerFactory.CreateLogger("Bytewizer.TinyCLR.Sockets");
        }

        /// <summary>
        /// A client has connected.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="channel">The socket channel for the connected end point.</param>
        protected override void ClientConnected(object sender, SocketChannel channel)
        {
            // Set channel error handler
            channel.ChannelError += ChannelError;
            
            try
            {
                // Get context from context pool
                var context = _contextPool.GetContext(typeof(SocketContext)) as SocketContext;
                
                // Assign channel
                context.Channel = channel;

                // Check to make sure channel contains data
                if (context.Channel.InputStream.Length > 0)
                {
                    // Invoke pipeline 
                    _options.Application.Invoke(context);
                }

                // Release context back to pool and close connection once pipeline is complete
                _contextPool.Release(context);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Unexpcted exception in {nameof(SocketServer)}.{nameof(ClientConnected)}");
                return;
            }
        }

        /// <summary>
        /// A client has disconnected.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="execption">The socket <see cref="Exception"/> for the disconnected endpoint.</param>
        protected override void ClientDisconnected(object sender, Exception execption)
        {
            _logger.LogError(execption, $"Remote client disconnected connection");
        }

        /// <summary>
        /// An internal channel error occured.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="execption">The <see cref="Exception"/> for the channel error.</param>
        private void ChannelError(object sender, Exception execption)
        {
            _logger.LogError(execption, $"Unexpcted channel exception in {nameof(SocketServer)}");
        }
    }
}