using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PathWatcher.ConsoleApp.Entities
{
    public class Log : EventArgs, ILogger
    {
        public Log() { }


        public void Gravar()
        {
            this.LogTrace("Algo na pasta aconteceu!");
        }

        #region Implementacao de Contrato
        
        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {

            var message = String.Empty;
            var parameters = (state as IEnumerable<KeyValuePair<string, object>>)?.ToDictionary(i => i.Key, i => i.Value);
            if (formatter != null)
            {
                message += formatter(state, exception);
            }

            using (StreamWriter sr = new StreamWriter($"{Guid.NewGuid()}.txt"))
                sr.WriteLine(DateTime.Now.ToLongDateString(), message);
        }

        #endregion
    }
}
