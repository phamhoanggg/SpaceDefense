using System;
using System.Collections;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject animatedObject;

    [SerializeField] private RectTransform[] transitionObjectList;


    private WaitForSeconds waitClose, waitOpen;

    private void Awake()
    {
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            switch (clip.name)
            {
                case "Close":
                    waitClose = new WaitForSeconds(clip.length);
                    break;
                case "Open":
                    waitOpen = new WaitForSeconds(clip.length);
                    break;
            }
        }

        animatedObject.SetActive(false);

        float height = Screen.height * 1.0f / transitionObjectList.Length;

        for (int i = 0; i < transitionObjectList.Length; i++)
        {
            transitionObjectList[i].SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            transitionObjectList[i].anchoredPosition = new Vector2(0, -height * (i + 0.5f));
        }
    }

    public Coroutine Close(Action onComplete = null)
    {
        return StartCoroutine(CloseRoutine(onComplete));
    }

    private IEnumerator CloseRoutine(Action onComplete)
    {
        animatedObject.SetActive(true);
        animator.SetTrigger("Close");
        yield return waitClose;
        onComplete?.Invoke();
    }

    public Coroutine Open(Action onComplete = null)
    {
        return StartCoroutine(OpenRoutine(onComplete));
    }

    private IEnumerator OpenRoutine(Action onComplete)
    {
        animator.SetTrigger("Open");
        yield return waitOpen;
        onComplete?.Invoke();
        animatedObject.SetActive(false);
    }
}


