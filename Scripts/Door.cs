using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour 
{
	public SignPost signpost = null;
	public bool locked 	= true;

	//original color
	private Color color_original	= Color.clear;

	void Awake()
	{
		//set origional color
		color_original = gameObject.GetComponent<MeshRenderer>().material.color;
	}


	public void Click()
	{
		if(locked)
		{
			Debug.Log ("Door locked");
			//stay closed
			//bonus credit: play locked animation
			gameObject.GetComponent<AudioSource>().Play();
		}
		else
		{
			//play a sound
			gameObject.GetComponent<AudioSource>().Play();

			//hide the door and it's collision
			gameObject.GetComponent<MeshRenderer>().enabled = false;
			gameObject.GetComponent<BoxCollider>().enabled = false;
			Debug.Log ("Door unlocked");
		}
	}


	public void Enter()
	{
		//check to see if the door is in range of being hilighted
		bool in_range = Vector3.Distance(gameObject.transform.position, Camera.main.transform.position) < 14.0f;

		if(in_range)
		{
			//save origional color
			color_original = gameObject.GetComponent<MeshRenderer>().material.color;

			//change to highlight color
			gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
		}
	}


	public void Exit()
	{
		//set to origional color
		gameObject.GetComponent<MeshRenderer>().material.color = color_original;
	}
}
