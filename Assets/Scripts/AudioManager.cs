using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Music")]
    public AudioSource musicSource;

    [Header("SFX")]
    public AudioSource sfxSource;
    public AudioClip clickSound;
    public AudioClip equipArmorSound;
    public AudioClip equipAccessorySound;

    void Awake()
    {
        if (instance == null) { instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
    }

    public void PlayClick() => sfxSource.PlayOneShot(clickSound);

    public void PlayEquip(bool isAccessory)
    {
        sfxSource.PlayOneShot(isAccessory ? equipAccessorySound : equipArmorSound);
    }
}