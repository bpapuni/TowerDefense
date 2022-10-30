using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TowerSelect : MonoBehaviour
{
    BuildManager buildManager;
    Status status;
    private Image border;

    void Start()
    {
        buildManager = BuildManager.instance;
        status = Status.instance;
        border = transform.parent.GetComponentInChildren<Image>();
    }

    public void PurchaseBallistaTower()
    {
        buildManager.SetPendingTower(buildManager.PendingBallistaTower);
        buildManager.SetTowerToBuild(buildManager.BallistaTower);
        buildManager.ShowPendingTower();
        HideHighlight();
    }

    public void PurchaseFrostTower()
    {
        buildManager.SetPendingTower(buildManager.PendingFrostTower);
        buildManager.SetTowerToBuild(buildManager.FrostTower);
        buildManager.ShowPendingTower();
        HideHighlight();
    }

    public void PurchaseTinyTower()
    {
        buildManager.SetPendingTower(buildManager.PendingTinyTower);
        buildManager.SetTowerToBuild(buildManager.TinyTower);
        buildManager.ShowPendingTower();
        HideHighlight();
    }

    public void ConfirmPurchaseTower()
    {
        // TODO move costs from status to buildManager
        int TowerCost = 0;
        GameObject towerToBuild = buildManager.GetTowerToBuild();

        if (towerToBuild.tag == "BallistaTower")
            TowerCost = buildManager.BallistaCost;
        else if (towerToBuild.tag == "FrostTower")
            TowerCost = buildManager.FrostCost;
        else if (towerToBuild.tag == "TinyTower")
            TowerCost = buildManager.TinyCost;

        if (status.gold < TowerCost)
        {
            CancelSelect();
            status.ShowMessage("Not enough gold!", 3);
            buildManager.confirmTowerChoice.SetActive(false);
            return;
        }

        status.UpdateGold(-TowerCost);
        buildManager.BuildTower();
        buildManager.confirmTowerChoice.SetActive(false);
    }

    public void ConfirmDeleteTower()
    {
        int TowerCost = 0;
        GameObject towerToDelete = buildManager.selectedTower;

        if (towerToDelete.tag == "BallistaTower")
            TowerCost = buildManager.BallistaCost;
        else if (towerToDelete.tag == "FrostTower")
            TowerCost = buildManager.FrostCost;
        else if (towerToDelete.tag == "TinyTower")
            TowerCost = buildManager.TinyCost;

        status.UpdateGold(TowerCost / 2);
        buildManager.DeleteTower();
        buildManager.confirmTowerDelete.SetActive(false);
    }

    public void CancelSelect()
    {
        // Fires if confirmTowerDelete scene is active
        if (buildManager.selectedTower != null)
            buildManager.CancelDelete();
        // Fires if towerChoice or confirmTowerChoice scene is active
        else
            buildManager.HidePendingTower();

        // Changes towerChoice selection border from green to normal
        if (transform.parent.name == "TowerChoice")
            border.color = new Color(255, 255, 255, 1);

        buildManager.towerChoice.SetActive(false);
        buildManager.confirmTowerChoice.SetActive(false);
        buildManager.confirmTowerDelete.SetActive(false);
    }

    public void ShowHighlight()
    {
        border.color = new Color(0, 255, 0, 1);
    }

    public void HideHighlight()
    {
        border.color = new Color(255, 255, 255, 1);
    }
}
