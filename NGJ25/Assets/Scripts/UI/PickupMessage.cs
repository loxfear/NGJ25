using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PickupMessage : MonoBehaviour
{
    public TMPro.TMP_Text message;
    public CanvasGroup canvasGroup;
    private RectTransform _transform; 
    public void Init(string text)
    {
        message.SetText(text);
        _transform = GetComponent<RectTransform>();
        StartCoroutine(Dissapear());
        Invoke("Reset",3 );
        
    }

    IEnumerator Dissapear()
    {
        for (int i = 0; i <= 100; i++)
        {
            canvasGroup.alpha = ((100-i) * 0.01f);
            _transform.anchoredPosition =_transform.anchoredPosition - new Vector2(0, i*Time.deltaTime *10 );
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    void Reset()
    {
        StopCoroutine(Dissapear());
        message.SetText("");
        Destroy(this);
    }
}
