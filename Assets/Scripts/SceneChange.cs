using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    public string SceneName;
    void Start()
    {
        Button thisBTN = GetComponent<Button>();
        thisBTN.onClick.RemoveAllListeners();
        thisBTN.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneName);
        });
    }
}
