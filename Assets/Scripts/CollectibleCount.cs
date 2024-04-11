
using UnityEngine;



public class CollectibleCount : MonoBehaviour
{
    TMPro.TMP_Text text;
    int count;
    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<TMPro.TMP_Text>();
       
        
    }

    void Start() => UpdateCount();
    

    void OnEnable() => Collectible.OnCollected += OnCollectibleCollected;
    void OnDisable() => Collectible.OnCollected -= OnCollectibleCollected;

    // Update is called once per frame
    void OnCollectibleCollected()
    {
        count++;
        UpdateCount();

    }
    void UpdateCount()
    {
        text.text = $"{count} / {Collectible.total}";
       
        
    }
}
