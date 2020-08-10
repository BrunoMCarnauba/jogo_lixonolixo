using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoNPC : MonoBehaviour {
//Tutorial: https://youtu.be/aEPSuGlcTUQ

	bool estaGerenciando = false;

	bool rotacaoDireita = false;
	bool rotacaoEsquerda = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(estaGerenciando == false){
			StartCoroutine(gerenciaMovimento());
		}



		if(rotacaoDireita == true){

		}else if(rotacaoEsquerda == true){

		}
	}

	IEnumerator gerenciaMovimento(){
		int direcaoRotacao = Random.Range(1,2);
		int tempoRotacao = Random.Range(1,3);
		int tempoAnda = Random.Range(1,4);

		estaGerenciando = true;


		if(direcaoRotacao == 1){	//Esquerda
			rotacaoDireita = true;
			yield return new WaitForSeconds(tempoRotacao);
			rotacaoDireita = false;
		}else if(direcaoRotacao == 2){	//Direita
			rotacaoEsquerda = true;
			yield return new WaitForSeconds(tempoRotacao);
			rotacaoEsquerda = false;
		}


		estaGerenciando = false;
	}
}
