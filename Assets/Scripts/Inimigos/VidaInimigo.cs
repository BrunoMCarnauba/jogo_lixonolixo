using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaInimigo : MonoBehaviour {

	//[Unity3d Episode 13] Enemy Mario death from jumping on top: https://www.youtube.com/watch?v=Es6AdrUCqdU&app=desktop
	//Destruir o objeto: https://www.youtube.com/watch?v=XO-E6QaTniQ

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider quemColidiu){
		if(quemColidiu.gameObject.tag == "Player"){	//Se colidir com o jogador
			Vector3 direcaoDano = quemColidiu.transform.position - transform.position;
			direcaoDano = direcaoDano.normalized;
			//Debug.Log("Teste - Direção do dano:"+direcaoDano);	//Teste
			if(direcaoDano.y>0){	//Se realmente tiver vindo de cima
				FindObjectOfType<SoundEffects>().tocarEliminaInimigo();	//Encontra o script SoundEffects e executa o método tocarEliminaInimigo dele.

				foreach(Transform child in transform){	//Para que ao levar dano, caso ainda tenha uma animação para executar (for levar um tempo antes de ser destruido), ele não dar dano durante esse tempo.
					child.gameObject.SetActive(false);
				}

				Destroy(transform.parent.gameObject);	//Destrói o parent (O objeto no qual ele está dentro). (Caso deva esperar um tempo antes de ser destruido, basta usar o segundo parâmetro da função.)
				FindObjectOfType<GameManager>().inimigosEmJogo--;
			}
		}
	}
}
