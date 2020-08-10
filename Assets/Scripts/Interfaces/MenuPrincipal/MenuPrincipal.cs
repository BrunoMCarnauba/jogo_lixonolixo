using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;	//Para poder manipular componentes de interfaces, como o botão.
using UnityEngine.SceneManagement;	//Para poder manipular as cenas, como usar o sceneManager para trocar de cena.

public class MenuPrincipal : MonoBehaviour {
//Tutorial: [Unity3d Episode 6] Pause script and GUI - https://www.youtube.com/watch?v=C4DVNM9SM8I
//Tutorial: [Unity3d Episode 11] Game Over Music, Switch Scene Once All Items Are Collected - https://youtu.be/7ySxbl_yuL0

	//Componentes da tela do menu principal
	public Transform menuPrincipal;	//Conterá o componente onde tem dentro os botões e texto.
	public Button botaoIniciarJogo;
	public Button botaoAjuda;
	//Componentes da tela do menu de ajuda
	public Transform menuAjuda;
	public Button botaoAjudaIniciarJogo;	//Da tela de ajuda
	public Button botaoVoltarMenuPrincipal;	//Botão que está na tela de ajuda para voltar para o menu principal.


	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		
		Button btnIniciarJogo = botaoIniciarJogo.GetComponent<Button>();
		btnIniciarJogo.onClick.AddListener(iniciarJogo);	//Quando for clicado, chama o método iniciarJogo();

		Button btnAjuda = botaoAjuda.GetComponent<Button>();
		btnAjuda.onClick.AddListener(mostrarAjuda);

		Button btnAjudaIniciarJogo = botaoAjudaIniciarJogo.GetComponent<Button>();
		btnAjudaIniciarJogo.onClick.AddListener(iniciarJogo);

		Button btnVoltarMenuPrincipal = botaoVoltarMenuPrincipal.GetComponent<Button>();
		btnVoltarMenuPrincipal.onClick.AddListener(voltarMenuPrincipal);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void iniciarJogo(){
		SceneManager.LoadScene("Level1");
	}

	public void mostrarAjuda(){
		if(menuPrincipal.gameObject.activeInHierarchy == true){
			menuPrincipal.gameObject.SetActive(false);
			menuAjuda.gameObject.SetActive(true);
		}
	}

	public void voltarMenuPrincipal(){	//Voltar do menu de ajuda para o menu principal
		if(menuAjuda.gameObject.activeInHierarchy == true){
			menuAjuda.gameObject.SetActive(false);
			menuPrincipal.gameObject.SetActive(true);
		}
	}
}
