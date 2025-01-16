using UnityEngine;

public class GravityPuzzle : MonoBehaviour
{
    [SerializeField] private GameObject door;     // The door to be moved
    [SerializeField] private float doorMoveSpeed = 2f; // Speed of the door's movement
    [SerializeField] private float doorMaxHeight = 5f; // Maximum height the door should reach

    private bool isTriggered = false;            // To check if the puzzle is solved
    private Vector3 doorStartPosition;           // Store the door's starting position

    private void Start()
    {
        // Save the door's initial position
        doorStartPosition = door.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the PuzzleCube
        if (other.CompareTag("PuzzleCube"))
        {
            isTriggered = true; // Puzzle is solved
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset the puzzle if the cube is removed from the trigger
        if (other.CompareTag("PuzzleCube"))
        {
            isTriggered = false;
        }
    }

    private void Update()
    {
        // Move the door upwards if the puzzle is solved
        if (isTriggered)
        {
            Vector3 targetPosition = doorStartPosition + new Vector3(0, doorMaxHeight, 0);
            door.transform.position = Vector3.MoveTowards(door.transform.position, targetPosition, doorMoveSpeed * Time.deltaTime);
        }
        else
        {
            // Move the door back to its starting position when the puzzle is reset
            door.transform.position = Vector3.MoveTowards(door.transform.position, doorStartPosition, doorMoveSpeed * Time.deltaTime);
        }
    }
}
