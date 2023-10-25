using UnityEngine;


namespace QFramework.Example
{
    public class FadeExample : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                ActionKit.Sequence()
                    // .Append(TransitionKit.FadeIn())
                    // .Append(TransitionKit.FadeOut())
                    // .Append(TransitionKit.FadeInOut())
                    .StartGlobal();
                
                
            }
        }
    }
}