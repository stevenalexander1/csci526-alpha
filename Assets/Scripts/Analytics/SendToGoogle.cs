
using UnityEngine;

public  static  class SendToGoogle 
{
    private static long _sessionID;
    private static int _cameraCount=0;
    private static int _rCount=0;

    //for player level success
    private static int _playerDieCount = 0;
    private static string _playerPassLevels = " ";
    private static string _playerFailLevels = " ";
    public static long prevSessionID=0;


    public static void resetParameters()
    {
        _playerFailLevels = "";
        _playerPassLevels = "";
        _playerDieCount = 0;
        _cameraCount = 0;
        _rCount = 0;
        _sessionID = System.DateTime.Now.Ticks;

    }
    public static void setSessionId(long s_id)
    {
        _sessionID = s_id;
    }
    public static void setCameraCount(int count)
    {
        _cameraCount += count;
    }
    public static void setrCount(int count)
    {
        _rCount += count;
    }
    public static  void setPlayerDieCount(int count)
    {
        _playerDieCount += count;
    }
    public static void setPlayerPassLevels(string str)
    {
        _playerPassLevels += str+",";
    }
    public static void setPlayerFailLevels(string str)
    {
        _playerFailLevels += str + ",";
    }

    // getter
    public static long getSessionId()
    {
       return  _sessionID;
    }
    public static int getCameraCount()
    {
        return _cameraCount;
    }
    public static int getrCount()
    {
        return _rCount;
    }
    
    public static int getPlayerDieCount()
    {
        return _playerDieCount;
    }
    public static string getPlayerPassLevels()
    {
        return _playerPassLevels;
    }
    public static string getPlayerFailLevels()
    {
        return _playerFailLevels;
    }




}
