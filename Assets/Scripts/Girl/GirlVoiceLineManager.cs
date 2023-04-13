using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Girl
{
    public class GirlVoiceLineManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _girlText;
        private string[] _girlLinesIntro = new string[]
        {
            "Oh ! Un visiteur... dans ma maison ? Ça fait longtemps ! Tu dois être ici pour le livre de mes parents. Il leur a causé beaucoup d'ennuis, tu sais... Prends la lampe de poche, tu en auras besoin... Et suis-moi !",
            "Peux-tu m'aider à trouver ma poupée ? Elle est quelque part dans ma chambre...",
            "Génial ! Retourne à l'entrée avec moi. Je dois te montrer quelque chose...",
            "Place ma poupée au bon endroit sur la table."
        };
        private string[] _girlLinesObjects = new string[]
        {
            "Oh attention ! J'entends mon frère. Il a... changé depuis toutes ces années. Il déteste la lumière. Garder les lumières allumées devrait le tenir occupé. Il s'amuse à les éteindre. Attention à ne pas te retrouver seul dans le noir avec lui... Trouve son cheval en bois et rapporte-le ici. Je vais t'attendre. Garde la lampe de poche près de toi...",
            "Je crois que mon père est de retour... Il déteste être dérangé quand il fume sa pipe. Tiens-toi loin de lui. Trouve sa pipe et ramène-la ici. Sois prudent...",
            "Oh non... J'entends ma mère fredonner. Elle est de loin la plus agressive de la famille. Ça n'a pas toujours été comme ça... Si elle t'aperçoit, cours le plus vite que tu peux... Elle déteste les invités. Ramène sa tasse de thé. C'est la dernière pièce manquante. Fais attention...",
            "Le voilà ! Le fameux livre qui nous tient prisonniers. Tu dois le détruire s'il te plaît ! Tu vas nous aider, promis ?"
        };
        [SerializeField] private AudioClip _bookVoiceline;
        [SerializeField] private AudioClip _goodEndingVoiceline;
        [SerializeField] private AudioClip _badEndingVoiceline;


        public void PlayGirlVoicelineIntro(int index)
        {
            _girlText.text = _girlLinesIntro[index];
            if (index == 0 || index == 2) Invoke("ClearText", 5f);
            Debug.Log("Playing girl voiceline intro #" + index);
        }
        public void PlayGirlVoicelineObject(int index)
        {
            _girlText.text = _girlLinesObjects[index];
            Debug.Log("Playing girl voiceline object #" + index);
        }

        public void PlayBookVoiceline()
        {
            Debug.Log("Playing girl book voiceline");
        }

        public void PlayEndingVoiceline(bool good)
        {
            var endingVL = good ? "good" : "bad";
            Debug.Log("Playing girl ending voiceline " + endingVL);
        }

        private void ClearText()
        {
            _girlText.text = "";
        }
    }
}
