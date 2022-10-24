using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;

/// <summary>
/// Code by Jib Tenshi, 10/2022
/// Unity 2021.3.11f1 Personnal
/// </summary>

public class IATenshi : MonoBehaviour
{
    // Script du jeu
    [SerializeField] Game game;

    // Variables
    int i = 99;                 // indique quelle joueur doit jouer, joueur 1 '1' ou joueur 2 '2'
    int j = 99;                 // indique si le joueur peut jouer, non '0' ou oui '1'
    int k = 99;                 // indique si le joueur 1 est une IA '1' ou humain '0'
    int f = 99;                 // indique si le joueur 2 est une IA '1' ou humain '0'
    int tour = 0;               // indique le tour en cours

    int checktime = 0;          // marqueur de boucle IA
    public int iachoix = 99;    // marqueur indiquant le choix de l'IA

    // Tableau du plateau
    int[] Plateau = new int[42] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    //
    //
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        if (checktime == 0)
        {
            TestIA();
        }
        else if (checktime == 1)
        {
            // Debug.Log("IA en attente ... ");
        }
        else if (checktime != 0 && checktime != 1)
        {
            Debug.Log("Erreur IA");
        }
    }

    void TestIA()
    {
        i = game.ijoueur;           // quel joueur peut jouer ?     | 1 = joueur 1, 2 = joueur 2
        j = game.vjoueur;           // le joueur peut-il jouer ?    | 0 = non, 1 = oui
        k = game.selectp1;          // suis-je l'IA joueur 1 ?      | 0 = non, 1 = oui (IATenshi), 2 = oui (IACerpo), 3 = oui (IATingMei)
        f = game.selectp2;          // suis-je l'IA joueur 2 ?      | 0 = non, 1 = oui (IATenshi), 2 = oui (IACerpo), 3 = oui (IATingMei)
        tour = game.pnbtour;        // quel tour de jeu est-ce ? 

        Debug.Log("IA valeur de i = " + i + " IA valeur de j = " + j);
        Debug.Log("IA joueur 1 est " + k + " IA joueur 2 est " + f);

        // Si je suis le joueur 1, que je suis une IA et que le jeu me permet de jouer
        if (i == 1 && k == 1 && j == 1)
        {
            Debug.Log("Je suis l'IA joueur 1, je peux jouer.");
            EtatPlateau();
        }

        // Si je suis le joueur 2, que je suis une IA et que le jeu me permet de jouer
        if (i == 2 && f == 1 && j == 1)
        {
            Debug.Log("Je suis l'IA joueur 2, je peux jouer.");
            EtatPlateau();
        }

        // Attente de x secondes avant prochaine vérification des valeurs
        StartCoroutine(TestWaitingSeconds());

    }

    IEnumerator TestWaitingSeconds()
    {
        checktime = 1;

        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 10 seconds.
        yield return new WaitForSeconds(10);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);

        checktime = 0;

    }

    void EtatPlateau()
    {
        // Ce script récupère les informations de l'état du plateau au moment où l'IA est autorisée à jouer
        //

        int x = 0;

        for (x = 0; x < 42; x++)
        {
            Plateau[x] = game.Plateau[x];
        }

        Debug.Log("Etat du plateau : ");
        Debug.Log(Plateau[5] + " " + Plateau[11] + " " + Plateau[17] + " " + Plateau[23] + " " + Plateau[29] + " " + Plateau[35] + " " + Plateau[41]);
        Debug.Log(Plateau[4] + " " + Plateau[10] + " " + Plateau[16] + " " + Plateau[22] + " " + Plateau[28] + " " + Plateau[34] + " " + Plateau[40]);
        Debug.Log(Plateau[3] + " " + Plateau[9] + " " + Plateau[15] + " " + Plateau[21] + " " + Plateau[27] + " " + Plateau[33] + " " + Plateau[39]);
        Debug.Log(Plateau[2] + " " + Plateau[8] + " " + Plateau[14] + " " + Plateau[20] + " " + Plateau[26] + " " + Plateau[32] + " " + Plateau[38]);
        Debug.Log(Plateau[1] + " " + Plateau[7] + " " + Plateau[13] + " " + Plateau[19] + " " + Plateau[25] + " " + Plateau[31] + " " + Plateau[37]);
        Debug.Log(Plateau[0] + " " + Plateau[6] + " " + Plateau[12] + " " + Plateau[18] + " " + Plateau[24] + " " + Plateau[30] + " " + Plateau[36]);

        IAScript();
    }


    /// <summary>
    ///  Veillez à ne rien modifier au dessus de IAScript
    /// </summary>
    void IAScript()
    {
        /// Vous devez enregistrer le choix de votre ia dans cette variable.
        /// iachoix = [votre numéro de colonne de 1 à 7];
        /// exemple iachoix = 1;
        ///
        /// Merci de veiller à ce que le choix soit possible
        /// 6 jetons max par colonne
        /// 4 jetons alignés en vertical, horizontal ou diagonal pour gagner la partie
        ///
        /// Vous pouvez vérifier et connaitre la valeur de chaque case du plateau dans le tableau Plateau
        /// La position de la case est disponible dans EtatPlateau
        /// exemple : Plateau[8] est la case de la colonne 2, ligne 3
        /// case vide = 0, jeton jaune = 1, jeton rouge = 2
        /// 

        // Script de l'IA de Tenshi
        // Version 1
        Debug.Log("Début du script IA de Tenshi.");

        
        

        
        // Fin du script
        Debug.Log("IA de Tenshi a choisi la colonne : " + iachoix);
        Debug.Log("Fin du script IA de Tenshi.");
    }

}
