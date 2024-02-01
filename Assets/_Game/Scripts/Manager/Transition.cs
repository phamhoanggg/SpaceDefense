using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScheduleUtils
{
    /// <summary>
    /// Delay method call by starting a coroutine
    /// </summary>
    /// <param name="mb"></param>
    /// <param name="delay"></param>
    /// <param name="task"></param>
    /// <returns></returns>
    public static Coroutine DelayTask(MonoBehaviour mb, float delay, Action task)
    {
        if (mb != null && mb.gameObject.activeInHierarchy)
        {
            return mb.StartCoroutine(DelayRoutine(delay, task));
        }

        return null;
    }

    private static IEnumerator DelayRoutine(float delayTime, Action task)
    {
        yield return new WaitForSeconds(delayTime);
        task?.Invoke();
    }

    /// <summary>
    /// Load scene then do something after completed
    /// </summary>
    /// <param name="mb"></param>
    /// <param name="scene"></param>
    /// <param name="onComplete"></param>
    /// <returns></returns>
    public static Coroutine LoadSceneAsync(MonoBehaviour mb, string scene, Action<float> onLoading, Action onComplete)
    {
        if (mb != null && mb.gameObject.activeInHierarchy)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);
            return mb.StartCoroutine(LoadSceneRoutine(asyncOperation, onLoading, onComplete));
        }

        return null;
    }

    private static IEnumerator LoadSceneRoutine(AsyncOperation asyncOperation, Action<float> onLoading, Action onComplete)
    {
        while (!asyncOperation.isDone)
        {
            onLoading?.Invoke(asyncOperation.progress);
            yield return null;
        }

        // Dung persistent thi onComplete moi dc goi
        onComplete?.Invoke();
    }
}
public class Transition : MonoBehaviour
{
    private Canvas parentCanvas;
    private Animator animator;
    private TransitionType currentAnimation;
    [SerializeField] private RectTransform[] transitionObjectList;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        parentCanvas = transform.parent.GetComponent<Canvas>();
        parentCanvas.sortingLayerName = "UI";
        transform.parent.gameObject.SetActive(false);

        float height = Screen.height * 1.0f / transitionObjectList.Length;

        for (int i = 0; i < transitionObjectList.Length; i++)
        {
            transitionObjectList[i].SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            transitionObjectList[i].anchoredPosition = new Vector2(0, -height * (i + 0.5f));
        }
    }

    public void ChangeAnimation(TransitionType nextAnimation, Action OnComplete = null)
    {
        animator.ResetTrigger(currentAnimation.ToString());
        currentAnimation = nextAnimation;
        animator.SetTrigger(nextAnimation.ToString());

        float length = animator.runtimeAnimatorController.animationClips[(int)nextAnimation].length;
        ScheduleUtils.DelayTask(this, length, () =>
        {
            OnComplete?.Invoke();
        });
    }
}

public enum TransitionType
{
    FadeIn,
    FadeOut,
}
