using UnityEngine;

public class skillcomplete : MonoBehaviour
{
    public static bool skillallget = false;
    void Update()
    {
        if (SkilGT5.ButtonONOFF)
        {
            if(SkilSO5.ButtonONOFF) 
            {
                if (SkilPS3.ButtonONOFF) 
                {
                    if (SkilPO3.ButtonONOFF)
                    {
                        if (SkilKS3.ButtonONOFF)
                        {
                            if (SkilKM5.ButtonONOFF)
                            {
                                if (SkilHT3.ButtonONOFF)
                                {
                                    if (SkilHR3.ButtonONOFF)
                                    {
                                        if (SkilHM5.ButtonONOFF)
                                        {
                                            if (SkilPM5.ButtonONOFF)
                                            {
                                                skillallget = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                } 
            }
        }
    }
}
