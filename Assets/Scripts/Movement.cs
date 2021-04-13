using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
  [SerializeField] float thrustModifier = 1000f;
  [SerializeField] float rotationModifier = 1f;
  [SerializeField] AudioClip mainEngine;
  [SerializeField] ParticleSystem thrustParticles;
  [SerializeField] ParticleSystem rotateParticlesLeft;
  [SerializeField] ParticleSystem rotateParticlesRight;
  Rigidbody rb;
  AudioSource audioSource;

  void Start() {
    rb = GetComponent<Rigidbody>();
    audioSource = GetComponent<AudioSource>();
  }

  void Update() {
    ProcessInput();
    ProcessRotation();
  }

  void ProcessInput() {
    if (Input.GetKey(KeyCode.Space)) {
      rb.AddRelativeForce(Vector3.up * Time.deltaTime * thrustModifier);
      if (!audioSource.isPlaying) {
        audioSource.PlayOneShot(mainEngine);
        thrustParticles.Play();
      }
    } else {
      if (audioSource.isPlaying) {
        audioSource.Stop();
        thrustParticles.Stop();
      }
    }
  }
  void ProcessRotation() {
    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
      ApplyRotate(rotationModifier);
      if (!rotateParticlesRight.isPlaying) {
        rotateParticlesRight.Play();
      }
    } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
      ApplyRotate(-rotationModifier);
      if (!rotateParticlesLeft.isPlaying) {
        rotateParticlesLeft.Play();
      }
    } else {
      rotateParticlesLeft.Stop();
      rotateParticlesRight.Stop();
    }
  }

  void ApplyRotate(float rotationThisFrame) {
    rb.freezeRotation = true;
    transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
    rb.freezeRotation = false;
  }
}
