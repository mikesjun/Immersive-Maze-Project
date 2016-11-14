using UnityEngine;
using System.Collections; 

public class AnimateHand : MonoBehaviour, IGvrGazeResponder {
	// Const for our animation speed
	[Header("Animation")]
	public float ROTATION_SPEED = 60f;
	public GameObject KeyPoof;

	[SerializeField]
	private AudioSource AnimationSound = null;


	[Header("Sounds")]
	public AudioClip clip_gaze	= null;


		// Use this for initialization
		void Awake() {
			SetGazedAt(false);
			AnimationSound = gameObject.GetComponent<AudioSource>();	
			AnimationSound.clip = clip_gaze;
			AnimationSound.playOnAwake 	= false;
		}
		
		// Hand animation is called once per frame - may have to refactor to only rotate when gazed at
		void Update () {
			transform.Rotate(Vector3.right, ROTATION_SPEED * Time.deltaTime);
		}

	public void OnKeyClicked(bool doorUnlocked)
	{
		

		// Call the Unlock() method
		SetGazedAt(false);

		if (doorUnlocked == true) {
			return;
		}

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
		Instantiate (KeyPoof, transform.position, transform.rotation);
		AnimationSound.Play();
		if(this.gameObject != null) Destroy (this.gameObject,1.5f);
		Debug.Log (gameObject.name + " has been destroyed"); 

	}

	#endregion




}
