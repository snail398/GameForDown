using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Text lifeCountText;
    [SerializeField] private IntVariable lifeCount;
    
    public void OnHpChanged()
    {
        lifeCountText.text = lifeCount.Value.ToString();
    }
}
