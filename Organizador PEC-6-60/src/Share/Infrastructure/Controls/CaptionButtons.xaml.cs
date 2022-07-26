﻿using System.Windows;
using System.Windows.Controls;

namespace Organizador_PEC_6_60.Share.Infrastructure.Controls;

/// <summary>
///     Lógica de interacción para CaptionButtons.xaml
/// </summary>
public partial class CaptionButtons : UserControl
{
    /// <summary>
    ///     Enum of the types of caption buttons
    /// </summary>
    public enum CaptionType
    {
        /// <summary>
        ///     All the buttons
        /// </summary>
        Full,

        /// <summary>
        ///     Only the close button
        /// </summary>
        Close,

        /// <summary>
        ///     Reduce and close buttons
        /// </summary>
        ReduceClose
    }

    /// <summary>
    ///     The dependency property for the Margin between the buttons.
    /// </summary>
    public static DependencyProperty MarginButtonProperty = DependencyProperty.Register(
        "MarginButton",
        typeof(Thickness),
        typeof(Window));

    /// <summary>
    ///     The dependency property for the Margin between the buttons.
    /// </summary>
    public static DependencyProperty TypeProperty = DependencyProperty.Register(
        "Type",
        typeof(CaptionType),
        typeof(Window),
        new PropertyMetadata(CaptionType.Full));

    /// <summary>
    ///     The parent Window of the control.
    /// </summary>
    private Window _parent;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CaptionButtons" /> class.
    /// </summary>
    public CaptionButtons()
    {
        InitializeComponent();
        Loaded += CaptionButtonsLoaded;
    }

    /// <summary>
    ///     Gets or sets the margin button.
    /// </summary>
    /// <value>The margin button.</value>
    public Thickness MarginButton
    {
        get => (Thickness)GetValue(MarginButtonProperty);
        set => SetValue(MarginButtonProperty, value);
    }

    /// <summary>
    ///     Gets or sets the visibility of the buttons.
    /// </summary>
    /// <value>The visible buttons.</value>
    public CaptionType Type
    {
        get => (CaptionType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>
    ///     Event when the control is loaded.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
    private void CaptionButtonsLoaded(object sender, RoutedEventArgs e)
    {
        _parent = GetTopParent();
    }

    /// <summary>
    ///     Action on the button to close the window.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
    private void CloseButtonClick(object sender, RoutedEventArgs e)
    {
        _parent.Close();
    }

    /// <summary>
    ///     Changes the view of the window (maximized or normal).
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
    private void RestoreButtonClick(object sender, RoutedEventArgs e)
    {
        _parent.WindowState = _parent.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
    }

    /// <summary>
    ///     Minimizes the Window.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
    private void MinimizeButtonClick(object sender, RoutedEventArgs e)
    {
        _parent.WindowState = WindowState.Minimized;
    }

    /// <summary>
    ///     Gets the top parent (Window).
    /// </summary>
    /// <returns>The parent Window.</returns>
    private Window GetTopParent()
    {
        return Window.GetWindow(this);
    }
}