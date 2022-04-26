using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.Reflection;

namespace C1.Web.Mvc.Extensions.FileProvider
{
    /// <summary>
    /// Provider for embedded resource 
    /// </summary>
    public class EmbeddedResourceFileProvider : IFileProvider
    {
        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return null;
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            return new EmbeddedResourceFileInfo(subpath);
        }

        public IChangeToken Watch(string filter)
        {
            throw new NotImplementedException();
        }
    }

    public class EmbeddedResourceFileInfo : IFileInfo
    {
        private string BASE_PATH = "C1.Web.Mvc.Extensions.Resources";
        private string path;
        private Stream contentStream;

        public bool Exists => contentStream != null;

        public long Length => contentStream != null ? contentStream.Length : -1;

        public string PhysicalPath => null;

        public string Name => Path.GetFileName(path);

        public DateTimeOffset LastModified => new DateTimeOffset(DateTime.Today);

        public bool IsDirectory => false;

        public EmbeddedResourceFileInfo(string path)
        {
            this.path = path;
            CreateReadStream();
        }

        /// <summary>
        /// create content stream for embedded file
        /// </summary>
        /// <returns></returns>
        public Stream CreateReadStream()
        {
            if (contentStream == null)
            {
                string resourceName = string.Concat(BASE_PATH, this.path.Replace('/', '.'));
                contentStream = typeof(EmbeddedResourceFileInfo).GetTypeInfo().Assembly.GetManifestResourceStream(resourceName);
            }
            return contentStream;
        }
    }
}
