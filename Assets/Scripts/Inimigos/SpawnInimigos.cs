using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInimigos : MonoBehaviour {

	public GameObject[] inimigos;
	public int esperaInicio;
	private float esperaSpawn;
	public float tempoMinimoEsperaSpawn;
	public float tempoMaximoEsperaSpawn;
	public Vector3 posicaoSpawn;
	public bool stop = false;

	private int inimigoAleatorio;

	// Use this for initialization
	//Tutorial Spawn unity: https://youtu.be/WGn1zvLSndk
	void Start () {
		StartCoroutine(spawnar());	//Começa o processo de spawn
	}
	
	// Update is called once per frame
	void Update () {
		esperaSpawn = Random.Range(tempoMinimoEsperaSpawn, tempoMaximoEsperaSpawn);	//Define o tempo de spawn de forma aleatória entre os determinados valores.
	}

	IEnumerator spawnar(){
		yield return new WaitForSeconds(esperaInicio); //Espera o tempo determinado para poder passar para a próxima instrução (a de spawn) (Começar o spawn de verdade)

		while(!stop){
			inimigoAleatorio = Random.Range(0,inimigos.Length);	//Escolhe qual inimigo vai spawnar (posição do Array de inimigos) - Exemplo do Random.Range: Se for (0,4), poderá cair os valores: 0,1,2,3

			if(FindObjectOfType<GameManager>().podeSpawnarInimigo() == true){	//TESTE
			Vector3 spawnPosition = new  Vector3(Random.Range(-posicaoSpawn.x, posicaoSpawn.x), 0, Random.Range(-posicaoSpawn.z,posicaoSpawn.z));	//X aleatório (entre o valor x negativo e positivo informado), Y = 0 e Z também aleatório

			Instantiate(inimigos[inimigoAleatorio], spawnPosition + transform.TransformPoint (0,0,0), gameObject.transform.rotation);	//Passa o objeto 3D, a posição definida do spawn + ..., rotação do objeto que está com esse script.

			FindObjectOfType<GameManager>().inimigosEmJogo++;	//TESTE
			}

			yield return new WaitForSeconds(esperaSpawn);	//Espera o tempo determinado para voltar a repetição.
		}
	}
}
