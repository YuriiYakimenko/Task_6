using UnityEngine;

public class RayShooter : MonoBehaviour
{
    [SerializeField] private Joystick AttackJoystick;
    [SerializeField] private Transform player;
    [SerializeField] private LineRenderer laserLine;

    void Update()
    {
        Vector3 shootDir = new Vector3(AttackJoystick.Horizontal, 0, AttackJoystick.Vertical);
        if (shootDir.magnitude > 0.5f)
        {
            Vector3 startPos = player.position + Vector3.up * 1f; 
            Vector3 endPos = startPos + shootDir.normalized * 100f; 

            Ray ray = new Ray(startPos, shootDir.normalized);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                endPos = hit.point;
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if (target != null)
                {
                    target.ReactToHit();
                }
            }
            laserLine.enabled = true;
            laserLine.SetPosition(0, startPos);
            laserLine.SetPosition(1, endPos);
        }
        else
        {
            laserLine.enabled = false;
        }
    }
}