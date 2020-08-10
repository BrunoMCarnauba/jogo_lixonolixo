using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(BoxCollider))]
public class InimigosAntigo : MonoBehaviour {
	//Tutoriais: - Seguir e atacar o jogador: https://www.youtube.com/watch?v=Fi9D_2pCvIs
	//Destroy: https://www.youtube.com/watch?v=XO-E6QaTniQ - Destruir depois de um tempo ou após colidir

	private bool estaGerenciandoAcoes = false;	//Se já está ocorrendo o processo de mudar de ações (exemplo: De farejar para seguir), de acordo com o tempo.
	private bool seguirJogador = true;
	//Tempo que ele ficará seguindo o jogador quando estiver próximo
	public float minTempoSeguir;	//Valor padrão: 10	-- Tempo mínimo que o rato ficará seguindo o jogador
	public float maxTempoSeguir;	//Valor padrão: 20	-- Tempo máximo que o rato ficará seguindo o jogador
	public float tempoSemSeguirAcao1;	//Valor padrão: 2	-- Tempo em que o rato ficará sem seguir o jogador, executando a ação 1 (Farejando)
	public float tempoSemSeguirAcao2;	//Valor padrão: 8	-- Tempo em que o rato ficará sem seguir o jogador, executando a ação 2 (Andando para outra direção)
	public bool estaRotacionando = true;	//Indicará quando o rato estará rotacionando
	public float velocidadeRotacao = 100f;	//Velocidade em que o rato rotacionará
	private int tempoProximoDoJogador = 0;	//Contará o tempo em que o rato ficou próximo do jogador
	private Animator componenteAnimator;	//É preenchido na função start - Usado para setar qual animação será feita
	public float velocidadeMovimento;	//Velocidade que o rato se moverá
	private float tempoVida = 0;	//Contará o tempo em que o rato ficou vivo
	private GameObject jogador;	//GameObject do jogador - Pois o rato irá segui-lo
	public NavMeshAgent navMesh;	//Navmesh para o rato poder seguir o jogador
	private float distanciaPersonagemParaInimigo;
	private int tipoAcao = 1;
	// Use this for initialization
	void Awake(){	//Executa antes do start. Foi usada, pois a tag do jogador em seus script só é setada no start. Então se colocasse essa também no start ele não iria encontrar tag nenhuma de jogador.
		jogador = GameObject.FindWithTag("Player");
		transform.tag = "Inimigo";
	}

	void Start () {
		navMesh = GetComponent<NavMeshAgent> ();
		navMesh.speed = velocidadeMovimento;	//https://forum.unity.com/threads/navmeshagent-speed.393898/
		componenteAnimator = GetComponent<Animator>();	//Pega o componente Animator quando o script é iniciado
	}
	
	// Update is called once per frame
	void Update () {
		//Ideia: O rato fica seguindo o personagem por determinado tempo, após esse tempo ele para de seguir durante 3 segundos, fica farejando e depois volta a seguir por mais algum tempo. Após esse tempo, ele para de segui-lo, vira para o outro lado e segue reto por algum tempo. Depois, repete o processo se ele não for destruido por ficar muito distante e passar determinado tempo.
		tempoVida += Time.deltaTime;

		distanciaPersonagemParaInimigo = Vector3.Distance(this.transform.position, jogador.transform.position); //Calcula a distância e atribui a variável distancia
		if(distanciaPersonagemParaInimigo < 20){	//Vai contando o tempo em que o rato ficou abaixo da distância 20 do jogador
			tempoProximoDoJogador++;
		}

		if(estaGerenciandoAcoes == false && tempoProximoDoJogador > 20){	//Quando já não tiver ocorrendo as mudanças de ações e o rato tiver proximo do jogador por tanto tempo ele chama o gerenciarAcoes para mudar de ações (correr para farejar).
			StartCoroutine(gerenciarAcoes());
		}

		if(seguirJogador == true){
			navMesh.isStopped = false;	//Permite seguir
			navMesh.destination = jogador.transform.position; 	//Seguir o personagem: https://www.youtube.com/watch?v=Fi9D_2pCvIs
			componenteAnimator.SetInteger("animacao", 1);	//Animação correndo
		}else if(seguirJogador == false && tipoAcao == 1){
			navMesh.isStopped = true;	//Para de seguir
			componenteAnimator.SetInteger("animacao", 2);	//Animação farejando
		}else if(seguirJogador == false && tipoAcao == 2){
			navMesh.isStopped = true;	//Para de seguir
			transform.Translate(0, 0, (velocidadeMovimento * Time.deltaTime));	//Move pra frente
			componenteAnimator.SetInteger("animacao", 1);	//Animação correndo
			if(estaRotacionando == true){
				transform.Rotate(transform.up * Time.deltaTime * velocidadeRotacao);	//Rotaciona aos poucos para a direita
			}
		}

		if(tempoVida > 90 && distanciaPersonagemParaInimigo > 50){
			Destroy(this.gameObject);	//Destruir o objeto após um tempo: https://www.youtube.com/watch?v=XO-E6QaTniQ
			FindObjectOfType<GameManager>().inimigosEmJogo--;
		}

	}

	IEnumerator gerenciarAcoes(){	//Baseado nesse vídeo: https://youtu.be/aEPSuGlcTUQ
		float tempoSeguirJogador = Random.Range(minTempoSeguir, maxTempoSeguir);
		float tempoSemSeguirJogador = 0;
		float tempoRotacao = Random.Range(0,tempoSemSeguirJogador/2);

		estaGerenciandoAcoes = true;
		
		seguirJogador = true;
		yield return new WaitForSeconds(tempoSeguirJogador);
		seguirJogador = false;
		if(tipoAcao == 1){
			tipoAcao++;	//Passa para a próxima ação (a que o rato anda para outra direção)
			tempoSemSeguirJogador = tempoSemSeguirAcao2;
			estaRotacionando = true;
			yield return new WaitForSeconds(tempoRotacao);
			estaRotacionando = false;
		}else{
			tipoAcao = 1;	//Volta para a primeira ação (a que o rato fica parado farejando)
			tempoSemSeguirJogador = tempoSemSeguirAcao1;
		}
		tempoProximoDoJogador = 0;
		yield return new WaitForSeconds(tempoSemSeguirJogador);

		estaGerenciandoAcoes = false;
	}



}
