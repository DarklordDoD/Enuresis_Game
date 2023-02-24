using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snacks : MonoBehaviour
{
    public List<ASnack> snacks;

    public void changeSnaks(string snackTybe, float givGlad, bool plus)
    {
        bool snakIsAdd = false;

        foreach (ASnack snack in snacks)
        {
            if (snack.snackType == snackTybe)
            {
                if (plus)
                    snack.amaunt += 1;
                else
                    snack.amaunt -= 1;

                snakIsAdd = true;
            }
        }

        if (!snakIsAdd && plus)
            snacks.Add(new ASnack {snackType = snackTybe, amaunt = 1, giveHappy = givGlad });
    }

    public void LoadSnacks()
    {
        List<string> theLoadList = GetComponent<Ressourcer>().gotList;

        for (int i = 6; i < theLoadList.Count; i++)
        {
            List<string> theSplitSnack = theLoadList[i].Split(",").ToList();

            snacks.Add(new ASnack { snackType = theSplitSnack[0], amaunt = int.Parse(theSplitSnack[1]), giveHappy = float.Parse(theSplitSnack[2]) });
        }
    }
}
