using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChangeTest : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer head, eyes, hair;
    [SerializeField]
    private List<Sprite> headSprites, eyeSprites, hairSprites;

    private int hairIndex = 0;
    private int headIndex = 0;
    private int eyesIndex = 0;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) { ChangeHair(false); }
        if (Input.GetKeyDown(KeyCode.W)) { ChangeHair(true); }

        if (Input.GetKeyDown(KeyCode.A)) { ChangeEyes(false); }
        if (Input.GetKeyDown(KeyCode.S)) { ChangeEyes(true); }

        if (Input.GetKeyDown(KeyCode.Z)) { ChangeSkin(false); }
        if (Input.GetKeyDown(KeyCode.X)) { ChangeSkin(true); }
    }

    public void ChangeHair(bool isNext)
    {
        hairIndex = isNext ? hairIndex + 1 : hairIndex - 1;

        if(hairIndex >= hairSprites.Count) { hairIndex = 0; }
        if (hairIndex < 0) { hairIndex = hairSprites.Count - 1; }

        hair.sprite = hairSprites[hairIndex];
    }
    public void ChangeEyes(bool isNext)
    {

        eyesIndex = isNext ? eyesIndex + 1 : eyesIndex - 1;

        if (eyesIndex >= eyeSprites.Count) { eyesIndex = 0; }
        if (eyesIndex < 0) { eyesIndex = eyeSprites.Count - 1; }

        eyes.sprite = eyeSprites[eyesIndex];

    }
    public void ChangeSkin(bool isNext)
    {
        headIndex = isNext ? headIndex + 1 : headIndex - 1;

        if (headIndex >= headSprites.Count) { headIndex = 0; }
        if (headIndex < 0) { headIndex = headSprites.Count - 1; }

        head.sprite = headSprites[headIndex];
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
