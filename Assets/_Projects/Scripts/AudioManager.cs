using UnityEngine;

public enum AudioType
{
    Button, Painted, Completed
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSO audioSO;
    [SerializeField] private float pitchClamp = .1f;

    private AudioSource audioSource;
    private float minPitch;
    private float maxPitch;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        minPitch = audioSource.pitch - pitchClamp;
        maxPitch = audioSource.pitch + pitchClamp;
    }

    private void OnEnable()
    {
        UIEvents.OnButtonClicked += HandleButtonClicked;
        GameEvents.OnCellPainted += HandlePlayCellPainted;
        GameEvents.OnLevelCompleted += HandleLevelCompleted;
    }

    private void OnDisable()
    {
        UIEvents.OnButtonClicked -= HandleButtonClicked;
        GameEvents.OnCellPainted -= HandlePlayCellPainted;
        GameEvents.OnLevelCompleted -= HandleLevelCompleted;
    }

    private void HandleButtonClicked() => PlayAudio(AudioType.Button);
    private void HandlePlayCellPainted() => PlayAudio(AudioType.Painted);
    private void HandleLevelCompleted(bool canLoadNextLevel, ResultData resultData) => PlayAudio(AudioType.Completed);

    private void PlayAudio(AudioType type)
    {
        AudioClip randomClip = audioSO.GetRandomClipByType(type);
        if (randomClip == null)
        {
            Debug.Log("Don't have any clips!");
            return;
        }

        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.PlayOneShot(randomClip);
    }
}
