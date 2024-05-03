using UnityEngine;

public class OutOfLimit : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag(TagConsts.PLATFORM))
        {
            Destroy(collision.gameObject);  
        }
    }
}
