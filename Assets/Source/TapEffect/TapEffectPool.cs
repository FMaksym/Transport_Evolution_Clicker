using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TapEffectPool : MonoBehaviour
{
    public ImprovementData improvementData;
    public GameObject visualEffectPrefab;
    public int poolSize = 10;

    public Queue<TapEffectAnimation> pool;
    public List<TMP_Text> textObjects;

    private void OnEnable()
    {
        ImprovementData.ClickCostChanged += UpdateAllTextObjects;
    }

    private void Awake()
    {
        pool = new Queue<TapEffectAnimation>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(visualEffectPrefab, gameObject.transform);

            TapEffectAnimation visualEffect = obj.GetComponent<TapEffectAnimation>();
            TMP_Text textComponent = obj.GetComponentInChildren<TMP_Text>();
            if (textComponent != null)
            {
                textObjects.Add(textComponent);
            }

            obj.SetActive(false);
            pool.Enqueue(visualEffect);
        }

        UpdateAllTextObjects();
    }

    public TapEffectAnimation GetVisualEffect(Vector2 tapPosition)
    {
        if (pool.Count > 0)
        {
            TapEffectAnimation effect = pool.Dequeue();
            effect.transform.position = tapPosition;
            effect.gameObject.SetActive(true);
            return effect;
        }
        else
        {
            Debug.LogWarning("VisualEffectPool: No more effects available in the pool.");
            return null;
        }
    }

    public void ReturnVisualEffect(TapEffectAnimation effect)
    {
        effect.gameObject.SetActive(false);
        effect.transform.position = Vector3.zero;
        pool.Enqueue(effect);
    }

    public void UpdateAllTextObjects()
    {
        foreach (var textObject in textObjects)
        {
            textObject.text = $"+{improvementData.ClickCost}";
        }
    }

    private void OnDisable()
    {
        ImprovementData.ClickCostChanged -= UpdateAllTextObjects;
    }
}
