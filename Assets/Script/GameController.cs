using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public enum spritesUIAudio
{
    activarSonido,
    desactivarSonido,
    activarMusica,
    desactivarMusica
}

public class GameController : MonoBehaviour
{
      

   

    public int level;
    public int row, col, countStep;
    public int rowBlank, colBlank;
    public int numeroRow, numeroCol;
    int contarPuntos = 0;
    int contarImagenKey = 0;
    int countComplete;
    
    public float timer = 0f;
    //public TMP_Text timerTexto;
    private int minutos, seconds, cents;
    private bool desactivarSonido = false;
    private Image btnSonidoMovimientosImg;
    public List<Sprite> sonidoMusicaUI;
    private bool desplegarMusica = true;
    private Image btnMusicaImg;
    private int cantMovimientos = 0;
    [SerializeField]
    public int nivelNumero;
   
    


    [HideInInspector]
    public bool finJuego = false;
    public TMP_Text tiempo;
    public TMP_Text movimientos;
    
    // Use this for initialization
    private bool desplegarAjustes = false;
    DateTime inicialTiempo, finTiempo;
    TimeSpan dif;
    int segundos, minut;
    string segundosCad, minutosCad;
  
    public TMP_Text movimientosFinal;
    public TMP_Text tiempoFinal;
    public GameObject panelCompleto;
    public GameObject panelMenu;
    public bool startControl = false;
    public bool checkComplete;
    public bool gameIsComplete;

    GameObject temp;


    public List<GameObject> imagenKeyList;
    public List<GameObject> imagenOfPictureList;
    public List<GameObject> checkPointList;

    GameObject[,] imagenKeyMatrix;
    GameObject[,] imagenOfPictureMatrix;
    GameObject[,] checkPointMatrix;

    private void Awake()
    {
           
        this.btnMusicaImg = GameObject.FindGameObjectWithTag("btnMusica").GetComponent<Image>();
        this.btnSonidoMovimientosImg = GameObject.FindGameObjectWithTag("btnSonidoMovimiento").GetComponent<Image>();
        this.panelMenu.SetActive(false);
    }
    void Start()
    {
        
        
        this.movimientos.text = " 0";
        this.inicialTiempo = DateTime.Now;
        panelCompleto.SetActive(false);
        imagenKeyMatrix = new GameObject[numeroRow, numeroCol];
        imagenOfPictureMatrix = new GameObject[numeroRow, numeroCol];
        checkPointMatrix = new GameObject[numeroRow, numeroCol];
        if (level == 1)
        {
            ImagenOfEasyLevel();
        }
        else if (level == 2)
        {
            ImagenOfNomalLevel();
        }
        else if (level == 3)
        {
            ImagenOfHardLevel();
        }
        CheckPointManager();
        ImagenKeyManager();
        for (int r = 0; r < numeroRow; r++)
        {
            for (int c = 0; c < numeroCol; c++)
            {
                if (imagenOfPictureMatrix[r, c].name.CompareTo("blank") == 0)
                {
                    rowBlank = r;
                    colBlank = c;
                    break;
                }

            }
        }
    }

    void CheckPointManager()
    {
        for (int r = 0; r < numeroRow; r++)
        {
            for (int c = 0; c < numeroCol; c++)
            {
                checkPointMatrix[r, c] = checkPointList[contarPuntos];
                contarPuntos++;
            }
        }
    }

    void ImagenKeyManager()
    {
        for (int r = 0; r < numeroRow; r++)
        {
            for (int c = 0; c < numeroCol; c++)
            {
                imagenKeyMatrix[r, c] = imagenKeyList[contarImagenKey];
                contarImagenKey++;
            }
        }
    }



    void Update()
    {
        if (!this.finJuego)
        {
            //obteniendo tiempo en la partida
            this.finTiempo = DateTime.Now;
            dif = this.finTiempo - this.inicialTiempo;
            this.segundos = (int)dif.Seconds;
            this.minutos = (int)dif.TotalMinutes;

            //actualizando y dandole formato al texto que muestra el tiempo
            this.segundosCad = ((this.segundos > 9) ? this.segundos.ToString() : "0" + this.segundos);
            this.minutosCad = ((this.minutos > 9) ? this.minutos.ToString() : "0" + this.minutos);
            this.tiempo.text = this.minutosCad + ":" + this.segundosCad;
        }



        if (startControl)
        {
            startControl = false;
            if (countStep == 1)
            {
                if (imagenOfPictureMatrix[row, col] != null && imagenOfPictureMatrix[row, col].name.CompareTo("blank") != 0)
                {
                    if (rowBlank != row && colBlank == col)
                    {
                        if (Mathf.Abs(row - rowBlank) == 1)
                        {
                            SortImage();
                            countStep = 0;
                        }
                        else
                        {
                            countStep = 0;
                        }

                    }
                    else if (rowBlank == row && colBlank != col)
                    {
                        if (Mathf.Abs(col - colBlank) == 1)
                        {
                            SortImage();
                            countStep = 0;
                        }
                        else
                        {
                            countStep = 0;
                        }

                    }
                    else if ((rowBlank == row && colBlank == col) || (rowBlank != row && colBlank != col))
                    {
                        countStep = 0;
                    }
                }
                else
                {
                    countStep = 0;
                }
            }
            this.cantMovimientos++;
            this.movimientos.text = (this.cantMovimientos > 9) ? this.cantMovimientos.ToString() : "" + this.cantMovimientos;

        }
       
    }

    void FixedUpdate()
    {
        if (checkComplete)
        {
            checkComplete = false;
            for (int r = 0; r < numeroRow; r++)
            {
                for (int c = 0; c < numeroCol; c++)
                {
                    if (imagenKeyMatrix[r, c].gameObject.name.CompareTo(imagenOfPictureMatrix[r, c].gameObject.name) == 0)
                    {
                        countComplete++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (countComplete == checkPointList.Count)
            {
                gameIsComplete = true;
                Debug.Log("Juego completado");
                this.finJuego = true;
                tiempoFinal.text = tiempo.text;
                movimientosFinal.text = movimientos.text;
                panelCompleto.SetActive(true);
                PlayerPrefs.SetInt("mins1", (int)this.dif.TotalMinutes);
                PlayerPrefs.SetInt("segs1", (int)this.dif.Seconds);
                PlayerPrefs.SetInt("movimientos1" ,(int)this.cantMovimientos);
                




            }
            else
            {
                countComplete = 0;
            }
           
        }

    }

    void SortImage()
    {
        temp = imagenOfPictureMatrix[rowBlank, colBlank];
        imagenOfPictureMatrix[rowBlank, colBlank] = null;

        imagenOfPictureMatrix[rowBlank, colBlank] = imagenOfPictureMatrix[row, col];
        imagenOfPictureMatrix[row, col] = null;

        imagenOfPictureMatrix[row, col] = temp;

        imagenOfPictureMatrix[rowBlank, colBlank].GetComponent<ImagenController>().target = checkPointMatrix[rowBlank, colBlank];
        imagenOfPictureMatrix[row, col].GetComponent<ImagenController>().target = checkPointMatrix[row, col];

        imagenOfPictureMatrix[rowBlank, colBlank].GetComponent<ImagenController>().startMove = true;
        imagenOfPictureMatrix[row, col].GetComponent<ImagenController>().startMove = true;

        rowBlank = row;
        colBlank = col;


    }

    void ImagenOfEasyLevel()
    {
        imagenOfPictureMatrix[0, 0] = imagenOfPictureList[0];
        imagenOfPictureMatrix[0, 1] = imagenOfPictureList[2];
        imagenOfPictureMatrix[0, 2] = imagenOfPictureList[5];
        imagenOfPictureMatrix[1, 0] = imagenOfPictureList[4];
        imagenOfPictureMatrix[1, 1] = imagenOfPictureList[1];
        imagenOfPictureMatrix[1, 2] = imagenOfPictureList[7];
        imagenOfPictureMatrix[2, 0] = imagenOfPictureList[3];
        imagenOfPictureMatrix[2, 1] = imagenOfPictureList[6];
        imagenOfPictureMatrix[2, 2] = imagenOfPictureList[8];
    }
    void ImagenOfNomalLevel()
    {
        imagenOfPictureMatrix[0, 0] = imagenOfPictureList[4];
        imagenOfPictureMatrix[0, 1] = imagenOfPictureList[0];
        imagenOfPictureMatrix[0, 2] = imagenOfPictureList[1];
        imagenOfPictureMatrix[0, 3] = imagenOfPictureList[2];
        imagenOfPictureMatrix[1, 0] = imagenOfPictureList[8];
        imagenOfPictureMatrix[1, 1] = imagenOfPictureList[6];
        imagenOfPictureMatrix[1, 2] = imagenOfPictureList[7];
        imagenOfPictureMatrix[1, 3] = imagenOfPictureList[11];
        imagenOfPictureMatrix[2, 0] = imagenOfPictureList[12];
        imagenOfPictureMatrix[2, 1] = imagenOfPictureList[5];
        imagenOfPictureMatrix[2, 2] = imagenOfPictureList[14];
        imagenOfPictureMatrix[2, 3] = imagenOfPictureList[10];
        imagenOfPictureMatrix[3, 0] = imagenOfPictureList[13];
        imagenOfPictureMatrix[3, 1] = imagenOfPictureList[9];
        imagenOfPictureMatrix[3, 2] = imagenOfPictureList[15];
        imagenOfPictureMatrix[3, 3] = imagenOfPictureList[3];
    }
    void ImagenOfHardLevel()
    {
        imagenOfPictureMatrix[0, 0] = imagenOfPictureList[5];
        imagenOfPictureMatrix[0, 1] = imagenOfPictureList[2];
        imagenOfPictureMatrix[0, 2] = imagenOfPictureList[3];
        imagenOfPictureMatrix[0, 3] = imagenOfPictureList[4];
        imagenOfPictureMatrix[0, 4] = imagenOfPictureList[9];
        imagenOfPictureMatrix[1, 0] = imagenOfPictureList[10];
        imagenOfPictureMatrix[1, 1] = imagenOfPictureList[1];
        imagenOfPictureMatrix[1, 2] = imagenOfPictureList[12];
        imagenOfPictureMatrix[1, 3] = imagenOfPictureList[7];
        imagenOfPictureMatrix[1, 4] = imagenOfPictureList[8];
        imagenOfPictureMatrix[2, 0] = imagenOfPictureList[15];
        imagenOfPictureMatrix[2, 1] = imagenOfPictureList[6];
        imagenOfPictureMatrix[2, 2] = imagenOfPictureList[13];
        imagenOfPictureMatrix[2, 3] = imagenOfPictureList[14];
        imagenOfPictureMatrix[2, 4] = imagenOfPictureList[19];
        imagenOfPictureMatrix[3, 0] = imagenOfPictureList[20];
        imagenOfPictureMatrix[3, 1] = imagenOfPictureList[11];
        imagenOfPictureMatrix[3, 2] = imagenOfPictureList[22];
        imagenOfPictureMatrix[3, 3] = imagenOfPictureList[17];
        imagenOfPictureMatrix[3, 4] = imagenOfPictureList[18];
        imagenOfPictureMatrix[4, 0] = imagenOfPictureList[21];
        imagenOfPictureMatrix[4, 1] = imagenOfPictureList[16];
        imagenOfPictureMatrix[4, 2] = imagenOfPictureList[23];
        imagenOfPictureMatrix[4, 3] = imagenOfPictureList[24];
        imagenOfPictureMatrix[4, 4] = imagenOfPictureList[0];

    }

    public void desplegarAjustesPanel()
    {
        this.desplegarAjustes = !this.desplegarAjustes;
        this.panelMenu.SetActive(this.desplegarAjustes);
    }
    public void establecerSonido()
    {

        this.desactivarSonido = !this.desactivarSonido;
        if (this.desactivarSonido)
        {
            this.btnSonidoMovimientosImg.sprite = this.sonidoMusicaUI[(int)spritesUIAudio.desactivarSonido];
            PlayerPrefs.SetInt("sonido", 2);//sonido desactivado
        }
        else
        {
            this.btnSonidoMovimientosImg.sprite = this.sonidoMusicaUI[(int)spritesUIAudio.activarSonido];
            PlayerPrefs.SetInt("sonido", 1);//sonido activo
        }
    }
    public void establecerMusica()
    {
        this.desplegarMusica = !this.desplegarMusica;

        if (this.desplegarMusica)
        {
            //this.musicaFondo.Play();
            this.btnMusicaImg.sprite = this.sonidoMusicaUI[(int)spritesUIAudio.activarMusica];
            PlayerPrefs.SetInt("musica", 1);//musica activa
        }
        else
        {
            //this.musicaFondo.Stop();
            this.btnMusicaImg.sprite = this.sonidoMusicaUI[(int)spritesUIAudio.desactivarMusica];
            PlayerPrefs.SetInt("musica", 2);//musica desactivada
        }



    }

    
}

