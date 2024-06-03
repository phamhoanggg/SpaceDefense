using I2.Loc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizeStringFormatter : MonoBehaviour
{
    [SerializeField] protected Text localizedTmp;
    [SerializeField] protected Localize localize;

    protected string param1 = "", param2 = "", param3 = "";
    protected void Awake()
    {
        if (localizedTmp == null)
        {
            localizedTmp = GetComponent<Text>();
        }

        if (localize == null)
        {
            localize = GetComponent<Localize>();
        }
    }

    protected virtual void OnEnable()
    {
        LocalizationManager.OnLocalizeEvent += UpdateLocalizedText;

        UpdateLocalizedText();
    }

    protected virtual void OnDisable()
    {
        LocalizationManager.OnLocalizeEvent -= UpdateLocalizedText;
    }

    /// <summary>
    /// Format localize text with parameters if needed
    /// </summary>
    public virtual void UpdateLocalizedText() {
        string localizedString = LocalizationManager.GetTermTranslation(localize.Term);
        localizedTmp.text = string.Format(localizedString, param1, param2, param3);
    }

    // Set value for 1 param and new term (optional)
    public virtual void SetAllParam(string param1, string mainTerm = "")
    {
        if (mainTerm != "")
        {
            localize.SetTerm(mainTerm);
        }

        this.param1 = param1;
        UpdateLocalizedText();
    }

    // Set value for 2 param and new term (optional)
    public virtual void SetAllParam(string param1, string param2, string mainTerm = "")
    {
        if (mainTerm != "")
        {
            localize.SetTerm(mainTerm);
        }

        this.param1 = param1;
        this.param2 = param2;
        UpdateLocalizedText();
    }

    // Set value for 3 param and new term (optional)
    public virtual void SetAllParam(string param1, string param2, string param3, string mainTerm = "")
    {
        if (mainTerm != "")
        {
            localize.SetTerm(mainTerm);
        }

        this.param1 = param1;
        this.param2 = param2;
        this.param3 = param3;
        UpdateLocalizedText();
    }


}
