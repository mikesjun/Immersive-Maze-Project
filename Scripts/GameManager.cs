using System;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static int currentScore;
	public static int highScore;

	public GameObject scoreBoard; 

	public float startTime;
	private string currentTime;

	public int coinCount = 37;
	public Coin coin = null;

	void Awake(){
		//Set the text to nothing ("") on wakeup
		//gameObject.GetComponent<UnityEngine.UI.Text>().text = "";
		DontDestroyOnLoad (gameObject);
	}

	void Update(){
		startTime -= Time.deltaTime;
		currentTime = string.Format("{0:0.0}",startTime);

		if (startTime <= 0.0f) {
			timerEnded ();
		}

		/*if (coin.OnGazeTrigger()) {
			coinCount -= 1;
			print(coinCount); 
		}*/
	}


	void timerStarted(){
		return;
	}
	void timerEnded(){
		startTime = 0.0f;
		//gameObject.GetComponent<UnityEngine.UI.Text>().text = "Time's up!";
	}

	
	
}

