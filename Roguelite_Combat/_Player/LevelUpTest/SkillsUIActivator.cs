using UnityEngine;

public class SkillsUIActivator : MonoBehaviour
{
    [SerializeField] private SkillSlotsManager skillSlotsManager;
    [SerializeField] private GameObject skillsPanel;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleSkillsPanel(!skillsPanel.activeInHierarchy);
        }
    }

        // add event for pause
    public void ToggleSkillsPanel(bool onOff)
    {
        skillsPanel.SetActive(onOff);
        if (onOff)
        {
            skillSlotsManager.CheckAndSetTierLocks();
        }
    }
}
