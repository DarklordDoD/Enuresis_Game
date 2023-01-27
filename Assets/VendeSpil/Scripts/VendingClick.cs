using UnityEngine;
using System.Collections;

public class VendingClick : MonoBehaviour
{
    public AudioClip CardFlip1;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();


    }

    void OnMouseOver()
    {

        if (Input.GetMouseButtonDown(0))
        {
            audioSource.enabled = true;
            if (!audioSource.isPlaying)
            {
                audioSource.clip = CardFlip1;
                audioSource.Play();
            }
        }






    }

}
