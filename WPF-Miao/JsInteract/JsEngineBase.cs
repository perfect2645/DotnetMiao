using Microsoft.ClearScript.V8;

namespace JsInteract
{
    public abstract class JsEngineBase
    {
        public V8ScriptEngine Engine { get; }

        public JsEngineBase()
        {
            Engine = new V8ScriptEngine();
        }

        public abstract void InitEngine(params string[] files);
    }
}
