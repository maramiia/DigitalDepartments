using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public GameObject cubePiecePrefab;
    public float explodeForce = 500f;
    public AudioClip destructionClip; // New

    [SerializeField] TextMeshProUGUI textScore;

    void Start()
    {
        textScore.text = "Score: " + Progress.Instance.PlayerInfo.Score.ToString();   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            ExplodeCube();
            AddScore();
        }
    }

    

    private void AddScore()
    {
        Progress.Instance.PlayerInfo.Score += 10;
        Progress.Instance.SaveScore();
        textScore.text = "Score: " + Progress.Instance.PlayerInfo.Score.ToString();
        
        if (Progress.Instance.PlayerInfo.Score >= 100)
        {
            Progress.Instance.ResetScore(); // Обнуляем очки перед перезапуском
            SceneManager.LoadScene("7workshop"); // Перезапуск сцены
        }
    }

   private void ExplodeCube()
    {
        AudioSource.PlayClipAtPoint(destructionClip, transform.position); // New

        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int z = 0; z < 4; z++)
                {
                    Vector3 piecePosition = transform.position + new Vector3(x, y, z) * 0.5f;
                    GameObject piece = Instantiate(cubePiecePrefab, piecePosition, Quaternion.identity);
                    Rigidbody pieceRigidbody = piece.GetComponent<Rigidbody>();
                    pieceRigidbody.AddExplosionForce(explodeForce, transform.position, 5f);
                }
            }
        }

        Destroy(gameObject);
    }
}

