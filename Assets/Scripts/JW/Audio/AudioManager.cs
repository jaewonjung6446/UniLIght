using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum BGMAudioPlayList
{
    StartBack = 0,
    TutorialBack = 1,
    Day1Back = 2,
    Day2Back = 3
}
public enum SFXPlayList
{
    Click = 0
}
public class AudioManager : MonoBehaviour
{
    BGMAudioPlayList BGMAudioPlayList;
    SFXPlayList SFXPlayList;
    private AudioSource audioSource;
    private List<AudioSource> plays;
    [SerializeField] private List<AudioClip> SFXList;
    [SerializeField] private List<AudioClip> BGMList;

    void Awake()
    {
        // Ensure this object persists across scenes
        // Add an AudioSource component if it doesn't exist
        //audioSource = gameObject.AddComponent<AudioSource>();
        //audioSource.loop = true; // Set to loop the music
        //audioSource.playOnAwake = false; // Prevent auto-play
        plays = new List<AudioSource>();
        GenAS();
    }
    private void Start()
    {
        BGMPlayMusic((int)BGMAudioPlayList.StartBack);
    }
    // Function to play the music
    public void BGMPlayMusic(int index)
    {
        BGMChangeMusic(index);
        Play(true);
    }
    public void SFXPlayMusic(int index)
    {
        SFXChangeMusic(index);
        Play(false);
    }
    public void Play(bool isloop)
    {
        GetAS().loop = isloop;
        if (GetAS() != null && !GetAS().isPlaying)
        {
            GetAS().Play();
        }
    }

    // Function to pause the music
    public void PauseMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    // Function to stop the music
    public void StopMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    // Function to set the volume
    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            GetAS().volume = Mathf.Clamp(volume, 0f, 1f);
        }
    }
    public void BGMChangeMusic(int index)
    {
        GetAS().clip = BGMList[index];
    }
    public void SFXChangeMusic(int index)
    {
        Debug.Log("효과음 출력 인덱스  ="+index);
        GetAS().clip = SFXList[index];
    }
    public void GenAS()
    {
        for (int i = 0; i < 10; i++)
        {
            plays.Add(this.gameObject.AddComponent<AudioSource>());
        }
    }
    public AudioSource GetAS()
    {
        for (int i = 0; i < plays.Count; i++)
        {
            if (!plays[i].isPlaying)
            {
                return plays[i];
            }
        }
        AudioSource added = gameObject.AddComponent<AudioSource>();
        return added;
    }
}
