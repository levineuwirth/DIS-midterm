using System;
using UnityEngine;

public class Item : MonoBehaviour {
    [Header("Properties")]

    [field: SerializeField] public bool isTrash { get; private set; }

    [field: SerializeField] public bool isNone { get; private set; }

    [field: SerializeField] public Vector3[] allowedLocations { get; private set; }

    [field: SerializeField] public IngredientType ingredientID { get; private set; }

    public enum IngredientType {
        Apple,
        Banana,
        Orange,
        Butter,
        Bread,
        Cheese,
        Bacon,
        Beef,
        Carrot,
        Potato,
        Water,
        Tomato,
        Rice,
        None
    }
}
