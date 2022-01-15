using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellScript : MonoBehaviour
{

    //holy spell things
    public GameObject holySpell;
    private Vector3 holySpellSpawnPoint;
    bool holySpellFlag;

    //vampiric touch things
    public GameObject VampiricGripClaw;
    public GameObject Player;
    bool SpellReady = true;
    float x_offset = 0;//0.074f; old offsets
    float y_offset = 0.5f;//0.91f;
    float z_offset = 0;//-5.39f;

    //Fireball things
    public GameObject Fireball;
    public float FireballSpeed;
    float yball = 1.25f;
    float zball = 1.25f;
    private Vector3 fireposition;
    bool firebool;


    void Update()
    {
        //Holy Spell
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            HolySpellDelayed(3.0f);
        }

        //Vampiric Touch Spell
        if (Input.GetKeyDown(KeyCode.Keypad2) && SpellReady)
        {

            gameObject.GetComponent<AudioSource>().Play();
            Invoke("CastSpell", 1.5f);
            SpellReady = false;
        }

        //Fireball Spell
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Castfireball();

        }
    }

    //holy spell///////////////////////////////////////////////////////////
    private void HolySpellDelayed(float delay)
    {
        if (!holySpellFlag)
        {
            gameObject.GetComponent<AudioSource>().Play();
            float xholy = 4.5f * Mathf.Sin(gameObject.transform.eulerAngles.y * Mathf.Deg2Rad);
            float zholy = 4.5f * Mathf.Cos(gameObject.transform.eulerAngles.y * Mathf.Deg2Rad);
            holySpellSpawnPoint = new Vector3(transform.position.x + xholy, transform.position.y, transform.position.z + zholy);
            Invoke("HolySpell", delay);
            holySpellFlag = true;
        }
    }
    private void HolySpell()
    {
        GameObject playerCam = GameObject.Find("PlayerCamera");
        playerCam.GetComponent<DirtyLensFlare>().enabled = true;
        Instantiate(holySpell, holySpellSpawnPoint, Quaternion.identity);
        GameObject spellToDestroy = GameObject.Find("Holy Spell (WIP)(Clone)");
        Destroy(spellToDestroy, 4.0f);
        StartCoroutine(SetHolySpellFlag(false, 4.0f));
    }
    IEnumerator SetHolySpellFlag(bool val, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        GameObject playerCam = GameObject.Find("PlayerCamera");
        playerCam.GetComponent<DirtyLensFlare>().enabled = false;
        holySpellFlag = val;
    }


    //vampiric touch spell//////////////////////////////////////////////////////////
    void CastSpell()
    {

        Vector3 spawnPoint = new Vector3(Player.transform.position.x + x_offset, //changed this so it spawns where we want it to
                                         Player.transform.position.y + y_offset,
                                         Player.transform.position.z + z_offset);
        Instantiate(VampiricGripClaw, spawnPoint, Player.transform.rotation, Player.transform);
        StartCoroutine(ExtendAnimation());

    }

    IEnumerator ExtendAnimation()
    {

        GameObject claw = GameObject.Find("Vampiric Grip(Clone)");

        for (float i = 0.5f; i < 1.0f; i += 0.025f)
        {
            claw.transform.localScale = new Vector3(claw.transform.localScale.x, claw.transform.localScale.y, i);
            yield return new WaitForSeconds(.01f);
        }

        for (float i = 1.0f; i > 0; i -= 0.025f)
        {
            claw.transform.localScale = new Vector3(claw.transform.localScale.x, claw.transform.localScale.y, i);
            yield return new WaitForSeconds(.01f);
        }

        Destroy(claw);
        SpellReady = true;
    }

    //fireball spell////////////////////////////////////////////////////////////
    void Castfireball()
    {
        

        Vector3 fireposition = new Vector3(transform.position.x + 0,transform.position.y + yball,transform.position.z + zball);

        //garbage code//Vector3 fireballvelocity = new Vector3(0, 0, transform.position.z * FireballSpeed);

       

        Instantiate(Fireball, fireposition,transform.rotation);

        GameObject fireballclone = GameObject.Find("Fireball(Clone)");

        firebool = true;




        Destroy(fireballclone.gameObject, 5);


    }
    private void FixedUpdate()
    {
        if (firebool)
        {
            GameObject.Find("Fireball(Clone)").GetComponent<Rigidbody>().AddForce(Vector3.forward * FireballSpeed);
            firebool = false;
          
        }


    }
}
    