using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampiricGrip : MonoBehaviour
{
	
	// public objects
	public GameObject VampiricGripClaw;
	public GameObject Player;
	
	// private variables
	bool SpellReady = true;
	float x_offset = 0.074f;
	float y_offset = 0.91f;
	float z_offset = -5.39f;
	
   // Update is called once per frame
    void Update() {
        
		if (Input.GetKeyDown(KeyCode.Keypad2) && SpellReady) {
				
			gameObject.GetComponent<AudioSource>().Play();
			Invoke("CastSpell", 1.5f);
			SpellReady = false;
        }
		
    }
	
	void CastSpell() {
		
		Vector3 spawnPoint = new Vector3(x_offset, y_offset, z_offset);
		Instantiate(VampiricGripClaw, spawnPoint, Player.transform.rotation, Player.transform);
		
		StartCoroutine(ExtendAnimation());
		
	}
	
	IEnumerator ExtendAnimation() {
		
		GameObject claw = GameObject.Find("Vampiric Grip(Clone)");
		
		for (float i=0.5f; i<1.0f; i += 0.025f) {
			claw.transform.localScale = new Vector3(claw.transform.localScale.x, claw.transform.localScale.y, i);
			//claw.transform.localPosition = new Vector3(claw.transform.localPosition.x, claw.transform.localPosition.y, claw.transform.localPosition.z);
			yield return new WaitForSeconds(.01f);
		}
		
		for (float i=1.0f; i>0; i -= 0.025f) {
			claw.transform.localScale = new Vector3(claw.transform.localScale.x, claw.transform.localScale.y, i);
			//claw.transform.localPosition = new Vector3(claw.transform.localPosition.x, claw.transform.localPosition.y, claw.transform.localPosition.z);
			yield return new WaitForSeconds(.01f);
		}
		
		Destroy(claw);
		SpellReady = true;
	}
}
