using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public TMP_Text randomNumberText;
    public TMP_Text hilfsText;
    public TMP_Text erklaerungsText;
    public TMP_Text counterText;
    public TMP_Text winText;

    public AudioClip correct;
    public AudioClip wrong;

    public Image fizz;
    public Image buzz;
    public Image fizzbuzz;

    public Button startAgainButton;
    public Button newNumberButton;

    public Camera mainCamera;
    public Color backgroundColor;

    public RawImage correctImage; // Neues RawImage für richtige Antworten
    public RawImage wrongImage;   // Neues RawImage für falsche Antworten

    private int currentRandomNumber;
    private int correctCount = 0;

    void Start()
    {
        InitializeGame();
    }

    void Update()
    {
        HandleUserInput();
    }

    private void InitializeGame()
    {
        correctCount = 0;
        startAgainButton.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        correctImage.gameObject.SetActive(false); // Bilder am Anfang ausblenden
        wrongImage.gameObject.SetActive(false);   // Bilder am Anfang ausblenden
        EnableGameElements(true);
        GenerateRandomNumber();
        UpdateCounterText();
    }

    private void GenerateRandomNumber()
    {
        currentRandomNumber = Random.Range(1, 100);
        randomNumberText.text = currentRandomNumber.ToString();
        mainCamera.backgroundColor = backgroundColor;
    }

    private void UpdateCounterText()
    {
        counterText.text = "Counter: " + correctCount.ToString() + "/10";
        if (correctCount >= 10)
        {
            GameWon();
        }
    }

    private void HandleUserInput()
    {
        if (correctCount >= 10) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            CheckFizz(currentRandomNumber);
            SetTemporaryColor(fizz, Color.gray);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            CheckBuzz(currentRandomNumber);
            SetTemporaryColor(buzz, Color.gray);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            CheckFizzBuzz(currentRandomNumber);
            SetTemporaryColor(fizzbuzz, Color.grey);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GenerateRandomNumber();
            ClearTexts();
        }
    }

    private void SetTemporaryColor(Image image, Color color)
    {
        image.color = color;
        Invoke("ResetColor", 2f);
    }

    private void ResetColor()
    {
        fizz.color = Color.white;
        buzz.color = Color.white;
        fizzbuzz.color = Color.white;
    }

    private void CheckFizz(int number)
    {
        if (number % 3 == 0 && number % 5 != 0)
        {
            ShowResult(true);
            PlaySound(correct);
            Debug.Log("Fizz - Richtig!");
            GenerateRandomNumber();
            correctCount++;
            UpdateCounterText();
            hilfsText.text = "Richtig! Die Zahl ist durch 3 teilbar.";
            mainCamera.backgroundColor = new Color(0.5f, 1.5f, 0.5f);
        }
        else
        {
            ShowResult(false);
            PlaySound(wrong);
            Debug.Log("Fizz - Falsch!");
            hilfsText.text = "Fizz bedeutet, dass die Zahl durch 3 teilbar ist.";
            erklaerungsText.text = "Dies ist sie, wenn ihre Quersumme durch 3 teilbar ist.";
            mainCamera.backgroundColor = new Color(1f, 0.6f, 0.5f);
        }
    }

    private void CheckBuzz(int number)
    {
        if (number % 5 == 0 && number % 3 != 0)
        {
            ShowResult(true);
            PlaySound(correct);
            Debug.Log("Buzz - Richtig!");
            GenerateRandomNumber();
            correctCount++;
            UpdateCounterText();
            hilfsText.text = "Richtig! Die Zahl ist durch 5 teilbar.";
            mainCamera.backgroundColor = new Color(0.5f, 1.5f, 0.5f);
        }
        else
        {
            ShowResult(false);
            PlaySound(wrong);
            Debug.Log("Buzz - Falsch!");
            hilfsText.text = "Buzz bedeutet, dass die Zahl durch 5 teilbar ist.";
            erklaerungsText.text = "Dies ist sie, wenn die letzte Ziffer 0 bzw 5 ist.";
            mainCamera.backgroundColor = new Color(1f, 0.6f, 0.5f);
        }
    }

    private void CheckFizzBuzz(int number)
    {
        if (number % 3 == 0 && number % 5 == 0)
        {
            ShowResult(true);
            PlaySound(correct);
            Debug.Log("FizzBuzz - Richtig!");
            GenerateRandomNumber();
            correctCount++;
            UpdateCounterText();
            hilfsText.text = "Richtig! Die Zahl ist durch 3 und 5 teilbar.";
            mainCamera.backgroundColor = new Color(0.5f, 1.5f, 0.5f);
        }
        else
        {
            ShowResult(false);
            PlaySound(wrong);
            Debug.Log("FizzBuzz - Falsch!");
            hilfsText.text = "FizzBuzz bedeutet, dass die Zahl durch 3 und 5 teilbar ist.";
            erklaerungsText.text = "Dies ist sie, wenn die Quersumme durch 3 teilbar ist oder die letzte Ziffer 0 bzw 5 ist.";
            mainCamera.backgroundColor = new Color(1f, 0.6f, 0.5f);
        }
    }

    private void GameWon()
    {
        EnableGameElements(false);
        winText.gameObject.SetActive(true);
        startAgainButton.gameObject.SetActive(true);
    }

    private void EnableGameElements(bool enable)
    {
        randomNumberText.gameObject.SetActive(enable);
        hilfsText.gameObject.SetActive(enable);
        erklaerungsText.gameObject.SetActive(enable);
        counterText.gameObject.SetActive(enable);
        fizz.gameObject.SetActive(enable);
        buzz.gameObject.SetActive(enable);
        fizzbuzz.gameObject.SetActive(enable);
        newNumberButton.gameObject.SetActive(enable);
    }

    private void ClearTexts()
    {
        hilfsText.text = "";
        erklaerungsText.text = "";
    }

    public void StartAgain()
    {
        InitializeGame();
    }

    private void PlaySound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }

    private void ShowResult(bool isCorrect)
    {
        if (isCorrect)
        {
            correctImage.gameObject.SetActive(true);
            Invoke("HideResultImages", 2f);
        }
        else
        {
            wrongImage.gameObject.SetActive(true);
            Invoke("HideResultImages", 2f);
        }
    }

    private void HideResultImages()
    {
        correctImage.gameObject.SetActive(false);
        wrongImage.gameObject.SetActive(false);
    }
}

