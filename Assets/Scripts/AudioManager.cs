using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Class singleton
    public static AudioManager obj;

    // Audio clips
    public AudioClip jump;
    public AudioClip coin;
    public AudioClip gui;
    public AudioClip hit;
    public AudioClip enemyHit;
    public AudioClip win;

    // Attribute to control de audioclips
    private AudioSource audioSource;

    // Instanciate the class
    private void Awake() {
        obj = this;
        // Save that the component has a audio source component
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayJump() { PlaySound(jump); }
    public void PlayCoin() { PlaySound(coin); }
    public void PlayGui() { PlaySound(gui); }
    public void PlayHit() { PlaySound(hit); }
    public void PlayEnemyHit() { PlaySound(enemyHit); }
    public void PlayWin() { PlaySound(win); }

    private void PlaySound(AudioClip clip) {
        audioSource.PlayOneShot(clip);
    }

    // Destruct the class
    private void OnDestroy() {
        obj = null;
    }
}
