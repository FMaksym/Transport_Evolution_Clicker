using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class UnityWebRequestExtensions
{
    public static TaskAwaiter<UnityWebRequestAsyncOperation> GetAwaiter(this UnityWebRequestAsyncOperation webAsyncOperation)
    {
        var taskCompletionSource = new TaskCompletionSource<UnityWebRequestAsyncOperation>();
        webAsyncOperation.completed += asyncOperation => taskCompletionSource.SetResult(webAsyncOperation);
        return taskCompletionSource.Task.GetAwaiter();
    }
}
