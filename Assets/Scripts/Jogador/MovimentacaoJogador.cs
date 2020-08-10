using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoJogador : MonoBehaviour {
	public float velocidadeMovimento = 60.0f;
	public float velocidadeRotacao = 170.0f;
	public float forcaPulo = 7;
	private Vector3 pos;
	private Animator componenteAnimator;
	private Rigidbody rb;
	public LayerMask groundLayers;
	private CapsuleCollider col;

	void Awake(){	//Acontece antes do start
		transform.tag = "Player";
	}

	//Use this for intitialization
	void Start () {
		componenteAnimator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		col = GetComponent<CapsuleCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		//Movimentar o personagem - https://www.satellasoft.com/?materia=mover-personagem-com-unity-3d
        float translate = (Input.GetAxis ("Vertical") * velocidadeMovimento) * Time.deltaTime;	//Movimentar para frente e para trás - Esse valor varia aos poucos (Não é fixo, tipo: 1 ou -1).
        float rotate = (Input.GetAxis ("Horizontal") * velocidadeRotacao) * Time.deltaTime;	//Rotacionar para os lados

        transform.Translate (0, 0, translate);
        transform.Rotate (0, rotate, 0);

		if(translate > 0)
			translate = 1;
		else if (translate < 0)
			translate = -1;
		else
			translate = 0;

		if(rotate > 0)
			rotate = 1;
		else if(rotate < 0)
			rotate = -1;
		else
			rotate = 0;
		

		//Altera animação - A maioria dos números das animações estão baseados no teclado numérico
		if(translate > 0){	//Se tiver se movendo para frente
			componenteAnimator.SetInteger("animacao", 8);	//Correndo para frente
		} else if (translate < 0){
			componenteAnimator.SetInteger("animacao", 2);	//Correndo para trás
		} else if(rotate > 0){
			componenteAnimator.SetInteger("animacao", 6);	//Virando para direita
		} else if (rotate < 0){
			componenteAnimator.SetInteger("animacao", 4);	//Virando para esquerda
		}

		if(rotate == 0 && translate == 0){
			componenteAnimator.SetInteger("animacao", 0);	//Idle
		}

		if(estaNoChao() && Input.GetKeyDown(KeyCode.Space)){	//Pular
			rb.AddForce(Vector3.up*forcaPulo, ForceMode.Impulse);
			componenteAnimator.SetInteger("animacao",5);	//Pular
		}
	}

	private bool estaNoChao(){
		return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .1f, groundLayers);
	}

}
