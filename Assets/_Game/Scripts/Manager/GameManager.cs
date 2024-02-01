using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingletonMB<GameManager>
{
    [Header("GAME CONFIG")]
    public bool isOneHit;
    public bool isUndying;

    #region CHANGE SCENE
    private Scene currentScene;
    public bool IsCurrentScene(Scene scene) => currentScene == scene;

    public void ChangeScene(Scene scene)
    {
        StartCoroutine(ChangeSceneRoutine(scene));
    }

    private IEnumerator ChangeSceneRoutine(Scene scene)
    {
        bool isCloseAnimationComplete = false;
        FadeInAnimation(() =>
        {
            isCloseAnimationComplete = true;
        });

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene.ToString());
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                if (!asyncOperation.allowSceneActivation)
                {
                    yield return new WaitUntil(() => isCloseAnimationComplete);
                    asyncOperation.allowSceneActivation = true;
                }
            }
            yield return null;
        }

        FadeOutAnimation();
    }
    #endregion

    #region TRANSITION
    public GameObject transitionObj;
    public Transition transition;

    public void FadeInAnimation(UnityAction OnComplete = null)
    {
        transitionObj.SetActive(true);

        transition.ChangeAnimation(TransitionType.FadeIn, () =>
        {
            OnComplete?.Invoke();
        });
    }

    public void FadeOutAnimation(UnityAction OnComplete = null)
    {
        transition.ChangeAnimation(TransitionType.FadeOut, () =>
        {
            transitionObj.SetActive(false);
            OnComplete?.Invoke();
        });
    }
    #endregion
}
