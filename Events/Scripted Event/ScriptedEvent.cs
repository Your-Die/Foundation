using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Chinchillada/Scripted Event", fileName = "Event")]
public class ScriptedEvent : ScriptableObject
{ 
    public event Action Raised;

    public void Raise()
    { 
        Raised?.Invoke();
    } 
}