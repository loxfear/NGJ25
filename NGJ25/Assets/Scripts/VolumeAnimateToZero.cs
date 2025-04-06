using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeAnimateToZero : MonoBehaviour
{
    private Volume volume;
    public float speed;
    public bool fadeIn = false;
    public float setTo = 0;

    // Start is called before the first frame update
    void Start()
    {
        volume = this.GetComponent<Volume>();
        volume.weight = setTo;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            FadeIn();
        }
        else
        {
            if (volume.weight > 0.01)
            {
                volume.weight = Mathf.Lerp(volume.weight, 0, Time.deltaTime * speed);
            }
            else
            {
                volume.weight = 0;
            }
        }

    }

    void FadeIn()
    {
        if (volume.weight < 0.9)
        {
            volume.weight = Mathf.Lerp(volume.weight, 1, Time.deltaTime * (speed * 2.5f));
        }
        else
        {
            volume.weight = 1;
        }
    }
}
