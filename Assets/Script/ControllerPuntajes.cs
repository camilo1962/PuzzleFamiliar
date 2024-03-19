using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPuntajes : MonoBehaviour {

	float puntosFacil = 0f;
	float puntosModerado = 0f;
	float puntosDificil = 0f;
	float puntajeTotal = 0f;

	int valueAnterior;
	public Text txtPuntosSubTotal;
	public Text txtPuntosTotal;
	
	Dropdown desplegableNivel;
	guardarCargarRecords cargarDatos;
	List<DatosGuardar> datosList;

	GameObject panelFacil;
	GameObject panelModerado;
	GameObject panelDificil;

    [SerializeField]
    private Text recordText1;
    [SerializeField]
    private Text recordText2;
    [SerializeField]
    private Text recordText3;
    [SerializeField]
    private Text recordText4;
    [SerializeField]
    private Text recordText5;
    [SerializeField]
    private Text recordText6;
    [SerializeField]
    private Text recordText7;
    [SerializeField]
    private Text recordText8;
    [SerializeField]
    private Text recordText9;
    [SerializeField]
    private Text recordText10;
    [SerializeField]
    private Text recordText11;
    [SerializeField]
    private Text recordText12;
    [SerializeField]
    private Text recordText13;
    [SerializeField]
    private Text recordText14;
    [SerializeField]
    private Text recordText15;
    [SerializeField]
    private Text recordText16;
    [SerializeField]
    private Text recordText17;
    [SerializeField]
    private Text recordText18;
    [SerializeField]
    private Text recordText19;
    [SerializeField]
    private Text recordText20;
    [SerializeField]
    private Text recordText21;
    [SerializeField]
    private Text recordText22;
    [SerializeField]
    private Text recordText23;
    [SerializeField]
    private Text recordText24;
    [SerializeField]
    private Text recordText25;
    [SerializeField]
    private Text recordText26;
    [SerializeField]
    private Text recordText27;
    [SerializeField]
    private Text recordText28;
    [SerializeField]
    private Text recordText29;
    [SerializeField]
    private Text recordText30;
    [SerializeField]
    private Text recordText31;
    [SerializeField]
    private Text recordText32;
    [SerializeField]
    private Text recordText33;
    [SerializeField]
    private Text recordText34;
    [SerializeField]
    private Text recordText35;
    [SerializeField]
    private Text recordText36;
    [SerializeField]
    private Text recordText37;
    [SerializeField]
    private Text recordText38;
    [SerializeField]
    private Text recordText39;
    [SerializeField]
    private Text recordText40;
    [SerializeField]
    private Text recordText41;
    [SerializeField]
    private Text recordText42;
    [SerializeField]
    private Text recordText43;
    [SerializeField]
    private Text recordText44;
    [SerializeField]
    private Text recordText45;
    


    private void Awake() {
		int nveces = PlayerPrefs.GetInt("nPuntajes");
		PlayerPrefs.SetInt("nPuntajes", 1+nveces);
		
		this.desplegableNivel = GameObject.FindGameObjectWithTag("desplegablePuntos").GetComponent<Dropdown>();
		this.cargarDatos = new guardarCargarRecords();

		//obteniendo referencia a los paneles
		this.panelFacil = GameObject.FindGameObjectWithTag("panelFacil");
		this.panelModerado = GameObject.FindGameObjectWithTag("panelModerado");
		this.panelDificil = GameObject.FindGameObjectWithTag("panelDificil");

      
	}

	// Use this for initialization
	public void Start ()
    {
        //this.ActualizarRecords();
        
        //iniciamos las puntuaciones de cada nivel 
        this.inicializarPuntaje(this.cargarDatos.FacilArchivo,ref this.puntosFacil);
		this.inicializarPuntaje(this.cargarDatos.ModeradoArchivo,ref this.puntosModerado);
		this.inicializarPuntaje(this.cargarDatos.DificilArchivo,ref this.puntosDificil);

		//inicializamos la puntuacion total
		this.puntajeTotal = this.puntosFacil + this.puntosModerado + this.puntosDificil;
		this.txtPuntosTotal.text = "Total Puntos: "+this.puntajeTotal;

		//obtenemos el valor del value inicial del dropdown
		this.valueAnterior = this.desplegableNivel.value;
		this.txtPuntosSubTotal.text = "Puntos Fácil: "+this.puntosFacil;

		this.seleccionPanelActualizar();
       
    }
	
	// Update is called once per frame
	void Update () {
        //verificamos que el valor seleccionado por el usuario sea diferente del ya establecido, para entonces
        //si modificar el texo a mostrarle como subtotal
       
        if (this.valueAnterior != this.desplegableNivel.value){

			//modificamos el valor del texto que muestra el subtotal de puntos, dependiendo la opcion
			//seleccionada por el usuario
			if(this.desplegableNivel.value == (int)buildIndexScenas.facil-1){
				this.txtPuntosSubTotal.text = "Puntos Fácil: "+this.puntosFacil;

			}else if(this.desplegableNivel.value == (int)buildIndexScenas.moderado-1){
				this.txtPuntosSubTotal.text = "Puntos Moderado: "+this.puntosModerado;
				
			}else if(this.desplegableNivel.value == (int)buildIndexScenas.dificil-1){
				this.txtPuntosSubTotal.text = "Puntos Difícil: "+this.puntosDificil;
				
			}

			//actualizamos el valor de la ultima seleccion, que corresponderá a la actual
			this.valueAnterior = this.desplegableNivel.value;
		}
       
        

    }

	void inicializarPuntaje(string archivo,ref float puntaje){
		this.cargarDatos.nombreArchivoDatos = archivo;
		this.datosList = this.cargarDatos.cargarTodos();

		if(this.datosList != null){
			foreach (var item in datosList){
				puntaje += item.puntos;
			}
		}	
	}

	public void seleccionPanelActualizar(){
		//activamos o desactivamos los paneles que contienen los elementos con la representacion de la puntuacion
		//lo hacemos dependiendo la seleccion que se haya realizado por el usuario en el dropdown		
		if(this.desplegableNivel.value == (int)buildIndexScenas.facil-1){
			this.panelFacil.SetActive(true);
			this.panelModerado.SetActive(false);
			this.panelDificil.SetActive(false);
		}else if(this.desplegableNivel.value == (int)buildIndexScenas.moderado-1){
			this.panelModerado.SetActive(true);
			this.panelFacil.SetActive(false);
			this.panelDificil.SetActive(false);
		}else if(this.desplegableNivel.value == (int)buildIndexScenas.dificil-1){
			this.panelDificil.SetActive(true);
			this.panelModerado.SetActive(false);
			this.panelFacil.SetActive(false);
		}
	}

    public void ActualizarRecords()
    {
        recordText1.text = PlayerPrefs.GetInt("record" + 1).ToString();
        recordText2.text = PlayerPrefs.GetInt("movimientos1" + 2).ToString();
        recordText3.text = PlayerPrefs.GetInt("movimientos1" + 3).ToString();
        recordText4.text = PlayerPrefs.GetInt("movimientos1" + 4).ToString();
        recordText5.text = PlayerPrefs.GetInt("movimientos1" + 5).ToString();
        recordText6.text = PlayerPrefs.GetInt("movimientos1" + 6).ToString();
        recordText7.text = PlayerPrefs.GetInt("movimientos1" + 7).ToString();
        recordText8.text = PlayerPrefs.GetInt("movimientos1" + 8).ToString();
        recordText9.text = PlayerPrefs.GetInt("movimientos1" + 9).ToString();
        recordText10.text = PlayerPrefs.GetInt("movimientos1" + 10).ToString();
        recordText11.text = PlayerPrefs.GetInt("movimientos1" + 11).ToString();
        recordText12.text = PlayerPrefs.GetInt("movimientos1" + 12).ToString();
        recordText13.text = PlayerPrefs.GetInt("movimientos1" + 13).ToString();
        recordText14.text = PlayerPrefs.GetInt("movimientos1" + 14).ToString();
        recordText15.text = PlayerPrefs.GetInt("movimientos1" + 15).ToString();
        recordText16.text = PlayerPrefs.GetInt("movimientos1" + 16).ToString();
        recordText17.text = PlayerPrefs.GetInt("movimientos1" + 17).ToString();
        recordText18.text = PlayerPrefs.GetInt("movimientos1" + 18).ToString();
        recordText19.text = PlayerPrefs.GetInt("movimientos1" + 19).ToString();
        recordText20.text = PlayerPrefs.GetInt("movimientos1" + 20).ToString();
        recordText21.text = PlayerPrefs.GetInt("movimientos1" + 21).ToString();
        recordText22.text = PlayerPrefs.GetInt("movimientos1" + 22).ToString();
        recordText23.text = PlayerPrefs.GetInt("movimientos1" + 23).ToString();
        recordText24.text = PlayerPrefs.GetInt("movimientos1" + 24).ToString();
        recordText25.text = PlayerPrefs.GetInt("movimientos1" + 25).ToString();
        recordText26.text = PlayerPrefs.GetInt("movimientos1" + 26).ToString();
        recordText27.text = PlayerPrefs.GetInt("movimientos1" + 27).ToString();
        recordText28.text = PlayerPrefs.GetInt("movimientos1" + 28).ToString();
        recordText29.text = PlayerPrefs.GetInt("movimientos1" + 29).ToString();
        recordText30.text = PlayerPrefs.GetInt("movimientos1" + 30).ToString();
        recordText31.text = PlayerPrefs.GetInt("movimientos1" + 31).ToString();
        recordText32.text = PlayerPrefs.GetInt("movimientos1" + 32).ToString();
        recordText33.text = PlayerPrefs.GetInt("movimientos1" + 33).ToString();
        recordText34.text = PlayerPrefs.GetInt("movimientos1" + 34).ToString();
        recordText35.text = PlayerPrefs.GetInt("movimientos1" + 35).ToString();
        recordText36.text = PlayerPrefs.GetInt("movimientos1" + 36).ToString();
        recordText37.text = PlayerPrefs.GetInt("movimientos1" + 37).ToString();
        recordText38.text = PlayerPrefs.GetInt("movimientos1" + 38).ToString();
        recordText39.text = PlayerPrefs.GetInt("movimientos1" + 39).ToString();
        recordText40.text = PlayerPrefs.GetInt("movimientos1" + 40).ToString();
        recordText41.text = PlayerPrefs.GetInt("movimientos1" + 41).ToString();
        recordText42.text = PlayerPrefs.GetInt("movimientos1" + 42).ToString();
        recordText43.text = PlayerPrefs.GetInt("movimientos1" + 43).ToString();
        recordText44.text = PlayerPrefs.GetInt("movimientos1" + 44).ToString();
        recordText45.text = PlayerPrefs.GetInt("movimientos1" + 45).ToString();
        
    }
}
