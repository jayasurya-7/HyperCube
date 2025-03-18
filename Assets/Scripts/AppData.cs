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


static class JediDataFormat
{
   
    static public string dformat { get; private set; }
    static public char[] dataTypes { get; private set; }
    static public int dataSize = -1;
    static public string[] dataLabels { get; private set; }
    static public readonly char[] FormatChars = new char[] { 'b', 'i', 'f' };
    static public readonly int[] FormatCharSize = new int[] { 1, 2, 4 };

    static public void ReadSetJediDataFormat(string filename)
    {
        dformat = "";
        // Open the file to read from.
        string[] allformatlines = File.ReadAllLines(filename);
        List<char> _dtypes = new List<char>();
        List<string> _dlabels = new List<string>();
        dataSize = 0;
        foreach (string line in allformatlines)
        {
            if (line.Length > 0)
            {
                // Split line by spaces
                string[] linecomps = line.Split(new char[] { ' ', }, StringSplitOptions.RemoveEmptyEntries);
                if (linecomps[0].Length == 1 && Array.Exists(JediDataFormat.FormatChars, chr => chr == linecomps[0][0]))
                {
                    dformat += linecomps[0];
                    _dtypes.Add(linecomps[0][0]);
                    _dlabels.Add(String.Join(" ", linecomps).Substring(2));
                    dataSize += FormatCharSize[Array.IndexOf(FormatChars, linecomps[0][0])];
                }
                else
                {
                    dformat = null;
                    dataTypes = null;
                    dataLabels = null;
                    dataSize = -1;
                    return;
                }
            }
        }
        dataTypes = _dtypes.ToArray();
        dataLabels = _dlabels.ToArray();
        
    }
    
    static public bool IsValidFormatLine(string line)
    {
        
        return false;
    }

    static public float GetFloatValue(int datainx, object dataval)
    {
        
        switch (dataTypes[datainx])
        {
            case 'b':
                return (float)((byte)dataval);
            case 'i':
                return (float)((UInt16)dataval);
            default:
                return (float)dataval;
        }
    }
}


public class ROM
{
    // Class attributes to store data read from the file
    public string datetime;
    public string side;

    //handle
    public float handleMin;
    public float handleMax;
    public float gripForce;

    //gross knob
    public float grossKnobMin;
    public float grossKnobMax;

    //fine knob
    public float fineKnobMin;
    public float fineKnobMax;

    //key Knob
    public float keyKnobMin;
    public float keyKnobMax;

    //Tripod Grasp
    public float tripodMin;
    public float tripodMax;



    public string filePath = AppData.idPath;

    // Constructor that reads the file and initializes values based on the mechanism
    public ROM()
    {
        string lastLine = "";
        string[] values;
        string fileName = $"{filePath}/rom.csv";

        try
        {
            using (StreamReader file = new StreamReader(fileName))
            {
                while (!file.EndOfStream)
                {
                    lastLine = file.ReadLine();
                }
            }
            values = lastLine.Split(',');
            if (values[0].Trim() != null)
            {
                // Assign values if mechanism matches
                datetime = values[0].Trim();
                handleMin = float.Parse(values[1].Trim());
                handleMax = float.Parse(values[2].Trim());
                gripForce = float.Parse(values[3].Trim());
                grossKnobMin = float.Parse(values[4].Trim());
                grossKnobMax = float.Parse(values[5].Trim());
                fineKnobMin = float.Parse(values[6].Trim());
                fineKnobMax = float.Parse(values[7].Trim());
                keyKnobMin = float.Parse(values[8].Trim());
                keyKnobMax = float.Parse(values[9].Trim());
                tripodMin = float.Parse(values[10].Trim());
                tripodMax = float.Parse(values[11].Trim());


            }
            else
            {
                // Handle case when no matching mechanism is found
                datetime = null;
                handleMin = 0;
                handleMax = 0;
                gripForce = 0;
                grossKnobMin = 0;
                grossKnobMax = 0;
                fineKnobMin = 0;
                fineKnobMax = 0;
                keyKnobMin = 0;
                keyKnobMax = 0;
                tripodMin = 0;
                tripodMax = 0;

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error reading the file: " + ex.Message);
        }
    }


    public ROM(float hMin, float hMax, float gForce, float gMin, float gMax, float fMin, float fMax, float kMin, float kMax, float tMin, float tMax, bool tofile)
    {
        handleMin = hMin;
        handleMax = hMax;
        gripForce = gForce;
        grossKnobMin = gMin;
        grossKnobMax = gMax;
        fineKnobMin = fMin;
        fineKnobMax = fMax;
        keyKnobMin = kMin;
        keyKnobMax = kMax;
        tripodMin = tMin;
        tripodMax = tMax;
        datetime = DateTime.Now.ToString();

        if (tofile)
        {
            // Write data to assessment file.
            WriteToAssessmentFile();
        }
    }

    public void WriteToAssessmentFile()
    {
        string _fname = Path.Combine(filePath , "rom.csv");
        UnityEngine.Debug.Log(_fname);
        using (StreamWriter file = new StreamWriter(_fname, true))
        {
            file.WriteLine(datetime + ", " + handleMin.ToString() + ", " + handleMax.ToString() + ", " + gripForce.ToString() + ", " + grossKnobMin.ToString() + "," 
                + grossKnobMax.ToString() + ", " + fineKnobMin.ToString() + ", " + fineKnobMax.ToString() +", "+ keyKnobMin.ToString() + ", " + keyKnobMax.ToString() + ", " + tripodMin.ToString() + ", " + tripodMax.ToString()+"");
        }
        Debug.Log("Writing");

    }


}


public static class gameData
{
    //Assessment check
    public static bool isPROMcompleted = false;
    public static bool isAROMcompleted = false;
    //AAN controller check
    public static bool isBallReached = false;
    public static bool targetSpwan = false;
    public static bool isAROMEnabled = false;
    //game
    public static bool isGameLogging;
    public static string game;
    public static int gameScore;
    public static int reps;
    public static int playerScore;
    public static int enemyScore;
    public static string playerPos = "0";
    public static string playerPosition = "0";
    public static string enemyPos = "0";
    public static string playerHit = "0";
    public static string enemyHit = "0";
    public static string wallBounce = "0";
    public static string enemyFail = "0";
    public static string playerFail = "0";
    public static int winningScore = 3;
    public static float moveTime;
    public static readonly string[] pongEvents = new string[] { "moving", "wallBounce", "playerHit", "enemyHit", "playerFail", "enemyFail" };
    public static readonly string[] hatEvents = new string[] { "moving", "BallCaught", "BombCaught", "BallMissed", "BombMissed" };
    public static readonly string[] tukEvents = new string[] { "moving", "collided", "passed" };
    public static readonly string[] pacManEvents = new string[] { "moving", "captured", "Dead" };
    public static readonly string[] spaceEvents = new string[] { "moving", "destroyed", "dead" };
    public static int events;
    public static string TargetPos;
    public static float successRate;
    public static float gameSpeedTT;
    public static float gameSpeedPP;
    public static float gameSpeedHT;
    public static float predictedHitY;
    public static bool setNeutral = false;
    private static DataLogger dataLog;
    private static readonly string[] gameHeader = new string[] {"playerPosY","enemyPosY","targetPosXY","events","playerScore","enemyScore"
    };
    private static readonly string[] tukTukHeader = new string[] {"playerPosx", "targetPos","events","playerScore"
    };
    private static readonly string[] pacManHeader = new string[] { "events", "playerScore" };
    public static readonly string[] spaceHeader = new string[] { "playerPosX", "events", "playerScore" };
    public static bool isLogging { get; private set; }
    public static bool moving = true; // used to manipulate events in HAT TRICK
    static public void StartDataLog(string fname)
    {
        if (dataLog != null)
        {
            StopLogging();
        }
        // Start new logger
        if (AppData.selectedGame == "pingPong")
        {
            if (fname != "")
            {
                string instructionLine = "0 - moving, 1 - wallBounce, 2 - playerHit, 3 - enemyHit, 4 - playerFail, 5 - enemyFail\n";
                string headerWithInstructions = instructionLine + String.Join(", ", gameHeader) + "\n";
                dataLog = new DataLogger(fname, headerWithInstructions);
                isLogging = true;
            }
            else
            {
                dataLog = null;
                isLogging = false;
            }
        }
        else if ((AppData.selectedGame == "autoRider")||(AppData.selectedGame == "highwayRacer"))
        {
            if (fname != "")
            {
                string instructionLine = "0 - moving, 1 - collided, 2 - passed\n";
                string headerWithInstructions = instructionLine + String.Join(", ", tukTukHeader) + "\n";
                dataLog = new DataLogger(fname, headerWithInstructions);
                isLogging = true;
            }
            else
            {
                dataLog = null;
                isLogging = false;
            }
        }
        else if (AppData.selectedGame == "spaceShooter")
        {
            if (fname != "")
            {
                string instructionLine = "0 - moving, 1 - enemyDestroyed, 2 - dead\n";
                string headerWithInstructions = instructionLine + String.Join(", ", spaceHeader) + "\n";
                dataLog = new DataLogger(fname, headerWithInstructions);
                isLogging = true;
            }
            else
            {
                dataLog = null;
                isLogging = false;
            }
        }
        else if ((AppData.selectedGame == "pacMan") || (AppData.selectedGame == "snakeGame"))
        {
            if (fname != "")
            {
                string instructionLine = "0 - moving, 1 - captured, 2 - Dead \n";
                string headerWithInstructions = instructionLine + String.Join(", ", pacManHeader) + "\n";
                dataLog = new DataLogger(fname, headerWithInstructions);
                isLogging = true;
            }
            else
            {
                dataLog = null;
                isLogging = false;
            }
        }
        else
        {
            Debug.Log("Unknown Game");
        }
    }
    static public void StopLogging()
    {
        if (dataLog != null)
        {
            UnityEngine.Debug.Log("Null log not");
            dataLog.stopDataLog(true);
            dataLog = null;
            isLogging = false;
        }
        else
            UnityEngine.Debug.Log("Null log");
    }

    static public void LogDataPP()
    {
        
            string[] _data = new string[] {
                playerPos,
                enemyPos,
                TargetPos,
                events.ToString("F2"),
                playerScore.ToString("F2"),
                enemyScore.ToString("F2")
            };
            string _dstring = String.Join(", ", _data);
            _dstring += "\n";
            dataLog.logData(_dstring);
        
    }

    static public void LogDataAR()
    {
        string[] _data = new string[] {
                playerPos,
                TargetPos,
                events.ToString("F2"),
                gameScore.ToString("F2")
            };
        string _dstring = String.Join(", ", _data);
        _dstring += "\n";
        dataLog.logData(_dstring);

    }
    static public void LogDataSS()
    {
        string[] _data = new string[] {
                playerPos,
                events.ToString("F2"),
                gameScore.ToString("F2")
            };
        string _dstring = String.Join(", ", _data);
        _dstring += "\n";
        dataLog.logData(_dstring);

    }
    static public void LogDataHR()
    {
            string[] _data = new string[] {
               playerPos,
               enemyPos,
               gameData.events.ToString("F2"),
               gameData.gameScore.ToString("F2")
            };
            string _dstring = String.Join(", ", _data);
            _dstring += "\n";
            dataLog.logData(_dstring);
        }

    static public void LogDataPM()
    {
        string[] _data = new string[] {
                events.ToString("F2"),
                gameScore.ToString("F2")
            };
        string _dstring = String.Join(", ", _data);
        _dstring += "\n";
        dataLog.logData(_dstring);

    }

}
public class DataLogger
{
 

    private string _currfilename;
    public string curr_fname
    {
        get { return _currfilename; }
    }
    public StringBuilder _filedata;
    public bool stillLogging
    {
        get { return (_filedata != null); }
    }

    public DataLogger(string filename, string header)
    {
        _currfilename = filename;
        _filedata = new StringBuilder(header);
    }

    public void stopDataLog(bool log = true)
    {
        if (log)
        {
            File.AppendAllText(_currfilename, _filedata.ToString());
        }
        _currfilename = "";
        _filedata = null;
    }

    public void logData(string data)
    {
        if (_filedata != null)
        {
            _filedata.Append(data);
        }
    }
}

public class DataLogger_afterbreak
{


    private string _currfilename1;
    public string curr_fname1
    {
        get { return _currfilename1; }
    }
    public StringBuilder _nextfile;
    public bool stillLogging
    {
        get { return (_nextfile != null); }
    }

    public DataLogger_afterbreak(string filename, string header)
    {
        _currfilename1 = filename;
        _nextfile = new StringBuilder(header);
    }

    public void stopDataLog_afterbreak(bool log = true)
    {
        if (log)
        {
            File.AppendAllText(_currfilename1, _nextfile.ToString());
        }
        _currfilename1 = "";
        _nextfile = null;
    }

    public void logData_afterbreak(string data)
    {
        if (_nextfile != null)
        {
            _nextfile.Append(data);
        }
    }
}
static class AppData
{
    static public string dataFolder = "Rawdata";
    static public string blockFolder = "blockdata";
    static public string calibFolder = "calib_data";
    static public string DetailFolder = "Trial ";
   
    static public string jdfFilename = "Assets\\jeditextformat.txt";
    static public string[] comPorts;

    static public string idPath = null;

    //def
    public static string selectedGame = null;
    public static string selectedMechanism = null;
    public static string hospno = null;
    public static int currentSessionNumber;
    public static string rawDataPath = null;
    public static string rawData = null;   
    public static string gameDataPath = null;
    public static string trialDataFileLocationTemp = null;


    //ROM values

        //handle Mech
        public static float handleAngleMax = 0f;
        public static float handleAngleMin = 0f;
        //grip force
        public static float handleGripForce= 0f;


        //gross knob
        public static float grossKnobMax = 0f;
        public static float grossKnobMin = 0f;

        //fine knob
        public static float fineKnobMin = 0f;
        public static float fineKnobMax = 0f;

        //key knob
        public static float keyKnobMin = 0f;
        public static float keyKnobMax = 0f;
        //TripodGrasp
        public static float graspMin = 0f;
        public static float graspMax = 0f;



    //testing purpose
    public static float avg = 0f;
    public static float dist = 0f;

    static public double nanosecPerTick = 1.0 / Stopwatch.Frequency;
    static public Stopwatch stp_watch = new Stopwatch();

    public static float timelog;
    public static int current_trial = 0;
    public static int current_block = 0;
    public static bool HyperCubeConnected = false;

    // Arduino related variables
    //static public JediSerialCom jediClient;

    // Incoming data format
    static public string dataFormat;

    // JEDI data buffer
    static public JediDataBuffers dataTime;
    static public JediDataBuffers dataBuffers;

    // Data Logger
    static public DataLogger dlogger;
    static public DataLogger_afterbreak afterLogger;

    // UDP Message Handling Variables.
    //static public List<string> msgQ = new List<string>();
    //static public bool logData = false;
    //static public bool exitApp = false;

    static public double CurrentTime()
    {
        return stp_watch.ElapsedTicks * nanosecPerTick;

    }
}

public static class JediSerialCom
{
    static public bool stop;
    static public bool pause;
    static public SerialPort serPort;
    static private Thread reader;
    static private uint _count;
    static public uint count

    {
        get { return _count; }
    }
    static public bool newData;
    static public int annotation;



    //public JediSerialCom(string port)
    //{
    //    serPort = new SerialPort();
    //    // Allow the user to set the appropriate properties.
    //    serPort.PortName = port;
    //    serPort.BaudRate = 115200;
    //    serPort.Parity = Parity.None;
    //    serPort.DataBits = 8;
    //    serPort.StopBits = StopBits.One;
    //    serPort.Handshake = Handshake.None;
    //    serPort.DtrEnable = true;

    //    // Set the read/write timeouts
    //    serPort.ReadTimeout = 500;
    //    serPort.WriteTimeout = 500;
    //    reader = new Thread(serialreaderthread);
     
    //}

    static public void InitSerialComm(string port)
    {
        serPort = new SerialPort();
        // Allow the user to set the appropriate properties.
        serPort.PortName = port;
        serPort.BaudRate = 115200;
        serPort.Parity = Parity.None;
        serPort.DataBits = 8;
        serPort.StopBits = StopBits.One;
        serPort.Handshake = Handshake.None;
        serPort.DtrEnable = true;

        // Set the read/write timeouts
        serPort.ReadTimeout = 250;
        serPort.WriteTimeout = 250;
    }

    static public void Connect()
    {
        stop = false;
        if (serPort.IsOpen == false)
        {
            try
            {
                serPort.Open();
            }
            catch (Exception ex)
            {
                Debug.Log("exception: " + ex);
            }
            // Create a new thread to read the serial port data.
            reader = new Thread(serialreaderthread);
            reader.Priority = System.Threading.ThreadPriority.AboveNormal;
            reader.Start();
        }
    }

    static public void Disconnect()
    {
        stop = true;
        if (serPort.IsOpen)
        {
            reader.Abort();
            serPort.Close();
        }
    }

  
   static  public void resetCount()
    {
        _count = 0;
    }

   static private void serialreaderthread()
    {
        byte[] _floatbytes = new byte[4];

        // start stop watch.
        while (stop == false)
        {
           
            // Do nothing if in pause
            if (pause)
            {  
                continue;
            }
            try
            {
                
                // Read full packet.
                if (readFullSerialPacket())
                {
                                                                                                                                                                                                                      
                    _count++;
                    JediSerialPayload.updateData();
                    newData = true;
                    AppData.HyperCubeConnected = true;
                    // Update data buffers.
                    if (AppData.dataBuffers != null)
                    {

                        AppData.dataTime.Add(new float[] { (float)AppData.CurrentTime() });
                        AppData.dataBuffers.Add(JediSerialPayload.data);
                    }
                    // Debug.Log(AppData.dlogger);
                    //// Check if data is to be logged.
                    if (AppData.dlogger != null)
                    {

                        string traildata = hyper1.timer_ + JediSerialPayload.GetFormatedData(JediSerialPayload.data) + "\n";

                        AppData.dlogger.logData(traildata);
                        // Debug.Log(traildata);
                    }
                    //if (AppData.afterLogger != null)
                    //{

                    //    string traildata = protocol.timestart + JediSerialPayload.GetFormatedData(JediSerialPayload.data) + "," + AppData.current_trial + "," + AppData.current_block + "\n";
                    //    //  Debug.Log(traildata);
                    //    AppData.afterLogger.logData_afterbreak(traildata);

                    //}

                    else
                    {
                       
                        annotation = 0;
                    }
                }
                else
                {
                    AppData.HyperCubeConnected = false;
                    Debug.Log($" hypercube : {AppData.HyperCubeConnected}");
                }
            }
            catch (TimeoutException)
            {
             
                continue;
            }
        }
        serPort.Close();
    }

    // Read a full serial packet.
    static private bool readFullSerialPacket()
    {
        int chksum = 0;
        int _chksum;
        int rawbyte;
      //  Debug.Log(serPort.ReadByte().ToString());
        // Header bytes
        if ((serPort.ReadByte() == 0xFF) && (serPort.ReadByte() == 0xFF))
        {

            JediSerialPayload.count++;
            chksum = 255 + 255;
            //  No. of bytes
            JediSerialPayload.plSz = serPort.ReadByte();
            chksum += JediSerialPayload.plSz;
          
            // read payload
            for (int i = 0; i < JediSerialPayload.plSz - 1; i++)
            {
                rawbyte = serPort.ReadByte();
                chksum += rawbyte;
                JediSerialPayload.payload[i] = rawbyte;
                JediSerialPayload.payloadBytes[i] = (byte)rawbyte;
            }

           //    De.Log("ping");
            // ensure check sum is correct.
           // Debug.Log(JediSerialPayload.plSz);
            _chksum = serPort.ReadByte();
           // Debug.Log(_chksum == (chksum & 0xFF));
            return (_chksum == (chksum & 0xFF));

        }
        
        
        //    Debug.Log("Wrong Data");
            return false;
        
    }

    // Write output serial packet
 static   public void WriteSerialPacket(byte[] msgParam)
    {
        //SerializeField] Text textbox_values;
        int chksum;
        // int j = 0;
        byte[] packet = new byte[msgParam.Length + 4];
        packet[0] = 0xFF;
        //Console.Write("\n{0} ", packet[_j++]);
        packet[1] = 0xFF;
        //Console.Write("{0} ", packet[_j++]);
        packet[2] = (byte)(msgParam.Length + 1);
        //Console.Write("{0}", packet[_j++]);
        chksum = 0xFF + 0xFF + packet[2];
        //Console.Write("({0}) ", chksum);
        for (int i = 0; i < msgParam.Length; i++)
        {
            packet[3 + i] = msgParam[i];
            //Console.Write("{0}", packet[_j++]);
            chksum += msgParam[i];
            //Console.Write("({0}) ", chksum);
        }
        packet[packet.Length - 1] = (byte)chksum;
        //Console.Write("{0}\n", packet[_j++]);
        serPort.Write(packet, 0, packet.Length);


    }
}

public class JediDataBuffers
{
    static public readonly int maxBufferLength = 1000;
    public int noOfChannels { get; private set; }
    private float[,] _rawData;
    public float[,] rawData
    {
        get { return _rawData; }
    }
    public int head { get; private set; }
    public int tail { get; private set; }
    public int count { get; private set; }
    public string[] channelLabels { get; private set; }

    public JediDataBuffers(int noChannels, string[] chLbls)
    {
        noOfChannels = noChannels;
        _rawData = new float[noChannels, maxBufferLength];
        channelLabels = (string[])chLbls.Clone();
        head = maxBufferLength - 1;
        tail = 0;
        count = 0;
    }

    public void Add(float[] newdata)
    {
        // Update tail and start
        head = (head + 1) % maxBufferLength;
        for (int i = 0; i < newdata.Length; i++)
        {
            _rawData[i, head] = newdata[i];
        }
        if (count == maxBufferLength)
        {
            tail = (tail + 1) % maxBufferLength;
        }
        else
        {
            count++;
        }
    }

    public void Add(List<object> newdata)
    {
        // Update tail and start
        head = (head + 1) % maxBufferLength;
        for (int i = 0; i < newdata.Count; i++)
        {
            _rawData[i, head] = JediDataFormat.GetFloatValue(i, newdata[i]);
        }
        if (count == maxBufferLength)
        {
            tail = (tail + 1) % maxBufferLength;
        }
        else
        {
            count++;
        }

    }
  
}




