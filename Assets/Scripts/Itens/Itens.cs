using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itens : MonoBehaviour {
	//[Unity3d Episode 4] Collecting Gems, sounds and removing gems from scene:  https://youtu.be/2UOXldTfr14

	// Use this for initialization
	void Start () {
		transform.Rotate(45,0,0);	//Pra deixár o item um pouco inclinado pro lado
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider quemColidiu){
		if(quemColidiu.gameObject.tag == "Player"){	//Se colidir com o jogador
			FindObjectOfType<QuantidadeItens>().itemPlastico();	//Adiciona +1 na variável itensPlastico

			AudioSource source = GetComponent<AudioSource>();
			source.Play();	//Dá play no audio que está no componente AudioSource

			foreach (Renderer r in GetComponentsInChildren<Renderer>())	//Para o objeto parar de ser renderizado, ainda antes de ser destruido (Foi feito isso, pois o destroy declarado abaixo está com delay).
				r.enabled = false;	//O objeto que está com o script não tem MeshRenderer, mas os filhos tem. Então, são desativados de todos os filhos. - https://answers.unity.com/questions/571068/disable-renderer-of-a-child.html
			gameObject.GetComponent<Collider>().enabled = false;	//Para que não aconteça de que ao colidir 1 vez, devido ao delay ao ser destruido, ele colida novamente e dê mais pontos do que era para dar.

			Destroy(gameObject, 1.0f);	//Está com delay de 1.0f para que dê tempo de tocar o audio por completo
		}
	}
}
