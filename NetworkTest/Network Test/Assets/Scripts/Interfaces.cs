using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ShipPart
{
    void SetID(int newID);
    int getID();

    void SetPosX(int newPosX);
    int GetPosX();

    void SetPosY(int newPosY);
    int GetPosY();

}
