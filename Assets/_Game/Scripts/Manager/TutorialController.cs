using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : SingletonMB<TutorialController>
{
    public List<GameObject> list_tut;
    public List<Button> list_Next_button;

    public int CurrentTut_index => current_tut_index;
    private int current_tut_index;
    private int current_button_index;

    [SerializeField] private GameObject skipBtn_obj;
    [SerializeField] private PopupSkipTutorial popupSKip;
    private void Start()
    {
        if (DataManager.Instance.gameData.currentLevelIndex == -1)
        {
            current_tut_index = 0;
            current_button_index = 0;
            InputManager.Instance.SetBlockInput(true);
            skipBtn_obj.SetActive(false);
            OpenTutorialDialog(0);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void OpenTutorialDialog(int tut_index)
    {
        list_tut[tut_index].SetActive(true);

        if (current_tut_index == 2) skipBtn_obj.SetActive(true);

        if (current_tut_index == 5)
        {
            InputManager.Instance.SetBlockInput(false);
        }

        if (current_tut_index == 7)
        {
            FormGameplay.Instance.DisplayBottomObject();
        }

        StartCoroutine(DisplayNextButtonAfter(3, current_button_index));
    }

    public IEnumerator DisplayNextButtonAfter(float sec, int button_index)
    {
        yield return new WaitForSeconds(sec);
        if (current_button_index < list_Next_button.Count)
        {
            current_button_index++;
            list_Next_button[button_index].gameObject.SetActive(true);
        } 
    }

    public void OnClickNextButton()
    {
        list_tut[current_tut_index].SetActive(false);
        current_tut_index++;
        
        if (current_tut_index < list_tut.Count)
        {
            OpenTutorialDialog(current_tut_index);
        }
    }

    public void SkipButton()
    {
        popupSKip.Open();
    }

    public void CompleteButton()
    {
        SceneLoader.Instance.LoadScene(SceneId.Menu);
    }
}
