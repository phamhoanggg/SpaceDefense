using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private ScriptableLevel level_SO;
    [SerializeField] private TMP_Text planetName_tmp;
    [SerializeField] private string planetName;
    [SerializeField] private GameObject resAvail_gameobject;
    [SerializeField] private GameObject lockObject;
    [SerializeField] private int index;
    [SerializeField] private GameObject launchBtn_Obj, unlock_text_Obj;

    private void Start()
    {
        lockObject.SetActive(index > DataManager.Instance.gameData.levelUnlocked);      
    }
    public void OnSelected()
    {
        FormSelectLevel.Instance.SetLevelSelecting(level_SO);
        planetName_tmp.text = planetName;
        resAvail_gameobject.SetActive(true);
        FormSelectLevel.Instance.SelectingLevelFrame.transform.position = transform.position;
        launchBtn_Obj.SetActive(index <= DataManager.Instance.gameData.levelUnlocked);
        unlock_text_Obj.SetActive(index > DataManager.Instance.gameData.levelUnlocked);
    }
}
