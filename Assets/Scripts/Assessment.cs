using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime;
using System.IO;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using TMPro;

public class Assessment : MonoBehaviour
{
    public TMPro.TMP_Text forceValues;
    public TMPro.TMP_Text HandleAngle;
    public TMPro.TMP_Text ThumbDistance;
    public TMPro.TMP_Text IndexDistance;
    public TMPro.TMP_Text Distance;
    public TMPro.TMP_Text KnobAngle;
    public TMPro.TMP_Text FineKnobAngle;
    public TMPro.TMP_Text KeyKnobAngle;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        forceValues.text = (Mathf.Round((JediSerialPayload.totalForce) * 10.0f)*0.1f).ToString();
        ThumbDistance.text = JediSerialPayload.distance_1.ToString();
        IndexDistance.text = JediSerialPayload.distance_2.ToString();
        HandleAngle.text = (Mathf.Round((JediSerialPayload.angle_1) * 10.0f) * 0.1f).ToString();
        Distance.text = (Mathf.Round((JediSerialPayload.btwDistance) * 10.0f) * 0.1f).ToString();
        KnobAngle.text = (Mathf.Round((JediSerialPayload.angle_2) * 10.0f) * 0.1f).ToString();
        FineKnobAngle.text = (Mathf.Round((JediSerialPayload.angle_4) * 10.0f) * 0.1f).ToString();
        KeyKnobAngle.text = (Mathf.Round((JediSerialPayload.angle_3) * 10.0f) * 0.1f).ToString();
    }

    
}
