﻿using System.ServiceModel;

namespace CefSharp.Internals
{
    [ServiceContract(CallbackContract = typeof(ISubProcessCallback))]
    public interface ISubProcessProxy
    {
        [OperationContract]
        void Initialize();

        [OperationContract]
        object EvaluateScript(int frameId, string script, double timeout);

        [OperationContract]
        void Terminate();
    }
}
