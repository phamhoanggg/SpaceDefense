using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] private GameObject on_obj, off_obj;

    protected bool isCurrentOn;

    public void OnInit(bool isOn)
    {
        isCurrentOn = isOn;
        on_obj.SetActive(isCurrentOn);
        off_obj.SetActive(!isCurrentOn);
    }

    public virtual void OnToggle()
    {
        isCurrentOn = !isCurrentOn;
        on_obj.SetActive(isCurrentOn);
        off_obj.SetActive(!isCurrentOn);
    }
}
