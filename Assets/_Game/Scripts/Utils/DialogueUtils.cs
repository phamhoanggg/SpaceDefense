using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUtils : MonoBehaviour
{
    [SerializeField] Text dialogue_txt;
    private void Start()
    {
        StartCoroutine(ShowDialogue());
    }

    IEnumerator ShowDialogue()
    {
        string fullText = dialogue_txt.text;
        string text = "";

        dialogue_txt.text = text;
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < fullText.ToCharArray().Length; i++)
        {
            text += fullText.ToCharArray()[i];
            dialogue_txt.text = text;
            yield return new WaitForSeconds(0.03f);
        }
    }
}
