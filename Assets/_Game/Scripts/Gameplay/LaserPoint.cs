using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPoint : MonoBehaviour
{
    private float dps;
    public void OnInit(float dps)
    {
        this.dps = dps;
        gameObject.layer = (int)GameLayer.Player_Bullet;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            if (collision.GetComponent<IFlyable>() != null) collision.GetComponent<IFlyable>().TakeAirDamage(dps * Time.deltaTime);
            else if (collision.GetComponent<IOnLand>() != null) collision.GetComponent<IOnLand>().TakeLandDamage(dps * Time.deltaTime);      
        }
    }
}
