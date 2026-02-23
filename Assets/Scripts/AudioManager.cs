using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Music Settings")]
    public AudioSource musicSource;

    [Header("SFX Settings")]
    public AudioSource sfxSource;
    public AudioClip[] clickSounds;
    public AudioClip equipArmorSound;
    public AudioClip equipAccessorySound;
    public AudioClip applySound;
    public AudioClip TerrariaLogo;

    void Awake()
    {
        instance = this;
    }

    public void PlayClick()
    {
        if (clickSounds.Length > 0)
            sfxSource.PlayOneShot(clickSounds[Random.Range(0, clickSounds.Length)]);
    }

    public void PlayApply()
    {
        if (applySound != null) sfxSource.PlayOneShot(applySound);
    }

    public void PlayLOGO()
    {
        if (TerrariaLogo != null) sfxSource.PlayOneShot(TerrariaLogo);
    }

    public void PlayEquip(bool isAccessory)
    {
        AudioClip clip = isAccessory ? equipAccessorySound : equipArmorSound;
        if (clip != null) sfxSource.PlayOneShot(clip);
    }
}