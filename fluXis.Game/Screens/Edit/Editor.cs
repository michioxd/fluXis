using fluXis.Game.Graphics.Background;
using fluXis.Game.Input;
using fluXis.Game.Map;
using fluXis.Game.Screens.Edit.Tabs;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.Screens;

namespace fluXis.Game.Screens.Edit
{
    public class Editor : Screen, IKeyBindingHandler<FluXisKeybind>
    {
        public MapInfo Map;

        private Container tabs;
        private int currentTab;

        public Editor(MapSet mapset = null, MapInfo map = null)
        {
            var set = mapset ?? new MapSet("");
            Map = map ?? (set.Maps.Count > 0 ? set.Maps[0] : new MapInfo(new MapMetadata()));
        }

        [BackgroundDependencyLoader]
        private void load(BackgroundStack backgrounds)
        {
            backgrounds.AddBackgroundFromMap(Map);

            InternalChildren = new Drawable[]
            {
                tabs = new Container
                {
                    Padding = new MarginPadding { Top = 50, Bottom = 10, Left = 10, Right = 10 },
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new SetupTab(this),
                        new ComposeTab(this),
                        new TimingTab(this),
                    }
                },
                new EditorToolbar(this)
            };

            ChangeTab(0);
        }

        public void ChangeTab(int to)
        {
            currentTab = to;

            if (currentTab < 0)
                currentTab = 0;
            if (currentTab >= tabs.Count)
                currentTab = tabs.Count - 1;

            for (var i = 0; i < tabs.Children.Count; i++)
            {
                Drawable tab = tabs.Children[i];
                tab.FadeTo(i == currentTab ? 1 : 0);
            }
        }

        public bool OnPressed(KeyBindingPressEvent<FluXisKeybind> e)
        {
            switch (e.Action)
            {
                case FluXisKeybind.Exit:
                    this.Exit();
                    return true;
            }

            return false;
        }

        public void OnReleased(KeyBindingReleaseEvent<FluXisKeybind> e)
        {
        }

        public override void OnEntering(ScreenTransitionEvent e)
        {
            this.FadeInFromZero(100);
            base.OnEntering(e);
        }

        public override bool OnExiting(ScreenExitEvent e)
        {
            this.FadeOut(100);
            return base.OnExiting(e);
        }
    }
}
