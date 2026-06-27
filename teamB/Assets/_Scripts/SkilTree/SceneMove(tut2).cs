using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMovetut2 : MonoBehaviour
{
    public static bool ButtonONOFF = false;
    public GameObject tut2;
    public static Button button;

    public void Update()
    {
        if (SceneMovetut1.ButtonONOFF)
        {
            button.interactable = true;
        }

    }
    public void Change_Scene()
    {
        if (SceneMovetut1.ButtonONOFF)
        {
            SceneManager.LoadScene("tut3");
            Destroy(tut2);
            ButtonONOFF = true;
        }
    }
}
