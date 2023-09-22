using System;
using System.Collections;
using Assets.Interface;
using UnityEngine;

namespace Assets.TeachingLevels.Cases
{
    public abstract class TeachingCase
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private WaitUntil _waitUntil;

        public TeachingCase(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        protected void EnterWaitUntilCase(Func<bool> temp)
        {
            _waitUntil = new WaitUntil(temp);
            _coroutineRunner.StartCoroutine(StartWaitUntilCaseCoroutine(_waitUntil));
        }

        private IEnumerator StartWaitUntilCaseCoroutine(WaitUntil waitUntil)
        {
            yield return _waitUntil;
        }
    }
}