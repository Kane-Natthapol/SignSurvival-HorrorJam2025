using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private SoundData _data;
    [SerializeField] private SoundData _dataBGM;
    [SerializeField] private AudioSource _sourceBGMMainmenu;
    [SerializeField] private AudioSource _sourceBGMCountdown;
    [SerializeField] private AudioSource _sourceBGMCutOff;
    [SerializeField] private AudioSource _sourceSFX;
    [SerializeField] private AudioMixer _audioMixer;
    float currentFinalBGMVolume;


    Coroutine _coroutine;
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

    public void SetPlayBGMmainMenu(bool isPlay)
    {
        if (isPlay) _sourceBGMMainmenu.Play();
        else _sourceBGMMainmenu.Stop();
    }

    public void SetPlayBGMCountdown(bool isPlay)
    {
        if(isPlay)
        {
            if (!_sourceBGMCountdown.isPlaying) _sourceBGMCountdown.Play();
        }
        else _sourceBGMCountdown.Pause();
    }

    public void SetPlayBGMCurOff(bool isPlay)
    {
        if (isPlay) _sourceBGMCutOff.Play();
        else _sourceBGMCutOff.Stop();
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

    /*public async void PlayBGMMain(string soundName, bool isFade, float fadeInDuration = 1f, float fadeOutDuration = 1f)
    {
        AudioClip clip = await GetSoundClipAsync(soundName, _dataBGM);

        if (clip != null)
        {
            if (_sourceBGMMain.clip == clip) return;

            if (isFade)
            {
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                    _coroutine = null;
                }
                _coroutine = StartCoroutine(FadeChangeBGM(clip, fadeInDuration, fadeOutDuration));
            }
            else
            {
                _sourceBGMMain.clip = clip;
                _sourceBGMMain.Play();
            }
        }
        else
        {
            Debug.LogError($"Audio clip not found for sound name: {soundName}");
        }
    }

    public void PauseBGMMAin()
    {
        _sourceBGMMain.Stop();
    }

    public void ContinueBGMMain()
    {
        _sourceBGMMain.Play();
    }

    public void FadeOutBGM()
    {
        if (_coroutine != null)
        {
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

        _sourceBGMMain.Stop();
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

        _sourceBGMMain.Stop();
        _sourceBGMMain.clip = newClip;
        _sourceBGMMain.Play();

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

    public async void PlayBGMHand(string soundName, bool isFade, float fadeInDuration = 1f, float fadeOutDuration = 1f)
    {
        AudioClip clip = await GetSoundClipAsync(soundName, _dataBGM);

        if (clip != null)
        {
            if (_sourceBGMHand.clip == clip) return;

            if (isFade)
            {
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                    _coroutine = null;
                }
                _coroutine = StartCoroutine(FadeChangeBGM(clip, fadeInDuration, fadeOutDuration));
            }
            else
            {
                _sourceBGMHand.clip = clip;
                _sourceBGMHand.Play();
            }
        }
        else
        {
            Debug.LogError($"Audio clip not found for sound name: {soundName}");
        }
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
    }*/
}