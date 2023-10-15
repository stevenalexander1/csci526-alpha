using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AnalyticsManager : MonoBehaviour
{
    private string URL;
    public  GameObject checkpoint;

    // Start is called before the first frame update
    void OnApplicationQuit()
    {
        // Gather and send necessary data to your analytics system here
        Send();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Awake()
    {
        // For Analytics:
        SendToGoogle.setSessionId(DateTime.Now.Ticks);
    }

    public void Send()
    {
        // Assign variables
        URL = "https://docs.google.com/forms/u/1/d/e/1FAIpQLSeTkEB_0KQ9_WZ4IDevxjPEKgVs28y3uYIxHBQnGPF2MyvGng/formResponse";
       // SendToGoogle.setCameraCount(UnityEngine.Random.Range(0, 101) ); 
       // SendToGoogle.setrCount(UnityEngine.Random.Range(0, 101));
        SendToGoogle.setPlayerPosition(transform.position);
        SendToGoogle.setCheckpointPosition(checkpoint.transform.position);

        string sid = SendToGoogle.getSessionId().ToString();
        string c_count = SendToGoogle.getCameraCount().ToString();
        string r_count = SendToGoogle.getrCount().ToString();
        string player_pos = SendToGoogle.getPlayerPosition().ToString();
        string checkpoint_pos = SendToGoogle.getCheckpointPosition().ToString();
        StartCoroutine(Post(sid, c_count,r_count,player_pos, checkpoint_pos));


    }



    private IEnumerator Post(string sessionID, string cameraCount, string rCount, string playerPosition, string checkpointPosition)
    {
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        form.AddField("entry.216176141", sessionID);
        form.AddField("entry.790488099", cameraCount);
        form.AddField("entry.950443003", rCount);
        form.AddField("entry.47542420", playerPosition);
        form.AddField("entry.724589010", checkpointPosition);

        // Send responses and verify result
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }




    }





}
