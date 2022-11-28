/***
 * Author : Stu Dent
 * Created: 11-2-22
 * Modified: 
 * Description: Controls weather effects
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class WeatherSys : MonoBehaviour
{
    public GameObject rainGO;
    ParticleSystem rainPS;

    public float rainTime = 10;

    public AudioMixerSnapshot raining;
    public AudioMixerSnapshot sunny;

    float timerTime;
    bool startTime;
    AudioSource audioSrc;

    bool isRaining;
    public bool IsRaining { get { return isRaining; } }

    public Volume rainProcess;


    float lerpValue;
    float lerpDurration = 10;
    float transitionTime;
    

    // Start is called before the first frame update
    void Start()
    {
        rainPS = rainGO.GetComponent<ParticleSystem>();
        audioSrc = rainGO.GetComponent<AudioSource>();

    }//end Start()

    // Update is called once per frame
    void Update()
    {
        if (startTime)
        {
            if(timerTime > 0)
            {
                timerTime -= Time.deltaTime;
                TintSky();
            }
            else
            {
                EndRain();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter Rain");
        
        if(other.tag == "Player")
        {
            if (!startTime)
            {
                timerTime = rainTime;
                startTime = true;
                isRaining = true;
                rainPS.Play();
                audioSrc.Play();
                raining.TransitionTo(2.0f); 
            }//end if(!startTime)
        }//end if(other)
    }//end OnTriggerEnter()


    void EndRain()
    {
        startTime = false;
        isRaining = false;
        rainPS.Stop();
        audioSrc.Stop();
        sunny.TransitionTo(2.0f);
    }//end EndRain()

    void TintSky()
    {
        if(transitionTime < lerpDurration)
        {
            lerpValue = Mathf.Lerp(0, 1, transitionTime / lerpDurration);
            transitionTime += Time.deltaTime;
            rainProcess.weight = lerpValue;
        }
    }



}
