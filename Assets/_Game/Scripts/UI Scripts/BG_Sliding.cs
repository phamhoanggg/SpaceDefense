using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BG_Sliding : MonoBehaviour
{
    [SerializeField] RawImage rawImg;
    [SerializeField] float y_slide;
    void Update()
    {
        rawImg.uvRect = new Rect(rawImg.uvRect.position + new Vector2(0, y_slide) * Time.deltaTime, rawImg.uvRect.size);
    }
}
