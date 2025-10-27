using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Int2Event : UnityEvent<int, int> { }
public class eventManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static eventManager current;
    

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Actualizar el UI de munición
    public Int2Event onAmmoChanged = new Int2Event();
    public UnityEvent newGunEvent= new UnityEvent();
    //public UnityEvent onPlayerDeath;
}
