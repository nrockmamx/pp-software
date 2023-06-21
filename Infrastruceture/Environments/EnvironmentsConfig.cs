using System.ComponentModel;
using System.Diagnostics;
using Domain.Environments;
using LazyCache;
using Microsoft.Extensions.Configuration;
using Serilog;
using ILogger = Serilog.ILogger;

namespace Infrastruceture.Environments
{
    public class EnvironmentsConfig : IEnvironmentsConfig
    {
        public delegate bool TryParseHandler<T>(string value, out T result);

        private readonly IConfiguration _hostConf;
        private readonly ILogger _log;
        private bool _suppressLog;
        private readonly IAppCache? _cache;


        public EnvironmentsConfig(IConfiguration pConfig, IAppCache? cache)
        {
            _hostConf = pConfig;

            _cache = cache;
            
            _log = Log.ForContext<EnvironmentsConfig>();

            Task statisticsTask = Task.Factory.StartNew(async () =>
            {
                await Task.Delay(1000 * 20);
                _suppressLog = true;
            });
        }

        public EnvironmentsConfig(IConfiguration pConfig)
        {
            _hostConf = pConfig;
            _log = Log.ForContext<EnvironmentsConfig>();

            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(1000 * 20);
                _suppressLog = true;
            });
        }

        public Dictionary<string, string>? GetKafkaConn(string pKey)
        {
            var envValue = Environment.GetEnvironmentVariable(pKey);

            var data = new Dictionary<string, string>();
            if (envValue != null)
                try
                {
                    var lines = envValue.Split(";");

                    foreach (var line in lines)
                    {
                        var pos = line.Replace(" ", "").Split('=');
                        data.Add(pos[0], pos[1]);
                    }

                    return data;
                }
                catch (Exception)
                {
                    _log.Error("Invalid ENV value of {Key}", pKey);
                    Process.GetCurrentProcess().Kill();
                    return null;
                }

            try
            {
                data = File.ReadAllLines("conf/confluent.consume.conf")
                    .Where(line => !line.StartsWith("#"))
                    .ToDictionary(
                        line => line.Substring(0, line.IndexOf('=')),
                        line => line.Substring(line.IndexOf('=') + 1));

                return data;
            }
            catch (Exception)
            {
                _log.Error("Invalid ENV value of {Key}", pKey);
                Process.GetCurrentProcess().Kill();
                return null;
            }
        }

        public T GetValue<T>(string pKey)
        {
            var envValue = Environment.GetEnvironmentVariable(pKey);
            if (envValue != null)
                try
                {
                    var converter = TypeDescriptor.GetConverter(typeof(T));
                    if (!_suppressLog)
                        _log.Information("Loaded config from :ENVIRONMENT: - {Key}", pKey);

                    if (_cache != null)
                    {
                        return _cache.GetOrAdd(pKey, () => (T) converter.ConvertFromString(envValue)!, new TimeSpan(0, 0, 0, 30));
                    }

                    return (T) converter.ConvertFromString(envValue)!;
                }
                catch
                {
                    _log.Error("Invalid ENV value of {Key}", pKey);
                    Process.GetCurrentProcess().Kill();
                    return default!;
                }

            var value = _hostConf[pKey];
            if (value != null)
            {
                if (!_suppressLog)
                    _log.Information($"Loaded config from :FILE: - {pKey}");
                return _hostConf.GetValue<T>(pKey);
            }

            _log.Error($"Not found ENV value of {pKey}");
            Process.GetCurrentProcess().Kill();

            return default!;
        }

        public string? GetConnectionString(string pKey)
        {
            var envValue = Environment.GetEnvironmentVariable(pKey);
            if (envValue != null)
            {
                //_log.Information("Loaded connstring from :ENVIRONMENT: - {Key}", pKey);
                return envValue;
            }

            envValue = _hostConf.GetConnectionString(pKey);
            if (envValue != null)
            {
                //_log.Information("Loaded connstring from :FILE: - {Key}", pKey);
                return envValue;
            }

            //_log.Error("Not found GetConnectionString of {Key}", pKey);
            Process.GetCurrentProcess().Kill();
            return null;
        }

        //var value = TryParse<int>("123", int.TryParse);
        //var value2 = TryParse<decimal>("123.123", decimal.TryParse);
        public T? TryParse<T>(string value, TryParseHandler<T> handler) where T : struct
        {
            if (string.IsNullOrEmpty(value))
                return null;
            T result;
            if (handler(value, out result))
                return result;
            _log.Warning("Invalid value '{0}'", value);
            return null;
        }
    }
}