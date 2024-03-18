using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission
{
    public int missionId; // cada misión debe tener una identificación
    public string description; // description de la mision

    [Space] // útil en la vista del inspector en Unity; vista mas limpia
    public bool active; // la misión es activa
    public bool permanentActive; // para misiones activas permanentes (siempre activo, incluso después de reiniciar misiones)
    public bool missionComplete; // se completó la misión
    [Space]
    public bool restartOnNextBall; // si la misión debe reiniciarse después de que la pelota se caiga
    public bool stopOnBallEnd; // si la misión puede continuar después de que la pelota caiga
    public bool resetOnComplete; // si la misión es repetible
    public bool canTriggerMultiball; // si la misión puede activar multibola
    [Space]
    public int score; // ¿Cuántos puntos obtiene el jugador después de completar la misión?
    public int amountToComplete; // cuántas veces el jugador tiene que hacer algo, p. cuantas veces la pelota debe golpear los parachoques
    public int currentAmount; // contador de lo anterior, p. 2 de 4

    public void ResetMission()
    {
        if (resetOnComplete)
        {
            if (permanentActive)
            {
                active = true;
            }
            else
            { 
                active = false;
            }
            missionComplete = false;
            currentAmount = 0;
        }
    }

    public void DeactivateMission()
    {
        /*
        si el indicador de misión active permanente está establecido en verdadero, entonces actívelo configurando el indicador activo en falso; de lo contrario, establezca el indicador activo en falso
        */
        if (permanentActive)
        {
            active = true;
        }
        else
        {
            active = false;
            currentAmount = 0;
        }

    }

    public void ResetRolloverMission()
    {
        ScoreManager.instance.ResetRolloverLights();
    }

    public void UpdateMission()
    {
        if (active && !missionComplete)
        {
            currentAmount++;

            //COMPROBAR SI LA MISIÓN ESTÁ COMPLETA
            CheckMissionComplete();
        }
    }

    void CheckMissionComplete()
    {
        if (currentAmount >= amountToComplete)
        {
            Debug.Log("Misión "+ missionId + " ¡Completado satisfactoriamente!");
            missionComplete = true;
            active = false;

            if (canTriggerMultiball) // si la misión puede iniciar multibola, active el GameObject multibola, luego la courotina multibola iniciará OnTrigger dentro de Collectible
            {
                Collectible.instance.gameObject.SetActive(true);
            }

            ScoreManager.instance.AddScore(score);
            // ZAGRAJ DŹWIĘK - mission complete
            GameObject.FindObjectOfType<AudioManager>().Play("MissionComplete");

            // si la misión es volcar la misión es restablecer
            if (missionId == 3)
            {
                ResetRolloverMission();
            }
            else
            {
                ResetMission();
            }
        }
    }

    public bool GetMissionActive()
    {
        return active;
    }
}
