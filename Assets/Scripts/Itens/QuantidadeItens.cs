using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuantidadeItens : MonoBehaviour {
	//[Unity3d Episode 4] Collecting Gems, sounds and removing gems from scene: https://youtu.be/2UOXldTfr14
	//[Unity3d Episode 11] Game Over Music, Switch Scene Once All Items Are Collected: https://youtu.be/7ySxbl_yuL0
	private int pontuacao = 0;
	public int totalItens; //Total de itens para poder passar de level
	private int itensPlastico = 0;
	private int itensMetal = 0;

	private GUIStyle guiStyle = new GUIStyle();	//Para ser feita uma GUIStyle personalizada
	//private GUIStyle corTexto;

	// Use this for initialization
	void Start () {
		totalItens = GameObject.FindGameObjectsWithTag("Item").Length;	//Atribui a quantidade de objetos com a tag "Item" que estão na cena. - https://answers.unity.com/questions/35825/count-the-number-of-objects-with-a-certain-tag.html
	}
	
	// Update is called once per frame
	void Update () {

	}

	//Foram feitas funções ao invés de incrementar direto na variável, pois pode ser que o valor que será incrementado varie de acordo com o level (No momento não faz isso, mas essa estrutura permite fazer isso mais facilmente)
	public void itemPlastico(){
		this.itensPlastico += 1;
		this.pontuacao += 1;
	}

	public void itemMetal(){
		this.itensMetal += 1;
		this.pontuacao += 1;
	}

	private void OnGUI(){	//OnGUI é chamado automaticamente (Precisa estar exatamente com esse nome, com as letras maiúsculas e minúsculas)
		guiStyle.fontSize = 20;
		guiStyle.normal.textColor = Color.red;

		GUI.contentColor = Color.red;
		GUI.Label(new Rect(40,60,0,0), "Pontuação: " + pontuacao + "\nTotal de itens: " + totalItens, guiStyle);	//Rect é um retângulo 2D, definido com X,Y,Largura e Altura. Largura e altura estão zerados pois está sendo usado somente o tamanho da fonte. O guiStyle no final é o modelo de GuiStyle que criamos acima.

		if(this.pontuacao == this.totalItens){
			//SceneManager.LoadScene("Level2");
			Debug.Log("Ganhou!!! :D");
			//Passar para próxima cena - https://answers.unity.com/questions/1141235/how-to-move-to-next-scene-using-scene-manager.html
			int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
			if (SceneManager.sceneCount > nextSceneIndex){
				SceneManager.LoadScene(nextSceneIndex);
			} else {				
				FindObjectOfType<GameManager>().ganhou();	//Chama o método ganhou do script GameManager
				//Busca objetos que contém o script SpawnInimigos.
				SpawnInimigos[] spawnInimigos = FindObjectsOfType<SpawnInimigos>();
				foreach(SpawnInimigos spawnInimigo in spawnInimigos){
					spawnInimigo.stop = true;	//Para o spawn de inimigos
				}
				//Destrói todos os objetos que tem a tag Inimigo
				foreach(GameObject objetoInimigo in GameObject.FindGameObjectsWithTag("Inimigo")){
					Destroy(objetoInimigo);
				}
			}
		}
	}
}
