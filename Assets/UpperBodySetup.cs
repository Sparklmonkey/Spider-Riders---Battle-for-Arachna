using System.Collections;
using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;

public class UpperBodySetup : MonoBehaviour
{
    [SerializeField]
    private List<SVGImage> torsoArmour, shoulderArmour, rightShoulderArmour, upperArmArmour, foreArmArmour, fistArmour;
    [SerializeField]
    private SVGImage torso, shoulder, shoulderR, upperArm, upperArmR, foreArm, foreArmR, fist, fistR, upperArmSkin, upperArmSkinR, foreArmSkin, foreArmSkinR;

    public BodySkinManager skinManager;

    public void SetupUpperBodyArmour(int index)
    {
        torso.sprite = torsoArmour[index].sprite;
        if(shoulderArmour.Count > 0)
        {
            shoulder.sprite = shoulderArmour[index].sprite;
            shoulderR.sprite = rightShoulderArmour[index].sprite;
        }
        else
        {
            shoulder.sprite = skinManager.GetSkinToneForPart(BodyPart.Shoulder).sprite;
            shoulderR.sprite = skinManager.GetSkinToneForPart(BodyPart.Shoulder).sprite;
        }

        if(fistArmour.Count > 0)
        {
            fist.sprite = fistArmour[index].sprite;
            fistR.sprite = fistArmour[index].sprite;
        }
        else
        {
            fist.sprite = skinManager.GetSkinToneForPart(BodyPart.Fist).sprite;
            fistR.sprite = skinManager.GetSkinToneForPart(BodyPart.Fist).sprite;
        }

        if (upperArmArmour.Count > 0)
        {
            upperArm.sprite = upperArmArmour[index].sprite;
            upperArmR.sprite = upperArmArmour[index].sprite;
        }

        if (foreArmArmour.Count > 0)
        {
            foreArm.sprite = foreArmArmour[index].sprite;
            foreArmR.sprite = foreArmArmour[index].sprite;
        }

        if (upperArmSkin != null)
        {
            upperArmSkin.sprite = skinManager.GetSkinToneForPart(BodyPart.UpperArm).sprite;
            upperArmSkinR.sprite = skinManager.GetSkinToneForPart(BodyPart.UpperArm).sprite;
        }

        if (foreArmSkin != null)
        {
            foreArmSkin.sprite = skinManager.GetSkinToneForPart(BodyPart.ForeArm).sprite;
            foreArmSkinR.sprite = skinManager.GetSkinToneForPart(BodyPart.ForeArm).sprite;
        }
    }
}
