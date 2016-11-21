using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IGvrGazeResponder 
{
	// Const for our animation speed
	[Header("Animation")]
	public float ROTATION_SPEED = 60f;
	public GameObject CoinPoof;

	[SerializeField]
	private AudioSource AnimationSound = null;


	[Header("Sounds")]
	public AudioClip clip_collide	= null;


	// Use this for initialization
	void Start() {
		AnimationSound = gameObject.GetComponent<AudioSource>();	
		AnimationSound.clip = clip_collide;
		AnimationSound.playOnAwake 	= false;
	}

	void Update () {
		transform.Rotate(Vector3.up, ROTATION_SPEED * Time.deltaTime);
	}

	public void SetGazedAt(bool gazedAt) {
		// Highlight the coin when gazed at
		GetComponent<Renderer>().material.color = gazedAt ? Color.white : Color.gray;
	}

    

	#region IGvrGazeResponder implementation

	/// Called when the user is looking on a GameObject with this script,
	/// as long as it is set to an appropriate layer (see GvrGaze).
	public void OnGazeEnter() {
		Debug.Log ("Entered Gaze on " + this.gameObject.name);

		// moved functionality for the sake of rubric

	}
	/// Called when the user stops looking on the GameObject, after OnGazeEnter
	/// was already called.
	public void OnGazeExit() {
		SetGazedAt(false);
		//if(this.gameObject != null){
		//	gameObject.GetComponent<MeshRenderer>().enabled = false;
		//} 
		//Destroy (this.gameObject);
	}

	/// Called when the viewer's trigger is used, between OnGazeEnter and OnGazeExit.
	public void OnGazeTrigger() {
		SetGazedAt(true);
		// Using IGvrGaze for this ---
		// Make sure the poof animates vertically
		// Destroy this coin. Check the Unity documentation on how to use Destroy
		// Instatiate the CoinPoof Prefab where this key is located
		if (this.gameObject != null) {
			Instantiate (CoinPoof, transform.position, Quaternion.Euler(-90, 0, 0));
			//Ding sounds
			AnimationSound.Play ();

			gameObject.GetComponent<MeshRenderer>().enabled = false;

			Debug.Log ("Triggered OnGaze");
		}

	}

	#endregion

}
