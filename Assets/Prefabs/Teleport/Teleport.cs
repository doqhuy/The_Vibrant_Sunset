using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public string TeleportName;
    public string TeleportScene;
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        animator.Play(TeleportName);
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            SceneManager.LoadScene(TeleportScene);
        }
    }
}
