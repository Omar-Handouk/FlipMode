using UnityEngine;

public class Collectable : MonoBehaviour
{
    private string colorName;

    public void SetColorName(string colorName) {
        this.colorName = colorName;
    }

    public string GetColorName() {
        return this.colorName;
    }
}
