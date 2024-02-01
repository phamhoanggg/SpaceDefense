using UnityEngine;

public class PopupPause : PopupBase
{
    public override void Open()
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
        GameManager.Instance.ChangeScene(Scene.MenuScene);
        Close();
    }
}
