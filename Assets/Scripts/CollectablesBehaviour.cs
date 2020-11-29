using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesBehaviour : MonoBehaviour
{
    public ColorMode[] colorModes;
    private Queue<int> collectables;
    private void Awake() {
        collectables = new Queue<int>();
    }

    private void OnEnable() {
        int numberOfObjectToActivate = Random.Range(0, 4);

        for (int i = 0; i < numberOfObjectToActivate; ++i) {
            int objectIndex = Random.Range(0, 3);

            GameObject child = this.gameObject.transform.GetChild(objectIndex).gameObject;
            child.SetActive(true);
            
            // Choose Color
            float colorChance = Random.Range(0f, 1f); // 0 -> 0.7 normal

            ColorMode colorMode = colorModes[Random.Range(0, colorModes.Length + (colorChance <= 0.7f ? -1 : 0))];

            child.GetComponent<Renderer>().material = colorMode.material;

            if (colorMode.name == "Purple") {
                child.tag = "HP";
            } else {
                child.tag = "Collectable";
            }

            child.GetComponent<Collectable>().SetColorName(colorMode.name);

            collectables.Enqueue(objectIndex);
        }
    }

    private void OnDisable() {
        while (collectables.Count != 0) {
            int objectIndex = collectables.Dequeue();
            GameObject child = this.gameObject.transform.GetChild(objectIndex).gameObject;
            child.SetActive(false);
            child.GetComponent<Collectable>().SetColorName(null);
        }
    }
}
