using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public delegate void TutorialAction();

public class TutorialPopUp : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI tutText;
    [SerializeField]
    private Button actionBtn;

    public void SetupTutorial(string text, TutorialAction action)
    {
        actionBtn.onClick.RemoveAllListeners();
        tutText.text = text;
        actionBtn.onClick.AddListener(delegate {
            action();
            gameObject.SetActive(false);
        });
        gameObject.SetActive(true);
    }
}
