using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public UiController uiController;
    public Transform topBound, bottomBound, leftBound, rightBound;
    public GameOver gameOver;
    public int currentScore, highScore = 0, score = 0, indexKey;
    public bool isLose = false, isFill = false;
    string hint, answer;
    private List<string> listLetters= new List<string>(){"A", "B", "C", "D", "E", "F", "G", 
                                                        "H", "I", "J", "K", "L", "M", "N", "O", "P", 
                                                        "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
    public Image timeBar;
    public float maxTime, timeMin;
    public List<string> listGenerateLetter = new List<string>();
    public Dictionary<string, string> listQuest = new Dictionary<string, string>(){
        {"DANI","MILK"}, {"COLD","SNOW"}, {"COLOR","BLUE"}, 
        {"!FALSE", "TRUE"}, {"TRAVEL", "CAR"}, {"PET","CAT"},
        {"TARDY", "LATE"}, {"...DOM", "FREE"}, {"TRUTH","FACT"},
        {"...CHAMP", "POG"}, {"!SHOW", "HIDE"}, {"...MAN","IRON"}
        };
    public Letter letterPrefabs;
    public List<Letter> listLetterObj;
    public List<int> letterCorrect;
    public List<string> listLetterCorrect;
    public List<string> answerTemp;
    public GameObject buttonsObject;
    public float deltaTimeFloat;

    public GameObject timeObject, hintText, answerText, halfCircle, player;

    void Awake(){
        
        gameManager = this;
    }
    void Start()
    {
        StartGame();
        
    }

    void FixedUpdate()
    {
        showTime();
    }

    public void TurnOff(){
        timeObject.SetActive(false);
        hintText.SetActive(false);
        answerText.SetActive(false);
        halfCircle.SetActive(false);
        player.SetActive(false);
    }

    public void TurnOn(){
        timeObject.SetActive(true);
        hintText.SetActive(true);
        answerText.SetActive(true);
        halfCircle.SetActive(true);
        player.SetActive(true);
    }

    void StartGame(){
        TurnOn();
        buttonsObject.SetActive(true);
        deltaTimeFloat = Time.deltaTime;
        timeMin = maxTime;
        gameOver.HideGameOver();
        isLose = true;
        currentScore = -1;
        UpdateScoreUI();
        // RandomQuest();
        // InvokeRepeating("showTime",0,0.1f);
        InvokeRepeating("CreateRandomQuest",0,30);
    }

    public void UpdateScoreUI(){
        currentScore++;
        if(currentScore > highScore){
            highScore = currentScore;
        }
        score = currentScore;
        uiController.UpdateCurrentScore(currentScore);
    }

    public void GameLose(){
        TurnOff();
        buttonsObject.SetActive(false);
        deltaTimeFloat = 0;
        // CancelInvoke("showTime");
        CancelInvoke("CreateRandomQuest");
        CancelInvoke("GenerateLetters");
        ResetPreLetters();
        isLose = false;
        uiController.HideCurrentScore();
        gameOver.ShowGameOver(score, highScore);
    }

    public void PlayGame(){
        // Player.mainPlayer.DisableBody();
        StartGame();
        uiController.ShowCurrentScore();
    }

    public void CreateRandomQuest(){
        CancelInvoke("GenerateLetters");
        indexKey = Random.Range(0, listQuest.Count);
        hint = listQuest.ElementAt(indexKey).Key;
        answer = listQuest.ElementAt(indexKey).Value;
        uiController.UpdateQuest(hint, answer);
        ResetLetterCorrected();
        InvokeRepeating("GenerateLetters", 0, 3.5f);
    }

    public void ResetLetterCorrected(){
        letterCorrect.Clear();
        listLetterCorrect.Clear();
        answerTemp.Clear();
        for(int i = 0; i < answer.Length; i++){
            letterCorrect.Add(1);
            listLetterCorrect.Add(char.ToString(answer[i]));
            answerTemp.Add(char.ToString(answer[i]));
        }
    }

    public void GenerateLetters(){
        // First letter is a letter of Answer
        int randomNumber = Random.Range(0, listLetterCorrect.Count);
        listGenerateLetter.Clear();
        listGenerateLetter.Add(listLetterCorrect[randomNumber]);
        // Create 3-4 another letters
        randomNumber = Random.Range(3,5);
        for(int i = 0; i < randomNumber; i++){
            listGenerateLetter.Add(listLetters[Random.Range(0,listLetters.Count)]);
        }
        Debug.Log(string.Join(" ", listGenerateLetter));
        SpawnLetter();
    }

    public int GetIDofListLetters(string a){
        for(int i = 0; i < listLetters.Count; i++){
            if(a == listLetters[i]){
                return i;
            }
        }
        return -1;
    }

    public void ResetPreLetters(){
        for(int i = 0; i < listLetterObj.Count; i++){
            if(listLetterObj[i] != null){
                Destroy(listLetterObj[i].gameObject);
            }
        }
        listLetterObj.Clear();
    }

    public void SpawnLetter(){
        ResetPreLetters();
        for(int i = 0; i < listGenerateLetter.Count; i++){
            float randX = Random.Range(leftBound.position.x, rightBound.position.x);
            float randY = Random.Range(bottomBound.position.y, topBound.position.y);
            var spawnedLetter = Instantiate(letterPrefabs, new Vector3(randX  + Random.Range(1f, 1.5f), randY, 0), Quaternion.identity);
            spawnedLetter.InitLetter(GetIDofListLetters(listGenerateLetter[i]));
            listLetterObj.Add(spawnedLetter);
        }
    }

    public void RemoveLetterCorrect(int id){
        string a = listLetters[id];
        if(IsCorrectLetter(a)){
            for(int i = 0; i < listLetterCorrect.Count; i++){
                if(a == listLetterCorrect[i] && !IsRemoved(a)){
                    listLetterCorrect.Remove(a);
                    answerTemp[answerTemp.IndexOf(a)] = "_";
                }
            }
        }
        else{
            Player.mainPlayer.SetActiveBody();
        }
        
        UpdateAnswer();
    }

    public bool IsCorrectLetter(string a){
        for(int i = 0; i < answer.Length; i++){
            if(a == char.ToString(answer[i])){
                
                return true;
            }
        }
        return false;
    }

    public bool IsRemoved(string a){
        for(int i = 0; i < listLetterCorrect.Count; i++){
            if(a == listLetterCorrect[i]){

                return false;
            }
        }
        return true;
    }

    public void UpdateAnswer(){
        string str = "";
        for(int i = 0; i < answer.Length; i++){
            if(answerTemp[i] == "_"){
                str += char.ToString(answer[i]);
            } 
            else{
                str += "_";
            }
        }
        uiController.UpdateAnswer(str);
        if(listLetterCorrect.Count == 0){
            isFill = true;
            currentScore++;
            score = currentScore;
            if(currentScore > highScore){
                highScore = currentScore;
            }
            uiController.UpdateCurrentScore(currentScore);
            CancelInvoke("CreateRandomQuest");
            InvokeRepeating("CreateRandomQuest",0,30);
        }
    }

    public void updateTime(float time)
    {
        timeMin = Mathf.Clamp(time, 0, maxTime);
        timeBar.fillAmount = timeMin / maxTime;
    }
    public void showTime()
    {
        updateTime(timeMin - deltaTimeFloat);
        if (timeMin == 0)
        {
           GameLose();
        }
    }

}
