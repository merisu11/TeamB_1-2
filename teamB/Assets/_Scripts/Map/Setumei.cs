using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Setumei : MonoBehaviour
{

    public float waitTime = 4.0f;

    //移動先のシーン名
    public string nextSceneName = "Tyutoriaru1";

    void Start()
    {
        StartCoroutine(ChangeSceneAfterTime());

    }

    IEnumerator ChangeSceneAfterTime()
    {
        //4秒待つ
        yield return new WaitForSeconds(waitTime);

        //指定したゲームシーン
        SceneManager.LoadScene(nextSceneName);
    }
}

