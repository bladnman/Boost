using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {
  [SerializeField] float levelLoadDelay = 2.0f;
  [SerializeField] AudioClip deathExplosion;
  [SerializeField] AudioClip success;
  [SerializeField] ParticleSystem deathParticles;
  [SerializeField] ParticleSystem successParticles;
  AudioSource audioSource;

  bool isTransitioning = false;
  bool isCollisionEnabled = true;

  private void Start() {
    audioSource = GetComponent<AudioSource>();
  }

  void OnCollisionEnter(Collision other) {
    if (isTransitioning) return;

    switch (other.gameObject.tag) {
      case "Friendly":
        break;
      case "Finish":
        StartSuccess();
        break;
      default:
        StartCrash();
        break;
    }
  }

  void StartSuccess() {
    isTransitioning = true;

    audioSource.Stop();
    audioSource.PlayOneShot(success);

    successParticles.Play();

    GetComponent<Movement>().enabled = false;
    Invoke("LoadNextLevel", levelLoadDelay);
  }
  void StartCrash() {
    if (!isCollisionEnabled) return;
    isTransitioning = true;

    audioSource.Stop();
    audioSource.PlayOneShot(deathExplosion);

    deathParticles.Play();

    GetComponent<Movement>().enabled = false;
    Invoke("ReloadLevel", levelLoadDelay);
  }
  void ReloadLevel() {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
  }
  void LoadNextLevel() {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex = currentSceneIndex + 1;
    if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings) {
      nextSceneIndex = 0;
    }
    SceneManager.LoadScene(nextSceneIndex);
  }
  void Update() {
    if (isCollisionEnabled && Input.GetKeyDown(KeyCode.C)) {
      Debug.Log("No more crashes!");
      isCollisionEnabled = false;
    }
    if (Input.GetKeyDown(KeyCode.L)) {
      LoadNextLevel();
    }
  }
}
