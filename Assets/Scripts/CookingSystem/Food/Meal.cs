using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMeal", menuName = "ScriptableObjects/CookingSystem/Meal")]
public class Meal : Item
{
    public MealType Type;
    public MealCookingType MealCookingType;
    public List<MealComponent> MealComponents;
}
