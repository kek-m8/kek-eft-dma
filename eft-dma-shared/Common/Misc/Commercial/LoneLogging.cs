using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace eft_dma_shared.Common.Misc.Commercial
{
    public static class LoneLogging
    {
        private static readonly Action<string> _writeLine;

        static LoneLogging()
        {
            /*string logFilePath = "dma.log";
            _writeLine = message =>
            {
                try
                {
                    File.AppendAllText(logFilePath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {message}{Environment.NewLine}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Logging failed: {ex}");
                }
            };*/
        }

        /// <summary>
        /// Write a message to the log with a newline.
        /// </summary>
        /// <param name="data">Data to log. Calls .ToString() on the object.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine(object data)
        {
            Debug.WriteLine(data);
            _writeLine?.Invoke(data.ToString());
        }
    }
}