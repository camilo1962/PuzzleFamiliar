using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagenController : MonoBehaviour
{
    public GameObject target;
    public bool startMove = false;
    GameController gameMN;

    void Start()
    {
        GameObject gameManager = GameObject.Find("GameController");
        gameMN = gameManager.GetComponent<GameController>();
    }

    void Update()
    {
        if (startMove)
        {
            startMove = false;
            this.transform.position = target.transform.position;
            gameMN.checkComplete = true;
        }
    }
}
