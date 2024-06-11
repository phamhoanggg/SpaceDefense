using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonConstruction : MonoBehaviour
{
    [SerializeField] Image frameImg;
    [SerializeField] Image constructionImg;
    [SerializeField] Construction prefabConstruction;

    private void Start()
    {
        constructionImg.sprite = prefabConstruction.avatarSprite;
    }

    public void SetPrefab(Construction prefab)
    {
        prefabConstruction = prefab;
    }

    public void OnClick()
    {
        FormGameplay.Instance.Selecting_block.transform.position = transform.position;
        CoreManager.Instance.selectingPrefab = prefabConstruction;
        FormGameplay.Instance.OpenPopupPlaceConstruction();
    }
}
