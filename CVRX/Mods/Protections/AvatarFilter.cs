using UnityEngine;

namespace CVRX.Mods.Protections
{
    internal class AvatarFilter
    {
        public static void AdjustAvatar(GameObject gameObject)
        {
            gameObject.SetActive(false);

            AudioSource[] AudioSources = gameObject.GetComponentsInChildren<AudioSource>(true);
            HandleAudios(AudioSources);

            Light[] Lights = gameObject.GetComponentsInChildren<Light>(true);
            HandleLights(Lights);

            ParticleSystem[] Particles = gameObject.GetComponentsInChildren<ParticleSystem>(true);
            HandleParticles(Particles);

            Animator[] Animators = gameObject.GetComponentsInChildren<Animator>(true);
            HandleAnimators(Animators);

            Collider[] Colliders = gameObject.GetComponentsInChildren<Collider>(true);
            HandleColliders(Colliders);

            BoxCollider[] BoxColliders = gameObject.GetComponentsInChildren<BoxCollider>(true);
            HandleColliders(BoxColliders);

            CapsuleCollider[] CapsuleColliders = gameObject.GetComponentsInChildren<CapsuleCollider>(true);
            HandleColliders(CapsuleColliders);

            SphereCollider[] SphereColliders = gameObject.GetComponentsInChildren<SphereCollider>(true);
            HandleColliders(SphereColliders);

            Renderer[] Renderers = gameObject.GetComponentsInChildren<Renderer>(true);
            HandleRenderers(Renderers);

            SkinnedMeshRenderer[] MeshRenderers = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>(true);
            HandleMeshRenderers(MeshRenderers);

            Rigidbody[] Rigidbodys = gameObject.GetComponentsInChildren<Rigidbody>(true);
            HandleRigidbodys(Rigidbodys);

            gameObject.SetActive(true);
        }
        private static void HandleAudios(AudioSource[] Audios)
        {
            int Count = 30;
            if (Audios.Length > Count)
            {
                for (int i = Count; i < Audios.Length; i++)
                {
                    GameObject.DestroyImmediate(Audios[i], true);
                }
                XConsole.Log("Protection", $"Avatar with Overflow of {Audios} {Audios.Length}");
            }
        }
        private static void HandleLights(Light[] Lights)
        {
            int Count = 20;
            if (Lights.Length > Count)
            {
                for (int i = Count; i < Lights.Length; i++)
                {
                    GameObject.DestroyImmediate(Lights[i], true);
                }
                XConsole.Log("Protection", $"Avatar with Overflow of {Lights} {Lights.Length}");
            }
        }

        private static void HandleParticles(ParticleSystem[] Particles)
        {
            int Count = 90;

            if (Particles.Length > Count)
            {
                for (int i = Count; i < Particles.Length; i++)
                {
                    GameObject.DestroyImmediate(Particles[i], true);
                }
                XConsole.Log("Protection", $"Avatar with Overflow of {Particles} {Particles.Length}");
            }
        }

        private static void HandleAnimators(Animator[] Animators)
        {
            int Count = 120;

            if (Animators.Length > Count)
            {
                for (int i = Count; i < Animators.Length; i++)
                {
                    GameObject.DestroyImmediate(Animators[i], true);
                }
                XConsole.Log("Protection", $"Avatar with Overflow of {Animators} {Animators.Length}");
            }
        }

        private static void HandleColliders(Collider[] Colliders)
        {
            int Count = 50;

            if (Colliders.Length > Count)
            {
                for (int i = Count; i < Colliders.Length; i++)
                {
                    GameObject.DestroyImmediate(Colliders[i], true);
                }
                XConsole.Log("Protection", $"Avatar with Overflow of {Colliders} {Colliders.Length}");
            }

            foreach (Collider collider in Colliders)
            {
                if (collider == null) continue;
                if (Wrappers.UnityWrappers.IsBadPosition(collider.transform.position) || Wrappers.UnityWrappers.IsBadRotation(collider.transform.rotation)) GameObject.DestroyImmediate(collider, true);
            }
        }

        private static void HandleRenderers(Renderer[] Renderers)
        {
            int Count = 350;

            if (Renderers.Length > Count)
            {
                for (int i = Count; i < Renderers.Length; i++)
                {
                    GameObject.DestroyImmediate(Renderers[i], true);
                }
                XConsole.Log("Protection", $"Avatar with Overflow of {Renderers} {Renderers.Length}");
            }
        }

        private static void HandleMeshRenderers(SkinnedMeshRenderer[] Renderers)
        {
            int Count = 45;

            if (Renderers.Length > Count)
            {
                for (int i = Count; i < Renderers.Length; i++)
                {
                    GameObject.DestroyImmediate(Renderers[i], true);
                }
                XConsole.Log("Protection", $"Avatar with Overflow of {Renderers} {Renderers.Length}");
            }

            foreach (SkinnedMeshRenderer renderer in Renderers)
            {
                if (renderer == null) continue;
                renderer.updateWhenOffscreen = false;
                renderer.sortingOrder = 0;
                renderer.sortingLayerID = 0;
                renderer.rendererPriority = 0;
            }
        }

        private static void HandleRigidbodys(Rigidbody[] Rigidbodys)
        {
            int Count = 30;

            if (Rigidbodys.Length > Count)
            {
                for (int i = Count; i < Rigidbodys.Length; i++)
                {
                    GameObject.DestroyImmediate(Rigidbodys[i], true);
                }
                XConsole.Log("Protection", $"Avatar with Overflow of {Rigidbodys} {Rigidbodys.Length}");
            }
        }
    }
}
