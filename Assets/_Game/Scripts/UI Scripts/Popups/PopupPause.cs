using UnityEngine;

public class PopupPause : PopupBase
{
    public override void Open(object args = null)
    {
        base.Open();

        Invoke(nameof(PauseTime), 0.5f);
    }
    public void ButtonContinue()
    {
        Time.timeScale = 1;
        Close();
    }

    public void ButtonExit()
    {
        Time.timeScale = 1;
        SceneLoader.Instance.LoadScene(SceneId.Menu);
        Close();
    }
}
