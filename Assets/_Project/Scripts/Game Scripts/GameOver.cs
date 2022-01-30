using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] Game _game;
    private void OnTriggerEnter(Collider other)
    {
        _game.SetDeath();
    }
}
