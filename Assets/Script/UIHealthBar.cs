using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIHealthBar : MonoBehaviour
{
    // public Mask mask;
    public static UIHealthBar instance { get; private set; } 
    public Mask mask;
    public Text enemyText;

    float originalSize;
    private void Awake()
    {
        instance = this;
        originalSize = mask.rectTransform.rect.width;
        Debug.Log(" Original Size: " + originalSize);
    }


    
    public void SetValue(float value)
    {
        Debug.Log(" Current Size: " +  value);

        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }

    public void SetEnemy(int enemiesCount)
    {
        if (enemiesCount < 5)
            enemyText.text = enemiesCount.ToString() + "/5";
        else {
            enemyText.color = Color.green;
            enemyText.text = "Mission Complete";
        }
    }

}
