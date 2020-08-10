using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(BoxCollider))]
public class Inimigos : MonoBehaviour {
	//Tutoriais: - Seguir e atacar o jogador: https://www.youtube.com/watch?v=Fi9D_2pCvIs
	//Destroy: https://www.youtube.com/watch?v=XO-E6QaTniQ - Destruir depois de um tempo ou após colidir
	//Exemplo de IA que se move aleatoriamente pelo cenário: https://youtu.be/aEPSuGlcTUQ 

	private bool estaGerenciandoAcoes = false;	//Se já está ocorrendo o processo de mudar de ações (exemplo: De farejar para seguir), de acordo com o tempo.
	private bool seguirJogador = true;	//Indicará o momento em que o rato deve seguir ou não o jogador
	//Tempo que ele ficará seguindo o jogador quando estiver próximo
	public float minTempoSeguir = 10;	//Valor padrão: 10	-- Tempo mínimo que o rato ficará seguindo o jogador
	public float maxTempoSeguir = 20;	//Valor padrão: 20	-- Tempo máximo que o rato ficará seguindo o jogador
	public float minTempoSemSeguir = 2;	//Valor padrão: 2 -- Tempo em que o rato ficará parado, farejando, sem seguir o jogador (a menos que ele chegue muito próximo)
	public float maxTempoSemSeguir = 6;	//Valor padrão: 6
	private int tempoProximoDoJogador = 0;	//Contará o tempo em que o rato ficou próximo do jogador
	public float maxTempoProximo = 20;	//Valor padrão: 20 - Maximo de tempo próximo do jogador para poder trocar de ação
	private float distanciaPersonagemParaInimigo;	//Ficará registrando a distância entre personagem e o inimigo
	//public float distanciaInterromperFarejar = 5;	//Valor padrão: 5 -- Distancia em que se o jogador chegar do rato, ele interrompe o farejar e volta a seguir
	private Animator componenteAnimator;	//É preenchido na função start - Usado para setar qual animação será feita
	public float minVelocidadeMovimento = 5;	//Valor padrão: 5 - Velocidade minima que o rato se moverá
	public float maxVelocidadeMovimento = 7;	//Valor padrão: 7 - Velocidade máximaque o rato se moverá
	private float tempoVida = 0;	//Conta o tempo em que o rato está vivo
	private GameObject jogador;	//GameObject do jogador - Pois o rato irá segui-lo
	public NavMeshAgent navMesh;	//Navmesh para o rato poder seguir o jogador
	
	// Use this for initialization
	void Awake(){	//Executa antes do start. Foi usada, pois a tag do jogador em seus script só é setada no start. Então se colocasse essa também no start ele não iria encontrar tag nenhuma de jogador.
		jogador = GameObject.FindWithTag("Player");
		transform.tag = "Inimigo";
	}

	void Start () {
		navMesh = GetComponent<NavMeshAgent> ();
		navMesh.speed = minVelocidadeMovimento;	//https://forum.unity.com/threads/navmeshagent-speed.393898/
		componenteAnimator = GetComponent<Animator>();	//Pega o componente Animator quando o script é iniciado
	}
	
	// Update is called once per frame
	void Update () {
		//Ideia: O rato fica seguindo o personagem por determinado tempo, após esse tempo ele para de seguir e fica farejando (para de farejar se o jogador chegar muito próximo). Após isso, volta a segui-lo.
		tempoVida += Time.deltaTime;

		distanciaPersonagemParaInimigo = Vector3.Distance(this.transform.position, jogador.transform.position); //Calcula a distância e atribui a variável distancia
		if(distanciaPersonagemParaInimigo < 20){
			tempoProximoDoJogador++;
		}

		if(estaGerenciandoAcoes == false && tempoProximoDoJogador > maxTempoProximo){	//Quando já não tiver ocorrendo as mudanças de ações e o rato tiver proximo do jogador por tanto tempo ele chama o gerenciarAcoes para mudar de ações (correr para farejar).
			StartCoroutine(gerenciarAcoes());
		}

		if(seguirJogador == true){
			navMesh.isStopped = false;
			navMesh.destination = jogador.transform.position; 	//Seguir o personagem: https://www.youtube.com/watch?v=Fi9D_2pCvIs
			componenteAnimator.SetInteger("animacao", 1);	//Animação correndo
		}else if(seguirJogador == false){
			navMesh.isStopped = true;
			componenteAnimator.SetInteger("animacao", 2);	//Animação farejando
			// if(distanciaPersonagemParaInimigo < distanciaInterromperFarejar){
			// 	seguirJogador = true;
			// }
		}

		if(tempoVida > 90 && distanciaPersonagemParaInimigo > 50){
			Destroy(this.gameObject);	//Destruir o objeto após um tempo: https://www.youtube.com/watch?v=XO-E6QaTniQ
			FindObjectOfType<GameManager>().inimigosEmJogo--;
		}

	}

	IEnumerator gerenciarAcoes(){	//Baseado nesse vídeo: https://youtu.be/aEPSuGlcTUQ
		float tempoSeguirJogador = Random.Range(minTempoSeguir, maxTempoSeguir);
		float tempoSemSeguirJogador = Random.Range(minTempoSemSeguir, maxTempoSemSeguir);
		float velocidadeMovimento = Random.Range(minVelocidadeMovimento, maxVelocidadeMovimento);

		estaGerenciandoAcoes = true;

		navMesh.speed = velocidadeMovimento;	//https://forum.unity.com/threads/navmeshagent-speed.393898/		
		seguirJogador = true;
		yield return new WaitForSeconds(tempoSeguirJogador);
		seguirJogador = false;
		yield return new WaitForSeconds(tempoSemSeguirJogador);

		estaGerenciandoAcoes = false;
	}



}
