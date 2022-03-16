using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcRhythm;
using Util;

public class test : MonoBehaviour
{
    public BMS bms;
    // Start is called before the first frame update
    void Start()
    {
        this.bms = new BMS().SetParam(JsonUtil.readJSON("StaffData\\" + "testStaff1" + ".json"));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
