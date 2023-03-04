using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperArmourManager : MonoBehaviour
{
    private readonly int armourSpritesPerSet = 4;
    [SerializeField]
    private SpriteRenderer shoulderArmourL, shoulderArmourR,
        upperArmArmourL, upperArmArmourR,
        foreArmArmourL, foreArmArmourR,
        fistArmourL, fistArmourR,
        chestArmour;

    [SerializeField]
    private List<Sprite> shoulderArmourLSprites, shoulderArmourRSprites,
        upperArmArmourSprites,
        foreArmArmourSprites,
        fistArmourSprites,
        chestArmourSprites;

    private int armourIndex, setIndex;
    private int index { get => (armourIndex * armourSpritesPerSet) + setIndex; }
    public void ChangeArmourType(bool isNext)
    {
        armourIndex = isNext ? armourIndex + 1 : armourIndex - 1;
        if(armourIndex < 0) { armourIndex = fistArmourSprites.Count / armourSpritesPerSet; }
        if(armourIndex >= fistArmourSprites.Count / armourSpritesPerSet) { armourIndex = 0; }

        SetArmourSprites();
    }

    public void ChangeArmourSet(bool isNext)
    {
        setIndex = isNext ? setIndex + 1 : setIndex - 1;
        if (setIndex < 0) { setIndex = armourSpritesPerSet - 1; }
        if (setIndex >= armourSpritesPerSet) { armourIndex = 0; }

        SetArmourSprites();
    }

    private void SetArmourSprites()
    {
        shoulderArmourL.sprite = shoulderArmourLSprites[index];
        shoulderArmourR.sprite = shoulderArmourRSprites[index];

        upperArmArmourL.sprite = upperArmArmourSprites[index];
        upperArmArmourR.sprite = upperArmArmourSprites[index];

        foreArmArmourL.sprite = foreArmArmourSprites[index];
        foreArmArmourR.sprite = foreArmArmourSprites[index];

        fistArmourL.sprite = fistArmourSprites[index];
        fistArmourR.sprite = fistArmourSprites[index];

        chestArmour.sprite = chestArmourSprites[index];
    }
}
