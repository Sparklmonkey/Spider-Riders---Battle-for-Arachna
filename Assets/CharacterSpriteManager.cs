using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterSpriteManager : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.U2D.Animation.SpriteLibrary currentLibrary;
    [SerializeField]
    private UnityEngine.U2D.Animation.SpriteLibraryAsset maleLibraryAsset, femaleLibraryAsset;
    private bool isMale = true;
    [SerializeField]
    private UnityEngine.U2D.Animation.SpriteResolver face, torso, upperArmL, upperArmR, foreArmL, foreArmR, upperLegL, upperLegR, lowerLegL, lowerLegR,                   //Skin
        fistL, fistR, shoulderL, shoulderR,                                                                                                     //Skin
        eyes, hair,                                                                                                                             //Other
        chestArmour, upperArmArmourL, upperArmArmourR, foreArmArmourL, foreArmArmourR, waistArmour, upperLegArmourL, upperLegArmourR,           //Armour
        lowerLegArmourL, lowerLegArmourR, shoeL, shoeR, fistArmourL, fistArmourR, shoulderArmourL, shoulderArmourR;                             //Armour

    private int skinIndex, upperSetIndex, lowerSetIndex, armourIndex, hairIndex, eyeIndex;
    // Start is called before the first frame update
    void Awake()
    {
        CharacterPreset characterPreset = TestPlayer<PlayerData>.GetCharacterPreset();
        skinIndex = characterPreset.skinIndex;
        upperSetIndex = characterPreset.upperSetIndex;
        lowerSetIndex = characterPreset.lowerSetIndex;
        armourIndex = characterPreset.armourIndex;
        hairIndex = characterPreset.hairIndex;
        eyeIndex = characterPreset.eyeIndex;
        isMale = characterPreset.isMale;
        SetupCharacter();
    }
    void SetupCharacter()
    {
        currentLibrary.spriteLibraryAsset = isMale ? maleLibraryAsset : femaleLibraryAsset;
        eyes.SetCategoryAndLabel("Eyes", $"Eyes_{eyeIndex}");
        hair.SetCategoryAndLabel("Hair", $"Hair_{hairIndex}");
        face.SetCategoryAndLabel("Face", $"Face_{skinIndex}");
        torso.SetCategoryAndLabel("TorsoSkin", $"TorsoSkin_{skinIndex}");

        upperArmL.SetCategoryAndLabel("UpperArmSkin", $"UpperArmSkin_{skinIndex}");
        upperArmR.SetCategoryAndLabel("UpperArmSkin", $"UpperArmSkin_{skinIndex}");

        shoulderL.SetCategoryAndLabel("ShoulderSkin", $"ShoulderSkin_{skinIndex}");
        shoulderR.SetCategoryAndLabel("ShoulderSkin", $"ShoulderSkin_{skinIndex}");

        foreArmL.SetCategoryAndLabel("ForeArmSkin", $"ForeArmSkin_{skinIndex}");
        foreArmR.SetCategoryAndLabel("ForeArmSkin", $"ForeArmSkin_{skinIndex}");

        fistL.SetCategoryAndLabel("FistSkin", $"FistSkin_{skinIndex}");
        fistR.SetCategoryAndLabel("FistSkin", $"FistSkin_{skinIndex}");

        upperLegL.SetCategoryAndLabel("UpperLegSkin", $"UpperLegSkin_{skinIndex}");
        upperLegR.SetCategoryAndLabel("UpperLegSkinR", $"UpperLegSkin_{skinIndex}");

        lowerLegL.SetCategoryAndLabel("LowerLegSkin", $"LowerLegSkin_{skinIndex}");
        lowerLegR.SetCategoryAndLabel("LowerLegSkin", $"LowerLegSkin_{skinIndex}");

        int upperIndex = upperSetIndex + (armourIndex * 4);
        int lowerIndex = lowerSetIndex + (armourIndex * 4);
        //Upper Set
        chestArmour.SetCategoryAndLabel("ChestArmour", $"ChestArmour_{upperIndex}");
        upperArmArmourL.SetCategoryAndLabel("UpperArmArmour", $"UpperArmArmour_{upperIndex}");
        upperArmArmourR.SetCategoryAndLabel("UpperArmArmour", $"UpperArmArmour_{upperIndex}");
        foreArmArmourL.SetCategoryAndLabel("ForeArmArmour", $"ForeArmArmour_{upperIndex}");
        foreArmArmourR.SetCategoryAndLabel("ForeArmArmour", $"ForeArmArmour_{upperIndex}");
        fistArmourL.SetCategoryAndLabel("FistArmour", $"FistArmour_{upperIndex}");
        fistArmourR.SetCategoryAndLabel("FistArmour", $"FistArmour_{upperIndex}");
        shoulderArmourL.SetCategoryAndLabel("ShoulderArmourL", $"ShoulderArmourL_{upperIndex}");
        shoulderArmourR.SetCategoryAndLabel("ShoulderArmourR", $"ShoulderArmourR_{upperIndex}");

        //Lower Set
        waistArmour.SetCategoryAndLabel("WaistArmour", $"WaistArmour_{lowerIndex}");
        upperLegArmourL.SetCategoryAndLabel("UpperLegArmourL", $"UpperLegArmour_{lowerIndex}");
        upperLegArmourR.SetCategoryAndLabel("UpperLegArmourR", $"UpperLegArmour_{lowerIndex}");
        lowerLegArmourL.SetCategoryAndLabel("LowerLegArmour", $"LowerLegArmour_{lowerIndex}");
        lowerLegArmourR.SetCategoryAndLabel("LowerLegArmour", $"LowerLegArmour_{lowerIndex}");
        shoeL.SetCategoryAndLabel("ShoeL", $"ShoeL_{lowerIndex}");
        shoeR.SetCategoryAndLabel("ShoeR", $"ShoeR_{lowerIndex}");
    }

    void UpdateCharacterPreset()
    {
        CharacterPreset characterPreset = new CharacterPreset();

        characterPreset.skinIndex = skinIndex;
        characterPreset.upperSetIndex = upperSetIndex;
        characterPreset.lowerSetIndex = lowerSetIndex;
        characterPreset.armourIndex = armourIndex;
        characterPreset.hairIndex = hairIndex;
        characterPreset.eyeIndex = eyeIndex;
        characterPreset.isMale = isMale;

        TestPlayer<PlayerData>.SaveCharacterPreset(characterPreset);
    }

    public void ChangeGender(bool isMale)
    {
        this.isMale = isMale;
        currentLibrary.spriteLibraryAsset = isMale ? maleLibraryAsset : femaleLibraryAsset;
        UpdateCharacterPreset();
    }

    public void ChangeEyes(bool isNext)
    {
        eyeIndex = isNext ? eyeIndex + 1 : eyeIndex - 1;

        if (eyeIndex < 0) { eyeIndex = 3; }
        if (eyeIndex >= 4) { eyeIndex = 0; }

        eyes.SetCategoryAndLabel("Eyes", $"Eyes_{eyeIndex}");
        UpdateCharacterPreset();
    }

    public void ChangeHair(bool isNext)
    {
        hairIndex = isNext ? hairIndex + 1 : hairIndex - 1;

        if (hairIndex < 0) { hairIndex = 3; }
        if (hairIndex >= 4) { hairIndex = 0; }

        hair.SetCategoryAndLabel("Hair", $"Hair_{hairIndex}");
        UpdateCharacterPreset();
    }

    public void ChangeSkinTone(bool isNext)
    {
        skinIndex = isNext ? skinIndex + 1 : skinIndex - 1;

        if (skinIndex < 0) { skinIndex = 3; }
        if (skinIndex >= 4) { skinIndex = 0; }

        face.SetCategoryAndLabel("Face", $"Face_{skinIndex}");
        torso.SetCategoryAndLabel("TorsoSkin", $"TorsoSkin_{skinIndex}");

        upperArmL.SetCategoryAndLabel("UpperArmSkin", $"UpperArmSkin_{skinIndex}");
        upperArmR.SetCategoryAndLabel("UpperArmSkin", $"UpperArmSkin_{skinIndex}");

        shoulderL.SetCategoryAndLabel("ShoulderSkin", $"ShoulderSkin_{skinIndex}");
        shoulderR.SetCategoryAndLabel("ShoulderSkin", $"ShoulderSkin_{skinIndex}");

        foreArmL.SetCategoryAndLabel("ForeArmSkin", $"ForeArmSkin_{skinIndex}");
        foreArmR.SetCategoryAndLabel("ForeArmSkin", $"ForeArmSkin_{skinIndex}");

        fistL.SetCategoryAndLabel("FistSkin", $"FistSkin_{skinIndex}");
        fistR.SetCategoryAndLabel("FistSkin", $"FistSkin_{skinIndex}");

        upperLegL.SetCategoryAndLabel("UpperLegSkin", $"UpperLegSkin_{skinIndex}");
        upperLegR.SetCategoryAndLabel("UpperLegSkinR", $"UpperLegSkin_{skinIndex}");

        lowerLegL.SetCategoryAndLabel("LowerLegSkin", $"LowerLegSkin_{skinIndex}");
        lowerLegR.SetCategoryAndLabel("LowerLegSkin", $"LowerLegSkin_{skinIndex}");

        UpdateCharacterPreset();
    }

    public void ChangeArmourIndex(bool isNext)
    {
        armourIndex = isNext ? armourIndex + 1 : armourIndex - 1;
        if (armourIndex < 0) { armourIndex = 6; }
        if (armourIndex >= 7) { armourIndex = 0; }
        UpdateArmour();
    }

    public void ChangeUpperSetIndex(bool isNext)
    {
        upperSetIndex = isNext ? upperSetIndex + 1 : upperSetIndex - 1;
        if (upperSetIndex < 0) { upperSetIndex = 3; }
        if (upperSetIndex >= 4) { upperSetIndex = 0; }
        UpdateArmour();
    }

    public void ChangeLowerSetIndex(bool isNext)
    {
        lowerSetIndex = isNext ? lowerSetIndex + 1 : lowerSetIndex - 1;
        if (lowerSetIndex < 0) { lowerSetIndex = 3; }
        if (lowerSetIndex >= 4) { lowerSetIndex = 0; }
        UpdateArmour();
    }

    private void UpdateArmour()
    {
        int upperIndex = upperSetIndex + (armourIndex * 4);
        int lowerIndex = lowerSetIndex + (armourIndex * 4);

        //Upper Set
        chestArmour.SetCategoryAndLabel("ChestArmour", $"ChestArmour_{upperIndex}");
        upperArmArmourL.SetCategoryAndLabel("UpperArmArmour", $"UpperArmArmour_{upperIndex}");
        upperArmArmourR.SetCategoryAndLabel("UpperArmArmour", $"UpperArmArmour_{upperIndex}");
        foreArmArmourL.SetCategoryAndLabel("ForeArmArmour", $"ForeArmArmour_{upperIndex}");
        foreArmArmourR.SetCategoryAndLabel("ForeArmArmour", $"ForeArmArmour_{upperIndex}");
        fistArmourL.SetCategoryAndLabel("FistArmour", $"FistArmour_{upperIndex}");
        fistArmourR.SetCategoryAndLabel("FistArmour", $"FistArmour_{upperIndex}");
        shoulderArmourL.SetCategoryAndLabel("ShoulderArmourL", $"ShoulderArmourL_{upperIndex}");
        shoulderArmourR.SetCategoryAndLabel("ShoulderArmourR", $"ShoulderArmourR_{upperIndex}");

        //Lower Set
        waistArmour.SetCategoryAndLabel("WaistArmour", $"WaistArmour_{lowerIndex}");
        upperLegArmourL.SetCategoryAndLabel("UpperLegArmourL", $"UpperLegArmour_{lowerIndex}");
        upperLegArmourR.SetCategoryAndLabel("UpperLegArmourR", $"UpperLegArmour_{lowerIndex}");
        lowerLegArmourL.SetCategoryAndLabel("LowerLegArmour", $"LowerLegArmour_{lowerIndex}");
        lowerLegArmourR.SetCategoryAndLabel("LowerLegArmour", $"LowerLegArmour_{lowerIndex}");
        shoeL.SetCategoryAndLabel("ShoeL", $"ShoeL_{lowerIndex}");
        shoeR.SetCategoryAndLabel("ShoeR", $"ShoeR_{lowerIndex}");
        UpdateCharacterPreset();
    }
}
