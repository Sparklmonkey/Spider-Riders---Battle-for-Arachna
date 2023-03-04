using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChangeTest : MonoBehaviour
{
    Controller spriterController;

    private int hairIndex = 0;
    private int skinIndex = 0;
    private int eyesIndex = 0;


    private void Awake()
    {
        spriterController = GetComponent<Controller>();
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeHair(bool isNext)
    {
        hairIndex = isNext ? hairIndex + 1 : hairIndex - 1;

        if(hairIndex > 3) { hairIndex = 0; }
        if (hairIndex < 0) { hairIndex = 3; }
        spriterController.ChangeCharacterMap(SpriteMapType.Hair, hairIndex);
    }
    public void ChangeEyes(bool isNext)
    {
        eyesIndex = isNext ? eyesIndex + 1 : eyesIndex - 1;

        if (eyesIndex > 3) { eyesIndex = 0; }
        if (eyesIndex < 0) { eyesIndex = 3; }
        spriterController.ChangeCharacterMap(SpriteMapType.Eyes, eyesIndex);
    }
    public void ChangeSkin(bool isNext)
    {
        skinIndex = isNext ? skinIndex + 1 : skinIndex - 1;

        if (skinIndex > 3) { skinIndex = 0; }
        if (skinIndex < 0) { skinIndex = 3; }
        spriterController.ChangeCharacterMap(SpriteMapType.Skin, skinIndex);
    }
}

public enum SpriteMapType
{
    Hair,
    Eyes,
    Skin,
    UpperArmour,
    LowerArmour
}
