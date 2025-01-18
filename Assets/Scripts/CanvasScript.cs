using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    public Image backImg;
    private void Update()
    {
        FixResolution();
    }

    void FixResolution() 
    {
        RectTransform canvas = GetComponent<RectTransform>();
        if (backImg != null && canvas != null) 
        {
            RectTransform backRect = backImg.GetComponent<RectTransform>();
            backRect.sizeDelta = new Vector2(canvas.rect.width, canvas.rect.height);
            backRect.anchoredPosition = Vector2.zero;
        }
    }

    
}
