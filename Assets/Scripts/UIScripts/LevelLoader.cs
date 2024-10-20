using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
        public Animator transitionAnimator;
        public static LevelLoader Instance;
        public float transitionTime;

        void Awake() {
            Instance = this;
        }

        public IEnumerator LoadLevel(int buildIndex) {
            transitionAnimator.SetTrigger("Start");

            yield return new WaitForSeconds(transitionTime);

            SceneManager.LoadSceneAsync(buildIndex);
        }
}
