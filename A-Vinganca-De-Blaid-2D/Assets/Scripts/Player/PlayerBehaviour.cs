using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;

    private void Start()
    {
        GameManager.Instance.inputManager.OnJump += HandleJump;
    }

    private void Update()
    {
        float moveDirection = GameManager.Instance.inputManager.Movement * Time.deltaTime * moveSpeed;
        transform.Translate(moveDirection, 0, 0);
    }

    private void HandleJump() {
        Debug.Log("Estou pulando!");
    }
}
