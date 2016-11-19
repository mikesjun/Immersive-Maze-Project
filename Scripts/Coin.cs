using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IGvrGazeResponder 
{
	// Const for our animation speed
	[Header("Animation")]
	public float ROTATION_SPEED = 60f;
	public GameObject KeyPoof;

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

    //Create a reference to the CoinPoofPrefab

    public void OnCoinClicked() {
		// Using IGvrGaze for this ---
        // Instantiate the CoinPoof Prefab where this coin is located
        // Make sure the poof animates vertically
        // Destroy this coin. Check the Unity documentation on how to use Destroy
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
		if(this.gameObject != null){
			gameObject.GetComponent<MeshRenderer>().enabled = false;
		} 
		//Destroy (this.gameObject);
	}

	/// Called when the viewer's trigger is used, between OnGazeEnter and OnGazeExit.
	public void OnGazeTrigger() {
		SetGazedAt(true);
		// Instatiate the KeyPoof Prefab where this key is located
		Instantiate (KeyPoof, transform.position, transform.rotation);
		//Ding sounds
		AnimationSound.Play();

	}

	#endregion

}
