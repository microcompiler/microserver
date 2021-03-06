﻿using System.Reflection;

using Bytewizer.TinyCLR.Sockets;

namespace Bytewizer.TinyCLR.Ftp
{
    /// <summary>
    /// Represents an implementation of the <see cref="SocketServer"/> for creating network servers.
    /// </summary>
    public class FtpServerOptions : ServerOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SocketServer"/> class.
        /// </summary>
        public FtpServerOptions()
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            BannerMessage = $"TinyCLR FTP Server Ready [{version}]";
        }

        /// <summary>
        /// Specifies the server banner message.
        /// </summary>
        public string BannerMessage { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether login is allowed by anonymous users.
        /// </summary>
        public bool AllowAnonymous { get; set; }
    }
}
