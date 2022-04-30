using System.Collections;
using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;

public class BodySkinManager : MonoBehaviour
{
    [SerializeField]
    private SVGImage head, shoulderL, shoulderR, foreArmL, foreArmR, upperArmL, upperArmR, fistL, fistR, torso, upperLegL, upperLegR, lowerLegL, lowerLegR;
    [SerializeField]
    private List<SVGImage> headTones, shoulderTones, foreArmTones, upperArmTones, fistTones, torsoTones, upperLegTones, lowerLegTones;

    private int currentToneIndex = 0;

    public void ChangeSkinTone(bool isNext)
    {
        int colourIndex;
        if (isNext)
        {
            if(currentToneIndex + 1 == headTones.Count)
            {
                colourIndex = 0;
            }
            else
            {
                colourIndex = currentToneIndex + 1;
            }
        }
        else
        {
            if (currentToneIndex - 1 < 0)
            {
                colourIndex = headTones.Count;
            }
            else
            {
                colourIndex = currentToneIndex - 1;
            }
        }

        head.sprite = headTones[colourIndex].sprite;
        shoulderL.sprite = shoulderTones[colourIndex].sprite;
        shoulderR.sprite = shoulderTones[colourIndex].sprite;
        foreArmL.sprite = foreArmTones[colourIndex].sprite;
        foreArmR.sprite = foreArmTones[colourIndex].sprite;
        upperArmL.sprite = upperArmTones[colourIndex].sprite;
        upperArmR.sprite = upperArmTones[colourIndex].sprite;
        fistL.sprite = fistTones[colourIndex].sprite;
        fistR.sprite = fistTones[colourIndex].sprite;
        torso.sprite = torsoTones[colourIndex].sprite;
        upperLegL.sprite = upperLegTones[colourIndex].sprite;
        upperLegR.sprite = upperLegTones[colourIndex].sprite;
        lowerLegL.sprite = lowerLegTones[colourIndex].sprite;
        lowerLegR.sprite = lowerLegTones[colourIndex].sprite;

        currentToneIndex = colourIndex;
    }
}
