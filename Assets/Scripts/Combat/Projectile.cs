using RPG.Attributes;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    [SerializeField] bool isTargetLock = true;
    [SerializeField] GameObject effectOnHit = null;
    [SerializeField] float maxTimeAlive = 4f;
    [SerializeField] GameObject[] toDestroyonHit;
    [SerializeField] float lifeTimeAfterDestroy = 2f;
    
    Health target;
    float damage = 0;
    GameObject instigator = null;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(GetAimLocation());
    }

    // Update is called once per frame
    void Update()
    {
        if (isTargetLock && !target.IsDead())
        {
            transform.LookAt(GetAimLocation());
        }
        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    public void SetTarget(Health target, GameObject instigator, float damage)
    {
        this.target = target;   
        this.damage = damage;
        this.instigator = instigator;
        Destroy(gameObject,maxTimeAlive) ;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (target.IsDead()) return;
        if (target == other.GetComponent<Health>())
        {
           
            target.TakingDamage(instigator, damage);
            if(effectOnHit != null)
            {
                Instantiate(effectOnHit, GetAimLocation(),transform.rotation);
            }
            foreach (var item in toDestroyonHit)
            {
                Destroy(item);
            }
            
            Destroy(gameObject, lifeTimeAfterDestroy);
        }
        
    }
    private Vector3 GetAimLocation()
    {
        if(target.GetComponent<CapsuleCollider>() != null)
        {
            return target.transform.position + Vector3.up * target.GetComponent<CapsuleCollider>().height / 2 ;
        }
        return target.transform.position;
    }
}
