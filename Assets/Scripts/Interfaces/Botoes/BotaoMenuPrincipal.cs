using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BotaoMenuPrincipal : MonoBehaviour {
//Tutorial: [Unity3d Episode 7] Menu Function, Pause, Quit Game, Main Menu - https://www.youtube.com/watch?v=RfJz7ROCfeY

	public Button menuPrincipal;

	// Use this for initialization
	void Start () {
		Button botao = menuPrincipal.GetComponent<Button>();	//Pega o componente botão e põe na variável botao
		botao.onClick.AddListener(chamarMenuPrincipal);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void chamarMenuPrincipal(){
		SceneManager.LoadScene("1-MenuPrincipal");	//Chama a cena do menu principal - https://forum.unity.com/threads/scenemanager-loadscene-or-application-loadlevel.495483/
		//Application.LoadLevel(SceneMenuPrincipal);	
	}
}
