using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxManager : MonoBehaviour
{
    private string _correctGemOrder = "BlueRedGreen";
    private string _enteredGemOrder = "";

    private int _amountOfGems = 3;
    private int _currentGem = 0;

    public Animator boxAnimator;

    public UnityEvent gameIsWon;

    public Gem[] gemsInScene;

    public void GemSelect(Gem currentSelectedGem)
    {
        //add the color of the gem to enteredGemOrder
        _enteredGemOrder += currentSelectedGem.gemColorName;
        //increment our current Gem
        _currentGem += 1;

        //make gem emissive
        currentSelectedGem.ChangeEmission(true);

           //if currentGem == amountofGems, compare to CorrectGemOrder
           if(_currentGem == 3)
            {
            CompareGemOrder();
            }
    }

    private void CompareGemOrder()
    {
        if(_enteredGemOrder == _correctGemOrder)
        {
            CompleteGame();
        }
        else
        {
            print(_enteredGemOrder);
            ResetGame();
        }
    }

    private void CompleteGame()
    {
        gameIsWon.Invoke();
    }

    private void ResetGame()
    {
        _currentGem = 0;
        _enteredGemOrder = "";

        foreach(Gem gem in gemsInScene)
        {
            gem.ChangeEmission(false);
        }

    }
}
