using UnityEngine;
using System.Collections.Generic;

public static class DestructionAnimation
{
    public static void DestroyEffects(List<MeshRenderer> myObject)
    {
        if (myObject == null) return;
        foreach (MeshRenderer mesh in myObject)
        {
            mesh.enabled = false;
        }
    }
    public static void DestroyEffects(AudioSource audio)
    {
        audio.Play();
    }

    public static void DestroyEffects(ParticleSystem particles)
    {
        particles.Play();
    }

    public static void DestroyEffects(AudioSource audio, ParticleSystem particles)
    {
        DestroyEffects(audio);
        DestroyEffects(particles);
    }

    public static void DestroyEffects(List<MeshRenderer> myObject, AudioSource audio)
    {
        DestroyEffects(audio);
        DestroyEffects(myObject);
    }

    public static void DestroyEffects(List<MeshRenderer> myObject, ParticleSystem particles)
    {
        DestroyEffects(particles);
        DestroyEffects(myObject);
    }

    public static void DestroyEffects(List<MeshRenderer> myObject, AudioSource audio, ParticleSystem particles)
    {
        DestroyEffects(audio, particles);
        DestroyEffects(myObject);
    }




}
