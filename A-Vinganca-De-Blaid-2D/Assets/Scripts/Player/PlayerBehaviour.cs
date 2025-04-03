using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private InputManager inputManager;
    [SerializeField] private float moveSpeed = 5;

    void Start()
    {
        inputManager = new InputManager();
    }

    private void Update()
    {
        float moveDirection = inputManager.Movement * Time.deltaTime * moveSpeed;
        transform.Translate(moveDirection, 0, 0);
    }
}
