using UnityEngine;

public class GlowBottleEffectManager : MonoSingleton<GlowBottleEffectManager>
{
    [SerializeField]
    private GlowBottle[] selectableBottles;

    private GameObject currentOveredObject;
    private GameObject currentSelectedObject;

    public void OverBottle(GameObject bottle)
    {
        // If the bottle isn't overred
        if (currentOveredObject != bottle)
        {
            // If there is already an overred bottle that isn't selected, stop the glow
            if (currentOveredObject != null && currentOveredObject != currentSelectedObject) currentOveredObject.GetComponent<GlowBottle>().StopGlow();

            // If the bottle exist and isn't selected, trigger the glow
            if (bottle != null && currentSelectedObject != bottle) bottle.GetComponent<GlowBottle>().TriggerGlow();

            currentOveredObject = bottle;
        }
    }

    public void SelectBottle(GameObject bottle)
    {
        // If the bottle isn't selected
        if (currentSelectedObject != bottle)
        {
            // If there is already a selected bottle, deselect the bottle
            if (currentSelectedObject != null) currentSelectedObject.GetComponent<GlowBottle>().StopGlow();

            bottle.GetComponent<GlowBottle>().Select();

            currentSelectedObject = bottle;
        }
    }

    public void RemoveAllGlow()
    {
        foreach (GlowBottle selectableBottle in selectableBottles)
        {
            selectableBottle.RemoveGlow();
        }

        currentOveredObject = null;
        currentSelectedObject = null;
    }
}
