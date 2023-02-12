using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class Galaxy : MonoBehaviour
{
    [Header("Configs for asteroids")]
    [SerializeField] private int numberOfEntities;
    [SerializeField] private float startDistance;
    [SerializeField] private float startVelocity;
    [SerializeField] private float startMass;
    [SerializeField] private float gravitationModifier;

    [SerializeField] private GameObject[] _asteroidsPrefabs;

    private NativeArray<Vector3> positions;
    private NativeArray<Vector3> velocities;
    private NativeArray<Vector3> accelerations;
    private NativeArray<float> masses;

    private TransformAccessArray transformAccessArray;

    private void Start()
    {
        positions = new NativeArray<Vector3>(numberOfEntities,Allocator.Persistent);
        velocities = new NativeArray<Vector3>(numberOfEntities,Allocator.Persistent);
        accelerations = new NativeArray<Vector3>(numberOfEntities,Allocator.Persistent);
        masses = new NativeArray<float>(numberOfEntities,Allocator.Persistent);

        Transform[] transforms = new Transform[numberOfEntities];

        for (int i = 0; i < numberOfEntities; i++)
        {
            positions[i] = Random.insideUnitSphere * Random.Range(0,startDistance);
            velocities[i] = Random.insideUnitSphere * Random.Range(0,startVelocity);
            accelerations[i] = Vector3.zero;
            masses[i] = Random.Range(1, startMass);
            transforms[i] = Instantiate(RandomAsteroidsPrefabs(),positions[i], Quaternion.identity).transform;
        }

        transformAccessArray = new TransformAccessArray(transforms);
    }
    private void Update()
    {
        GravitationJob gravitationJob = new GravitationJob()
        {
            Positions = positions,
            Velocities = velocities,
            Accelerations = accelerations,
            Masses = masses,
            GravitationModifier = gravitationModifier,
            DeltaTime = Time.deltaTime
        };

        JobHandle gravitationHandle = gravitationJob.Schedule(numberOfEntities, 0);

        MoveJob moveJob = new MoveJob()
        {
            Positions = positions,
            Velocities = velocities,
            Accelerations = accelerations,
            DeltaTime = Time.deltaTime
        };

        JobHandle moveHandle = moveJob.Schedule(transformAccessArray,gravitationHandle);

        moveHandle.Complete();
    }

    private void OnDestroy()
    {
        positions.Dispose();
        velocities.Dispose();
        accelerations.Dispose();
        masses.Dispose();
        transformAccessArray.Dispose();
    }

    private GameObject RandomAsteroidsPrefabs()
    {
        GameObject randomAsteroid = _asteroidsPrefabs[Random.Range(0, _asteroidsPrefabs.Length)];
        randomAsteroid.transform.localScale = new Vector3(Random.Range(0.3f, 3), Random.Range(0.3f, 3), Random.Range(0.3f, 3));
        return randomAsteroid;
    }
}
