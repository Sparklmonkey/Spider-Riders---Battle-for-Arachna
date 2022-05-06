using System.Collections;
using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;

public class BodySkinManager : MonoBehaviour
{
    [SerializeField]
    private SVGImage head, torso, lowerLegL, lowerLegR;
    [SerializeField]
    private List<SVGImage> headTones, shoulderTones, foreArmTones, upperArmTones, fistTones, torsoTones, upperLegTones, lowerLegTones;

    private int currentToneIndex = 0;
    public BodyCustomizeManager customizeManager;
    public SVGImage GetSkinToneForPart(BodyPart part)
    {
        switch (part)
        {
            case BodyPart.Head:
                return headTones[currentToneIndex];
            case BodyPart.Shoulder:
                return shoulderTones[currentToneIndex];
            case BodyPart.UpperLeg:
                return upperLegTones[currentToneIndex];
            case BodyPart.LowerLeg:
                return lowerLegTones[currentToneIndex];
            case BodyPart.UpperArm:
                return upperArmTones[currentToneIndex];
            case BodyPart.ForeArm:
                return foreArmTones[currentToneIndex];
            case BodyPart.Fist:
                return fistTones[currentToneIndex];
            case BodyPart.Torso:
                return torsoTones[currentToneIndex];
            default:
                return torsoTones[currentToneIndex];
        }
    }

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
        torso.sprite = torsoTones[colourIndex].sprite;

        currentToneIndex = colourIndex;
        customizeManager.UpdateArmourSets();
    }
}

public enum BodyPart
{
    Head,
    Shoulder,
    UpperLeg,
    LowerLeg,
    UpperArm,
    ForeArm,
    Fist,
        Torso
}