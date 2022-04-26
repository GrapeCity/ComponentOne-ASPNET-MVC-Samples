using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcExplorer.Models
{
    public interface ITreeItem
    {
        string Header { get; set; }
        IList<ITreeItem> Children { get; }
    }

    public class Folder : ITreeItem
    {
        public string Header { get; set; }
        public IList<ITreeItem> Children { get; private set; }

        public Folder(string name)
        {
            Header = name;
            Children = new List<ITreeItem>();
        }

        public static Folder Create(string path)
        {
            var folder = new Folder(System.IO.Path.GetFileName(path));
            System.IO.Directory.GetDirectories(path).ToList().ForEach(d => folder.Children.Add(Folder.Create(d)));
            System.IO.Directory.GetFiles(path).ToList().ForEach(f => folder.Children.Add(File.Create(f)));
            return folder;
        }
    }

    public class File : ITreeItem
    {
        public string Header { get; set; }
        public DateTime DateModified { get; set; }
        public long Size { get; set; }
        public IList<ITreeItem> Children { get { return null; } }

        public File(string name)
        {
            Header = name;
        }

        public static File Create(string path)
        {
            var file = new File(System.IO.Path.GetFileName(path));
            var info = new System.IO.FileInfo(path);
            file.DateModified = info.LastWriteTime;
            file.Size = info.Length;
            return file;
        }
    }
}