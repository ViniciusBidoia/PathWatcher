using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PathWatcher.ConsoleApp.Entities
{
    public class PathWatcher
    {
        public string Path { get; private set; }
        public string Extension { get; private set; }
        public string Classe { get; private set; }
        public string Method { get; private set; }
        public string AssemblyName { get; private set; }

        //Eventos disponiveis no FileSystemWatcher
        private event EventHandler OnCreated;
        private event EventHandler OnDeleted;
        private event EventHandler OnRenamed;
        private event EventHandler OnChanged;

        public PathWatcher(string path, string extension, string classe, string method, string assemblyName)
        {
            Path = path;
            Extension = extension;
            Classe = classe;
            Method = method;
            AssemblyName = assemblyName;

            //Escolhe dependendo da regra de negocio
            OnCreated += PathWatcher_OnCreated; 
            //OnDeleted += PathWatcher_OnDeleted; 
            //OnRenamed += PathWatcher_OnRenamed; 
            //OnChanged += PathWatcher_OnChanged; 

            Towork();
        }

        private void Towork()
        {
            var watcher = new FileSystemWatcher();
            watcher.Path = Path;
            watcher.Filter = $"*.{Extension}";
            watcher.Created += new FileSystemEventHandler(OnCreated);
            //watcher.Deleted += new FileSystemEventHandler(OnDeleted);
            //watcher.Renamed += new FileSystemEventHandler(OnRenamed);
            //watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
        }

        private void PathWatcher_OnChanged(object sender, EventArgs e)
        {
            ExecuteMethod();
        }

        private void PathWatcher_OnRenamed(object sender, EventArgs e)
        {
            ExecuteMethod();
        }

        private void PathWatcher_OnDeleted(object sender, EventArgs e)
        {
            ExecuteMethod();
        }

        private void PathWatcher_OnCreated(object sender, EventArgs e)
        {
            ExecuteMethod();
        }
               
        private void ExecuteMethod()
        {
            //Regra do que deve fazer após ser disparado o handler

            Assembly assembly = Assembly.LoadFrom($"{AssemblyName}.dll");
            var type = assembly.GetTypes().SingleOrDefault(x => x.Name == Classe);
            ConstructorInfo Constructor = type.GetConstructor(Type.EmptyTypes);
            object obj = Constructor.Invoke(new object[] { });
            MethodInfo methodInfo = type.GetMethod(Method);
            methodInfo.Invoke(obj, null);
        }

       

    }
}
