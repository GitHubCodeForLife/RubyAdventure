using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIHealthBar : MonoBehaviour
{
    // public Mask mask;
    public static UIHealthBar instance { get; private set; } 
    public Mask mask;

    float originalSize;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        originalSize = mask.rectTransform.rect.width;
        Debug.Log(" Original Size: " + originalSize);
    }
    
    public void SetValue(float value)
    {
       // Debug.Log(" Current Size: " +  value);

        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }


}
