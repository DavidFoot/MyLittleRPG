using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultFileName="MyLittleRPgSave";
        void Save()
        {
            GetComponent<SavingSystem>().Save(defaultFileName);
        }
        private void Load()
        {
            GetComponent<SavingSystem>().Load(defaultFileName);
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
        }
    }

}

