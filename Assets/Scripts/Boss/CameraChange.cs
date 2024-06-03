using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    private GameObject Player;
    public GameObject Boss;
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, Player.transform.position);
        if(distance < 3f)
        {
            Boss.SetActive(true);
            GameObject MainCameraObject = Player.transform.Find("Main Camera").gameObject;
            MainCameraObject.SetActive(false);
            GameObject BossCameraObject = Boss.transform.Find("Camera").gameObject;
            BossCameraObject.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
