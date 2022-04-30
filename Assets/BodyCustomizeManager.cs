using System.Collections;
using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;

public class BodyCustomizeManager : MonoBehaviour
{
    [SerializeField]
    private SVGImage eyes, torso, shoulderL, shoulderR, upperArmL, upperArmR, foreArmL, foreArmR, fistL, fistR;
    [SerializeField]
    private List<SVGImage> eyeOptions;
    [SerializeField]
    private List<UpperBodySet> upperBodySets;
    [SerializeField]
    private List<GameObject> hairOptions;
    private int eyeIndex, hairIndex, upperBodyindex, armourIndex;
    private bool isArmourOn;
    
    public void ChangeArmour(bool isNext)
    {
        if (isNext)
        {
            if (armourIndex + 1 == 7)
            {
                armourIndex = 0;
            }
            else
            {
                armourIndex += 1;
            }
        }
        else
        {
            if (armourIndex - 1 < 0)
            {
                armourIndex = 6;
            }
            else
            {
                armourIndex -= 1;
            }
        }
    }

    public void ChangeUpperBody(bool isNext)
    {
        if (isNext)
        {
            if (upperBodyindex + 1 == 4)
            {
                upperBodyindex = 0;
            }
            else
            {
                upperBodyindex += 1;
            }
        }
        else
        {
            if (upperBodyindex - 1 < 0)
            {
                upperBodyindex = 3;
            }
            else
            {
                upperBodyindex -= 1;
            }
        }

        torso.gameObject.SetActive(false);
        shoulderL.gameObject.SetActive(false);
        shoulderR.gameObject.SetActive(false);
        upperArmL.gameObject.SetActive(false);
        upperArmR.gameObject.SetActive(false);
        foreArmL.gameObject.SetActive(false);
        foreArmR.gameObject.SetActive(false);
        fistL.gameObject.SetActive(false);
        fistR.gameObject.SetActive(false);

        if (upperBodySets[isArmourOn ? upperBodyindex : upperBodyindex + (armourIndex * 4)].torsoArmour != null)
        {
            torso.gameObject.SetActive(true);
            torso.sprite = upperBodySets[isArmourOn ? upperBodyindex : upperBodyindex + (armourIndex * 4)].torsoArmour.sprite;
        }

        if (upperBodySets[isArmourOn ? upperBodyindex : upperBodyindex + (armourIndex * 4)].shoulderArmour != null)
        {
            shoulderL.gameObject.SetActive(true);
            shoulderR.gameObject.SetActive(true);
            shoulderL.sprite = upperBodySets[isArmourOn ? upperBodyindex : upperBodyindex + (armourIndex * 4)].shoulderArmour.sprite;
            shoulderR.sprite = upperBodySets[isArmourOn ? upperBodyindex : upperBodyindex + (armourIndex * 4)].shoulderArmour.sprite;
        }

        if (upperBodySets[isArmourOn ? upperBodyindex : upperBodyindex + (armourIndex * 4)].upperArmArmour != null)
        {
            upperArmL.gameObject.SetActive(true);
            upperArmR.gameObject.SetActive(true);
            upperArmL.sprite = upperBodySets[isArmourOn ? upperBodyindex : upperBodyindex + (armourIndex * 4)].upperArmArmour.sprite;
            upperArmR.sprite = upperBodySets[isArmourOn ? upperBodyindex : upperBodyindex + (armourIndex * 4)].upperArmArmour.sprite;
        }

        if (upperBodySets[isArmourOn ? upperBodyindex : upperBodyindex + (armourIndex * 4)].foreArmArmour != null)
        {
            foreArmL.gameObject.SetActive(true);
            foreArmR.gameObject.SetActive(true);
            foreArmL.sprite = upperBodySets[isArmourOn ? upperBodyindex : upperBodyindex + (armourIndex * 4)].foreArmArmour.sprite;
            foreArmR.sprite = upperBodySets[isArmourOn ? upperBodyindex : upperBodyindex + (armourIndex * 4)].foreArmArmour.sprite;
        }

        if (upperBodySets[isArmourOn ? upperBodyindex : upperBodyindex + (armourIndex * 4)].fistArmour != null)
        {
            fistL.gameObject.SetActive(true);
            fistR.gameObject.SetActive(true);
            fistL.sprite = upperBodySets[isArmourOn ? upperBodyindex : upperBodyindex + (armourIndex * 4)].fistArmour.sprite;
            fistR.sprite = upperBodySets[isArmourOn ? upperBodyindex : upperBodyindex + (armourIndex * 4)].fistArmour.sprite;
        }
    }

    public void ChangeEyes(bool isNext)
    {
        if (isNext)
        {
            if (eyeIndex + 1 == eyeOptions.Count)
            {
                eyeIndex = 0;
            }
            else
            {
                eyeIndex += 1;
            }
        }
        else
        {
            if (eyeIndex - 1 < 0)
            {
                eyeIndex = eyeOptions.Count - 1;
            }
            else
            {
                eyeIndex -= 1;
            }
        }
        eyes.sprite = eyeOptions[eyeIndex].sprite;
    }
    public void ChangeHair(bool isNext)
    {
        hairOptions[hairIndex].SetActive(false);
        if (isNext)
        {
            if (hairIndex + 1 == hairOptions.Count)
            {
                hairIndex = 0;
            }
            else
            {
                hairIndex += 1;
            }
        }
        else
        {
            if (hairIndex - 1 < 0)
            {
                hairIndex = hairOptions.Count - 1;
            }
            else
            {
                hairIndex -= 1;
            }
        }
        hairOptions[hairIndex].SetActive(true);
    }
}
