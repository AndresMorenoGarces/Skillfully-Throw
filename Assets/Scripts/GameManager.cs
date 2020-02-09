using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform[] dianeArray;
    public Transform[] arrowArray;
    public Transform[] levels;
    public Transform launchedArrows;
    public Transform arrowPosition;
    public TextMeshPro resultText;
    public int arrowLimit = 8;

    [HideInInspector]
    public int currentLevel = 0;
    [HideInInspector]
    public bool isFailure = false;
    [HideInInspector]
    public bool isTheLevelStart = false;

    GameObject firstArrowLevel;
    bool isTheLevelPassed = false;
    
    void BeginLevel()
    {
        if (isTheLevelStart)
        {
            DesactiveOldLevel();
            levels[currentLevel].gameObject.SetActive(true);
            dianeArray[currentLevel].gameObject.SetActive(true);
            arrowArray[currentLevel].gameObject.SetActive(true);
            isTheLevelStart = false;
        }

        void DesactiveOldLevel()
        {
            if (currentLevel < levels.Length && currentLevel > 0)
            {
                levels[currentLevel - 1].gameObject.SetActive(false);
                arrowArray[currentLevel - 1].gameObject.SetActive(false);
                dianeArray[currentLevel - 1].gameObject.SetActive(false);
            }
            else
                currentLevel = 0;
        }
    }
    
    void PassLevel()
    {
        if (launchedArrows.childCount == arrowLimit)
        {
            isTheLevelPassed = true;
            ActiveResultText(true);
            StartCoroutine(UpgradeCurrentLevel());
        }
    }
    void LoseGame()
    {
        if (isFailure)
        {
            ActiveResultText(false);
            StartCoroutine(UpgradeCurrentLevel());
        }
    }
    IEnumerator UpgradeCurrentLevel()
    {
        yield return new WaitForSeconds(2);

        if (isTheLevelPassed || isFailure)
        {
            isTheLevelStart = true;
            if (isTheLevelPassed)
                currentLevel++;
            BeginLevel();
            
            resultText.gameObject.SetActive(false);
            StructureTheLevel();
            isTheLevelPassed = false;
            isFailure = false;
        }
    }

    void ActiveResultText(bool won)
    {
        resultText.gameObject.SetActive(true);
        if (won)
            resultText.text = "You Pass!!!";
        else
            resultText.text = "You lose!!!";
    }
    
    void StructureTheLevel()
    {
        launchedArrows.SetParent(null);
        for (int i = 0; i < launchedArrows.childCount; i++)
            Destroy(launchedArrows.GetChild(i).gameObject);
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    void Start()
    {
        levels[currentLevel].gameObject.SetActive(true);
    }
    void Update()
    {
        BeginLevel();
        PassLevel();
        LoseGame();
    }
}
