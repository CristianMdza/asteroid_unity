using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class OscProcess : MonoBehaviour
{
    public OSCReceiver oscReceiver;
    public OSCTransmitter oscTransmitter;
    
    // Variable de type AsteroidDodger pour l'étape : Pointer vers le script AsteroidDodger du GameObject GameManager
    public AsteroidDodger asteroidDodger;

    // PROPRIÉTÉS DE CALIBRATION POUR LA PHOTORÉSISTANCE
    public int photoInMin = 0;
    public int photoInMax = 1023;
    public float photoOutMin = 0.0f; 
    public float photoOutMax = 1.0f;

    // Démarrage est appelée avant la première frame
    void Start()
    {
        // Effectuer le Bind pour lier l'adresse OSC /photo à la méthode TraiterMessageBeta
        {
            oscReceiver.Bind("/photo", TraiterMessagePhoto);
        }
    }

    // Mise à jour est appelée une fois par frame
    void Update()
    {
        if ( asteroidDodger.GetShipIsVisible() ) {
            var oscMessage = new OSCMessage("/lumiere"); 
            oscMessage.AddValue( OSCValue.Int(1) ); // Ajoute l’entier 1
            oscTransmitter.Send( oscMessage ); // Envoyer le message 
        } else {
            var oscMessage = new OSCMessage("/lumiere"); 
            oscMessage.AddValue( OSCValue.Int(0) ); // Ajoute l’entier 0
            oscTransmitter.Send( oscMessage ); // Envoyer le message 
        }
    }

    // MÉTHODE DE TRAITEMENT DÉDIÉE POUR LA PHOTORÉSISTANCE
    void TraiterMessagePhoto(OSCMessage message)
    {
        // Traiter un entier et l’appliquer à une propriété (flux continu)

        // Récupérer la valeur du premier argument en tant qu’entier :
        int valeur = message.Values[0].IntValue;

        // Deboguer
        // Debug.Log("Reçu : " + message.Address + " " + valeur);

        // Ajustement de la valeur
        float ajustee = (
            ((float)valeur - photoInMin) / (photoInMax - photoInMin) * (photoOutMax - photoOutMin) + photoOutMin
        );
        
        // DESTINATION : Appliquer la valeur ajustée à la position verticale du vaisseau
        if (asteroidDodger != null)
        {
            asteroidDodger.SetShipY(ajustee);
        }
    }
}