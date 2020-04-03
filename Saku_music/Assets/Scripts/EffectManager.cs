using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : SingletonMonoBehaviour<EffectManager> {

    public ParticleSystem[] particles;

    public void PlayEffect(int num)
    {
        particles[num].Play(true);
    }
}
