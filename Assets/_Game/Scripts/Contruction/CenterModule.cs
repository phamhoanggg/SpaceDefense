public class CenterModule : Contruction
{
    public override void TakeLandDamage(float dmg)
    {
        base.TakeLandDamage(dmg);
        if (curHP <= 0)
        {
            UIManager.Instance.OpenPopupPopupLose();
        }
    }
}
