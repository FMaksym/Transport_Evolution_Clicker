using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TapEffectPool : MonoBehaviour
{
    public int poolSize = 10;
    public ImprovementData improvementData;
    public GameObject visualEffectPrefab;

    private Queue<TapEffectAnimation> _pool;
    private List<TMP_Text> _textObjects;

    private void OnEnable()
    {
        ImprovementData.ClickCostChanged += UpdateAllTextObjects;
    }

    private void Awake()
    {
        _pool = new Queue<TapEffectAnimation>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(visualEffectPrefab, gameObject.transform);

            TapEffectAnimation visualEffect = obj.GetComponent<TapEffectAnimation>();
            TMP_Text textComponent = obj.GetComponentInChildren<TMP_Text>();
            if (textComponent != null)
            {
                _textObjects.Add(textComponent);
            }

            obj.SetActive(false);
            _pool.Enqueue(visualEffect);
        }

        UpdateAllTextObjects();
    }

    public TapEffectAnimation GetVisualEffect(Vector2 tapPosition)
    {
        if (_pool.Count > 0)
        {
            TapEffectAnimation effect = _pool.Dequeue();
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
        _pool.Enqueue(effect);
    }

    public void UpdateAllTextObjects()
    {
        foreach (var textObject in _textObjects)
        {
            textObject.text = $"+{improvementData.ClickCost}";
        }
    }

    private void OnDisable()
    {
        ImprovementData.ClickCostChanged -= UpdateAllTextObjects;
    }
}
