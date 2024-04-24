using System;
using System.Collections;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject animatedObject;

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


