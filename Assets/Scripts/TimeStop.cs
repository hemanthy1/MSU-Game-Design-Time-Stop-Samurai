using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStop : MonoBehaviour
{

    public KeyCode timeStopKey;

    public GameObject timePostProcess;

    public GameObject postProcess;

    public bool timeStopped=false;




    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(timeStopKey))
        {
   
            timeStopped = !timeStopped;
            
            if(timeStopped)
            {
                timePostProcess.SetActive(true);
                postProcess.SetActive(false);
            }
            else
            {
                timePostProcess.SetActive(false);
                postProcess.SetActive(true);
            }
        }
            
    
    }
}
