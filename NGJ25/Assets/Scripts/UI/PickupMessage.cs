using UnityEngine;

public class PickupMessage : MonoBehaviour
{
    public TMPro.TMP_Text message;
    public void Init(string text)
    {
        message.SetText(text);
        Invoke("Reset",1);
    }

    void Reset()
    {
        message.SetText("");
    }
}
