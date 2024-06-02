using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System;

public abstract class PopupBase : MonoBehaviour
{
    [SerializeField] protected Transform boxTransform;

    protected virtual void Awake()
    {
        boxTransform.localScale = Vector3.zero;
    }

    public virtual void Open(object args = null)
    {
        if (gameObject.activeInHierarchy)
        {
            DOTween.Kill(boxTransform);
        }
        else
        {
            gameObject.SetActive(true);
        }

        transform.SetAsLastSibling();


        OpenAnimation();

    }

    protected virtual void OpenAnimation(Action OnComplete = null)
    {
        boxTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }

    public virtual void Close()
    {
        if (gameObject.activeInHierarchy)
        {
            CloseAnimation(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }

    protected virtual void CloseAnimation(UnityAction OnComplelete)
    {
        boxTransform.DOScale(Vector3.zero, 0.25f).OnComplete(() =>
        {
            OnComplelete?.Invoke();
        });
    }

    public void PauseTime()
    {
        Time.timeScale = 0;
    }
}
