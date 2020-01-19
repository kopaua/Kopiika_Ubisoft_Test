using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T Singelton
    {
        get
        {
            if (instance == null)
            {
                // look for gameobjects first with component T
                instance = FindObjectOfType<T>();

                //if no such component is still not found, create gameobject with component
                if (instance == null)
                {
                    var obj = new GameObject();
                    //obj.hideFlags = HideFlags.HideAndDontSave;
                    instance = obj.AddComponent<T>();
                }               
            }

            return instance;
        }

        set
        {
            instance = value;
        }
    }  
}
