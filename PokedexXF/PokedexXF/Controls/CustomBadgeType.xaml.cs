using Microsoft.Maui.Graphics.Text;
using Microsoft.Maui;
using System.Runtime.CompilerServices;

namespace PokedexXF.Controls;

public partial class CustomBadgeType : ContentView
{
    private readonly Dictionary<string, Action> _propertyChangeActions;

    public static readonly BindableProperty BadgeBackgroundColorProperty =
        BindableProperty.Create(
            nameof(BadgeBackgroundColor),
            typeof(Color),
            typeof(CustomBadgeType),
            default(Color));

    public static readonly BindableProperty IconSourceProperty =
        BindableProperty.Create(
            nameof(IconSource),
            typeof(ImageSource),
            typeof(CustomBadgeType),
            default(ImageSource));

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(CustomBadgeType),
            string.Empty,
            BindingMode.TwoWay);

    public Color BadgeBackgroundColor
    {
        get => (Color)GetValue(BadgeBackgroundColorProperty);
        set => SetValue(BadgeBackgroundColorProperty, value);
    }

    public ImageSource IconSource
    {
        get => (ImageSource)GetValue(IconSourceProperty);
        set => SetValue(IconSourceProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public CustomBadgeType()
    {
        InitializeComponent();
        SetPropertyChangeHandler(ref _propertyChangeActions);
    }

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName == null)
            return;

        if (_propertyChangeActions != null &&
            _propertyChangeActions.TryGetValue(propertyName, out var handlePropertyChange))
            handlePropertyChange();
    }

    private void SetPropertyChangeHandler(ref Dictionary<string, Action> propertyChangeActions)
    {
        propertyChangeActions = new Dictionary<string, Action>
        {
            { nameof(Text), () => OnTextChanged(Text) },
            { nameof(BadgeBackgroundColor), () => OnBadgeBackgroundColorPChanged(BadgeBackgroundColor) },
            { nameof(IconSource), () => OnIconSourceChanged(IconSource) },
        };
    }

    private void OnTextChanged(string text) =>
        labelType.Text = text;

    private void OnBadgeBackgroundColorPChanged(Color badgeBackgroundColor) =>
        badgeContainer.BackgroundColor = badgeBackgroundColor;

    private void OnIconSourceChanged(ImageSource iconSource) =>
        icon.Source = iconSource;
}