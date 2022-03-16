using System.Security.Cryptography.X509Certificates;
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
        BaseOperator bo = this.GetComponent<BaseOperator>();
        bo.o = bms.operSet[0];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
