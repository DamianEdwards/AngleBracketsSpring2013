using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AdvancedWebForms.Async
{
    public class FileDownload : HttpTaskAsyncHandler
    {
        private const int _blockSize = 1024 * 8;

        public override async Task ProcessRequestAsync(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Buffer = false;
            context.Response.BufferOutput = false;

            // Imagine we're getting the file from somewhere more exciting such that
            // it couldn't be served directly by IIS, e.g. a database
            var filePath = context.Server.MapPath("~/Async/lorem.txt");

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: _blockSize, useAsync: true))
            {
                var buffer = new byte[_blockSize];

                while (fileStream.Position < fileStream.Length)
                {
                    var blockSize = fileStream.Position + _blockSize < fileStream.Length
                        ? _blockSize
                        : fileStream.Length - fileStream.Position;

                    if (blockSize != _blockSize)
                    {
                        buffer = new byte[blockSize];
                    }

                    // Read a block of bytes from the file async
                    await fileStream.ReadAsync(buffer, 0, (int)blockSize);

                    // Write the block of bytes to the response stream
                    context.Response.BinaryWrite(buffer);

                    // Flush the block of bytes to the response stream async
                    await context.Response.FlushAsync();
                }
            }
        }
    }
}