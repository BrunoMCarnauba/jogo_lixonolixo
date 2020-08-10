using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueInimigo : MonoBehaviour {
	//[Unity3d Episode 5] Knockback, Health and Damage: https://youtu.be/lGUPG7smpXo

	public int danoAtaque = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider quemColidiu){	//Quando entrar na trigger (BoxCollider que está com isTrigger ativado), quer dizer que colidiu
		if(quemColidiu.gameObject.tag == "Player"){	//Se foi o jogador que colidiu
			//Debug.Log("DirecaoDano = "+quemColidiu.transform.position+" - "+transform.position);	//Para teste
			Vector3 direcaoDano = quemColidiu.transform.position - transform.position;	//De acordo com o sinal dos eixos (- ou +), nós conseguimos saber de qual direção veio o dano. Exemplo: Quanto mais pra direita, maior é o X. Então se o jogador tava na esquerda e levou um dano da direita, o X nessa subtração (maior - menor) vai ser negativo.
			//Debug.Log("Nao normalizado = "+direcaoDano);	//Para teste
			direcaoDano = direcaoDano.normalized;
			//Debug.Log("Normalizado = "+direcaoDano);	//Para teste
	
			FindObjectOfType<VidaJogador>().tirarVida(danoAtaque, direcaoDano);
		}
	}

}
