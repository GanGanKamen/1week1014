using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeCanvas : MonoBehaviour
{
    [SerializeField] private Image faderImg;
    [SerializeField] private float time;
    public float alpha;
    public int fadeSwitch;
    private float fadeDelta;
    // Start is called before the first frame update
    void Start()
    {
        fadeDelta = Time.deltaTime / time;
    }

    // Update is called once per frame
    void Update()
    {
        faderImg.color = new Color(faderImg.color.r, faderImg.color.g, faderImg.color.b, alpha);
        switch (fadeSwitch)
        {
            default:
                break;
            case 1:
                if (alpha > 0)
                {
                    alpha -= fadeDelta;
                }
                else
                {
                    alpha = 0;
                    fadeSwitch = 0;
                }
                break;
            case 2:
                if (alpha < 1)
                {
                    alpha += fadeDelta;
                }
                else
                {
                    alpha = 1;
                    fadeSwitch = 0;
                }
                break;
        }
    }
}
