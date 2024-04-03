using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultFileName="MyLittleRPgSave";
        [SerializeField] float fadeOutTime = 1f;

        IEnumerator Start()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.ImmediateOpacityScreen();
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultFileName);
            yield return fader.FadeOut(fadeOutTime);
        }
        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultFileName);
        }
        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultFileName);
        }
        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(defaultFileName);
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L)){
                Debug.Log("Call to Load function");
                Load();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
                Debug.Log("Call to Save function");
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Delete();
                Debug.Log("Call to Delete function");
            }
        }
    }

}

