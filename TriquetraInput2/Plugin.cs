using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ModLoader.Framework;
using ModLoader.Framework.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

namespace Triquetra.Input
{
    [ItemId("danku-triquetra2")]
    public class Plugin : VtolMod
    {
        private GameObject imguiObject;
        private static string bindingsPath;

        public static bool asyncLoadingBindings;

        public static void Write(object msg)
        {
            Debug.Log(msg);
        }

        public void Awake()
        {
            Enable();
        }

        public void Enable()
        {
            imguiObject = new GameObject();
            imguiObject.AddComponent<TriquetraInputBinders>();
            GameObject.DontDestroyOnLoad(imguiObject);

            bindingsPath = PilotSaveManager.saveDataPath + "/triquetrainput.xml";
            //LoadBindings();
            StartCoroutine(LoadBindingsCoroutine());
        }

        public void Disable()
        {
            Debug.Log("Destroying Triquetra Input Object");
            GameObject.Destroy(imguiObject);
        }

        public static bool IsFlyingScene()
        {
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            return buildIndex == 7 || buildIndex == 11;
        }

        public static void SaveBindings()
        {
            XmlSerializer serializer = new XmlSerializer(Binding.Bindings.GetType());
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, Binding.Bindings);
                Debug.Log(writer.ToString());
            }
            using (TextWriter writer = new StreamWriter(bindingsPath))
            {
                serializer.Serialize(writer, Binding.Bindings);
            }
        }

        public static void LoadBindings()
        {
            Stopwatch sw = Stopwatch.StartNew();
            XmlSerializer serializer = new XmlSerializer(Binding.Bindings.GetType());
            if (File.Exists(bindingsPath))
            {
                using (Stream reader = new FileStream(bindingsPath, FileMode.Open))
                {
                    lock (Binding.Bindings)
                    {
                        Binding.Bindings = (List<Binding>)serializer.Deserialize(reader);
                    }
                }
            }
        }

        private static IEnumerator LoadBindingsCoroutine()
        {
            asyncLoadingBindings = true;
            var task = AsyncLoadBindings();
            yield return new WaitUntil(() => task.IsCompleted);
            asyncLoadingBindings = false;
        }

        private static async Task AsyncLoadBindings()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Binding>));
            if (File.Exists(bindingsPath))
            {
                using (Stream reader = new BufferedStream(new FileStream(bindingsPath, FileMode.Open)))
                {
                    var bindings = await Task.Run(() => (List<Binding>)serializer.Deserialize(reader));
                    Binding.Bindings = bindings;
                }
            }
        }

        public override void UnLoad()
        {
            Disable();
        }
    }
}