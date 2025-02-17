﻿//-----------------------------------------------------------------------
// <copyright>
//      Created by Matt Weber <matt@badecho.com>
//      Copyright @ 2025 Bad Echo LLC. All rights reserved.
//
//      Bad Echo Technologies are licensed under the
//      GNU Affero General Public License v3.0.
//
//      See accompanying file LICENSE.md or a copy at:
//      https://www.gnu.org/licenses/agpl-3.0.html
// </copyright>
//-----------------------------------------------------------------------

using System.IO;
using BadEcho.Extensions;
using BadEcho.Vision.Extensibility;

namespace BadEcho.Vision;

/// <summary>
/// Provides a watcher for a Vision module's message file that will notify listeners upon any changes being made to it.
/// </summary>
internal sealed class MessageFileWatcher : IMessageFileProvider, IDisposable
{
    private readonly FileSystemWatcher _watcher;
    private readonly bool _processNewMessagesOnly;
    
    private long _lastReadPosition;
    private bool _disposed;

    /// <inheritdoc/>
    public event EventHandler<EventArgs<string>>? NewMessages;

    /// <inheritdoc/>
    public string? CurrentMessages 
    { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MessageFileWatcher"/> class.
    /// </summary>
    /// <param name="module">The vision module whose message file this watcher will be monitoring.</param>
    /// <param name="messageFilesDirectory"
    /// >The path relative from Vision's base directory to the directory where all message files are being written to.
    /// </param>
    public MessageFileWatcher(IVisionModule module, string messageFilesDirectory)
    {
        Require.NotNull(module, nameof(module));

        // FileSystemWatcher requires a valid path value -- it doesn't treat an empty path as the current directory.
        if (string.IsNullOrEmpty(messageFilesDirectory)) 
            messageFilesDirectory = AppContext.BaseDirectory;

        _processNewMessagesOnly = module.ProcessNewMessagesOnly;
        string messageFilePath = Path.Combine(messageFilesDirectory, module.MessageFile);

        var messageFile = new FileInfo(messageFilePath);

        if (messageFile.Exists)
        {
            CurrentMessages = messageFile.ReadAllText(FileShare.ReadWrite);
            _lastReadPosition = messageFile.Length;
        }

        _watcher = new FileSystemWatcher
                   {
                       Filter = module.MessageFile,
                       NotifyFilter = NotifyFilters.LastWrite,
                       Path = messageFilesDirectory
                   };

        _watcher.Changed += HandleMessageFileChanged;
        _watcher.EnableRaisingEvents = true;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
            return;

        _watcher.Dispose();

        _disposed = true;
    }

    private void HandleMessageFileChanged(object sender, FileSystemEventArgs e)
    {
        var messageFile = new FileInfo(e.FullPath);

        if (messageFile.Length == 0)
            return;

        // Injected Omnified code will be writing to the message file at high frequency, so we should assume the file is
        // almost always open by that process.
        CurrentMessages = _processNewMessagesOnly
            ? messageFile.ReadAllText(FileShare.ReadWrite, _lastReadPosition)
            : messageFile.ReadAllText(FileShare.ReadWrite);

        if (!string.IsNullOrEmpty(CurrentMessages))
        {
            _lastReadPosition = messageFile.Length;
            NewMessages?.Invoke(this, new EventArgs<string>(CurrentMessages));
        }
    }
}