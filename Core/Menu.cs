namespace MultiplayCore
{
    using MultiplayCore;
    using System.Collections;
    using MultiplayCore.UI;
    using UnityEditor.Rendering.LookDev;

    public class Menu : Scene
    {
        protected override IEnumerator OnActivate()
        {
            yield return base.OnActivate();

            if (ApplicationSettings.IsQuickPlay == true)
            {
                UIMultiplayerView multiplayerView = Context.UI.Open<UIMultiplayerView>();
                multiplayerView.StartQuickPlay();
            }
        }
    }
}
