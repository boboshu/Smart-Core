using System;
using UnityEngine;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Particles Helper")]
    public class ParticlesHelper : RedirectableHelper
    {
        //-------------------------------------------------------------------------------

        private void EnumMain(Action<ParticleSystem.MainModule> action)
        {
            Enum<ParticleSystem>(ps => action(ps.main));
        }

        private void EnumEmission(Action<ParticleSystem.EmissionModule> action)
        {
            Enum<ParticleSystem>(ps => action(ps.emission));
        }

        //-------------------------------------------------------------------------------

        public void SetMaxParticles(int value)
        {
            EnumMain(pm => pm.maxParticles = value);
        }

        public void SetSimulationSpeed(float value)
        {
            EnumMain(pm => pm.simulationSpeed = value);
        }

        public void SetDuration(float value)
        {
            EnumMain(pm => pm.duration = value);
        }

        public void SetGravityModifierMultiplier(float value)
        {
            EnumMain(pm => pm.gravityModifierMultiplier = value);
        }

        public void SetStartSizeMultiplier(float value)
        {
            EnumMain(pm => pm.startSizeMultiplier = value);
        }

        public void SetStartDelayMultiplier(float value)
        {
            EnumMain(pm => pm.startDelayMultiplier = value);
        }

        public void SetStartLifetimeMultiplier(float value)
        {
            EnumMain(pm => pm.startLifetimeMultiplier = value);
        }

        public void SetStartRotationMultiplier(float value)
        {
            EnumMain(pm => pm.startRotationMultiplier = value);
        }

        public void SetStartRotationXMultiplier(float value)
        {
            EnumMain(pm => pm.startRotationXMultiplier = value);
        }

        public void SetStartRotationYMultiplier(float value)
        {
            EnumMain(pm => pm.startRotationYMultiplier = value);
        }

        public void SetStartRotationZMultiplier(float value)
        {
            EnumMain(pm => pm.startRotationZMultiplier = value);
        }

        public void SetStartSizeXMultiplier(float value)
        {
            EnumMain(pm => pm.startSizeXMultiplier = value);
        }

        public void SetStartSizeYMultiplier(float value)
        {
            EnumMain(pm => pm.startSizeYMultiplier = value);
        }

        public void SetStartSizeZMultiplier(float value)
        {
            EnumMain(pm => pm.startSizeZMultiplier = value);
        }

        public void SetStartSpeedMultiplier(float value)
        {
            EnumMain(pm => pm.startSpeedMultiplier = value);
        }

        public void SetRandomizeRotationDirection(float value)
        {
            EnumMain(pm => pm.randomizeRotationDirection = value);
        }

        public void SetPrewarm(bool value)
        {
            EnumMain(pm => pm.prewarm = value);
        }

        public void SetPlayOnAwake(bool value)
        {
            EnumMain(pm => pm.playOnAwake = value);
        }

        public void SetLoop(bool value)
        {
            EnumMain(pm => pm.loop = value);
        }

        public void SetStartSize3D(bool value)
        {
            EnumMain(pm => pm.startSize3D = value);
        }

        public void SetStartRotation3D(bool value)
        {
            EnumMain(pm => pm.startRotation3D = value);
        }

        //-------------------------------------------------------------------------------

        public void SetRateOverTime(float value)
        {
            EnumEmission(pe => pe.rateOverTimeMultiplier = value);
        }

        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------

        public void SetMaterial(Material value)
        {
            Enum<ParticleSystemRenderer>(psr => psr.material = value);
        }

        private void EnumMaterial(Action<Material> action)
        {
            Enum<ParticleSystemRenderer>(psr => action(psr.material));
        }

        //-------------------------------------------------------------------------------

        public void SetMaterialColor(Color value)
        {
            EnumMaterial(m => m.color = value);
        }

        public void SetTintColor(Color value)
        {
            EnumMaterial(m => m.SetColor("_TintColor", value));
        }

        public void SetMaterialOffset(Vector2 value)
        {
            EnumMaterial(m => m.mainTextureOffset = value);
        }

        public void SetMaterialScale(Vector2 value)
        {
            EnumMaterial(m => m.mainTextureScale = value);
        }

        //-------------------------------------------------------------------------------
    }
}