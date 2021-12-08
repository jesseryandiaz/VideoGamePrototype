using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelScript : MonoBehaviour
{
    bool inEthereal; //true for ethereal, false for physical

    //All the objects controlled by this script are assigned in the inspector.
    public GameObject audioMusic;
    public Light etherealLight;
    public Light physicalLight;
    public Light etherealEnemyLight;
    public Light physicalEnemyLight;
    public GameObject bkgd;
    public GameObject bkgd2;
    public GameObject bkgd3;
    public GameObject bkgd4;
    public Camera playerCamera;
    public ParticleSystem fog;
    public GameObject enemyEthereal;
    public GameObject enemyPhysical;
    public GameObject arena;

    void Start()
    {
        inEthereal = false;
        playerCamera.cullingMask = 503;
        //Debug.Log(playerCamera.cullingMask);
    }

    //This function is called by the player script, when it is called, all realm switch actions will be performed
    public void SwitchRealms()
    {
        inEthereal = !inEthereal;
        if (inEthereal)
        {
            //Audio
            audioMusic.GetComponent<AudioLowPassFilter>().enabled = true;
            audioMusic.GetComponent<AudioDistortionFilter>().enabled = true;
            audioMusic.GetComponent<AudioReverbFilter>().enabled = true;
            audioMusic.GetComponent<AudioSource>().volume = 0.2f;
            //Lights
            etherealLight.enabled = true;
            physicalLight.enabled = false;
            etherealEnemyLight.enabled = true;
            physicalEnemyLight.enabled = false;
            //bkgd
            bkgd.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 0.3f);
            bkgd.GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 0.1f);
            bkgd2.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 0.3f);
            bkgd2.GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 0.1f);
            bkgd3.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 0.3f);
            bkgd3.GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 0.1f);
            bkgd4.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 0.3f);
            bkgd4.GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 0.1f);
            //Fog
            playerCamera.cullingMask = 2147483647;
            //Enemies
            enemyEthereal.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("body").GetComponent<SkinnedMeshRenderer>().enabled = false;
            GameObject.Find("eye").GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
        else //inPhysical
        {
            //Audio
            audioMusic.GetComponent<AudioLowPassFilter>().enabled = false;
            audioMusic.GetComponent<AudioDistortionFilter>().enabled = false;
            audioMusic.GetComponent<AudioReverbFilter>().enabled = false;
            audioMusic.GetComponent<AudioSource>().volume = 0.5f;
            //Lights
            etherealLight.enabled = false;
            physicalLight.enabled = true;
            etherealEnemyLight.enabled = false;
            physicalEnemyLight.enabled = true;
            //bkgd
            bkgd.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 0.0f);
            bkgd.GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 0.5f);
            bkgd2.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 0.0f);
            bkgd2.GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 0.5f);
            bkgd3.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 0.0f);
            bkgd3.GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 0.5f);
            bkgd4.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", 0.0f);
            bkgd4.GetComponent<MeshRenderer>().material.SetFloat("_Metallic", 0.5f);
            //Fog
            playerCamera.cullingMask = 503;
            //Enemies
            enemyEthereal.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("body").GetComponent<SkinnedMeshRenderer>().enabled = true;
            GameObject.Find("eye").GetComponent<SkinnedMeshRenderer>().enabled = true;
        }
    }
    //Let other scripts check this variable
    public bool getInEthereal()
    {
        return inEthereal;
    }
}