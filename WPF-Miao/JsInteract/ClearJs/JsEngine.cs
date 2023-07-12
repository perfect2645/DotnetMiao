namespace JsInteract.ClearJs
{
    public class JsEngine : JsEngineBase
    {
        public override void InitEngine(params string[] files)
        {
            try
            {
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
