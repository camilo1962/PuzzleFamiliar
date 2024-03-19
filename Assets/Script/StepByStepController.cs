using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepByStepController : MonoBehaviour
{
    public int row, col;
    GameController gameMN;
    public GameObject piezaSeleccionada;

    void Start()
    {
        GameObject gameManager = GameObject.Find("GameController");
        gameMN = gameManager.GetComponent<GameController>();
    }

    
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        //    if (hit.transform.CompareTag("Puzzle"))
        //    {
        //        piezaSeleccionada = hit.transform.gameObject;
        //        Debug.Log("Es la Fila :" + row + " Es la Columna :" + col);
        //        gameMN.countStep += 1;
        //        gameMN.row = row;
        //        gameMN.col = col;
        //        gameMN.startControl = true;
        //    }
        //}
    }
    void OnMouseDown() //onTouchEnd  OnMouseDown
    {
        Debug.Log("Es la Fila :" + row + " Es la Columna :" + col);
        gameMN.countStep += 1;
        gameMN.row = row;
        gameMN.col = col;
        gameMN.startControl = true;
    }
}
