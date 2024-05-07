using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemConstruction : MonoBehaviour
{
    public GameObject locked_Frame;
    public Image constructionImage;

    public void OnInit(ConstructionData data)
    {
        locked_Frame.SetActive(!data.IsUnlocked);
        constructionImage.sprite = data.constructionPrefab.avatarSprite;
    }

    public void SetUnlock(bool isUnlocked)
    {
        locked_Frame.SetActive(!isUnlocked);
    }
}
