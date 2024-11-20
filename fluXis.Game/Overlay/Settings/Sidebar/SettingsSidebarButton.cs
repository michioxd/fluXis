using System;
using fluXis.Game.Audio;
using fluXis.Game.Graphics.Sprites;
using fluXis.Game.Graphics.UserInterface;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osuTK;

namespace fluXis.Game.Overlay.Settings.Sidebar;

public partial class SettingsSidebarButton : Container
{
    public Action ClickAction { get; init; }

    [Resolved]
    private UISamples samples { get; set; }

    private SettingsSubSection section { get; }

    private Container content;
    private HoverLayer hover;
    private FlashLayer flash;

    public SettingsSidebarButton(SettingsSubSection subSection)
    {
        section = subSection;
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        RelativeSizeAxes = Axes.X;
        Height = 50;

        InternalChild = content = new Container
        {
            RelativeSizeAxes = Axes.Both,
            Masking = true,
            CornerRadius = 10,
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0
                },
                hover = new HoverLayer(),
                flash = new FlashLayer(),
                new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Direction = FillDirection.Horizontal,
                    Spacing = new Vector2(10),
                    Padding = new MarginPadding(10) { Horizontal = 15 },
                    Children = new Drawable[]
                    {
                        new FluXisSpriteIcon
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Icon = section.Icon,
                            Size = new Vector2(20)
                        },
                        new FluXisSpriteText
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Text = section.Title,
                            FontSize = 24
                        }
                    }
                }
            }
        };
    }

    protected override bool OnClick(ClickEvent e)
    {
        flash.Show();
        samples.Click();
        ClickAction?.Invoke();
        return true;
    }

    protected override bool OnHover(HoverEvent e)
    {
        hover.Show();
        samples.Hover();
        return true;
    }

    protected override void OnHoverLost(HoverLostEvent e)
    {
        hover.Hide();
    }

    protected override bool OnMouseDown(MouseDownEvent e)
    {
        content.ScaleTo(0.9f, 1000, Easing.OutQuint);
        return true;
    }

    protected override void OnMouseUp(MouseUpEvent e)
    {
        content.ScaleTo(1, 1000, Easing.OutElastic);
    }
}
