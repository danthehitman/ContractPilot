using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ContractPilot;

public class OutlinedText : FrameworkElement
{
	private GlyphRun glyphRun;

	private Geometry outline;

	public static readonly DependencyProperty TextProperty = TextBlock.TextProperty.AddOwner(typeof(OutlinedText), new FrameworkPropertyMetadata(delegate(DependencyObject o, DependencyPropertyChangedEventArgs e)
	{
		((OutlinedText)o).glyphRun = null;
	})
	{
		AffectsMeasure = true
	});

	public static readonly DependencyProperty FontSizeProperty = TextBlock.FontSizeProperty.AddOwner(typeof(OutlinedText), new FrameworkPropertyMetadata(delegate(DependencyObject o, DependencyPropertyChangedEventArgs e)
	{
		((OutlinedText)o).glyphRun = null;
	})
	{
		AffectsMeasure = true
	});

	public static readonly DependencyProperty FontFamilyProperty = TextBlock.FontFamilyProperty.AddOwner(typeof(OutlinedText), new FrameworkPropertyMetadata(delegate(DependencyObject o, DependencyPropertyChangedEventArgs e)
	{
		((OutlinedText)o).glyphRun = null;
	})
	{
		AffectsMeasure = true
	});

	public static readonly DependencyProperty FontStyleProperty = TextBlock.FontStyleProperty.AddOwner(typeof(OutlinedText), new FrameworkPropertyMetadata(delegate(DependencyObject o, DependencyPropertyChangedEventArgs e)
	{
		((OutlinedText)o).glyphRun = null;
	})
	{
		AffectsMeasure = true
	});

	public static readonly DependencyProperty FontWeightProperty = TextBlock.FontWeightProperty.AddOwner(typeof(OutlinedText), new FrameworkPropertyMetadata(delegate(DependencyObject o, DependencyPropertyChangedEventArgs e)
	{
		((OutlinedText)o).glyphRun = null;
	})
	{
		AffectsMeasure = true
	});

	public static readonly DependencyProperty FontStretchProperty = TextBlock.FontStretchProperty.AddOwner(typeof(OutlinedText), new FrameworkPropertyMetadata(delegate(DependencyObject o, DependencyPropertyChangedEventArgs e)
	{
		((OutlinedText)o).glyphRun = null;
	})
	{
		AffectsMeasure = true
	});

	public static readonly DependencyProperty ForegroundProperty = TextBlock.ForegroundProperty.AddOwner(typeof(OutlinedText), new FrameworkPropertyMetadata(delegate(DependencyObject o, DependencyPropertyChangedEventArgs e)
	{
		((OutlinedText)o).glyphRun = null;
	})
	{
		AffectsMeasure = true
	});

	public static readonly DependencyProperty BackgroundProperty = TextBlock.BackgroundProperty.AddOwner(typeof(OutlinedText), new FrameworkPropertyMetadata(Brushes.White, delegate(DependencyObject o, DependencyPropertyChangedEventArgs e)
	{
		((OutlinedText)o).glyphRun = null;
	})
	{
		AffectsMeasure = true
	});

	public static readonly DependencyProperty OutlineThicknessProperty = DependencyProperty.Register("OutlineThickness", typeof(double), typeof(OutlinedText), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsMeasure, delegate(DependencyObject o, DependencyPropertyChangedEventArgs e)
	{
		((OutlinedText)o).glyphRun = null;
	}));

	public string Text
	{
		get
		{
			return (string)GetValue(TextProperty);
		}
		set
		{
			SetValue(TextProperty, value);
		}
	}

	public double FontSize
	{
		get
		{
			return (double)GetValue(FontSizeProperty);
		}
		set
		{
			SetValue(FontSizeProperty, value);
		}
	}

	public FontFamily FontFamily
	{
		get
		{
			return (FontFamily)GetValue(FontFamilyProperty);
		}
		set
		{
			SetValue(FontFamilyProperty, value);
		}
	}

	public FontStyle FontStyle
	{
		get
		{
			return (FontStyle)GetValue(FontStyleProperty);
		}
		set
		{
			SetValue(FontStyleProperty, value);
		}
	}

	public FontWeight FontWeight
	{
		get
		{
			return (FontWeight)GetValue(FontWeightProperty);
		}
		set
		{
			SetValue(FontWeightProperty, value);
		}
	}

	public FontStretch FontStretch
	{
		get
		{
			return (FontStretch)GetValue(FontStretchProperty);
		}
		set
		{
			SetValue(FontStretchProperty, value);
		}
	}

	public Brush Foreground
	{
		get
		{
			return (Brush)GetValue(ForegroundProperty);
		}
		set
		{
			SetValue(ForegroundProperty, value);
		}
	}

	public Brush Background
	{
		get
		{
			return (Brush)GetValue(BackgroundProperty);
		}
		set
		{
			SetValue(BackgroundProperty, value);
		}
	}

	public double OutlineThickness
	{
		get
		{
			return (double)GetValue(OutlineThicknessProperty);
		}
		set
		{
			SetValue(OutlineThicknessProperty, value);
		}
	}

	protected override Size MeasureOverride(Size availableSize)
	{
		return CheckGlyphRun() ? outline.Bounds.Size : default(Size);
	}

	protected override void OnRender(DrawingContext drawingContext)
	{
		if (CheckGlyphRun())
		{
			Point location = outline.Bounds.Location;
			drawingContext.PushTransform(new TranslateTransform(0.0 - location.X, 0.0 - location.Y));
			drawingContext.DrawGeometry(Background, null, outline);
			drawingContext.DrawGlyphRun(Foreground, glyphRun);
		}
	}

	private bool CheckGlyphRun()
	{
		if (glyphRun == null)
		{
			if (string.IsNullOrEmpty(Text))
			{
				return false;
			}
			Typeface typeface = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);
			if (!typeface.TryGetGlyphTypeface(out var glyphTypeface))
			{
				return false;
			}
			ushort[] glyphIndices = new ushort[Text.Length];
			double[] advanceWidths = new double[Text.Length];
			for (int i = 0; i < Text.Length; i++)
			{
				ushort glyphIndex = (glyphIndices[i] = glyphTypeface.CharacterToGlyphMap[Text[i]]);
				advanceWidths[i] = glyphTypeface.AdvanceWidths[glyphIndex] * FontSize;
			}
			glyphRun = new GlyphRun(glyphTypeface, 0, isSideways: false, FontSize, 1f, glyphIndices, default(Point), advanceWidths, null, null, null, null, null, null);
			outline = glyphRun.BuildGeometry().GetWidenedPathGeometry(new Pen(null, OutlineThickness * 2.0));
		}
		return true;
	}
}
