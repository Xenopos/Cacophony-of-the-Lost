using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Player;
using UnityEngine.SceneManagement;

public class Sanity_Cutscene : MonoBehaviour
{
    public VideoPlayer LowSan;
    public VideoPlayer HighSan;

    void Start()
    {

        end();
    }

    void Update()
    {
        
    }

    public void end()
    {
            if(PlayerStateManager.sanityLevel <= 50)
            {

                StartCoroutine(WaitforVideoEnd3());
                LowSan.enabled = true;
            }

            else if(PlayerStateManager.sanityLevel > 50)
            {

                StartCoroutine(WaitforVideoEnd1());
                HighSan.enabled = true;
            }

    }

    IEnumerator WaitforVideoEnd1()
    {
        float videolength = 14f;
        yield return new WaitForSeconds(videolength);
        SceneManager.LoadScene("Alingal");
    }
        IEnumerator WaitforVideoEnd3()
    {
        float videolengthr = 44f;
        yield return new WaitForSeconds(videolengthr);
        SceneManager.LoadScene("Alingalalter");
    }

}
