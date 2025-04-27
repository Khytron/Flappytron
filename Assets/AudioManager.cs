
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

  private void Start() {
    musicSource.clip = background;
    musicSource.Play();
  } 
  
  public void PlaySFX(AudioClip clip)
  {
    SFXSource.PlayOneShot(clip);
  }

}
