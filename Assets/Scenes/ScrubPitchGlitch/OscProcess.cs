using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class OSC : MonoBehaviour
{
    public OSCReceiver oscReceiver;
    public OSCTransmitter oscTransmitter;
    public ScrubPitchGlitch scrubPitchGlitch;
    public int potInMin = 0;
    public int potInMax = 1023;
    public float potOutMin = 0.0f;
    public float potOutMax = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        oscReceiver.Bind("/valider", TraiterMessageValider);
        oscReceiver.Bind("/tourner", TraiterMessageTourner);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TraiterMessageValider(OSCMessage message)
    {
        // Validez qu’il y a bien le nombre attendu d’arguments (1 dans l’exemple) :
        if (message.Values.Count != 1)
        {
            Debug.Log("Le message " + message.Address  + " n'a pas le bon nombre d'arguments");
            return; // Quitte la fonction sans exécuter la suite
        }

        // Vérifiez que l’argument est du type attendu (`int` dans l’exemple) :
        if (message.Values[0].Type != OSCValueType.Int)
        {
            Debug.Log("Le premier argument du message " + message.Address  + " n'est pas un entier");
            return; // Quitte la fonction sans exécuter la suite
        }

        // Récupérer la valeur de l’argument :
        int valeur = message.Values[0].IntValue;

        // Deboguer
        // Debug.Log("Reçu : " + message.Address + " " + valeur);

        // FAIRE DE QUOI AVEC LA VARIABLE VALEUR ICI !
        if (valeur == 1)
        {
            scrubPitchGlitch.NextVideo(); // Set to maximum value
        } else {

        }

    }

    void TraiterMessageTourner(OSCMessage message)
    {
        // Validez qu’il y a bien le nombre attendu d’arguments (1 dans l’exemple) :
        if (message.Values.Count != 1)
        {
            Debug.Log("Le message " + message.Address  + " n'a pas le bon nombre d'arguments");
            return; // Quitte la fonction sans exécuter la suite
        }

        // Vérifiez que l’argument est du type attendu (`int` dans l’exemple) :
        if (message.Values[0].Type != OSCValueType.Int)
        {
            Debug.Log("Le premier argument du message " + message.Address  + " n'est pas un entier");
            return; // Quitte la fonction sans exécuter la suite
        }

        // Récupérer la valeur de l’argument :
        int valeur = message.Values[0].IntValue;

        Debug.Log("Reçu : " + message.Address + " " + valeur);

        // FAIRE DE QUOI AVEC LA VARIABLE VALEUR ICI !
       
        float ajuste = (((float)valeur - potInMin) / (potInMax - potInMin) * (potOutMax - potOutMin) + potOutMin);
        scrubPitchGlitch.Set(ajuste);
       
    }
}
