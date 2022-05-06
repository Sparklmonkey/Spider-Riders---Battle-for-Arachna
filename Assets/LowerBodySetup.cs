using System.Collections;
using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;

public class LowerBodySetup : MonoBehaviour
{
    [SerializeField]
    private List<SVGImage> waistArmour, leftShoeArmour, rightShoeArmour, upperLegArmour, lowerLegArmour;
    [SerializeField]
    private SVGImage waist, shoeL, shoeR, upperLegL, upperLegR, lowerLegL, lowerLegR, lowerLegSkinL, lowerLegSkinR;

    public BodySkinManager skinManager;
    public void SetupLowerBodyArmour(int index)
    {
        waist.sprite = waistArmour[index].sprite;
        shoeL.sprite = leftShoeArmour[index].sprite;
        shoeR.sprite = rightShoeArmour[index].sprite;

        upperLegL.sprite = upperLegArmour[index].sprite;
        upperLegR.sprite = upperLegArmour[index].sprite;

        lowerLegL.sprite = lowerLegArmour[index].sprite;
        lowerLegR.sprite = lowerLegArmour[index].sprite;



        if (lowerLegSkinL != null)
        {
            lowerLegSkinL.sprite = skinManager.GetSkinToneForPart(BodyPart.LowerLeg).sprite;
            lowerLegSkinR.sprite = skinManager.GetSkinToneForPart(BodyPart.LowerLeg).sprite;
        }
    }
}
