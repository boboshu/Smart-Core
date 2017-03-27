using System;
using Smart.Extensions;
using UnityEngine;

namespace Smart.Helpers
{
    [AddComponentMenu("Smart/Helpers/Particles Helper")]
    public class ParticlesHelper : RedirectableHelper
    {
        //-------------------------------------------------------------------------------

        private void WithParticles(Action<ParticleSystem.MainModule> action)
        {
            var ps = TargetGameObject?.GetComponent<ParticleSystem>();
            if (ps) action.Invoke(ps.main);
        }

        private void WithParticles_InChilds(Action<ParticleSystem.MainModule> action)
        {
            TargetGameObject?.GetComponentsInChildren<ParticleSystem>().Do(ps => action.Invoke(ps.main));
        }

        //-------------------------------------------------------------------------------

        public void SetMaxParticles(int value)
        {
            WithParticles(p => p.maxParticles = value);
        }

        public void SetSimulationSpeed(float value)
        {
            WithParticles(p => p.simulationSpeed = value);
        }

        public void SetDuration(float value)
        {
            WithParticles(p => p.duration = value);
        }

        public void SetGravityModifierMultiplier(float value)
        {
            WithParticles(p => p.gravityModifierMultiplier = value);
        }

        public void SetStartSizeMultiplier(float value)
        {
            WithParticles(p => p.startSizeMultiplier = value);
        }

        public void SetStartDelayMultiplier(float value)
        {
            WithParticles(p => p.startDelayMultiplier = value);
        }

        public void SetStartLifetimeMultiplier(float value)
        {
            WithParticles(p => p.startLifetimeMultiplier = value);
        }

        public void SetStartRotationMultiplier(float value)
        {
            WithParticles(p => p.startRotationMultiplier = value);
        }

        public void SetStartRotationXMultiplier(float value)
        {
            WithParticles(p => p.startRotationXMultiplier = value);
        }

        public void SetStartRotationYMultiplier(float value)
        {
            WithParticles(p => p.startRotationYMultiplier = value);
        }

        public void SetStartRotationZMultiplier(float value)
        {
            WithParticles(p => p.startRotationZMultiplier = value);
        }

        public void SetStartSizeXMultiplier(float value)
        {
            WithParticles(p => p.startSizeXMultiplier = value);
        }

        public void SetStartSizeYMultiplier(float value)
        {
            WithParticles(p => p.startSizeYMultiplier = value);
        }

        public void SetStartSizeZMultiplier(float value)
        {
            WithParticles(p => p.startSizeZMultiplier = value);
        }

        public void SetStartSpeedMultiplier(float value)
        {
            WithParticles(p => p.startSpeedMultiplier = value);
        }

        public void SetRandomizeRotationDirection(float value)
        {
            WithParticles(p => p.randomizeRotationDirection = value);
        }

        public void SetPrewarm(bool value)
        {
            WithParticles(p => p.prewarm = value);
        }

        public void SetPlayOnAwake(bool value)
        {
            WithParticles(p => p.playOnAwake = value);
        }

        public void SetLoop(bool value)
        {
            WithParticles(p => p.loop = value);
        }

        public void SetStartSize3D(bool value)
        {
            WithParticles(p => p.startSize3D = value);
        }

        public void SetStartRotation3D(bool value)
        {
            WithParticles(p => p.startRotation3D = value);
        }

        //-------------------------------------------------------------------------------

        public void SetMaxParticles_InChilds(int value)
        {
            WithParticles_InChilds(p => p.maxParticles = value);
        }

        public void SetSimulationSpeed_InChilds(float value)
        {
            WithParticles_InChilds(p => p.simulationSpeed = value);
        }

        public void SetDuration_InChilds(float value)
        {
            WithParticles_InChilds(p => p.duration = value);
        }

        public void SetGravityModifierMultiplier_InChilds(float value)
        {
            WithParticles_InChilds(p => p.gravityModifierMultiplier = value);
        }

        public void SetStartSizeMultiplier_InChilds(float value)
        {
            WithParticles_InChilds(p => p.startSizeMultiplier = value);
        }

        public void SetStartDelayMultiplier_InChilds(float value)
        {
            WithParticles_InChilds(p => p.startDelayMultiplier = value);
        }

        public void SetStartLifetimeMultiplier_InChilds(float value)
        {
            WithParticles_InChilds(p => p.startLifetimeMultiplier = value);
        }

        public void SetStartRotationMultiplier_InChilds(float value)
        {
            WithParticles_InChilds(p => p.startRotationMultiplier = value);
        }

        public void SetStartRotationXMultiplier_InChilds(float value)
        {
            WithParticles_InChilds(p => p.startRotationXMultiplier = value);
        }

        public void SetStartRotationYMultiplier_InChilds(float value)
        {
            WithParticles_InChilds(p => p.startRotationYMultiplier = value);
        }

        public void SetStartRotationZMultiplier_InChilds(float value)
        {
            WithParticles_InChilds(p => p.startRotationZMultiplier = value);
        }

        public void SetStartSizeXMultiplier_InChilds(float value)
        {
            WithParticles_InChilds(p => p.startSizeXMultiplier = value);
        }

        public void SetStartSizeYMultiplier_InChilds(float value)
        {
            WithParticles_InChilds(p => p.startSizeYMultiplier = value);
        }

        public void SetStartSizeZMultiplier_InChilds(float value)
        {
            WithParticles_InChilds(p => p.startSizeZMultiplier = value);
        }

        public void SetStartSpeedMultiplier_InChilds(float value)
        {
            WithParticles_InChilds(p => p.startSpeedMultiplier = value);
        }

        public void SetRandomizeRotationDirection_InChilds(float value)
        {
            WithParticles_InChilds(p => p.randomizeRotationDirection = value);
        }

        public void SetPrewarm_InChilds(bool value)
        {
            WithParticles_InChilds(p => p.prewarm = value);
        }

        public void SetPlayOnAwake_InChilds(bool value)
        {
            WithParticles_InChilds(p => p.playOnAwake = value);
        }

        public void SetLoop_InChilds(bool value)
        {
            WithParticles_InChilds(p => p.loop = value);
        }

        public void SetStartSize3D_InChilds(bool value)
        {
            WithParticles_InChilds(p => p.startSize3D = value);
        }

        public void SetStartRotation3D_InChilds(bool value)
        {
            WithParticles_InChilds(p => p.startRotation3D = value);
        }

        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------

        public void SetMaterial(Material value)
        {
            var psr = TargetGameObject?.GetComponent<ParticleSystemRenderer>();
            if (psr) psr.material = value;
        }

        public void SetMaterial_InChilds(Material value)
        {
            TargetGameObject?.GetComponentsInChildren<ParticleSystemRenderer>(true).Do(psr => psr.material = value);
        }

        //-------------------------------------------------------------------------------

        private void WithMaterial(Action<Material> action)
        {
            var mat = TargetGameObject?.GetComponent<ParticleSystemRenderer>()?.material;
            if (mat) action.Invoke(mat);
        }

        private void WithMaterial_InChilds(Action<Material> action)
        {
            TargetGameObject?.GetComponentsInChildren<ParticleSystemRenderer>(true).Do(psr =>
            {
                var mat = psr.material;
                if (mat) action.Invoke(mat);
            });
        }

        //-------------------------------------------------------------------------------

        public void SetMaterialColor(Color value)
        {
            WithMaterial(m => m.color = value);
        }

        //-------------------------------------------------------------------------------

        public void SetMaterialColor_InChilds(Color value)
        {
            WithMaterial_InChilds(m => m.color = value);
        }

        //-------------------------------------------------------------------------------
    }
}