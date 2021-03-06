﻿using Bytewizer.TinyCLR.Sockets;

namespace Bytewizer.TinyCLR.Sntp
{
    /// <summary>
    /// Represents an options method to configure <see cref="SocketServer"/> specific features.
    /// </summary>
    /// <param name="configure">The <see cref="SocketServer"/> configuration specific features.</param>
    public delegate void ServerOptionsDelegate(SntpServerOptions configure);
}