
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   [Header("-------------Audio Source-------------")]
   [SerializeField] AudioSource musicSource;
   [SerializeField] AudioSource SFXSource;

   [Header("-------------Audio Clip-------------")]
   public AudioClip background;
   public AudioClip death;
   public AudioClip flap;
   public AudioClip hit;
   public AudioClip point;
   public AudioClip swooshing;

   private float volumeLevel = 1f;

  private void Start() {
    musicSource.clip = background;
    musicSource.volume = volumeLevel;
    SFXSource.volume = volumeLevel;
    musicSource.Play();
  } 
  
  public void PlaySFX(AudioClip clip)
  {
    SFXSource.PlayOneShot(clip);
  }

  public void SetVolume(float newVolumeLevel)
    {
        volumeLevel = newVolumeLevel;
        musicSource.volume = volumeLevel;
        SFXSource.volume = volumeLevel;
    }

}
