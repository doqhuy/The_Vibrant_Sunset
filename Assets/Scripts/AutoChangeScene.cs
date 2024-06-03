using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoChangeScene : MonoBehaviour
{
    public float second;
    public string sceneName;
    void Start()
    {
        StartCoroutine(WaitForAction());
    }

    IEnumerator WaitForAction()
    {
        yield return new WaitForSeconds(second);
        SceneManager.LoadScene(sceneName);
    }

}
