using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour {
//Tutorial: [Unity3d Episode 6] Pause script and GUI - https://www.youtube.com/watch?v=C4DVNM9SM8I
//Tutorial: [Unity3d Episode 11] Game Over Music, Switch Scene Once All Items Are Collected - https://youtu.be/7ySxbl_yuL0

	public Button botaoRespawn;
	//public Transform jogador;

	// Use this for initialization
	void Start () {
		Button btnRespawn = botaoRespawn.GetComponent<Button>();
		btnRespawn.onClick.AddListener(respawnar);	//Quando clicado, chamará o método respawnar.
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void respawnar(){
		//jogador.gameObject.SetActive(true);	//Para caso o jogador tenha sumido, ele voltar a aparecer
		FindObjectOfType<VidaJogador>().renascer();
		FindObjectOfType<GameManager>().renasceu();
		gameObject.SetActive(false);	//Esconde esse menu
	}
}
