using UnityEngine;
using System.Collections.Generic;

public class sonsyou : MonoBehaviour
{
    [SerializeField] int healRequiredCount = 3;

    private List<kessyouban> arrivedPlatelets = new List<kessyouban>();

    // ŒŒ¬”Â‚ª“’…‚µ‚½‚Æ‚«‚ÉŒÄ‚Î‚ê‚é
    public void OnPlateletArrived(kessyouban platelet)
    {
        if (!arrivedPlatelets.Contains(platelet))
            arrivedPlatelets.Add(platelet);

        Debug.Log($"ŒŒ¬”Â“’…: {arrivedPlatelets.Count}/{healRequiredCount}");

        if (arrivedPlatelets.Count >= healRequiredCount)
        {
            HealFloor();
        }
    }

    private void HealFloor()
    {
        Debug.Log("áŠQ•¨‚ªœ‹‚³‚ê‚Ü‚µ‚½I");

        foreach (var p in arrivedPlatelets)
        {
            if (p != null) Destroy(p.gameObject);
        }

        Destroy(this.gameObject); // áŠQ•¨‚²‚ÆÁ‚·
    }
}