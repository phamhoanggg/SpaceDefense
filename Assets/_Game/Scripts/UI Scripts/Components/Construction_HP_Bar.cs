using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Construction_HP_Bar : MonoBehaviour
{
    public Gradient gradient;
    public GameObject hp_Bar;
    public Image fillImage;

    public void SetFillAmount(float amount)
    {
        hp_Bar.SetActive(amount < 1);
        fillImage.fillAmount = amount;
        fillImage.color = gradient.Evaluate(amount);
    }
}
