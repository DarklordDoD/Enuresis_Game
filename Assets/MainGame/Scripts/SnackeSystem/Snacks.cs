using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snacks : MonoBehaviour
{
    public List<ASnack> snacks;

    public void changeSnaks(string snackTybe, float givGlad, int changeAmount = 1, bool plus = true)
    {
        bool snakIsAdd = false;

        foreach (ASnack snack in snacks)
        {
            if (snack.snackType == snackTybe)
            {
                if (plus)
                    snack.amaunt += changeAmount;
                else
                    snack.amaunt -= 1;

                snakIsAdd = true;
            }
        }

        if (!snakIsAdd && plus)
            snacks.Add(new ASnack {snackType = snackTybe, amaunt = changeAmount, giveHappy = givGlad });
    }

    public void LoadSnacks()
    {
        List<string> theLoadList = GetComponent<Ressourcer>().gotList;

        for (int i = 8; i < theLoadList.Count; i++)
        {
            List<string> theSplitSnack = theLoadList[i].Split(",").ToList();

            snacks.Add(new ASnack { snackType = theSplitSnack[0], amaunt = int.Parse(theSplitSnack[1]), giveHappy = float.Parse(theSplitSnack[2]) });
        }
    }
}
