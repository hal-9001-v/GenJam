using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager {
    // Start is called before the first frame update

    public enum Sound {

      WWHITS,
      WWDIES,
      WWDIES2,
      WWISHIT,
      WWISHIT2,
      WWJUMP,
      FANHITS,
      FANDIES,
      PUNCHHITS,
      SWINGSPUNCH,
      SYNTHDIES,
      SYNTHGRUNT,
      SYNTHJUMP,
      SYNTHTHROWS,
      COOKATTACKS,
      COOKDIES,
      SYNTHSHIELDHIT,
      SLEEPAUDIO,
      ILLO,
      ILLODIE,
      ILLOBALLOON,
      BANGSOUND,
      ELECTRICSOUND,
      BULLTHROWS,
      BULLDIES,
      GETVIROTE,
      BALLISTALAUNCH,
      LOADBALLISTA,
      BREAKROPE,
      WRECKINGJUMP,
      WRECKINGFRENZY1,
      WRECKINGFRENZY2,
      WRECKINGFRENZY3,
      WRECKINGDIE,
      DOOR,
    }

    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;
    
    public static void PlaySound(Sound sound, float vol) {

        if (oneShotGameObject == null) {
            oneShotGameObject = new GameObject("OneShotSound");
            oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
        }

        oneShotAudioSource.PlayOneShot(GetAudioClip(sound), vol);
        
    }

    private static AudioClip GetAudioClip(Sound sound) {
        foreach(GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArryay) {

            if (soundAudioClip.sound == sound) {
                return soundAudioClip.audioClip;
            }

        }

        Debug.LogError("Sound" + sound + " not found!");
        return null;
        

    }

}
