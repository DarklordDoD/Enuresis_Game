using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerTest : MonoBehaviour
{
    [SerializeField] public AudioClip buttonClickSFX;
    [SerializeField] public AudioClip music1;
    [SerializeField] public AudioClip music2;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            MusicManager.Instance.PlaySFX(buttonClickSFX, 1);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            MusicManager.Instance.PlayMusic(music1);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            MusicManager.Instance.PlayMusic(music2);

        if (Input.GetKeyDown(KeyCode.Alpha4))
            MusicManager.Instance.PlayMusicWithFade(music1);

        if (Input.GetKeyDown(KeyCode.Alpha5))
            MusicManager.Instance.PlayMusicWithFade(music2);

        if (Input.GetKeyDown(KeyCode.Alpha6))
            MusicManager.Instance.PlayMusicWithCrossFade(music1, 3.0f);

        if (Input.GetKeyDown(KeyCode.Alpha7))
            MusicManager.Instance.PlayMusicWithCrossFade(music2, 3.0f);
    }
}
