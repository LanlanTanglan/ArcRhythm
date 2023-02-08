using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArkRhythm;
[CreateAssetMenu(fileName = "BMSData", menuName = "ArcRhythm/BMSData", order = 0)]
public class BMSData : ScriptableObject
{
    [SerializeField] public BMS _bms;
    
}
