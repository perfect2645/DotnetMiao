namespace JsInteract.ClearJs
{
    public class JsEngine : JsEngineBase
    {
        public override void InitEngine(params string[] files)
        {
            try
            {
                Engine.DocumentSettings.AccessFlags = Microsoft.ClearScript.DocumentAccessFlags.EnableFileLoading;
                Engine.DefaultAccess = Microsoft.ClearScript.ScriptAccess.Full;
                foreach(var file in files)
                {
                    Engine.ExecuteDocument(file);
                }
            }
            catch(Exception ex)
            {
                Logging.GLog.Logger.Error(ex);
            }
        }
    }
}
