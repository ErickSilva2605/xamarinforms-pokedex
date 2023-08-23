using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Extensions;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PokedexXF.Controls;

public partial class CustomSearchBar : ContentView, IDisposable
{
    private readonly Dictionary<string, Action> _propertyChangeActions;

    public static readonly BindableProperty NormalStateBackgroundColorProperty =
        BindableProperty.Create(
            nameof(NormalStateBackgroundColor),
            typeof(Color),
            typeof(CustomSearchBar),
            default(Color));

    public static readonly BindableProperty FocusedStateBackgroundColorProperty =
        BindableProperty.Create(
            nameof(FocusedStateBackgroundColor),
            typeof(Color),
            typeof(CustomSearchBar),
            null);

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(CustomSearchBar),
            string.Empty,
            BindingMode.TwoWay);

    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(
            nameof(TextColor),
            typeof(Color),
            typeof(CustomSearchBar),
            default(Color));

    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(CustomSearchBar),
            string.Empty);

    public static readonly BindableProperty PlaceholderColorProperty =
        BindableProperty.Create(
            nameof(PlaceholderColor),
            typeof(Color),
            typeof(CustomSearchBar),
            default(Color));

    public static readonly BindableProperty LeadingIconSourceProperty =
        BindableProperty.Create(
            nameof(LeadingIconSource),
            typeof(ImageSource),
            typeof(CustomSearchBar),
            default(ImageSource));

    public static readonly BindableProperty LeadingIconColorProperty =
        BindableProperty.Create(
            nameof(LeadingIconColor),
            typeof(Color),
            typeof(CustomSearchBar),
            default(Color));

    public static readonly BindableProperty ReturnTypeProperty =
        BindableProperty.Create(
            nameof(ReturnType),
            typeof(ReturnType),
            typeof(CustomSearchBar),
            default(ReturnType));

    public static readonly BindableProperty TextChangeCommandProperty =
        BindableProperty.Create(
            nameof(TextChangeCommand),
            typeof(ICommand),
            typeof(CustomSearchBar));

    public static readonly BindableProperty CompletedCommandProperty =
        BindableProperty.Create(
            nameof(CompletedCommand),
            typeof(ICommand),
            typeof(CustomSearchBar));

    public Color NormalStateBackgroundColor
    {
        get => (Color)GetValue(NormalStateBackgroundColorProperty);
        set => SetValue(NormalStateBackgroundColorProperty, value);
    }

    public Color FocusedStateBackgroundColor
    {
        get => (Color)GetValue(FocusedStateBackgroundColorProperty);
        set => SetValue(FocusedStateBackgroundColorProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public Color PlaceholderColor
    {
        get => (Color)GetValue(PlaceholderColorProperty);
        set => SetValue(PlaceholderColorProperty, value);
    }

    public ImageSource LeadingIconSource
    {
        get => (ImageSource)GetValue(LeadingIconSourceProperty);
        set => SetValue(LeadingIconSourceProperty, value);
    }

    public Color LeadingIconColor
    {
        get => (Color)GetValue(LeadingIconColorProperty);
        set => SetValue(LeadingIconColorProperty, value);
    }

    public ReturnType ReturnType
    {
        get => (ReturnType)GetValue(ReturnTypeProperty);
        set => SetValue(ReturnTypeProperty, value);
    }

    public ICommand TextChangeCommand
    {
        get => (ICommand)GetValue(TextChangeCommandProperty);
        set => SetValue(TextChangeCommandProperty, value);
    }

    public ICommand CompletedCommand
    {
        get => (ICommand)GetValue(CompletedCommandProperty);
        set => SetValue(CompletedCommandProperty, value);
    }

    public new event EventHandler<FocusEventArgs> Focused;
    public new event EventHandler<FocusEventArgs> Unfocused;
    public event EventHandler<TextChangedEventArgs> TextChanged;
    public event EventHandler Completed;

    public CustomSearchBar()
    {
        InitializeComponent();

        SetPropertyChangeHandler(ref _propertyChangeActions);
        SetControl();
    }

    private void SetControl()
    {
        inputText.TextChanged += Entry_TextChanged;
        inputText.Focused += Entry_Focused;
        inputText.Unfocused += Entry_Unfocused;
        inputText.Completed += Entry_Completed;

        tapGesture.Command = new Command(() =>
        {
            if (!inputText.IsFocused)
                inputText.Focus();
        });
    }

    public void Dispose()
    {
        inputText.TextChanged -= Entry_TextChanged;
        inputText.Focused -= Entry_Focused;
        inputText.Unfocused -= Entry_Unfocused;
        inputText.Completed -= Entry_Completed;
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
            { nameof(TextColor), () => OnTextColorChanged(TextColor) },
            { nameof(Placeholder), () => OnPlaceholderChanged(Placeholder) },
            { nameof(PlaceholderColor), () => OnPlaceholderColorChanged(PlaceholderColor) },
            { nameof(NormalStateBackgroundColor), () => OnNormalStateBackgroundColorChanged(NormalStateBackgroundColor) },
            { nameof(FocusedStateBackgroundColor), () => OnFocusedStateBackgroundColorChanged(FocusedStateBackgroundColor) },
            { nameof(ReturnType), () => OnReturnTypeChanged(ReturnType) },
            { nameof(LeadingIconSource), () => OnLeadingIconSourceChanged(LeadingIconSource) },
            { nameof(LeadingIconColor), () => OnLeadingIconColorChanged(LeadingIconColor) },
        };
    }

    private void OnTextChanged(string text) =>
        inputText.Text = text;

    private void OnTextColorChanged(Color textColor) =>
        inputText.TextColor = textColor;

    private void OnPlaceholderChanged(string placeholderText) =>
        inputText.Placeholder = placeholderText ?? string.Empty;

    private void OnPlaceholderColorChanged(Color placeholderColor) =>
        inputText.PlaceholderColor = placeholderColor;

    private void OnNormalStateBackgroundColorChanged(Color normalStateBackgroundColor) =>
        container.BackgroundColor = normalStateBackgroundColor;

    private void OnFocusedStateBackgroundColorChanged(Color focusedStateBackgroundColor) =>
        container.BackgroundColor = focusedStateBackgroundColor;

    private void OnReturnTypeChanged(ReturnType returnType) =>
        inputText.ReturnType = returnType;

    private void OnLeadingIconSourceChanged(ImageSource imageSource)
    {
        leadingIcon.Source = imageSource;
        OnLeadingIconColorChanged(LeadingIconColor);
    }

    private void OnLeadingIconColorChanged(Color leadingIconColor) =>
        leadingIcon.Behaviors.Add(new IconTintColorBehavior { TintColor = leadingIconColor });

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        TextChangeCommand?.Execute(null);
        TextChanged?.Invoke(this, e);

    }

    private void Entry_Completed(object sender, EventArgs e)
    {
        CompletedCommand?.Execute(null);
        Completed?.Invoke(this, EventArgs.Empty);
    }

    private void Entry_Focused(object sender, FocusEventArgs e)
    {
        if (FocusedStateBackgroundColor != null && !container.BackgroundColor.Equals(FocusedStateBackgroundColor))
            AnimateToFocusedState();

        Focused?.Invoke(this, e);
    }

    private void Entry_Unfocused(object sender, FocusEventArgs e)
    {
        if (FocusedStateBackgroundColor != null && !container.BackgroundColor.Equals(NormalStateBackgroundColor))
            AnimateToUnfocusedState();

        Unfocused?.Invoke(this, e);
    }

    private void AnimateToFocusedState()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Task.WhenAll
            (
                container.BackgroundColorTo(FocusedStateBackgroundColor)
            );
        });
    }

    private void AnimateToUnfocusedState()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Task.WhenAll
            (
                container.BackgroundColorTo(NormalStateBackgroundColor)
            );
        });
    }
}