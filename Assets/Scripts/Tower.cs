using System.Collections;
using TMPro;
using UnityEngine;

public class Tower : MonoBehaviour
{
    BuildManager buildManager;
    Status status;
    private Transform target;

    [Header("Attributes")]
    public float damage;
    public float range;
    public float fireRate;
    private float fireCountdown = 0f;

    [Header("Unity Fields")]
    public Transform partToRotate;
    public float turnSpeed = 10f;
    public GameObject Bullet;
    public Transform firePoint;
    public Color hoverColor;

    private Renderer[] rend;
    private Color[] startColor;
    private GameObject buildPlate;
    private Renderer bpRend;
    private TMP_Text confirmDelete;
    public GameObject rangeIndicator;

    void Awake()
    {
        buildManager = BuildManager.instance;
        buildPlate = buildManager.selectedPlate;
        bpRend = buildPlate.GetComponent<Renderer>();
        status = Status.instance;
        rend = gameObject.GetComponentsInChildren<Renderer>();
        startColor = new Color[rend.Length];
        for (int i = 0; i < rend.Length; i++)
        {
            if (rend[i].material.HasProperty("_Color"))
                startColor[i] = rend[i].material.color;
        }
        confirmDelete = buildManager.confirmTowerDelete.GetComponentInChildren<TMP_Text>();
        if (buildPlate.GetComponent<BuildingPlate>().type == BuildingPlate.plateType.FIRE)
        {
            damage = damage * 1.2f;
        }
        else if (buildPlate.GetComponent<BuildingPlate>().type == BuildingPlate.plateType.RANGE)
        {
            range = range * 1.2f;
        }
        else if (buildPlate.GetComponent<BuildingPlate>().type == BuildingPlate.plateType.SPEED)
        {
            fireRate = fireRate * 1.2f;
        }

        if (buildManager.selectedPlate.GetComponent<BuildingPlate>().type == BuildingPlate.plateType.RANGE)
            rangeIndicator = Instantiate(buildManager.bonusRangeIndicator, buildManager.bonusRangeIndicator.transform.position, buildManager.bonusRangeIndicator.transform.rotation);
        else
            rangeIndicator = Instantiate(buildManager.rangeIndicatorObj, buildManager.rangeIndicator.transform.position, buildManager.rangeIndicator.transform.rotation);

        rangeIndicator.SetActive(false);
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void OnMouseEnter()
    {
        if (buildManager.selectedPlate != null)
            return;

        for (int i = 0; i < rend.Length; i++)
            rend[i].material.color = hoverColor;
        bpRend.material.color = hoverColor;
        rangeIndicator.SetActive(true);
    }

    void OnMouseExit()
    {
        if (buildManager.selectedPlate != null)
            return;

        for (int i = 0; i < rend.Length; i++)
            rend[i].material.color = startColor[i];
        bpRend.material.color = Color.white;
        rangeIndicator.SetActive(false);
    }

    void OnMouseUp()
    {
        if (buildManager.selectedPlate != null)
            return;

        buildManager.selectedPlate = buildPlate;
        int TowerCost = 0;
        buildManager.selectedTower = gameObject;

        if (gameObject.tag == "BallistaTower")
            TowerCost = buildManager.BallistaCost;
        else if (gameObject.tag == "FrostTower")
            TowerCost = buildManager.FrostCost;
        else if (gameObject.tag == "TinyTower")
            TowerCost = buildManager.TinyCost;

        confirmDelete.text = "Do you wish to sell this tower for " + TowerCost / 2 + " gold?";
        buildManager.confirmTowerDelete.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        Vector3 dir = target.position - transform.GetChild(0).position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            StartCoroutine(Shoot());
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

    }

    IEnumerator Shoot()
    {
        // Wait required for ballista tower to aim, else arrow shoots out sideways
        yield return new WaitForSeconds(0.3f);
        GameObject bulletGO = (GameObject)Instantiate(Bullet, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
            target = null;
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.DrawWireSphere(transform.position, range);
    //}
}
