using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource BackgroundMusic;
    public AudioSource EffectMusic;

    //0.Hit
    //1.Jump
    //2.Shoot
    //3.Burst
    //4.Explosion
    //5.DameTaken
    //6.Dead

    public List<AudioClip> AudioClipList;
    void Start()
    {
        
    }

    public void BossMusic(AudioClip clip)
    {
        BackgroundMusic.clip = clip;
        BackgroundMusic.Play();
    }

    public void HurtSound()
    {
        EffectMusic.clip = AudioClipList[5];
        EffectMusic.Play();
    }

    public void ExplosionSound()
    {
        EffectMusic.clip = AudioClipList[4];
        EffectMusic.Play();
    }

    public void BurstSound()
    {
        EffectMusic.clip = AudioClipList[3];
        EffectMusic.Play();
    }

    public void ShootSound()
    {
        EffectMusic.clip = AudioClipList[2];
        EffectMusic.Play();
    }


    public void HitSound()
    {
        EffectMusic.clip = AudioClipList[0];
        EffectMusic.Play();
    }

    public void JumpSound()
    {
        EffectMusic.clip = AudioClipList[1];
        EffectMusic.Play();
    }

    public void DeadSound()
    {
        EffectMusic.clip = AudioClipList[6];
        EffectMusic.Play();
    }
}
