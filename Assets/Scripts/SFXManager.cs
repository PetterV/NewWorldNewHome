using System.Collections;
using System.Collections.Generic; // Not needed?
using UnityEngine;

public class SFXManager : MonoBehaviour
{

	public static SFXManager instance;

	public AudioSource[] source; // Should be as many as the list below (5?) Drag'n'drop

	public AudioClip[] resourceGatheredSFX = new AudioClip[3]; // Wood, Food, Tools
	public AudioClip[] settingUpCampSFX; 	// Pause, Unpause movement
	public AudioClip[] encounterSFX; 		// Triggered, Dealt with
	public AudioClip[] gameStartEndSFX;		// Start, End (and a Game OVer?)
	public AudioClip[] miscSFX;				// Anything else. Or make a new category.

	
	void Awake ()
	{
		if (instance == null)
		{
			instance = this as SFXManager;
		}
		
		else
		{
			Destroy(gameObject);
		}	
	}

	void Start () {
		// Play sounds...
		if (gameStartEndSFX[0] == null){ //Make sure sounds exist...
			Debug.Log ("Missing SFX: Go to SFXManager and assign clip to GameStartEndSFX[0]");
		}

		int _source = 3;
		source[_source].clip = gameStartEndSFX[0];
		source[_source].Play();
	}

	// Uses source: 0
	public void PlayResourceGatheredSFX (string _type) //, int _audioSource)
	{
		int _source = 0; //Chooses Audio source assigned in the Inspector


		// Play sounds...
		if (_type == "wood") {
			if (resourceGatheredSFX[0] == null){ //Make sure sounds exist...
				Debug.Log ("Missing SFX: Go to SFXManager and assign clip to ResourceGatheredSFX[0]");
			}
			source[_source].clip = resourceGatheredSFX[0];
			source[_source].Play();
		}

		else if (_type == "food")
		{
			if (resourceGatheredSFX[1] == null){ //Make sure sounds exist...
				Debug.Log ("Missing SFX: Go to SFXManager and assign clip to ResourceGatheredSFX[1]");
			}
			source[_source].clip = resourceGatheredSFX[1];
			source[_source].Play();
		}
		else if (_type == "tools")
		{
			if (resourceGatheredSFX[2] == null){ //Make sure sounds exist...
				Debug.Log ("Missing SFX: Go to SFXManager and assign clip to ResourceGatheredSFX[2]");
			}
			source[_source].clip = resourceGatheredSFX[2];
			source[_source].Play();
		}
		else {
			Debug.Log ("Missing SFX: ResourceGatheredSFX requires 'wood', 'food', or 'tools' entered.");
		}
	}

	// Uses audio source: 1
	public void PlayCampSFX (bool _moving)
	{
		int _source = 1; //Chooses Audio source assigned in the Inspector

		// Play sounds...
		if (_moving == false)
		{
			if (settingUpCampSFX[0] == null){ //Make sure sounds exist...
				Debug.Log ("Missing SFX: Go to SFXManager and assign clip to SettingUpCampSFX[0]");
			}
			source[_source].clip = settingUpCampSFX[0];
			source[_source].Play();
		}
		else
		{
			if (settingUpCampSFX[1] == null){ //Make sure sounds exist...
				Debug.Log ("Missing SFX: Go to SFXManager and assign clip to SettingUpCampSFX[1]");
			}
			source[_source].clip = settingUpCampSFX[1];
			source[_source].Play();
		}
	}

	// Uses audio source: 2
	public void EncounterSFX (string _type)
	{
		int _source = 2; //Chooses Audio source assigned in the Inspector

		// Play sounds...
		if (_type == "on_fire")
		{
			if (encounterSFX[0] == null){ //Make sure sounds exist...
				Debug.Log ("Missing SFX: Go to SFXManager and assign clip to EncounterSFX[0]");
			}
			source[_source].clip = encounterSFX[0];
			source[_source].Play();
		}
		else if (_type == "on_exit")
		{
			if (encounterSFX[1] == null){ //Make sure sounds exist... 
				Debug.Log ("Missing SFX: Go to SFXManager and assign clip to EncounterSFX[1]");
			}
			source[_source].clip = encounterSFX[1];
			source[_source].Play();
		}
		else {
			Debug.Log ("Missing SFX: EncounterSFX requires string 'event_fired' or 'event_exited' to be sent from encounters.");
		}
	}

//	public void PlaySFX (AudioClip _soundEffect, int _audioSource)
//	{
//		float rdmPitch = Random.Range (0.5f,1.5f);
//		source[_audioSource].pitch = rdmPitch;
//		source[_audioSource].clip = _soundEffect;
//		source[_audioSource].Play();
//	}
//
//	public void PlayButtonSFX (AudioClip _soundEffect, int _audioSource)
//	{
//		source[0].clip = _soundEffect;
//		source[_audioSource].Play();
//	}

}