using UnityEngine;

namespace QFramework
{
    public class TransitionKit
    {
        public static TransitionInOut<FadeTransition,FadeTransition> FadeInOut()
        {
            var fadeIn = new FadeTransition()
                .From(0)
                .To(1)
                .Duration(1.0f)
                .Color(Color.black);
            
            var fadeOut =  new FadeTransition()
                .From(1)
                .To(0)
                .Duration(1.0f)
                .Color(Color.black);

            var transition = new TransitionInOut<FadeTransition,FadeTransition>()
                .SetIn(fadeIn)
                .SetOut(fadeOut);

            return transition;
        }

        public static FadeTransition FadeIn()
        {
            var fade =  new FadeTransition()
                .From(0)
                .To(1)
                .Duration(1.0f)
                .Color(Color.black);
            return fade;
        }
        
        public static FadeTransition FadeOut()
        {
            var fade =  new FadeTransition()
                .From(1)
                .To(0)
                .Duration(1.0f)
                .Color(Color.black);
            return fade;
        }
    }
}