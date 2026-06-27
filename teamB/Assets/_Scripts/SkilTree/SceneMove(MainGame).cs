using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMoveMainGame : MonoBehaviour
{
    public void Change_Scene()
    {
        SceneManager.LoadScene("MainGame");
    }
}
