using MoonSharp.Interpreter;

namespace GameEngine.Scripting
{
    public class ScriptFunction
    {
        private readonly string _functionName;
        private readonly Script _script;

        internal ScriptFunction(Script script, string functioName)
        {
            _script = script;
            _functionName = functioName;
        }
        public DynValue Call(params object[] parameters)
        {
            return _script.Call(_script.Globals[_functionName], parameters);
        }
    }
}