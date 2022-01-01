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
	Vector3 spawnPoint;
	
   // Update is called once per frame
    void Update()
    {
        
		if (Input.GetKeyDown(KeyCode.Keypad2) && SpellReady)
        {
				
			//gameObject.GetComponent<AudioSource>().Play();
			Invoke("CastSpell", 1.5f);
			SpellReady = false;
			print("HELLO");
        }
		
    }
	
	void CastSpell() {
		
		float x_offset = 0.5f * Mathf.Sin(gameObject.transform.eulerAngles.y * Mathf.Deg2Rad);
        float z_offset = 0.5f * Mathf.Cos(gameObject.transform.eulerAngles.y * Mathf.Deg2Rad);
		Vector3 spawnPoint = new Vector3(transform.position.x + x_offset, transform.position.y, transform.position.z + z_offset);
		Instantiate(VampiricGripClaw, spawnPoint, Player.transform.rotation);
		
		GameObject claw = GameObject.Find("Vampiric Claw (Clone)");
		StartCoroutine(ExtendAnimation(claw));
		
	}
	
	IEnumerator ExtendAnimation(GameObject claw) {
		for (float i=1.0f; i<3.0f; i += 0.1f) {
			claw.transform.localScale = new Vector3(i, 1, 1);
			yield return new WaitForSeconds(.1f);
		}
		
		for (float i=3.0f; i>1.0f; i -= 0.1f) {
			claw.transform.localScale = new Vector3(i, 1, 1);
			yield return new WaitForSeconds(.1f);
		}
		
		Destroy(claw);
		SpellReady = true;
	}
}
