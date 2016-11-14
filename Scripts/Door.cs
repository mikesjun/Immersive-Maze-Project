using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour 
{
	private enum State
	{
		Closed,
		Focused,
		Clicked,
		Open
	}

	[SerializeField]
	private State _state				= State.Closed;
	private Color _color_original		= new Color(1.0f, 1.0f, 1.0f, 1.0f);
	private Color _color				= Color.white;
	private float 	_animated_lerp		= 1.0f;
	private AudioSource _audio_source	= null;

	[Header("Material")]
	public Color color_hilight			= new Color(0.0f, 0.8f, 1.0f, 0.125f);	

	[Header("State Blend Speeds")]
	public float lerp_idle 				= 0.0f;
	public float lerp_focus 			= 0.0f;
	public float lerp_hide				= 0.0f;
	public float lerp_clicked			= 0.0f;

	[Header("State Animation Scales")]
	public float scale_clicked_max		= 0.0f;
	public float scale_animation		= 3.0f;	
	public float scale_idle_min 		= 0.0f;
	public float scale_idle_max 		= 0.0f;
	public float scale_focus_min		= 0.0f;
	public float scale_focus_max		= 0.0f;

	[Header("Sounds")]
	public AudioClip clip_click			= null;	

	void Awake()
	{		
		_color						= _color_original;
		_audio_source				= gameObject.GetComponent<AudioSource>();	
		_audio_source.clip		 	= clip_click;
		_audio_source.playOnAwake 	= false;
	}
    // Create a boolean value called "locked" that can be checked in Update() 

    /*void Update() {
        // If the door is unlocked and it is not fully raised
            // Animate the door raising up
    }*/
	void Update()
	{

		switch(_state)
		{
		case State.Closed:
			Closed();
			break;

		case State.Focused:
			Focus();
			break;

		case State.Clicked:
			Clicked();
			break;

		case State.Open:
			Open();
			break;

		default:
			break;
		}

		gameObject.GetComponentInChildren<MeshRenderer>().material.color 	= _color;
	}


	public void Closed()
	{
		_state = State.Closed;
	}
		
	public void Focus()
	{
		Color color				= Color.Lerp( _color_original,  color_hilight, _animated_lerp);
		_color					= Color.Lerp(_color, color,	lerp_focus);
	}
	public void Click()
	{
		_state = _state == State.Focused ? State.Clicked : _state;

		_audio_source.Play();

		Camera.main.transform.position 	= gameObject.transform.position;
	}
	public void Clicked()
	{	
		_color	= Color.Lerp(_color,  color_hilight, lerp_clicked);
	}
		
	public void Open()
	{
		// Open state	
		transform.position = new Vector3(0.36f, 24.0f, 47.65f);
	}

    /*public void Unlock()
    {
        // You'll need to set "locked" to true here

    }*/
} 
 