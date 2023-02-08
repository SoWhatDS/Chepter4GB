using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;

public struct IJobStruct : IJob
{
    public NativeArray<int> NumbersArray;

    public void Execute()
    {
        for (int i = 0; i < NumbersArray.Length; i++)
        {
            if (NumbersArray[i] > 10)
            {
                NumbersArray[i] = 0;
            }
        }
    }
}
