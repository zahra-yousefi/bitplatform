﻿namespace Bit.BlazorUI;

public partial class BitTag : BitComponentBase
{
    private BitVariant? variant;
    private BitSeverity? severity;



    /// <summary>
    /// Child content of component, the content that the tag will apply to.
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Custom CSS classes for different parts of the BitTag.
    /// </summary>
    [Parameter] public BitTagClassStyles? Classes { get; set; }

    /// <summary>
    /// The icon to show inside the tag.
    /// </summary>
    [Parameter] public string? IconName { get; set; }

    /// <summary>
    /// Click event handler of the tag.
    /// </summary>
    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

    /// <summary>
    /// Dismiss button click event, if set the dismiss icon will show up.
    /// </summary>
    [Parameter] public EventCallback<MouseEventArgs> OnDismiss { get; set; }

    /// <summary>
    /// The severity of the tag.
    /// </summary>
    [Parameter]
    public BitSeverity? Severity
    {
        get => severity;
        set
        {
            if (severity == value) return;

            severity = value;

            ClassBuilder.Reset();
        }
    }

    /// <summary>
    /// Custom CSS styles for different parts of the BitTag.
    /// </summary>
    [Parameter] public BitTagClassStyles? Styles { get; set; }

    /// <summary>
    /// The text of the tag.
    /// </summary>
    [Parameter] public string? Text { get; set; }

    /// <summary>
    /// The visual variant of the tag.
    /// </summary>
    [Parameter]
    public BitVariant? Variant
    {
        get => variant;
        set
        {
            if (variant == value) return;

            variant = value;

            ClassBuilder.Reset();
        }
    }


    protected override string RootElementClass => "bit-tag";

    protected override void RegisterCssClasses()
    {
        ClassBuilder.Register(() => Classes?.Root);

        ClassBuilder.Register(() => Variant switch
        {
            BitVariant.Fill => "bit-tag-fil",
            BitVariant.Outline => "bit-tag-otl",
            BitVariant.Text => "bit-tag-txt",
            _ => "bit-tag-fil"
        });

        ClassBuilder.Register(() => Severity switch
        {
            BitSeverity.Info => "bit-tag-inf",
            BitSeverity.Success => "bit-tag-suc",
            BitSeverity.Warning => "bit-tag-wrn",
            BitSeverity.SevereWarning => "bit-tag-swr",
            BitSeverity.Error => "bit-tag-err",
            _ => string.Empty
        });
    }

    protected override void RegisterCssStyles()
    {
        StyleBuilder.Register(() => Styles?.Root);
    }

    private async Task HandleOnDismissClick(MouseEventArgs e)
    {
        if (IsEnabled)
        {
            await OnDismiss.InvokeAsync(e);
        }
    }

    private async Task HandleOnClick(MouseEventArgs e)
    {
        if (IsEnabled)
        {
            await OnClick.InvokeAsync(e);
        }
    }
}