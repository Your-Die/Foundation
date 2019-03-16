using UnityEngine;
using UnityEngine.Events;

public class ScriptedEventListener : MonoBehaviour
{
    [SerializeField] private ScriptedEvent _event;

    public UnityEvent Response;

    private void OnEnable()
    {
        _event.Raised += OnEventRaised;
    }

    private void OnDisable()
    {
        _event.Raised -= OnEventRaised;
    }

    public void OnEventRaised()
    {
        Response?.Invoke();
    }
}
