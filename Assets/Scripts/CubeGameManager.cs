using UnityEngine;
using TMPro;

public class CubeGameManager : MonoBehaviour
{
    public GameObject[] cubes;
    public GameObject winText;
    public GameObject loseText;
    public GameObject nameText;
    public GameObject restartButton;
    public TMP_Text scoreText;
    
    private int correctCubeIndex;
    private int score = 0;
    private Vector3[] initialPositions; // Массив для хранения начальных позиций кубов

    void Start()
    {
        // Сохраняем начальные позиции кубов
        initialPositions = new Vector3[cubes.Length];
        for (int i = 0; i < cubes.Length; i++)
        {
            initialPositions[i] = cubes[i].transform.position;
        }

        winText.SetActive(false);
        loseText.SetActive(false);
        restartButton.SetActive(false);
        UpdateScore();
        ChooseRandomCube();
    }

    void ChooseRandomCube()
    {
        correctCubeIndex = Random.Range(0, 3); // Выбирам правильный куб
    }

    public void OnButtonClick(int buttonIndex)
    {
        if (buttonIndex == correctCubeIndex) // Выбран правильный куб
        {
            nameText.SetActive(false);
            winText.SetActive(true);
            score += 15;
            
            for (int i = 0; i < cubes.Length; i++)
            {
                if (i != correctCubeIndex)
                {
                    cubes[i].GetComponent<Rigidbody>().useGravity = true;
                }
            }
        }
        else // Выбран неправильный куб
        {
            nameText.SetActive(false);
            loseText.SetActive(true);
            score = Mathf.Max(0, score - 5);

            foreach (var cube in cubes)
            {
                cube.GetComponent<Rigidbody>().useGravity = true;
            }
        }
        
        UpdateScore();
        restartButton.SetActive(true);
    }

    void UpdateScore()
    {
        scoreText.text = "Очки: " + score.ToString();
    }

    public void ResetGame()
    {
        // Сбрасываем UI
        winText.SetActive(false);
        loseText.SetActive(false);
        restartButton.SetActive(false);
        nameText.SetActive(true);

        // Восстанавливаем кубы
        for (int i = 0; i < cubes.Length; i++)
        {
            var rb = cubes[i].GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            cubes[i].transform.position = initialPositions[i];
            cubes[i].GetComponent<Renderer>().material.color = Color.white;
        }

        // Выбираем новый правильный куб
        ChooseRandomCube();
    }
}