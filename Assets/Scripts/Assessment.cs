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

    public static hyper1 instance;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        forceValues.text = (Mathf.Round((hyper1.instance.force_total) * 10.0f)*0.1f).ToString();
        ThumbDistance.text = hyper1.instance.distance1.ToString();
        IndexDistance.text = hyper1.instance.distance2.ToString();
        HandleAngle.text = (Mathf.Round((hyper1.instance.ang1) * 10.0f) * 0.1f).ToString();
        Distance.text = (Mathf.Round((hyper1.instance.Btw_dist) * 10.0f) * 0.1f).ToString();
        KnobAngle.text = (Mathf.Round((hyper1.instance.ang2) * 10.0f) * 0.1f).ToString();
        FineKnobAngle.text = (Mathf.Round((hyper1.instance.ang4) * 10.0f) * 0.1f).ToString();
        KeyKnobAngle.text = (Mathf.Round((hyper1.instance.ang3) * 10.0f) * 0.1f).ToString();
    }

    
}
