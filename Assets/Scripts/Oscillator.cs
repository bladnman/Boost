using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour {
  Vector3 startingPosition;
  [SerializeField] Vector3 movementVector;
  [SerializeField] [Range(0, 1)] float movementFactor;
  [SerializeField] float period = 2f;

  // Start is called before the first frame update
  void Start() {
    startingPosition = transform.position;
  }

  // Update is called once per frame
  void Update() {
    // if we have a period (above 0)
    if (period > Mathf.Epsilon) {
      UpdatePosition();
    }
  }

  void UpdatePosition() {
    float cycles = Time.time / period;
    const float tau = Mathf.PI * 2;
    float rawSinWave = Mathf.Sin(cycles * tau);

    movementFactor = (rawSinWave + 1f) / 2; // get it 0 -> 1 instead of -1 -> 1

    Vector3 offset = movementFactor * movementVector;
    Vector3 newPosition = transform.position;
    Vector3 movementPosition = startingPosition + offset;

    if (movementVector.x > Mathf.Epsilon) {
      newPosition = new Vector3(movementPosition.x, newPosition.y, newPosition.z);
    }
    if (movementVector.y > Mathf.Epsilon) {
      newPosition = new Vector3(newPosition.x, movementPosition.y, newPosition.z);
    }
    if (movementVector.z > Mathf.Epsilon) {
      newPosition = new Vector3(newPosition.x, newPosition.y, movementPosition.z);
    }

    transform.position = newPosition;
  }
}
