using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private SoundData _data;
    [SerializeField] private SoundData _dataBGM;
    [SerializeField] private AudioSource _sourceBGM;
    [SerializeField] private AudioSource _sourceSFX;
    [SerializeField] private AudioMixer _audioMixer;
    float currentFinalBGMVolume;

    [Header("Reverse Input")]
    [SerializeField] private AudioSource _sourceReverseInput;
    Coroutine _reverseInputCoroutine;

    Coroutine _coroutine;
    public bool IsEmpty { get { return _data == null || _sourceBGM == null || _sourceSFX == null; } }
    public bool CanPlayCharacterSound { get; private set; }
    int _isDrawingInt;
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    public async void PlayOneShot(string soundName)
    {
        AudioClip clip = await GetSoundClipAsync(soundName, _data);
        if (clip != null)
        {
            _sourceSFX.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError($"Audio clip not found for sound name: {soundName}");
        }
    }

    public async void PlayBGM(string soundName, bool isFade, float fadeInDuration = 1f, float fadeOutDuration = 1f)
    {
        AudioClip clip = await GetSoundClipAsync(soundName, _dataBGM);

        if (clip != null)
        {
            if (_sourceBGM.clip == clip) return;

            if (isFade)
            {
                if (_coroutine != null)
                {
                    Debug.Log("WETHHH");
                    StopCoroutine(_coroutine);
                    _coroutine = null;
                }
                _coroutine = StartCoroutine(FadeChangeBGM(clip, fadeInDuration, fadeOutDuration));
            }
            else
            {
                _sourceBGM.clip = clip;
                _sourceBGM.Play();
            }
        }
        else
        {
            Debug.LogError($"Audio clip not found for sound name: {soundName}");
        }
    }

    public void PauseBGM()
    {
        _sourceBGM.Stop();
    }

    public void FadeOutBGM()
    {
        if (_coroutine != null)
        {
            Debug.Log("WETHHH");
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        _coroutine = StartCoroutine(FadeOutBGM(1));
    }
    private IEnumerator FadeOutBGM(float fadeInDuration)
    {
        float currentTime = 0f;
        float startVolume;

        _audioMixer.GetFloat("Music_Sound", out startVolume);
        startVolume = Mathf.Pow(10, startVolume / 20f);

        while (currentTime < fadeInDuration)
        {
            currentTime += Time.deltaTime;
            float volume = Mathf.Lerp(startVolume, 0f, currentTime / fadeInDuration);
            SetVolume("Music_Sound", volume);
            yield return null;
        }

        _sourceBGM.Stop();
    }
    private async Task<AudioClip> GetSoundClipAsync(string soundName, SoundData data)
    {
        // Simulate asynchronous loading
        //await Task.Delay(10); // Simulated delay for async loading
        foreach (var sound in data.Sounds)
        {
            if (sound.SoundName == soundName)
            {
                return sound.SoundClip;
            }
        }
        return null;  // Return null if no sound matches the soundName.
    }

    private IEnumerator FadeChangeBGM(AudioClip newClip, float fadeInDuration, float fadeOutDuration)
    {
        float currentTime = 0f;
        float startVolume;

        _audioMixer.GetFloat("Music_Sound", out startVolume);
        startVolume = Mathf.Pow(10, startVolume / 20f);

        // Fade out
        while (currentTime < fadeInDuration)
        {
            currentTime += Time.deltaTime;
            float volume = Mathf.Lerp(startVolume, 0f, currentTime / fadeInDuration);
            SetVolume("Music_Sound", volume);
            yield return null;
        }

        _sourceBGM.Stop();
        _sourceBGM.clip = newClip;
        _sourceBGM.Play();

        currentTime = 0f;
        while (currentTime < fadeOutDuration)
        {
            currentTime += Time.deltaTime;
            float volume = Mathf.Lerp(0f, currentFinalBGMVolume, currentTime / fadeOutDuration);
            SetVolume("Music_Sound", volume);
            yield return null;
        }

        SetVolume("Music_Sound", currentFinalBGMVolume);
    }

    public void MasterVolume(float volume)
    {
        SetVolume("Master_Sound", volume);
    }

    public void MusicVolume(float volume)
    {
        SetVolume("Music_Sound", volume);
        currentFinalBGMVolume = volume;
        //_audioMixer.SetFloat("Music_Sound", Mathf.Log10(volume) * 20);
    }

    public void SFXVolume(float volume)
    {
        SetVolume("SFX_Sound", volume);
        //_audioMixer.SetFloat("SFX_Sound", Mathf.Log10(volume) * 20);
    }

    void SetVolume(string parameterName, float volume)
    {
        if (volume == 0)
        {
            _audioMixer.SetFloat(parameterName, -80f);
        }
        else
        {
            _audioMixer.SetFloat(parameterName, Mathf.Log10(volume) * 20);
        }
    }
    public void SetCharacterSound(bool canPlay)
    {
        CanPlayCharacterSound = canPlay;
    }
    public void PlaySoundReverseInput(bool isPlay)
    {
        if (_reverseInputCoroutine != null) StopCoroutine(_reverseInputCoroutine);
        _reverseInputCoroutine = StartCoroutine(ReverseInputCoroutine(isPlay));
    }
    IEnumerator ReverseInputCoroutine(bool isPlay)
    {
        float startVolume = _sourceReverseInput.volume;
        float targetVolume = isPlay ? 1f : 0f;

        if (isPlay && _sourceReverseInput.isPlaying) yield break; // If already playing, do nothing
        if (!isPlay && !_sourceReverseInput.isPlaying) yield break; // If already stopped, do nothing
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _sourceReverseInput.volume = Mathf.Lerp(startVolume, targetVolume, elapsed / duration);
            yield return null;
        }
        _sourceReverseInput.volume = targetVolume;

        if (isPlay)
        {
            _sourceReverseInput.Play();
        }
        else
        {
            _sourceReverseInput.Stop();
        }
        _reverseInputCoroutine = null;
    }
}
