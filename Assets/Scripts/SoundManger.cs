using UnityEngine;

public class SoundManger : MonoBehaviour
{
    [SerializeField] AudioSource InstrAudioSrc;
    [SerializeField] AudioClip[] clips;
    public void PlayInstrClip(int index)
    {
        if (clips.Length > 0 && index < clips.Length)
            InstrAudioSrc.clip = clips[index];
        InstrAudioSrc.Play();
    }

    public void StopSound()
    {
        InstrAudioSrc.Stop();
    }
}
