using Chinchillada.Foundation;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class TextMirror : MonoBehaviour
{
    [SerializeField, Required] private TMP_Text toMirror;
    [SerializeField, Required, FindComponent] private TMP_Text mirroringText;

    [Button]
    private void MirrorText()
    {
        this.mirroringText.text = this.toMirror.text;
    }
    
    private void OnEnable() => this.toMirror.RegisterDirtyVerticesCallback(this.MirrorText);

    private void OnDisable() => this.toMirror.UnregisterDirtyVerticesCallback(this.MirrorText);


}
