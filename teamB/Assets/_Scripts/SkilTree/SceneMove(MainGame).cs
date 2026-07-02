using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMoveMainGame : MonoBehaviour
{
    public static Button button;
    public static bool tut1ONOFF = false;
    public static bool tut2ONOFF = false;

    public void Change_Scene()
    {
        StartCoroutine(ChangeSceneDelay());
    }

    IEnumerator ChangeSceneDelay()
    {
        yield return new WaitForSeconds(0.2f);

        if (tut1ONOFF)
        {
            if (tut2ONOFF)
            {
                SceneManager.LoadScene("MainGame");
            }
            else
            {
                SceneManager.LoadScene("tut3");
                tut2ONOFF = true;
            }
        }
        else
        {
            SceneManager.LoadScene("tut2");
            tut1ONOFF = true;
        }
    }
}
