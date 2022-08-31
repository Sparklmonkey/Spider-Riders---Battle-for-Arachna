using System.Collections;
using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;

public class SpiderCustomizer : MonoBehaviour
{
    [SerializeField]
    private SVGImage abdomen, head;
    [SerializeField]
    private List<SVGImage> legs, headSprites, abdomenSprites, legSprites;

    private bool isMale = true;

    private int headIndex, abdomenIndex, legIndex;
    public void ChangeHeadSprite(bool isNext)
    {
        List<SVGImage> tempList;
        if (isMale)
        {
            tempList = new List<SVGImage> { headSprites[0], headSprites[1], headSprites[2], headSprites[3] };
        }
        else
        {
            tempList = new List<SVGImage> { headSprites[4], headSprites[5], headSprites[6], headSprites[7] };
        }

        headIndex = isNext ? headIndex + 1 : headIndex - 1;

        if (headIndex < 0)
        {
            headIndex = tempList.Count - 1;
        }

        if (headIndex == tempList.Count)
        {
            headIndex = 0;
        }

        head.sprite = tempList[headIndex].sprite;
    }

    public void ChangeLegSprite(bool isNext)
    {
        List<SVGImage> tempList;

        if (isMale)
        {
            tempList = new List<SVGImage> { legSprites[0], legSprites[1], legSprites[2] };
        }
        else
        {
            tempList = new List<SVGImage> { legSprites[3], legSprites[4], legSprites[5] };
        }


        legIndex = isNext ? legIndex + 1 : legIndex - 1;

        if (legIndex < 0)
        {
            legIndex = tempList.Count - 1;
        }

        if (legIndex == tempList.Count)
        {
            legIndex = 0;
        }
        foreach (var leg in legs)
        {
            leg.sprite = tempList[legIndex].sprite;
        }
    }

    public void ChangeAbdomenSprite(bool isNext)
    {
        List<SVGImage> tempList;
        if (isMale)
        {
            tempList = new List<SVGImage> { abdomenSprites[0], abdomenSprites[1], abdomenSprites[2], abdomenSprites[3] };
        }
        else
        {
            tempList = new List<SVGImage> { abdomenSprites[4], abdomenSprites[5], abdomenSprites[6], abdomenSprites[7] };
        }

        abdomenIndex = isNext ? abdomenIndex + 1 : abdomenIndex - 1;

        if (abdomenIndex < 0)
        {
            abdomenIndex = tempList.Count - 1;
        }

        if (abdomenIndex == tempList.Count)
        {
            abdomenIndex = 0;
        }

        abdomen.sprite = tempList[abdomenIndex].sprite;
    }

    public void ChangeGender(bool isMale)
    {
        this.isMale = isMale;

        List<SVGImage> tempList;
        if (isMale)
        {
            tempList = new List<SVGImage> { abdomenSprites[0], abdomenSprites[1], abdomenSprites[2], abdomenSprites[3] };
        }
        else
        {
            tempList = new List<SVGImage> { abdomenSprites[4], abdomenSprites[5], abdomenSprites[6], abdomenSprites[7] };
        }

        abdomen.sprite = tempList[abdomenIndex].sprite;

        if (isMale)
        {
            tempList = new List<SVGImage> { legSprites[0], legSprites[1], legSprites[2] };
        }
        else
        {
            tempList = new List<SVGImage> { legSprites[3], legSprites[4], legSprites[5] };
        }

        foreach (var leg in legs)
        {
            leg.sprite = tempList[legIndex].sprite;
        }


        if (isMale)
        {
            tempList = new List<SVGImage> { headSprites[0], headSprites[1], headSprites[2], headSprites[3] };
        }
        else
        {
            tempList = new List<SVGImage> { headSprites[4], headSprites[5], headSprites[6], headSprites[7] };
        }

        head.sprite = tempList[headIndex].sprite;
    }
}
