using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePoolController : SingletonMB<ParticlePoolController>
{
    public List<ParticlePool> listPool;

    private Dictionary<ParticleType, ParticlePool> dict = new Dictionary<ParticleType, ParticlePool>();

    private void Awake()
    {
        foreach (ParticlePool pool in listPool)
        {
            ParticlePool newPool = new ParticlePool(pool.particlePrefab, pool.amount,pool.root);
            dict.Add(pool.type, newPool);
        }
    }
    public void Play(ParticleType type, Vector2 position)
    {
        dict[type].Play(position);
    }

    [Serializable]
    public class ParticlePool
    {
        public Transform root;
        public ParticleType type;
        public ParticleSystem particlePrefab;
        public int amount;

        private List<ParticleSystem> particle_list;

        public ParticlePool(ParticleSystem particlePrefab, int amount, Transform root) {
            this.root = root;
            this.particlePrefab = particlePrefab;

            particle_list = new List<ParticleSystem>();
            for (int i = 0; i < amount; i++) {
                ParticleSystem particle = Instantiate(particlePrefab, root);
                particle_list.Add(particle);
            }
        }
        public void Play(Vector2 pos)
        {
            if (particle_list.Count == 0)
            {
                ParticleSystem newParticle = Instantiate(particlePrefab, root);
                particle_list.Add(newParticle);
            }

            ParticleSystem particle = particle_list.Find(x => x.isPlaying == false);

            if (particle == null) { 
                particle = Instantiate(particlePrefab, root);
                particle_list.Add(particle);
            }

            particle.transform.position = pos;
            particle.Play();
        }
    }
}


