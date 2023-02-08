using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;

public class Task2Starter : MonoBehaviour
{
    [SerializeField] private int _lengthArray;
    private NativeArray<Vector3> _positions;
    private NativeArray<Vector3> _velocities;
    private NativeArray<Vector3> _finalPositions;

    private JobHandle _jobHandle;

    private void Start()
    {
        _positions = CreateNativeArray(_lengthArray);
        _velocities = CreateNativeArray(_lengthArray);
        _finalPositions = CreateNativeArray(_lengthArray);

        RandomInitializationArrays();

        JobParallelStruct jobParallelStruct = new JobParallelStruct()
        {
            Positions = _positions,
            Velocities = _velocities,
            FinalPositions = _finalPositions
        };

        _jobHandle = jobParallelStruct.Schedule(_lengthArray,5);
        _jobHandle.Complete();
        PrintFinalPositions(_positions,_velocities,_finalPositions);

        Dispose();     
    }

    private NativeArray<Vector3> CreateNativeArray(int length)
    {
        NativeArray<Vector3> array = new NativeArray<Vector3>(length,Allocator.TempJob);
        return array;
    }

    private void RandomInitializationArrays()
    {
        RandomInitializationArray(_positions);
        RandomInitializationArray(_velocities);
    }

    private void RandomInitializationArray(NativeArray<Vector3> array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10));
        }
    }

    private void PrintFinalPositions(NativeArray<Vector3> Positions,NativeArray<Vector3> Velocities,NativeArray<Vector3> FinalPositions)
    {
        for (int i = 0; i < FinalPositions.Length; i++)
        {
            Debug.Log($"Positions: {Positions[i]} + Velocities: {Velocities[i]} = FinalPositions: {FinalPositions[i]}");
        }
    }

    private void Dispose()
    {
        _positions.Dispose();
        _velocities.Dispose();
        _finalPositions.Dispose();
    }
}
