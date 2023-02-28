using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Xamarin.Forms.Internals;

/// <summary>
/// This class was imported from the Xam.Plugin.SimpleBottomDrawer project with some modifications
/// Ref: https://github.com/galadril/Xam.Plugin.SimpleBottomDrawer
/// </summary>
namespace PokedexXF.Controls
{
    /// <summary>
    /// Bottom drawer control with a border
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CustomBottomDrawer : Frame
    {
        #region Fields

        /// <summary>
        /// Needed to save the startposition on iOS
        /// </summary>
        private double translationYStart;

        /// <summary>
        /// Is the drawer being dragged
        /// </summary>
        private bool isDragging = false;

        /// <summary>
        /// Get width
        /// </summary>
        private double _width;

        /// <summary>
        /// Get height
        /// </summary>
        private double _height;

        /// <summary>
        /// Bindable property for the <see cref="IsOpen"/> property
        /// </summary>
        public static readonly BindableProperty IsOpenProperty = BindableProperty.Create(
            propertyName: nameof(IsOpen),
            returnType: typeof(bool),
            declaringType: typeof(CustomBottomDrawer),
            defaultValue: default(bool),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: IsOpenPropertyChanged);

        /// <summary>
        /// Bindable property for the <see cref="ExpandedPercentage"/> property
        /// </summary>
        public static readonly BindableProperty ExpandedPercentageProperty = BindableProperty.Create(
            propertyName: nameof(ExpandedPercentage),
            returnType: typeof(double),
            declaringType: typeof(CustomBottomDrawer),
            defaultValue: default(double),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: ExpandedPercentageChanged);

        /// <summary>
        /// Bindable property for the <see cref="ExpandedPercentage"/> property
        /// </summary>
        public static readonly BindableProperty LockStatesProperty = BindableProperty.Create(
            propertyName: nameof(LockStates),
            returnType: typeof(double[]),
            declaringType: typeof(CustomBottomDrawer),
            defaultValue: new double[] { 0, .4, .75 });

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CustomBottomDrawer()
        {
            // Set the default values for this control
            BackgroundColor = Color.White;
            CornerRadius = 0;
            HasShadow = false;

            // Set the pan gesture listeners
            var panGestures = new PanGestureRecognizer();
            panGestures.PanUpdated += OnPanChanged;
            GestureRecognizers.Add(panGestures);

            // Add click gesture listeners
            var tapGestures = new TapGestureRecognizer();
            tapGestures.Tapped += OnTapped;
            GestureRecognizers.Add(tapGestures);
        }

        #endregion

        #region Properties


        /// <summary>
        /// Gets or sets the lock statues
        /// </summary>
        public double[] LockStates
        {
            get => (double[])GetValue(LockStatesProperty);
            set => SetValue(LockStatesProperty, value);
        }

        /// <summary>
        /// Gets or sets the is expanded value
        /// </summary>
        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }

        /// <summary>
        /// Gets or sets the is expanded percentage
        /// </summary>
        public double ExpandedPercentage
        {
            get => (double)GetValue(ExpandedPercentageProperty);
            set => SetValue(ExpandedPercentageProperty, value);
        }

        #endregion

        #region Protected

        /// <summary>
        /// Make sure we collapse the view on orientation change
        /// </summary>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != _width || height != _height)
            {
                _width = width;
                _height = height;
                IsOpen = false;
            }
        }

        #endregion Protected

        #region Private

        /// <summary>
        /// Handle the change of the <see cref="IsOpen"/> property
        /// </summary>
        /// <param name="bindable">The bindable object</param>
        /// <param name="oldValue">The old value</param>
        /// <param name="newValue">The new value</param>
        private static void IsOpenPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue is bool isOpened && bindable is CustomBottomDrawer drawer)
            {
                if (!drawer.isDragging)
                {
                    if (!isOpened)
                        drawer.Close();
                    else
                        drawer.Open();
                }
            }
        }

        /// <summary>
        /// Handle the change of the <see cref="ExpandedPercentage"/> property
        /// </summary>
        /// <param name="bindable">The bindable object</param>
        /// <param name="oldValue">The old value</param>
        /// <param name="newValue">The new value</param>
        private static void ExpandedPercentageChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue is double expandValue && bindable is CustomBottomDrawer drawer)
            {
                if (!drawer.isDragging)
                {
                    var finalTranslation = Math.Max(Math.Min(0, -1000), -Math.Abs(drawer.getProportionCoordinate(expandValue)));
                    if (expandValue < 0)
                        drawer.TranslateTo(drawer.X, finalTranslation, 250, Easing.SpringIn);
                    else
                        drawer.TranslateTo(drawer.X, finalTranslation, 250, Easing.SpringOut);
                }
            }
        }

        /// <summary>
        /// On pan gesture changed
        /// </summary>
        private void OnPanChanged(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    isDragging = true;
                    var Y = (Device.RuntimePlatform == Device.Android ? this.TranslationY : translationYStart) + e.TotalY;
                    // Translate and ensure we don't y + e.TotalY pan beyond the wrapped user interface element bounds.
                    var translateY = Math.Max(Math.Min(0, Y), -Math.Abs((Height * .10) - Height));
                    this.TranslateTo(this.X, translateY, 1);
                    ExpandedPercentage = GetPropertionDistance(Y);
                    break;
                case GestureStatus.Completed:
                    // At the end of the event - snap to the closest location
                    var finalTranslation = Math.Max(Math.Min(0, -1000), -Math.Abs(getProportionCoordinate(GetClosestLockState(e.TotalY + this.TranslationY))));

                    // Depending on Swipe Up or Down - change the snapping animation
                    if (DetectSwipeUp(e))
                        this.TranslateTo(this.X, finalTranslation, 250, Easing.SpringIn);
                    else
                        this.TranslateTo(this.X, finalTranslation, 250, Easing.SpringOut);

                    ExpandedPercentage = GetClosestLockState(e.TotalY + this.TranslationY);
                    isDragging = false;
                    break;
                case GestureStatus.Started:
                    translationYStart = this.TranslationY;
                    break;
            }

            if (ExpandedPercentage > LockStates[LockStates.Length - 1])
                ExpandedPercentage = LockStates[LockStates.Length - 1];

            IsOpen = ExpandedPercentage > 0;
        }

        /// <summary>
        /// On tapped event
        /// </summary>
        private void OnTapped(object sender, EventArgs e)
        {
            if (!IsOpen)
            {
                ExpandedPercentage = LockStates[1];
                IsOpen = ExpandedPercentage > 0;
            }
        }

        /// <summary>
        /// Check if the action is a swipe up
        /// </summary>
        private bool DetectSwipeUp(PanUpdatedEventArgs e)
        {
            return e.TotalY < 0;
        }

        /// <summary>
        /// Find the closest lock state when swip is finished
        /// </summary>
        private double GetClosestLockState(double TranslationY)
        {
            // Play with these values to adjust the locking motions - this will change depending on the amount of content ona  apge
            double current = GetPropertionDistance(TranslationY);

            // Calculate which lockstate it's the closest to
            var smallestDistance = 10000.0;
            var closestIndex = 0;

            for (int i = 0; i < LockStates.Length; i++)
            {
                var state = LockStates[i];
                var absoluteDistance = Math.Abs(state - current);
                if (absoluteDistance < smallestDistance)
                {
                    smallestDistance = absoluteDistance;
                    closestIndex = i;
                }
            }

            return LockStates[closestIndex];
        }

        /// <summary>
        /// Get the current proportion of the sheet in relation to the screen
        /// </summary>
        private double GetPropertionDistance(double TranslationY)
        {
            return Math.Abs(TranslationY) / Height;
        }

        /// <summary>
        /// Get proportion coordinates
        /// </summary>
        private double getProportionCoordinate(double proportion)
        {
            return proportion * Height;
        }

        #endregion

        #region Public

        /// <summary>
        /// Close the bottom drawer
        /// </summary>
        public void Close()
        {
            var finalTranslation = Math.Max(Math.Min(0, -1000), -Math.Abs(getProportionCoordinate(LockStates[0])));
            this.TranslateTo(this.X, finalTranslation, 450, Device.RuntimePlatform == Device.Android ? Easing.SpringOut : null);
        }

        /// <summary>
        /// Open the bottom drawer
        /// </summary>
        public void Open()
        {
            var finalTranslation = Math.Max(Math.Min(0, -1000), -Math.Abs(getProportionCoordinate(LockStates[1])));
            this.TranslateTo(this.X, finalTranslation, 150, Device.RuntimePlatform == Device.Android ? Easing.SpringIn : null);
        }

        #endregion Public
    }
}
