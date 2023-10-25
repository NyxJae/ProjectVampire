using EGO.Framework;
using EGO.ViewController;
using UnityEditor;

namespace EGO
{
    public class EGOWindow : Window
    {
        [MenuItem("EGO/MainWindow %t")]
        static void Open()
        {
            Open<EGOWindow>("EGO Window");
        }

        protected override void OnInit()
        {
            ViewController = CreateViewController<TodoListController>();
        }

        protected override void OnDispose()
        {
        }
    }
}