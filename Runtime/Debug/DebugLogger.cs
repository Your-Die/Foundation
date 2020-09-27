using JetBrains.Annotations;
using UnityEngine;

public class DebugLogger : MonoBehaviour
{
    [SerializeField] private string message;

    [UsedImplicitly]
    public void LogFieldMessage() => this.LogMessage(this.message);

    public void LogMessage(string message)
    {
        Debug.Log(message);
    }
}
