using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSpriteManager : MonoBehaviour
{
    public static ButtonSpriteManager instance;

    public List<CustomBtnWIconSprites> buttonList;

    private static List<CustomBtnWIconSprites> staticButtonList;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            staticButtonList = buttonList;
        }
    }

    public static void TurnOffAllButtons()
    {
        foreach (var button in staticButtonList)
        {
            button.TurnOffButton();
        }
    }

    public void MoveToMapScene()
    {
        SceneManager.LoadScene("MissionOne");
    }
}
