using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using System.IO;

public class CAR_spawnTargets1 : MonoBehaviour
{
    public static CAR_spawnTargets1 instance;


    //AAN aan = new AAN();

    // static int trailNumber;
    //public Text trailNUmber;

    // details of AAN;
    int val;
    static int steps = 10;
    static float stepSize;
    //public static float[] assistanceAngle = new float[steps];
    //public static float[] assistanceTorque = new float[steps];
    //public static float[] assistancePerformace = new float[steps];

    //runnnin game 
    public float trailDuration = 3.5f;
    public float stopClock;
    public bool reached;
    public bool onceReached;
    public float reduceOppositeTimer = 0;
    public float playSize = 0;
    private string mech;
    private string hospitalnum;
    //public static float[] aRom = { 0, 0 };
    //public static float[] pRom = { 0, 0 };
    float prevAng;
    bool angChange;
    public static float targetAngle;
    //GameObject target;
    float toqAmp;
    public int count = 0;
    GameObject target;
    GameObject player;


    public float blockduration = 10;
    public static bool stopAssistance = true;
    public float initialDirection;
    public float initialTorque;
    public float prevTorq;
    public int win;
    int index;

    // 
    int[] successRate;
    float avgSuccessRate;
    bool dontAssistTrial;

    float[] First4Targets;
    int targetcount = 0;

    public Toggle isFlaccidToggle;

    public bool isFlaccidControlOn;

    public int lastValue_rand;
    bool paramSet;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);

        Application.targetFrameRate = 300;
        QualitySettings.vSyncCount = 0;


    }

    // Start is called before the first frame update
    void Start()
    {
        //paramSet = false;
        //AppData.regime = "MINIMAL ASSIST";
        //successRate = new int[5] { 0, 0, 0, 0, 0 };
        //Application.targetFrameRate = 300;
        //hospitalnum = AppData.subjHospNum;
        //targetcount = -1;
        //targetAngle = -999;
        //AppData.plutoData.mechIndex = 0;
        //Debug.Log(AppData.plutoData.mechs[AppData.plutoData.mechIndex]);
        //setPrameters();
        //First4Targets = new float[] { calculateAngle(4.7f, pRom), calculateAngle(-4.7f, pRom) };

        //System.Random rnd = new System.Random();
        //First4Targets = First4Targets.OrderBy(x => rnd.Next()).ToArray();
        //mech = AppData.plutoData.mechs[AppData.plutoData.mechIndex];




        //ActiveRangeOfMotion activeRange = new ActiveRangeOfMotion(AppData.subjHospNum,
        //           mech);
        //PassiveRangeOfMotion passiveRange = new PassiveRangeOfMotion(AppData.subjHospNum,
        //           mech);


        //stopClock = trailDuration;
        //stepSize = (pRom[1] - pRom[0]) / steps;
        //for (int i = 0; i < assistanceAngle.Length; i++)
        //{
        //    assistanceAngle[i] = pRom[0] + stepSize * i;
        //    //Debug.Log(assistanceAngle[i]);
        //}



        //for (int i = 0; i < assistanceTorque.Length; i++)
        //{
        //    assistanceTorque[i] = 0.2F;
        //}
        //SendToRobot.ControlParam(mech, ControlType.TORQUE, false, false);
        // Set control parameters
        //SendToRobot.ControlParam(mech, ControlType.TORQUE, false, true);
        //AppData.plutoData.desTorq = 0;
        //SendToRobot.ControlParam(mech, ControlType.TORQUE, true, false);


        //foreach (var val in assistanceTorque){
        //    val =val +0.1f

        //}

        /// read previous game performace
        //AppData.ReadGamePerformance();
        //PlutoDataStructures.AAN aanprofile = new PlutoDataStructures.AAN(AppData.subjHospNum, AppData.plutoData.mechs[AppData.plutoData.mechIndex]);
        





    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Time.timeScale);

        player = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.FindGameObjectWithTag("Target");
        //gameduration += Time.deltaTime;
        stopClock -= Time.deltaTime;
        //AppData.isflalccidControl = isFlaccidControlOn ? 1 : 0;
        //if ((AppData.plutoData.angle < (1.2f * AppData.pROM()[0] - 20)) || (AppData.plutoData.angle > (AppData.pROM()[1] * 1.2f + 20)))
        //{

        //    AppData.plutoData.desTorq = TorqueProfile(getTorque(targetAngle)) / 1.5f;
        //}
        //else
        //{
        //    if (GameManager_Car2D.instance.isPlaying && Time.timeScale != 0)
        //    {
        //        AppData.plutoData.desTorq = TorqueProfile(getTorque(targetAngle));
        //        AppData.plutoData.desTorq = Mathf.Clamp(AppData.plutoData.desTorq, -1.2f, 1.2f);
        //    }
        //}




        //if (!GameManager_Car2D.instance.isPlaying || Time.timeScale == 0 || Mathf.Abs(AppData.plutoData.angle) > 130)
        //{
        //    AppData.plutoData.desTorq = 0;
        //    stopClock = trailDuration;
        //    prevTorq = 0;
        //    initialTorque = 0;

        //}
        //SendToRobot.ControlParam(mech, ControlType.TORQUE, true, false);
        





    }

    public Vector2 TargetSpawn()
    {
        playSize = PlayerController_Car2D.playSize;
        //Debug.Log(isInPROM(targetAngle) + "," + avgSuccessRate + "," + dontAssistTrial);
        //if (AppData.isGameLogging)
        //{
        //    GameLog.aanProfile = "\"" + String.Join(",", assistanceTorque) + "\"";
        //}
        // AppData.plutoData.desTorq = 0;
        //  SendToRobot.ControlParam(mech, ControlType.TORQUE, true, false);

        count++;
        player = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.FindGameObjectWithTag("Target");


        // assistanceTorque = aan.adaptation(assistanceTorque, assistancePerformace, targetAngle, reached);
        //GameObject target;
        // assistanceTorque = aan.adaptation(assistanceTorque, assistancePerformace, targetAngle, reached);


        // updateSucess

        //UpdateSuccessRate();



        onceReached = false;


        reduceOppositeTimer = 0;

        Vector2 targetPos = new Vector2(0, 6f);
        targetcount++;
        targetPos.x = Random.Range(-6, 6);
        //if (targetcount > 1)
        //{
        //    targetAngle = GetRandomAngle(aRom, pRom);
        //}
        //else
        //{

        //    targetAngle = First4Targets[targetcount];

        //}
        // Debug.Log(angle);
        //targetAngle = angle;
        //dontAssistTrial = false;
        //if (isInPROM(targetAngle) && avgSuccessRate >= 0.8)
        //{
        //    dontAssistTrial = true;
        //}
        //index = (int)aan.GetindexCorrected(targetAngle);
        //targetPos.x = Angle2Screen(targetAngle);

        //prevAng = AppData.plutoData.angle;
        //initialDirection = getDirection();
        //initialTorque = prevTorq;
        // onceReached = false;
        //   Debug.Log(initialTorque);
        //Debug.Log(targetPos.x.ToString() + "," + angle.ToString());
        // targetPos.y = target.transform.position.y;
        //AppData.trailNumber = targetcount;
        //AppData.isAssisted = dontAssistTrial ? 0 : 1;



        return targetPos;

    }

    //public bool isInPROM(float angle)
    //{
    //    if (angle < AppData.aROM()[0] || angle > AppData.aROM()[1])
    //    {
    //        Debug.Log("prom target");
    //        return true;
    //    }
    //    else
    //        return false;

    //}

    //public void OnFlaccidToggleSelect()
    //{
    //    if (paramSet)
    //    {
    //        isFlaccidControlOn = isFlaccidToggle.isOn;
    //        string _fname = Path.Combine(SubjectData.Get_Subj_Assessment_Dir(AppData.subjHospNum), "aan_" + mech + ".csv");
    //        using (StreamWriter file = new StreamWriter(_fname, true))
    //        {
    //            AppData.dateTime = DateTime.Now.ToString("Dyyyy-MM-ddTHH-mm-ss");
    //            string res = String.Join(",", assistanceTorque);
    //            file.WriteLine(AppData.dateTime + ", " + AppData.pROM()[0].ToString() + ", " + AppData.pROM()[1].ToString() + ", " + "10" + "," + res.ToString() + "," + Convert.ToInt32(isFlaccidControlOn).ToString());
    //            Debug.Log(_fname);
    //        }
    //    }
    //}
    //public void UpdateSuccessRate()
    //{
    //    if (isInPROM(targetAngle))
    //    {

    //        int val = onceReached || reached ? 1 : 0;
    //        Debug.Log(val);
    //        for (int i = 0; i < successRate.Length; i++)
    //        {
    //            if (i <= successRate.Length - 2)
    //            {
    //                successRate[i] = successRate[i + 1];
    //            }
    //            else
    //                successRate[i] = val;

    //        }

    //    }
    //    avgSuccessRate = (float)successRate.Sum() / (float)successRate.Length;
    //    Debug.Log(avgSuccessRate);
    //}


    //float getDirection()
    //{
    //    return Mathf.Sign(targetAngle - AppData.plutoData.angle);
    //}

    //public float RandomAngle()
    //{
    //    float prevtargetAngle = targetAngle;
    //    float tempAngle = Random.Range(AppData.pROM()[0], AppData.pROM()[1]);


    //    while (Mathf.Abs(tempAngle - prevtargetAngle) < Mathf.Abs(pRom[1] - pRom[0]) / 2.5f)
    //    {
    //        tempAngle = Random.Range(AppData.pROM()[0], AppData.pROM()[1]);
    //    }


    //    return tempAngle;

    //}
    //public float Angle2Screen(float angle)
    //{


        //return (-playSize + (angle - AppData.pROM()[0]) * (2 * playSize) / (AppData.pROM()[1] - AppData.pROM()[0]));


    //}
    //public void setPrameters()
    //{

    //    mech = AppData.plutoData.mechs[AppData.plutoData.mechIndex];
    //    aRom = AppData.aROM();
    //    pRom = AppData.pROM();
    //    isFlaccidControlOn = false;

    //    //checkIfFlaccid();



    //    PlutoDataStructures.AAN aanprofile = new PlutoDataStructures.AAN(AppData.subjHospNum, AppData.plutoData.mechs[AppData.plutoData.mechIndex]);
    //    assistanceTorque = aanprofile.profile;

    //    isFlaccidControlOn = aanprofile.isFlaccid == 1 ? true : false;
    //    isFlaccidToggle.isOn = isFlaccidControlOn;

    //    Debug.Log(String.Join(",", assistanceTorque));
    //    initialTorque = 0;
    //    stopClock = trailDuration;
    //    onceReached = false;
    //    index = (int)aan.GetindexCorrected(targetAngle);
    //    Debug.Log(targetAngle + "," + index);
    //    stepSize = (pRom[1] - pRom[0]) / (steps - 1);

    //    for (int i = 0; i < assistanceAngle.Length; i++)
    //    {
    //        assistanceAngle[i] = pRom[0] + stepSize * i;
    //        if (i == assistanceAngle.Length)
    //        {
    //            assistanceAngle[i] = pRom[1];
    //        }

    //    }
    //    paramSet = true;
    //}


    //void checkIfFlaccid()
    //{
    //    float[] maxROM = { 100, 50, 120, 75, 100, 100 };

    //    if (Mathf.Abs(aRom[1] - aRom[0]) < 10 && Mathf.Abs(pRom[1] - pRom[0]) >= maxROM[AppData.plutoData.mechIndex])
    //    {
    //        isFlaccidControlOn = true;
    //    }
    //    else
    //        isFlaccidControlOn = false;
    //}
    //public float getTorque(float targetAngle)
    //{
    //    float torque;
    //    targetAngle = Mathf.Clamp(targetAngle, AppData.pROM()[0], AppData.pROM()[1]);
    //    int i = Array.FindIndex(assistanceAngle, k => targetAngle <= k);
    //    i = i == -1 ? assistanceAngle.Length - 1 : i;

    //    if (i > 0)
    //    {
    //        torque = assistanceTorque[i - 1] + (targetAngle - assistanceAngle[i - 1]) * (assistanceTorque[i] - assistanceTorque[i - 1]) / (assistanceAngle[i] - assistanceAngle[i - 1]);

    //    }
    //    else
    //    {
    //        torque = assistanceTorque[i];
    //    }
    //    Debug.Log(String.Join(",", assistanceAngle));
    //    Debug.Log(String.Join(",", assistanceTorque));
    //    Debug.Log("Index:" + i + "Target:" + targetAngle);
    //    Debug.Log("Index:" + i + "Target:" + torque);

    //    torque = Mathf.Clamp(torque, assistanceTorque.Min(), assistanceTorque.Max());
    //    return (torque);
    //}

    private void OnApplicationQuit()
    {
        // make 


    }
    //public float TorqueProfile(float amp)
    //{

    //    if (!isFlaccidControlOn)
    //    {
    //        return (normalController(amp));
    //    }
    //    else
    //    {
    //        float assistanceTorque = Mathf.Abs(amp) < 0.2 ? 0.2f : Mathf.Abs(amp);
    //        Debug.Log("flaccid" + assistanceTorque);
    //        return (flaccidController(assistanceTorque));
    //    }


    //}

    //float normalController(float amp)
    //{
    //    float time = trailDuration - stopClock;
    //    time = (time / trailDuration);

    //    //Debug.Log(amp);
    //    if (dontAssistTrial)
    //    {
    //        prevTorq = 0;
    //    }
    //    else
    //    {
    //        if (Mathf.Abs(targetAngle - AppData.plutoData.angle) > 5 && initialDirection == getDirection() && !onceReached)
    //        {
    //            reduceOppositeTimer = 0;
    //            prevTorq = Mathf.SmoothStep(initialTorque, amp, Mathf.Clamp(time, 0, trailDuration));
    //        }
    //        else
    //        {
    //            onceReached = true;

    //            if (Mathf.Abs(targetAngle - AppData.plutoData.angle) > 3)
    //            {
    //                reduceOppositeTimer += Time.deltaTime;
    //                reduceOppositeTimer = Mathf.Min(reduceOppositeTimer, 3);
    //                if (Mathf.Abs(prevTorq) > 0.05)
    //                    prevTorq = prevTorq + getDirection() * reduceOppositeTimer * 0.01f;
    //            }


    //        }
    //    }
    //    prevTorq = Mathf.Clamp(prevTorq, assistanceTorque.Min(), assistanceTorque.Max());

    //    return prevTorq;
    //}
    //float flaccidController(float amp)
    //{

    //    float time = trailDuration - stopClock;
    //    time = (time / trailDuration);
    //    if (AppData.regime == "MINIMAL ASSIST" && GameManager_Car2D.instance.isPlaying)
    //    {

    //        if (getDirection() == initialDirection)
    //        {
    //            // reduceOppositeTimer = 0;
    //            if (onceReached == false)
    //            {
    //                prevTorq = Mathf.SmoothStep(initialTorque, getDirection() * Mathf.Abs(amp), Mathf.Clamp(time, 0, trailDuration));
    //                if (AppData.plutoData.mechIndex != 2)
    //                    return prevTorq;
    //                else
    //                    return -prevTorq;
    //            }
    //            else
    //            {
    //                reduceOppositeTimer += Time.deltaTime;
    //                reduceOppositeTimer = Mathf.Min(reduceOppositeTimer, 3);
    //                if (Mathf.Abs(prevTorq) > 0.05)
    //                    prevTorq = prevTorq + Mathf.Sign(prevTorq) * reduceOppositeTimer * 0.01f;
    //                if (AppData.plutoData.mechIndex != 2)
    //                    return prevTorq;
    //                else
    //                    return -prevTorq;
    //            }

    //        }
    //        else
    //        {
    //            reduceOppositeTimer += Time.deltaTime;
    //            onceReached = true;
    //            if (Mathf.Abs(prevTorq) > 0.05)
    //                prevTorq = prevTorq - Mathf.Sign(prevTorq) * reduceOppositeTimer * 0.01f;
    //            if (AppData.plutoData.mechIndex != 2)
    //                return prevTorq;
    //            else
    //                return -prevTorq;
    //        }
    //    }
    //    else
    //        return 0;


    //}


    //public float GetRandomAngle(float[] aRom, float[] pRom)
    //{
    //    float tempAngle;
    //    //{
    //    float[] Possibleangles = { calculateAngle(-4.7f, pRom), calculateAngle(-3f, pRom), calculateAngle(-1f, pRom), calculateAngle(1f, pRom), calculateAngle(3f, pRom), calculateAngle(4.7f, pRom) };
    //    tempAngle = Possibleangles[UniqueRandomInt(0, Possibleangles.Length)];
    //    //}
    //    //else
    //    //{
    //    //    float[] Possibleangles = { calculateAngle(-4.7f, aRom), calculateAngle(-3f, aRom), calculateAngle(-1f, aRom), calculateAngle(1f, aRom), calculateAngle(3f, aRom), calculateAngle(4.7f, aRom) };
    //    //    tempAngle = Possibleangles[UniqueRandomInt(0, Possibleangles.Length)];
    //    //}
    //    return tempAngle;
    //}


    //public float calculateAngle(float pos, float[] pRom)
    //{
    //    return (((pos + 5f) * (pRom[1] - pRom[0]) / (2 * 5f)) + pRom[0]);
    //}

    //public int UniqueRandomInt(int min, int max)
    //{
    //    int val = Random.Range(min, max);
    //    while (lastValue_rand == val)
    //    {
    //        val = Random.Range(min, max);
    //    }
    //    lastValue_rand = val;
    //    return val;
    //}


    //public class AAN
    //{


    //    public bool isInAROM(float angle)
    //    {
    //        if (aRom[0] <= angle && angle <= aRom[1])
    //        {
    //            return true;
    //        }

    //        else
    //        {
    //            return false;
    //        }

    //    }



    //    public float Getindex(float angle)
    //    {
    //        float temp = -999;
    //        temp = (angle - pRom[0]) / stepSize;

    //        return temp;
    //    }

    //    public int GetindexCorrected(float angle)
    //    {
    //        int i = Array.FindIndex(assistanceAngle, k => angle <= k);

    //        return i;

    //    }




    //}
}




