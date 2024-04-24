using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneLoader : SingletonMB<SceneLoader>
{
    public enum Mode { Before, With, After }

    [SerializeField]
    private SceneTransition transition;

    private bool isLoading;
    private SceneId currentSceneId = SceneId.None;

    public bool IsLoading => isLoading;
    public SceneId CurrentSceneId => currentSceneId;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isLoading = false;

        if (Enum.TryParse(scene.name, out SceneId id))
        {
            currentSceneId = id;
        }
        else
        {
            Debug.LogError($"There is no id corresponding to this scene: {scene.name}");

            currentSceneId = SceneId.None;
        }
    }

    [Button(ButtonStyle.FoldoutButton)]
    public void LoadScene(SceneId sceneId, Mode mode = Mode.After,
        Action<float> onLoading = null, Action onComplete = null)
    {
        isLoading = true;

        switch (mode)
        {
            case Mode.Before:
                StartCoroutine(LoadBefore(sceneId, onLoading, onComplete));
                break;
            case Mode.With:
                StartCoroutine(LoadWith(sceneId, onLoading, onComplete));
                break;
            case Mode.After:
                StartCoroutine(LoadAfter(sceneId, onLoading, onComplete));
                break;
        }
    }

    IEnumerator LoadBefore(SceneId sceneId, Action<float> onLoading, Action onComplete = null)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId.ToString());
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            onLoading?.Invoke(operation.progress);

            if (operation.progress >= 0.9f && !operation.allowSceneActivation)
            {
                yield return transition.Close();

                CleanupBeforeLoadScene();
                operation.allowSceneActivation = true;
            }

            yield return null;
        }

        onComplete?.Invoke();
    }

    IEnumerator LoadWith(SceneId sceneId, Action<float> onLoading, Action onComplete = null)
    {
        bool isComplete = false;
        transition.Close(() =>
        {
            CleanupBeforeLoadScene();
            isComplete = true;
        });

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId.ToString());
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            onLoading?.Invoke(operation.progress);

            if (operation.progress >= 0.9f && !operation.allowSceneActivation)
            {
                yield return new WaitUntil(() => isComplete);

                operation.allowSceneActivation = true;
            }

            yield return null;
        }

        onComplete?.Invoke();
    }

    IEnumerator LoadAfter(SceneId sceneId, Action<float> onLoading, Action onComplete = null)
    {
        yield return transition.Close();

        CleanupBeforeLoadScene();
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId.ToString());

        while (!operation.isDone)
        {
            onLoading?.Invoke(operation.progress);

            yield return null;
        }

        onComplete?.Invoke();
    }

    private void CleanupBeforeLoadScene()
    {
        DOTween.KillAll();
        DataManager.Instance.SaveData();
    }

    public void OpenAnimation(Action onComplete = null)
    {
        transition.Open(onComplete);
    }
}
