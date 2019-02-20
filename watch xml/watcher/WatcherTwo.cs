using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace watch_xml.watcher
{
    class WatcherTwo
    {
        public Retransmission dataFile { get; set; }
        public MainWindow context { get; set; }

        public bool watcherXml = false, watcherXml2 = false;
        Thread myThread, myThread2;
        FileSystemWatcher watcher, watcher2;
        

        public void Start()
        {
            if (!watcherXml)
            {
                watcherXml = true;
                myThread = new Thread(Run); 
                myThread.Start();
                readFileXml();
            }
        }

        public void Stop()
        {
            Stop2();
            if (watcherXml)
            {
                watcherXml = false;
                watcher.EnableRaisingEvents = false;
                myThread.Abort();
                myThread.Join();
                update("", "", "", "off");
            }
        }


        private void Start2()
        {
            if (!watcherXml2)
            {
                watcherXml2 = true;
                myThread2 = new Thread(Run2); 
                myThread2.Start();
                readFileXml2();
            }
        }

        private void Stop2()
        {
            if (watcherXml2)
            {
                watcherXml2 = false;
                watcher2.EnableRaisingEvents = false;
                myThread2.Abort();
                myThread2.Join();
            }
        }


        private void update(string err, string name, string artist, string status)
        {
            dataFile.err = err;
            dataFile.name = name;
            dataFile.artist = artist;
            dataFile.status = status;
            if(artist == "" && name == "")
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
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            watcher.Changed += OnChanged;
            watcher.Created += OnChanged;
            watcher.Deleted += OnDeleted;
            watcher.Renamed += OnRenamed;
            watcher.EnableRaisingEvents = true;
        }
        private void Run2()
        {
            watcher2 = new FileSystemWatcher();
            watcher2.Path = Path.GetDirectoryName(dataFile.xml2);
            watcher2.Filter = Path.GetFileName(dataFile.xml2);
            watcher2.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            watcher2.Changed += OnChanged2;
            watcher2.Created += OnChanged2;
            watcher2.Deleted += OnDeleted2;
            watcher2.Renamed += OnRenamed2;
            watcher2.EnableRaisingEvents = true;
        }

        private void OnDeleted(object source, FileSystemEventArgs e)
        {
            Stop2();
            update("Ожидаем xml1", "", "", "warning");
        } 
        private void OnChanged(object source, FileSystemEventArgs e) => readFileXml();
        private void OnRenamed(object source, RenamedEventArgs e) => Console.WriteLine("OnRenamed");
        
        private void OnDeleted2(object source, FileSystemEventArgs e) => update("Ожидаем xml2", "", "", "warning");
        private void OnChanged2(object source, FileSystemEventArgs e) => readFileXml2();
        private void OnRenamed2(object source, RenamedEventArgs e) => Console.WriteLine("OnRenamed");

        private void readFileXml2()
        {
            ReadParserXml rpx = new ReadParserXml();
            rpx.readXml(dataFile.xml2); 
            rpx.parseXml();
            if (rpx.isRet == 0)
                update(rpx.err, "", "", "warning");
            else
                update("", rpx.name, rpx.artist, "on");
        }


        private void readFileXml()
        {
            ReadParserXml rpx = new ReadParserXml();
            rpx.readXml(dataFile.xml);
            rpx.parseXml();
            if (rpx.isRet == 0)
            {
                update(rpx.err, "", "", "warning");
            }
            else
            {
                if (rpx.retransmission == "0")
                {
                    Console.WriteLine("Start Start2");
                    Start2();
                }
                else
                {
                    Stop2();
                    update("", rpx.name, rpx.artist, "on");
                }
            }
        }
    }
}
