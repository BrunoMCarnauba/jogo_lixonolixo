using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour {
	//[Unity3d Episode 11] Game Over Music, Switch Scene Once All Items Are Collected: https://youtu.be/7ySxbl_yuL0?t=430
	public AudioSource levelMusic;
	public AudioSource gameOverSong;
	public AudioSource outrosAudios;

	public AudioClip eliminaInimigo;
	public AudioClip danoPlayer;

	//Para poder organizar, para quando tocar um o outro parar.
	public bool levelSongAtivo = true;
	public bool gameOverSongAtivo = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void tocarLevelMusic(){
		this.levelSongAtivo = true;
		this.gameOverSongAtivo = false;
		this.levelMusic.Play();
	}

	public void tocarGameOverSong(){
		if(this.levelMusic.isPlaying){
			this.levelSongAtivo = false;
			this.levelMusic.Stop();
		}

		if(!gameOverSong.isPlaying && this.gameOverSongAtivo == false){
			gameOverSong.Play();
			this.gameOverSongAtivo = true;
		}
	}

	public void tocarEliminaInimigo(){
		this.outrosAudios.clip = eliminaInimigo;
		this.outrosAudios.Play();
	}

	public void tocarDanoPlayer(){
		this.outrosAudios.clip = danoPlayer;
		this.outrosAudios.Play();
	}
}
