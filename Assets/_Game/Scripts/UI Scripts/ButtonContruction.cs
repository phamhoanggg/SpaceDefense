using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonContruction : MonoBehaviour
{
    [SerializeField] Image frameImg;
    [SerializeField] Image contructionImg;
    [SerializeField] Contruction prefabContruction;

    private void Start()
    {
        contructionImg.sprite = prefabContruction.avatarSprite;
    }

    public void OnClick()
    {
        FormGameplay.Instance.Selecting_block.transform.position = transform.position;
        CoreManager.Instance.selectingPrefab = prefabContruction;
        FormGameplay.Instance.OpenPopupPlaceConstruction();
    }
}
