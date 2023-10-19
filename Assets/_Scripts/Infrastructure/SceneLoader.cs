using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        public SceneLoader(ICoroutineRunner coroutineRunner) => _coroutineRunner = coroutineRunner;

        public void Load(int sceneIndex, Action onLoaded = null) =>
            _coroutineRunner.StartCoroutine(LoadScene(sceneIndex, onLoaded));
        public IEnumerator LoadScene(int sceneIndex, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().buildIndex == sceneIndex)
            {
                onLoaded?.Invoke();
                yield break;
            }
            
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(sceneIndex);

            while (!waitNextScene.isDone)
                yield return null;
            
            onLoaded?.Invoke();
        }
    }
}