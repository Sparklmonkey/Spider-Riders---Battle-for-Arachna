using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;

[CreateAssetMenu(fileName = "UpperBodySet", menuName = "ScriptableObjects/UpperBodySet", order = 1)]
public class UpperBodySet : ScriptableObject
{
    public SVGImage torsoArmour;
    public SVGImage shoulderArmour;
    public SVGImage upperArmArmour;
    public SVGImage foreArmArmour;
    public SVGImage fistArmour;
}