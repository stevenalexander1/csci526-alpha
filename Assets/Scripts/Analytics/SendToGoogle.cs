
using UnityEngine;

public  static  class SendToGoogle 
{
    private static long _sessionID;
    private static int _cameraCount=0;
    private static int _rCount=0;
    private static Vector3  _playerPosition;
    private static Vector3 _checkpointPosition;
    
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
    public static void setPlayerPosition(Vector3 pos)
    {
        _playerPosition = pos;
    }
    public static void setCheckpointPosition(Vector3 pos)
    {
        _checkpointPosition = pos;
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
    public static Vector3 getPlayerPosition()
    {
        return _playerPosition ;
    }
    public static Vector3 getCheckpointPosition()
    {
       return  _checkpointPosition;
    }



   
}
