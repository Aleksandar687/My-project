using UnityEngine;

public class HoverDetector : MonoBehaviour
{
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);/*пропуска ray(лъч) от камерата към позицията на мишката*/ 

        if (Physics.Raycast(ray, out RaycastHit hit)) /* проверява докоснал ли е Game Object*/
        {
            Bubble bubble = hit.collider.GetComponent<Bubble>(); /* Гледа дали елементът е балон XP  */

            if (bubble != null)
            {

            }
        }
    }
}
