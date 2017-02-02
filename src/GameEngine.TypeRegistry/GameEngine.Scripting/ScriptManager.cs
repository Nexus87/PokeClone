using System.Collections.Generic;
using MoonSharp.Interpreter;

namespace GameEngine.Scripting
{

    public class ScriptManager
    {
        private readonly Dictionary<string, string> _scripts = new Dictionary<string, string>();

        public void AddScriptFile(string path, string functionName)
        {
            _scripts[functionName] = path;
        }

        public ScriptFunction GetScriptFunction<T1, T2, T3>(string functionName)
        {
            var script = new Script();
            script.DoFile(_scripts[functionName]);

            return new ScriptFunction(script, functionName);
        }
    }
}