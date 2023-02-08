using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;

public class Task1Starter : MonoBehaviour
{
    [SerializeField] private int _arrayLength;

    private NativeArray<int> _numbersArray;
    private JobHandle _jobHandle;

    private void Start()
    {
        _numbersArray = new NativeArray<int>(_arrayLength,Allocator.TempJob);
        RandomNumbersInitialization();

        IJobStruct jobStruct = new IJobStruct()
        {
            NumbersArray = _numbersArray
        };
        JobHandle jobHandle = jobStruct.Schedule();
        jobHandle.Complete();
        PrintNumbersInArray();
        _numbersArray.Dispose();
    }

    private void RandomNumbersInitialization()
    {
        for (int i = 0; i < _numbersArray.Length; i++)
        {
            _numbersArray[i] = Random.Range(0, 15);
        }
    }

    private void PrintNumbersInArray()
    {
        for (int i = 0; i < _numbersArray.Length; i++)
        {
            Debug.Log(_numbersArray[i]);
        }
    }

}
