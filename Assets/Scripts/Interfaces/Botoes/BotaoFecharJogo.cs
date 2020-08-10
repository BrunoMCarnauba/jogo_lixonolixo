using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotaoFecharJogo : MonoBehaviour {
//Tutorial: [Unity3d Episode 7] Menu Function, Pause, Quit Game, Main Menu - https://www.youtube.com/watch?v=RfJz7ROCfeY

	public Button fecharJogo;

	// Use this for initialization
	void Start () {
		Button botao = fecharJogo.GetComponent<Button>();	//Pega o componente botão e põe na variável botao
		botao.onClick.AddListener(exitGame);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void exitGame(){
		Application.Quit();	//Fecha a aplicação
	}
}
