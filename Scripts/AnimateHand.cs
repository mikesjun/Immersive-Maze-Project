using UnityEngine;
using System.Collections; 
using System.Collections.Generic;

public class AnimateHand : MonoBehaviour, IGvrGazeResponder {

	// Reference to KeyPoof & Door Prefab
	public Door door = null;
	public GameObject KeyPoof;

	// Const for our animation speed
	[Header("Animation")]
	public float ROTATION_SPEED = 60f;


	//[SerializeField]
	private AudioSource AnimationSound = null;


	//[Header("Sounds")]
	public AudioClip clip_click	= null;


	// Use this for initialization
	void Awake() {
		SetGazedAt(false);
		AnimationSound = gameObject.GetComponent<AudioSource>();	
		AnimationSound.clip = clip_click;
		AnimationSound.playOnAwake 	= false;
	}
	
	// Hand animation is called once per frame - may have to refactor to only rotate when gazed at
	void Update () {
		transform.Rotate(Vector3.right, ROTATION_SPEED * Time.deltaTime);
	}

	public void OnKeyClicked()
	{
		//unused

	}

	public void SetGazedAt(bool gazedAt) {
		// Highlight the key when clicked
		GetComponent<Renderer>().material.color = gazedAt ? Color.white : Color.gray;
	}

	#region IGvrGazeResponder implementation

	/// Called when the user is looking on a GameObject with this script,
	/// as long as it is set to an appropriate layer (see GvrGaze).
	public void OnGazeEnter() {
		Debug.Log ("Entered Gaze on " + this.gameObject.name);
		SetGazedAt(true);
	}

	/// Called when the user stops looking on the GameObject, after OnGazeEnter
	/// was already called.
	public void OnGazeExit() {
		SetGazedAt(false);
	}

	/// Called when the viewer's trigger is used, between OnGazeEnter and OnGazeExit.
	public void OnGazeTrigger() {
		// Instatiate the KeyPoof Prefab where this key is located
		Instantiate (KeyPoof, transform.position, Quaternion.Euler(-90, 0, 0));
		//transform.Translate (0, 10 * Time.deltaTime, 0, Space.World);
			
		gameObject.GetComponent<AudioSource>().Play();
		// Call the Door Click() method
		door.locked = false;

		//Hide object
		gameObject.GetComponent<MeshRenderer> ().enabled = false;
		//Debug.Log (gameObject.name + " has been destroyed");
		//sets gaze to false :D
		SetGazedAt (false);
	}

	#endregion

	public void Enter()
	{
		SetGazedAt(false);
		//change to highlight color
		gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
	}


	public void Exit()
	{
		SetGazedAt(false);
		//set to origional color
		gameObject.GetComponent<MeshRenderer>().material.color = Color.clear;
	}



}
