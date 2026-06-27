using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMovetut1 : MonoBehaviour
{
    public GameObject tut1;
    public static bool ButtonONOFF = false;
    public void Change_Scene()
    {
        SceneManager.LoadScene("tut2");
        Destroy(tut1);
        ButtonONOFF = true;
    }
}
