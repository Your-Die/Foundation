namespace Chinchillada.Foundation
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Mutiny/LogData")]
    public class LogHandler : ScriptableObject
    {
        [SerializeField] private bool allowLog;

        [SerializeField] private LogType type = LogType.Log;
        
        public void Log(string message, object context = null)
        {
            if (this.allowLog) 
                LogInternal(message, context);
        }

        private void LogInternal(string message, object context)
        {
            if (context == null)
                Debug.unityLogger.Log(this.type, message);
            else
                Debug.unityLogger.Log(this.type, message, context);
        }
    }
}