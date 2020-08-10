using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaJogador : MonoBehaviour {
	//[Unity3d Episode 5] Knockback, Health and Damage: https://www.youtube.com/watch?v=lGUPG7smpXo
	//[Unity3d Episode 11] Game Over Music, Switch Scene Once All Items Are Collected: https://youtu.be/7ySxbl_yuL0
	public int vidaMaxima = 100;
	public int vidaAtual;
	public int forcaEmpurro;

	//Para gameOver e respawn:
	public bool vivo = true;	//Saberá se está vivo ou não
	// //public Transform jogador;	//Conterá o objeto do Jogador, para caso quiser usá-lo para colocar para o jogador desaparecer quando perder
	// public Transform respawnTransform;	//Conterá o objeto que está no local onde será respawnado o personagem. Será pego a posição desse objeto e colocado na variável respawnLocation.
	// private Vector3 respawnLocation;	//Vai armazenar a posição Vector3 do respawnTarget
	// private bool respawning; 

	private GUIStyle guiStyle = new GUIStyle();	//Para ser feita uma GUIStyle personalizada
	// Use this for initialization
	void Start () {
		vidaAtual = vidaMaxima;

		// respawnLocation = respawnTransform.transform.position;	//Pega a posição do objeto respawnTarget
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void tirarVida(int danoSofrido, Vector3 direcaoDano){	//Chamado pela script AtaqueInimigo do inimigo
		vidaAtual = vidaAtual - danoSofrido;
		FindObjectOfType<SoundEffects>().tocarDanoPlayer();	//Encontra o script SoundEffects e executa o método tocarEliminaInimigo dele.
		
		if(vidaAtual <= 0){
			vivo = false;
			perdeu();
		} else{
			empurrarParaTras(direcaoDano);
		}
	}

	private void empurrarParaTras(Vector3 direcaoEmpurro){	//O jogador é empurrado para trás ao levar dano
		if(GetComponent<Rigidbody>().velocity.magnitude < 5){	//Pra evitar que o rato por ter acertado o personagem em pouco tempo, não seja adicionado mais força que o esperado e ocorra que o jogador seja empurrado com muita força. - https://answers.unity.com/questions/323400/reading-dorce-on-rigidbody.html
			direcaoEmpurro.y = 0.4f;	//Para que a direção no eixo Y seja sempre a mesma (o persoagem sempre dê um pulinho)
			GetComponent<Rigidbody>().AddForce(direcaoEmpurro * forcaEmpurro * 100);
		}
	}

	private void OnGUI(){	//OnGUI é chamado automaticamente (Precisa estar exatamente com esse nome, com as letras maiúsculas e minúsculas)
		guiStyle.fontSize = 30;
		guiStyle.normal.textColor = Color.red;

		GUI.contentColor = Color.red;
		GUI.Label(new Rect(40,20,0,0), "Vida: "+vidaAtual, guiStyle);	//Rect é um retângulo 2D, definido com X,Y,Largura e Altura. Largura e altura estão zerados pois está sendo usado somente o tamanho da fonte. O guiStyle no final é o modelo de GuiStyle que criamos acima.
	}

	public void perdeu(){
		if(vivo == false){
			FindObjectOfType<GameManager>().perdeu();
			//jogador.gameObject.SetActive(false);	//Se quiser que o jogador desapareça quando perder
		}
	}

	public void renascer(){
		vivo = true;
		vidaAtual = vidaMaxima;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);	//Restarta a cena ativa - https://answers.unity.com/questions/802253/how-to-restart-scene-properly.html
		//this.transform.position = respawnLocation;
	}
}
