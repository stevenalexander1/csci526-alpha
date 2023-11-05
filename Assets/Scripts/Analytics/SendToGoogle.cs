
using UnityEngine;

public  static  class SendToGoogle 
{  
    //For Analytics: R count
    private static long _sessionID;
    private static int _cameraCount=0;
    private static int _rCount=0;

    //For Analytics: player level success
    private static int _playerDieCount = 0;
    private static string _playerPassLevels = " ";
    private static string _playerFailLevels = " ";
    public static long prevSessionID=0;

    //For Analytics: player vs lasers
    private static bool _isLaserDeath = false;
    private static string _laserPassLevels = " ";
    private static string _laserFailLevels = " ";

    //For Analytics : player vs guards
    private static bool _isGuardDeath = false;
    private static string _guardPassLevels = " ";
    private static string _guardFailLevels = " ";

    public static bool laserExists = false;
    public static bool guardExists = false;



    public static void resetParameters()
    {
        _playerFailLevels = " ";
        _playerPassLevels = " ";
        _playerDieCount = 0;
        _cameraCount = 0;
        _rCount = 0;
        _sessionID = System.DateTime.Now.Ticks;
        _guardFailLevels = " ";
        _guardPassLevels = " ";
        _laserFailLevels = " ";
        _laserPassLevels = " ";
        _isGuardDeath = false;
        _isLaserDeath = false;
        laserExists = false;
        guardExists = false;

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

    public static void setLaserFailLevels(string str)
    {
        _laserFailLevels += str + ",";
    }

    public static void setLaserPassLevels(string str)
    {
        _laserPassLevels += str + ",";
    }

    public static void setIsLaserDeath(bool val)
    {
        _isLaserDeath = val;
    }

    public static void setGuardFailLevels(string str)
    {
        _guardFailLevels += str + ",";
    }

    public static void setGuardPassLevels(string str)
    {
        _guardPassLevels += str + ",";
    }

    public static void setIsGuardDeath(bool val)
    {
        _isGuardDeath= val;
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

    public static string getLaserPassLevels()
    {
        return _laserPassLevels;
    }
    public static string getLaserFailLevels()
    {
        return _laserFailLevels;
    }
    public static bool getIsLaserDeath()
    {
        return _isLaserDeath;
    }

    public static string getGuardPassLevels()
    {
        return _guardPassLevels;
    }
    public static string getGuardFailLevels()
    {
        return _guardFailLevels;
    }
    public static bool getIsGuardDeath()
    {
        return _isGuardDeath;
    }


}
