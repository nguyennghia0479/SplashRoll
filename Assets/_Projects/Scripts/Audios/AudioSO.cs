using UnityEngine;

[CreateAssetMenu(fileName = "Audio - ", menuName = "Scriptable Objects/AudioSO")]
public class AudioSO : ScriptableObject
{
    [SerializeField] private AudioData[] audioList;

    public AudioClip GetRandomClipByType(AudioType type)
    {
        foreach (var audio in audioList)
        {
            if (audio.clips.Length > 0 && audio.type == type)
            {
                AudioClip randomClip = audio.clips[Random.Range(0, audio.clips.Length)];
                return randomClip;
            }
        }

        return null;
    }
}

[System.Serializable]
public struct AudioData
{
    public AudioClip[] clips;
    public AudioType type;
}
