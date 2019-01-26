using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SFX : MonoBehaviour
{
	
	public AudioSource source;
	public AudioClip[] launchSFX;
	public AudioClip[] impactSFX;
	public AudioClip[] otherSFX;
	

	void Start () {
		source = gameObject.GetComponent<AudioSource>();
	}

	void Update () {

//		if(Input.GetKeyDown(KeyCode.Q)){
		//
//			int rdm = Random.Range (0,launchSFX.Length);
//			source.clip = launchSFX[rdm];
//			SFXManager.instance.PlaySFX(source.clip, 0);
//		}
//
//		if(Input.GetKeyDown(KeyCode.W)){
			//
//			int rdm = Random.Range (0,impactSFX.Length);
//			source.clip = impactSFX[rdm];
//			SFXManager.instance.PlaySFX(source.clip, 1);
//		}
//
//		if(Input.GetKeyDown(KeyCode.E)){
			//
//			int rdm = Random.Range (0,otherSFX.Length);
//			source.clip = otherSFX[rdm];
//			SFXManager.instance.PlaySFX(source.clip, 1);
//		}

		if(Input.GetKeyDown(KeyCode.Q)){
			SFXManager.instance.PlayResourceGatheredSFX("wood");
		}

		if(Input.GetKeyDown(KeyCode.W)){
			SFXManager.instance.PlayResourceGatheredSFX("food");
		}

		if(Input.GetKeyDown(KeyCode.E)){
			SFXManager.instance.PlayResourceGatheredSFX("tools");
		}
	}


	//void OnCollisionEnter(Collision other) {
	//	
	//	if (other.gameObject.tag == "Enemy")
	//	{
	//		source[0].Stop();
	//		if (fallsPlayed<=2)
	//		{
	//			if (!hasPlayedLandingSound) //ta bort denna if-sats?
	//			{
	//				int rdm = Random.Range (0,landingSound.Length);
	//				AudioSource.PlayClipAtPoint(landingSound[rdm], gameObject.transform.position);
	//				EnemyWaveSFX.instance.IncreasePlays("falls");
	//				//hasPlayedLandingSound = true;
	//				
	//				//Destroy (this, 5);
	//			}
	//		}
	//		else
	//		{
	//			source[0].Stop();
	//			source[1].Stop();
	//			source[2].Stop();
	//			
	//			if (!source[0].isPlaying && !source[1].isPlaying && !source[2].isPlaying)
	//			{
	//				Invoke ("SetFallsToZero", 0.5f);
	//				//fallsPlayed = 0;
	//			}
	//		}
	//	}
	//}
}
