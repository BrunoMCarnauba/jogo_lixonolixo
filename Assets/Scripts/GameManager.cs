using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {
//Tutorial: [Unity3d Episode 6] Pause script and GUI - https://www.youtube.com/watch?v=C4DVNM9SM8I
//Tutorial: [Unity3d Episode 11] Game Over Music, Switch Scene Once All Items Are Collected - https://youtu.be/7ySxbl_yuL0
//How to Fade Between Scenes in Unity: https://youtu.be/Oadq-IrOazg

	private GameManager gameManager;
	public Transform menuPause;	//Conterá a interface do menu de pause
	public Transform camera;


	public Transform menuGameOver;	//Conterá a interface de gameOver
	public bool gameOver = false;
	public float gameOverDelay = 3f;	//Tempo para aparecer a tela de gameOver

	public Transform menuVitoria;
	public bool vitoria = false;

	public int inimigosEmJogo = 0;
	public int maximoInimigo = 5;

	// Use this for initialization
	void Start () {
		inimigosEmJogo = GameObject.FindGameObjectsWithTag("Inimigo").Length;	//Atribui inimigos que já começaram na cena. - https://answers.unity.com/questions/35825/count-the-number-of-objects-with-a-certain-tag.html
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			pausarJogo();
		}
	}

	public bool podeSpawnarInimigo(){
		if(inimigosEmJogo < maximoInimigo){
		return true;	}
		else{
		return false;	}
	}

	public void pausarJogo(){
		if(menuPause.gameObject.activeInHierarchy == false){	//Se o menu não estiver desativado
			menuPause.gameObject.SetActive(true);	//Ativa/Mostra o menu
			Time.timeScale = 0;	//Para o jogo
			AudioListener.pause = true;	//Para os áudios do jogo
			//camera.GetComponent<AdvanceCamera>().enabled = false;	//Desativa o script AdvanceCamera da camera
		} else {	//Se já estiver ativo
			menuPause.gameObject.SetActive(false);	//Desativa/Esconde o menu
			Time.timeScale = 1;	//Despausa o jogo
			AudioListener.pause = false;	//Despausa os áudios do jogo
			//camera.GetComponent<AdvanceCamera>().enabled = true;	//Reativa o script AdvanceCamera da camera
		}
	}

	public void perdeu(){
		if(gameOver == false){
			gameOver = true;
			//Invoke("gameOverGUI", gameOverDelay);	//O que é o Invoke: https://youtu.be/sLYcVjWG2O8 - Ele chamará o método gameOverGUI depois de tantos segundos
			StartCoroutine(gameOverGUI(gameOverDelay));	//O método gameOverGUI só vai ser chamado quando tiver passado o gameOverDelay.
			FindObjectOfType<SoundEffects>().tocarGameOverSong();	//Encontra o script SoundEffects e exeuta o método tocarGameOverSong dele.
			//camera.GetComponent<AdvanceCamera>().enabled = false;	//Desativa o script AdvanceCamera da camera
			Time.timeScale = 0;	//Pausa o jogo (Animações, etc...)
		}
	}

	private IEnumerator gameOverGUI(float seconds)	//https://forum.unity.com/threads/invoke-and-time-timescale.82380/. Foi usado isso ao invés de Invoke, porque com ele o método não estava sendo chamado por causa do Time.timeScale = 0.
	{
		yield return new WaitForSecondsRealtime(seconds);
		gameOverGUI();	//Só executa esse método depois de ter passado os segundos do parâmetro.
	}

	private void gameOverGUI(){
		if(menuGameOver.gameObject.activeInHierarchy == false){	//Se o objeto não estiver ativo na cena
			menuGameOver.gameObject.SetActive(true);	//Ativa/Mostra o menu
		}
	}

	public void renasceu(){
		//camera.GetComponent<AdvanceCamera>().enabled = true;	//Reativa o script AdvanceCamera da camera
		FindObjectOfType<SoundEffects>().tocarLevelMusic();	//Encontra o script SoundEffects e exeuta o método tocarLevelMusic dele.
		Time.timeScale = 1;	//Despause o jogo
		gameOver = false;
	}

	public void ganhou(){
		if(vitoria == false){
			vitoria = true;
			if(menuVitoria.gameObject.activeInHierarchy == false){	//Se o objeto não estiver ativo na cena
				menuVitoria.gameObject.SetActive(true);	//Ativa/Mostra o menu
			}
		}
	}
}
