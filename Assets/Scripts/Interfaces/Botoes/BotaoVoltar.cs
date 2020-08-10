using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotaoVoltar : MonoBehaviour {
//Tutorial: [Unity3d Episode 7] Menu Function, Pause, Quit Game, Main Menu - https://www.youtube.com/watch?v=RfJz7ROCfeY

	public Button voltar;

	// Use this for initialization
	void Start () {
		Button botao = voltar.GetComponent<Button>();	//Pega o componente botão e põe na variável botao
		botao.onClick.AddListener(despausar);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void despausar(){
		FindObjectOfType<GameManager>().pausarJogo();	//Encontra o script GameManager e chama o método pausarJogo()
	}
}
