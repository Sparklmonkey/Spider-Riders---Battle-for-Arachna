using System.Collections;
using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;

public class BodyCustomizeManager : MonoBehaviour
{
    [SerializeField]
    private SVGImage eyes;
    [SerializeField]
    private List<UpperBodySetup> upperBodySets;
    [SerializeField]
    private List<LowerBodySetup> lowerBodySets;
    [SerializeField]
    private List<SVGImage> eyeOptions;
    [SerializeField]
    private List<GameObject> hairOptions;
    private int eyeIndex, hairIndex, upperBodyindex, armourIndex, lowerBodyindex;
    private bool isArmourOn;
    

    public void ChangeArmour(bool isNext)
    {
        if (armourIndex == 0)
        {
            upperBodySets[upperBodyindex].gameObject.SetActive(false);
            lowerBodySets[lowerBodyindex].gameObject.SetActive(false);
        }
        else
        {
            upperBodySets[armourIndex + 3].gameObject.SetActive(false);
            lowerBodySets[armourIndex + 3].gameObject.SetActive(false);
        }

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


        if (armourIndex == 0)
        {
            upperBodySets[upperBodyindex].gameObject.SetActive(true);
            lowerBodySets[lowerBodyindex].gameObject.SetActive(true);
        }
        else
        {
            upperBodySets[armourIndex + 3].gameObject.SetActive(true);
            lowerBodySets[armourIndex + 3].gameObject.SetActive(true);
            upperBodySets[armourIndex + 3].SetupUpperBodyArmour(upperBodyindex);
            lowerBodySets[armourIndex + 3].SetupLowerBodyArmour(lowerBodyindex);
        }
    }

    public void ChangeUpperBody(bool isNext)
    {

        if (armourIndex == 0)
        {
            upperBodySets[upperBodyindex].gameObject.SetActive(false);
        }
        else
        {
            upperBodySets[armourIndex + 3].gameObject.SetActive(false);
        }

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


        if (armourIndex == 0)
        {
            upperBodySets[upperBodyindex].gameObject.SetActive(true);
            upperBodySets[upperBodyindex].SetupUpperBodyArmour(0);
        }
        else
        {
            upperBodySets[armourIndex + 3].gameObject.SetActive(true);
            upperBodySets[armourIndex + 3].SetupUpperBodyArmour(upperBodyindex);
        }
    }

    public void ChangeLowerBody(bool isNext)
    {

        if (armourIndex == 0)
        {
            lowerBodySets[lowerBodyindex].gameObject.SetActive(false);
        }
        else
        {
            lowerBodySets[armourIndex + 3].gameObject.SetActive(false);
        }

        if (isNext)
        {
            if (lowerBodyindex + 1 == 4)
            {
                lowerBodyindex = 0;
            }
            else
            {
                lowerBodyindex += 1;
            }
        }
        else
        {
            if (lowerBodyindex - 1 < 0)
            {
                lowerBodyindex = 3;
            }
            else
            {
                lowerBodyindex -= 1;
            }
        }


        if (armourIndex == 0)
        {
            lowerBodySets[lowerBodyindex].gameObject.SetActive(true);
            lowerBodySets[lowerBodyindex].SetupLowerBodyArmour(0);
        }
        else
        {
            lowerBodySets[armourIndex + 3].gameObject.SetActive(true);
            lowerBodySets[armourIndex + 3].SetupLowerBodyArmour(lowerBodyindex);
        }
    }

    public void UpdateArmourSets()
    {

        if (armourIndex == 0)
        {
            lowerBodySets[lowerBodyindex].SetupLowerBodyArmour(0);
            upperBodySets[upperBodyindex].SetupUpperBodyArmour(0);
        }
        else
        {
            lowerBodySets[armourIndex + 3].SetupLowerBodyArmour(lowerBodyindex);
            upperBodySets[armourIndex + 3].SetupUpperBodyArmour(upperBodyindex);
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
