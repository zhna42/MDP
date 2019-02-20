using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace watch_xml.watcher
{
    class WatcherOne
    {
        public Broadcasting dataFile { get; set; }
        public MainWindow context { get; set; }
        public bool watcherXml = false;

        Thread myThread;
        FileSystemWatcher watcher;

        public void Start()
        {
            if (!watcherXml)
            {
                watcherXml = true;
                myThread = new Thread(Run); 
                myThread.Start();
                readFile();
            }
        }

        public void Stop()
        {
            if (watcherXml)
            {
                watcherXml = false;
                watcher.EnableRaisingEvents = false;
                myThread.Abort();
                myThread.Join();
                update("", "", "", "off");
            }
        }
        private void update(string err, string name, string artist, string status)
        {
            dataFile.err = err;
            dataFile.name = name;
            dataFile.artist = artist;
            dataFile.status = status;
            if (artist == "" && name == "")
                File.WriteAllText(dataFile.txt, "");
            else
                File.WriteAllText(dataFile.txt, artist + " - " + name);

            context.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                context.list_name_main.Items.Refresh();
            });
        }
        private void Run()
        {
            watcher = new FileSystemWatcher();
            watcher.Path = Path.GetDirectoryName(dataFile.xml);
            watcher.Filter = Path.GetFileName(dataFile.xml);
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.LastAccess;
            watcher.Changed += OnChanged;
            watcher.Created += OnChanged;
            watcher.Deleted += OnDeleted;
            watcher.Renamed += OnRenamed;
            watcher.EnableRaisingEvents = true;
        }
        private void OnChanged(object source, FileSystemEventArgs e) => readFile();
        private static void OnRenamed(object source, RenamedEventArgs e) => Console.WriteLine("OnRenamed");
        private void OnDeleted(object source, FileSystemEventArgs e) => update("Файл исчез", "", "", "warning");

        private void readFile()
        {
            ReadParserXml rpx = new ReadParserXml();
            rpx.readXml(dataFile.xml); 
            rpx.parseXml(); 
            if (rpx.isRet == 0)
                update(rpx.err, "", "", "warning");
            else
                update("", rpx.name, rpx.artist, "on");
        }
        
    }
}
