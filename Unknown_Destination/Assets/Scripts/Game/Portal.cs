using UnityEngine;
using System.Collections;


using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Portal : MonoBehaviour{ 
	
	public Image black;
	public Animator anim;
	public int index;
	public string levelName;
	

	IEnumerator Fading(){
		anim.SetBool ("Fade", true);
		yield return new WaitUntil (() => black.color.a == 1);
		SceneManager.LoadScene (index);
	}
	void OnTriggerEnter2D(Collider2D other){

		if(other.CompareTag("player"))
		{
			
			StartCoroutine(Fading());
		}

	}

}
