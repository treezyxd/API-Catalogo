using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Logging
{
    public class CustomerLogger : ILogger
    {
        readonly string loggerName;

        readonly CustomLoggerProviderConfiguration loggerConfig;

        public CustomerLogger(string name, CustomLoggerProviderConfiguration config)
        {
            loggerName = name;
            loggerConfig = config;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == loggerConfig.LogLevel;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
                Exception exception, Func<TState, Exception, string> formatter)
        {
            string mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";

            EscreverTextoNoArquivo(mensagem);
        }

        public void EscreverTextoNoArquivo(string mensagem)
        {
            string caminhoArquivoLog = @"d:\dados\log\treezyLog.txt";

            using (StreamWriter streamWriter = new StreamWriter(caminhoArquivoLog, true))
            {
                try
                {
                    streamWriter.WriteLine(mensagem);
                    streamWriter.Close();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}