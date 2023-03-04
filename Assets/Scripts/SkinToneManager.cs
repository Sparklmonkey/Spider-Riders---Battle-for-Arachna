using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinToneManager : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer head,
        torso,
        fistL, fistR,
        shoulderL, shoulerR,
        upperArmL, upperArmR,
        foreArmL, foreArmR,
        upperLegL, upperLegR,
        lowerLegL, lowerLegR;

    [SerializeField]
    private List<Sprite> headTones,
        torsoTones,
        fistTones,
        shoulderTones,
        upperArmTones,
        foreArmTones,
        upperLegTones,
        lowerLegTones;

    private int skinToneIndex;


    public void UpdateSkinTone(bool isNext)
    {
        skinToneIndex = isNext ? skinToneIndex + 1 : skinToneIndex - 1;

        if(skinToneIndex < 0) { skinToneIndex = headTones.Count - 1; }
        if (skinToneIndex >= headTones.Count) { skinToneIndex = 0; }

        head.sprite = headTones[skinToneIndex];
        torso.sprite = torsoTones[skinToneIndex];

        fistL.sprite = fistTones[skinToneIndex];
        fistR.sprite = fistTones[skinToneIndex];

        shoulderL.sprite = shoulderTones[skinToneIndex];
        shoulerR.sprite = shoulderTones[skinToneIndex];

        upperArmL.sprite = upperArmTones[skinToneIndex];
        upperArmR.sprite = upperArmTones[skinToneIndex];

        foreArmL.sprite = foreArmTones[skinToneIndex];
        foreArmR.sprite = foreArmTones[skinToneIndex];

        upperLegL.sprite = upperLegTones[skinToneIndex];
        upperLegR.sprite = upperLegTones[skinToneIndex];

        lowerLegL.sprite = lowerLegTones[skinToneIndex];
        lowerLegR.sprite = lowerLegTones[skinToneIndex];

    }
}
