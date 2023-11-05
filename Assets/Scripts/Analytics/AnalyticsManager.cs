using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using UnityEngine;
using UnityEngine.Networking;

public class AnalyticsManager : MonoBehaviour
{
    private string URL;
 
    // Start is called before the first frame update
    
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
        URL = "https://docs.google.com/forms/u/1/d/e/1FAIpQLSc2qypU9cSSGMJ3e-wkbSoxs7b-DXlTFtE_Hh4koQizjJZb_g/formResponse";

        string sid = SendToGoogle.getSessionId().ToString();
        string c_count = SendToGoogle.getCameraCount().ToString();
        string r_count = SendToGoogle.getrCount().ToString();
        string pass_levels = SendToGoogle.getPlayerPassLevels().ToString();
        string fail_levels = SendToGoogle.getPlayerFailLevels().ToString();
        StartCoroutine(Post(sid, c_count,r_count,pass_levels, fail_levels));


    }



    public IEnumerator Post(string sessionID, string cameraCount, string rCount, string passLevels, string failLevels)
    {
        
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        form.AddField("entry.1045228917", sessionID);
        form.AddField("entry.2000942908", cameraCount);
        form.AddField("entry.159052367", rCount);
        form.AddField("entry.1717970724", passLevels);
        form.AddField("entry.1476162569", failLevels);
       

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
void OnApplicationQuit()
    {
        // Send data to google forms.
        if (SendToGoogle.prevSessionID != SendToGoogle.getSessionId())
        {
            Send();
            SendToGoogle.prevSessionID = SendToGoogle.getSessionId();
            SendToGoogle.resetParameters();
        }

    }




}
