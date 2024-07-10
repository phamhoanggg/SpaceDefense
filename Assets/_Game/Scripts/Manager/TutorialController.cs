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

    bool isFirstOpen;
    private void Start()
    {
        if (DataManager.Instance.gameData.currentLevelIndex == -1)
        {
            current_tut_index = 0;
            current_button_index = 0;
            InputManager.Instance.SetBlockInput(true);
            skipBtn_obj.SetActive(false);
            isFirstOpen = DataManager.Instance.gameData.isFirstOpen;
            DataManager.Instance.gameData.isFirstOpen = false;
            StartCoroutine(OpenTutorialDialog(0, 1f));
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public IEnumerator OpenTutorialDialog(int tut_index, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        list_tut[tut_index].SetActive(true);

        if (current_tut_index == 2 && !isFirstOpen) skipBtn_obj.SetActive(true);

        if (current_tut_index == 5)
        {
            InputManager.Instance.SetBlockInput(false);
        }

        if (current_tut_index == 7)
        {
            FormGameplay.Instance.DisplayBottomObject();
        }

        if (list_tut[tut_index].GetComponent<DialogueUtils>() != null)
        {
            yield return new WaitUntil(() => list_tut[tut_index].GetComponent<DialogueUtils>().IsComplete);

            StartCoroutine(DisplayNextButtonAfter(1f, current_button_index));
        }
        
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

    public void NextTutorial(float delayTime)
    {
        list_tut[current_tut_index].SetActive(false);
        current_tut_index++;
        
        if (current_tut_index < list_tut.Count)
        {
            StartCoroutine(OpenTutorialDialog(current_tut_index, delayTime));
        }
    }

    public void ClickNextButton()
    {
        NextTutorial(0.5f);
        AudioManager.Instance.PlaySound(SoundId.Click);
    }

    public void SkipButton()
    {
        popupSKip.Open();
    }

    public void CompleteButton()
    {
        DataManager.Instance.gameData.ReFillResource();
        SceneLoader.Instance.LoadScene(SceneId.Menu);
    }
}
