using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace Smart.Utils
{
    public static class CommandLine
    {
        //--------------------------------------------------------------------------------------------------------------------------

        static CommandLine()
        {
            arguments = new Arguments(Environment.GetCommandLineArgs());
        }

        public static readonly Arguments arguments;

        //--------------------------------------------------------------------------------------------------------------------------

        public static void WindowsExecute(string cmdLine)
        {
            var fn = Path.GetFullPath(cmdLine);
            Process.Start(new ProcessStartInfo { WorkingDirectory = Path.GetDirectoryName(fn) ?? "", FileName = fn });
        }

        public static void WindowsExecute(string cmdLine, string args)
        {
            var fn = Path.GetFullPath(cmdLine);
            Process.Start(new ProcessStartInfo { WorkingDirectory = Path.GetDirectoryName(fn) ?? "", FileName = fn, Arguments = args });
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public class Arguments
        {
            private readonly StringDictionary _parameters;

            public Arguments(string[] args)
            {
                _parameters = new StringDictionary();

                var Spliter = new Regex(@"^-{1,2}|^/|=|:");
                var Remover = new Regex(@"^['""]?(.*?)['""]?$");

                string parameter = null;

                // Valid parameters forms:
                // {-,/,--}param{ ,=,:}((",')value(",'))
                // Examples: 
                // -param1 value1 --param2 /param3:"Test-:-work" 
                //   /param4=happy -param5 '--=nice=--'
                foreach (var txt in args)
                {                    
                    // Look for new parameters (-,/ or --) and a
                    // possible enclosed value (=,:)
                    var parts = Spliter.Split(txt, 3);

                    switch (parts.Length)
                    {
                        // Found a value (for the last parameter found (space separator))
                        case 1:
                            if (parameter != null)
                            {
                                if (!_parameters.ContainsKey(parameter))
                                {
                                    parts[0] = Remover.Replace(parts[0], "$1");
                                    _parameters.Add(parameter, parts[0]);
                                }
                                parameter = null;
                            }
                            // else Error: no parameter waiting for a value (skipped)
                            break;

                        // Found just a parameter
                        case 2:
                            // The last parameter is still waiting. With no value, set it to true.
                            if (parameter != null)
                            {
                                if (!_parameters.ContainsKey(parameter))
                                    _parameters.Add(parameter, "true");
                            }
                            parameter = parts[1];
                            break;

                        // Parameter with enclosed value
                        case 3:
                            // The last parameter is still waiting. With no value, set it to true.
                            if (parameter != null)
                            {
                                if (!_parameters.ContainsKey(parameter))
                                    _parameters.Add(parameter, "true");
                            }

                            parameter = parts[1];

                            // Remove possible enclosing characters (",')
                            if (!_parameters.ContainsKey(parameter))
                            {
                                parts[2] = Remover.Replace(parts[2], "$1");
                                _parameters.Add(parameter, parts[2]);
                            }

                            parameter = null;
                            break;
                    }
                }
                // In case a parameter is still waiting
                if (parameter != null)
                {
                    if (!_parameters.ContainsKey(parameter))
                        _parameters.Add(parameter, "true");
                }
            }

            public bool Has(string param)
            {
                return _parameters[param] != null;
            }

            public string Get(string param, string defaultValue)
            {
                return _parameters[param] ?? defaultValue;
            }

            public int Get(string param, int defaultValue)
            {
                var s = _parameters[param];
                if (string.IsNullOrEmpty(s)) return defaultValue;
                
                int res;
                return int.TryParse(s, out res) ? res : defaultValue;
            }

            public float Get(string param, float defaultValue)
            {
                var s = _parameters[param];
                if (string.IsNullOrEmpty(s)) return defaultValue;
                
                float res;
                return float.TryParse(s, out res) ? res : defaultValue;
            }

            public bool Get(string param, bool defaultValue)
            {
                var s = _parameters[param];
                if (string.IsNullOrEmpty(s)) return defaultValue;

                bool res;
                return bool.TryParse(s, out res) ? res : defaultValue;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
