using System;
using System.Collections.Generic;
using System.IO;
using C1.Web.Api.Storage;

namespace CustomPdfProvider.Models
{
    public class StreamFileStorage : IFileStorage
    {
        private Stream _stream;

        public StreamFileStorage(Stream stream)
        {
            _stream = stream;
        }

        public string Path
        {
            get;
            private set;
        }

        public bool Exists
        {
            get
            {
                return true;
            }
        }

        public string Name
        {
            get;
            private set;
        }

        public bool ReadOnly
        {
            get
            {
                return true;
            }
        }

        public void Delete()
        {
        }

        public Stream Read()
        {
            return _stream;
        }

        public void Write(Stream stream)
        {
        }

    public List<ListResult> List()
    {
      throw new NotImplementedException();
    }
  }
}