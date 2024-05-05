public class CenterModule : Construction
{
    public override void TakeLandDamage(float dmg)
    {
        base.TakeLandDamage(dmg);
        if (curHP <= 0)
        {
            FormGameplay.Instance.OpenPopupLose();
        }
    }
}
