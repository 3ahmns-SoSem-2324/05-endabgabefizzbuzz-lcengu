
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public TMP_Text randomNumberText, hilfsText, erklaerungsText, counterText;
    private int currentRandomNumber;
    private int correctCount = 0;
    public AudioClip correct;
    public AudioClip wrong;

    public Image fizz;
    public Image buzz;
    public Image fizzbuzz;

    public Camera maincam;
    public Color backColor;


    void Start()
    {
        GenerateRandomNumber();
        UpdateCounterText();
    }


    void GenerateRandomNumber()
    {
        currentRandomNumber = Random.Range(1, 50);
        randomNumberText.text = currentRandomNumber.ToString();
        maincam.backgroundColor = backColor;
    }

    void UpdateCounterText()
    {
        counterText.text = "Counter: " + correctCount.ToString() + "/10";
    }

    void Update()
    {
        if (correctCount < 10)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Alpha1))
            {
                CheckFizz(currentRandomNumber);
                fizz.color = Color.gray;
                Invoke("ResetColor", 2f);
                

            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Alpha2))
            {
                CheckBuzz(currentRandomNumber);
                buzz.color = Color.gray;
                Invoke("ResetColor", 2f);

            }
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                CheckFizzBuzz(currentRandomNumber);
                fizzbuzz.color = Color.grey;
                Invoke("ResetColor", 2f); 
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                GenerateRandomNumber();
                hilfsText.text = "";
                erklaerungsText.text = ""; 
            }
        }

    }


    void PlaySound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }

    void CheckFizz(int number)
    {
        if (number % 3 == 0 && number % 5 != 0)
        {
            PlaySound(correct);
            Debug.Log("Fizz - Stimmt!");
            GenerateRandomNumber();
            correctCount++;
            UpdateCounterText();
            hilfsText.text = "Richtig die Zahl ist durch 3 teilbar!";
            maincam.backgroundColor = new Color(0.5f, 1.5f, 0.5f);

        }
        else
        {
            PlaySound(wrong);
            Debug.Log("Fizz - Falsch!");
            hilfsText.text = "Fizz bedeutet, dass die Zahl nur durch 3 teilbar sein muss.";
            erklaerungsText.text = "Eine Zahl ist durch 3 teilbar, wenn die Quersumme der Ziffern durch 3 teilbar ist.";
            maincam.backgroundColor = new Color(1f, 0.6f, 0.5f);
        }
    }

    void CheckBuzz(int number)
    {
        if (number % 5 == 0 && number % 3 != 0)
        {
            PlaySound(correct);
            Debug.Log("Buzz - Stimmt!");
            GenerateRandomNumber();
            correctCount++;
            UpdateCounterText();
            hilfsText.text = "Richtig, die Zahl ist durch 5 teilbar!";
            maincam.backgroundColor = new Color(0.5f, 1.5f, 0.5f);
        }
        else
        {
            PlaySound(wrong);
            Debug.Log("Buzz - Falsch!");
            hilfsText.text = "Buzz bedeutet, dass die Zahl nur durch 5 teilbar sein muss.";
            erklaerungsText.text = "Eine Zahl ist durch 5 teilbar, wenn ihre letzte Ziffer 0 oder 5 ist.";
            maincam.backgroundColor = new Color(1f, 0.6f, 0.5f);
            
        }
    }

    void CheckFizzBuzz(int number)
    {
        if (number % 3 == 0 && number % 5 == 0)
        {
            PlaySound(correct);
            Debug.Log("FizzBuzz - Stimmt!");
            GenerateRandomNumber();
            correctCount++;
            UpdateCounterText();
            hilfsText.text = "Richtig, die Zahl ist durch 3 und 5 teilbar!";
            maincam.backgroundColor = new Color(0.5f, 1.5f, 0.5f);
        }
        else
        {
            PlaySound(wrong);
            Debug.Log("FizzBuzz - Falsch!");
            hilfsText.text = "FizzBuzz musss durch 5 und 3 teilbar sein.";
            erklaerungsText.text = "Eine Zahl ist durch 3 und 5 teilbar, wenn ihre letzte Ziffer 0 oder 5 ist, sowohl die Quersumme durch 3 teilbar ist.";
            maincam.backgroundColor = new Color(1f, 0.6f, 0.5f);
        }
    }

    void ResetColor()
    {
        fizz.color = Color.white;
        buzz.color = Color.white;
        fizzbuzz.color = Color.white; 
        
    }

}