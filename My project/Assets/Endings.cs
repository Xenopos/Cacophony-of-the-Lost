using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Player;
using UnityEngine.SceneManagement;

public class Endings : MonoBehaviour
{
    public VideoPlayer Badending;
    public VideoPlayer Goodending;

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

                StartCoroutine(WaitforVideoEnd4());
                Badending.enabled = true;
            }

            else if(PlayerStateManager.sanityLevel > 50)
            {

                StartCoroutine(WaitforVideoEnd6());
                Goodending.enabled = true;
            }

    }

    IEnumerator WaitforVideoEnd6()
    {
        float videolength2 = 192f;
        yield return new WaitForSeconds(videolength2);
        SceneManager.LoadScene("MainMenu");
    }
        IEnumerator WaitforVideoEnd4()
    {
        float videolength3 = 185f;
        yield return new WaitForSeconds(videolength3);
        SceneManager.LoadScene("MainMenu");
    }

}
