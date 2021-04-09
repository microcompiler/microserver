﻿using System;

namespace Bytewizer.TinyCLR.Http.WebSockets
{
    /// <summary>
    /// A base class for a Websocket hub.
    /// </summary>
    public abstract class Hub
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Hub"/> class.
        /// </summary>
        protected Hub()
        {
        }

        public WebSocketClients Clients { get; set; }

        /// <summary>
        /// Gets or sets the hub caller context.
        /// </summary>
        public HubCallerContext Context { get; set; }

        /// <summary>
        /// Called when a new connection is established with the hub.
        /// </summary>
        public virtual void OnConnected()
        {
        }

        /// <summary>
        /// Called when a connection with the hub is terminated.
        /// </summary>
        public virtual void OnDisconnected(Exception exception)
        {
        }
    }
}
