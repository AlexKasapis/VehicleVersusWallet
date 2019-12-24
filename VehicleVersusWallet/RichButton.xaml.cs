using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VehicleVersusWallet
{
	public partial class RichButton : UserControl
	{
		public RichButton()
		{
			InitializeComponent();
		}

		public String ButtonText {
			get { return GetValue(ButtonTextProperty).ToString(); }
			set { SetValue(ButtonTextProperty, value); }
		}
		public static readonly DependencyProperty ButtonTextProperty =
			DependencyProperty.Register("ButtonText", typeof(string), typeof(RichButton), new FrameworkPropertyMetadata(""));

		public ImageSource ButtonImage {
			get { return (ImageSource)GetValue(ButtonImageProperty); }
			set { SetValue(ButtonImageProperty, value); }
		}
		public static readonly DependencyProperty ButtonImageProperty =
			DependencyProperty.Register("ButtonImage", typeof(ImageSource), typeof(RichButton), new FrameworkPropertyMetadata(null));

		public Brush ButtonBackground {
			get { return (Brush)GetValue(ButtonBackgroundProperty); }
			set { SetValue(ButtonBackgroundProperty, value); }
		}
		public static readonly DependencyProperty ButtonBackgroundProperty =
			DependencyProperty.Register("ButtonBackground", typeof(Brush), typeof(RichButton), new FrameworkPropertyMetadata(null));

		public Brush ButtonBackgroundHover {
			get { return (Brush)GetValue(ButtonBackgroundHoverProperty); }
			set { SetValue(ButtonBackgroundHoverProperty, value); }
		}
		public static readonly DependencyProperty ButtonBackgroundHoverProperty =
			DependencyProperty.Register("ButtonBackgroundHover", typeof(Brush), typeof(RichButton), new FrameworkPropertyMetadata(null));

		public Brush ButtonBorderBrush {
			get { return (Brush)GetValue(ButtonBorderBrushProperty); }
			set { SetValue(ButtonBorderBrushProperty, value); }
		}
		public static readonly DependencyProperty ButtonBorderBrushProperty =
			DependencyProperty.Register("ButtonBorderBrush", typeof(Brush), typeof(RichButton), new FrameworkPropertyMetadata(null));

		public double? ButtonTextWidth {
			get { return (double?)GetValue(ButtonTextWidthProperty); }
			set { SetValue(ButtonTextWidthProperty, value); }
		}
		public static readonly DependencyProperty ButtonTextWidthProperty =
			DependencyProperty.Register("ButtonTextWidth", typeof(double?), typeof(RichButton), new FrameworkPropertyMetadata(null));

		public double? ButtonWidth {
			get { return (double?)GetValue(ButtonWidthProperty); }
			set { SetValue(ButtonWidthProperty, value); }
		}
		public static readonly DependencyProperty ButtonWidthProperty =
			DependencyProperty.Register("ButtonWidth", typeof(double?), typeof(RichButton), new FrameworkPropertyMetadata(null));

		public double? ButtonHeight {
			get { return (double?)GetValue(ButtonHeightProperty); }
			set { SetValue(ButtonHeightProperty, value); }
		}
		public static readonly DependencyProperty ButtonHeightProperty =
			DependencyProperty.Register("ButtonHeight", typeof(double?), typeof(RichButton), new FrameworkPropertyMetadata(null));

		public double? ButtonImageWidth {
			get { return (double?)GetValue(ButtonImageWidthProperty); }
			set { SetValue(ButtonImageWidthProperty, value); }
		}
		public static readonly DependencyProperty ButtonImageWidthProperty =
			DependencyProperty.Register("ButtonImageWidth", typeof(double?), typeof(RichButton), new FrameworkPropertyMetadata(null));

		public double? ButtonImageHeight {
			get { return (double?)GetValue(ButtonImageHeightProperty); }
			set { SetValue(ButtonImageHeightProperty, value); }
		}
		public static readonly DependencyProperty ButtonImageHeightProperty =
			DependencyProperty.Register("ButtonImageHeight", typeof(double?), typeof(RichButton), new FrameworkPropertyMetadata(null));

		public CornerRadius? ButtonCornerRadius {
			get { return (CornerRadius?)GetValue(ButtonCornerRadiusProperty); }
			set { SetValue(ButtonCornerRadiusProperty, value); }
		}
		public static readonly DependencyProperty ButtonCornerRadiusProperty =
			DependencyProperty.Register("ButtonCornerRadius", typeof(CornerRadius?), typeof(RichButton), new FrameworkPropertyMetadata(null));

		public Thickness? ButtonBorderThickness {
			get { return (Thickness?)GetValue(ButtonBorderThicknessProperty); }
			set { SetValue(ButtonBorderThicknessProperty, value); }
		}
		public static readonly DependencyProperty ButtonBorderThicknessProperty =
			DependencyProperty.Register("ButtonBorderThickness", typeof(Thickness?), typeof(RichButton), new FrameworkPropertyMetadata(null));

		public double? ButtonFontSize {
			get { return (double?)GetValue(ButtonFontSizeProperty); }
			set { SetValue(ButtonFontSizeProperty, value); }
		}
		public static readonly DependencyProperty ButtonFontSizeProperty =
			DependencyProperty.Register("ButtonFontSize", typeof(double?), typeof(RichButton), new FrameworkPropertyMetadata(null));

		public Brush ButtonTextBrush {
			get { return (Brush)GetValue(ButtonTextBrushProperty); }
			set { SetValue(ButtonTextBrushProperty, value); }
		}
		public static readonly DependencyProperty ButtonTextBrushProperty =
			DependencyProperty.Register("ButtonTextBrush", typeof(Brush), typeof(RichButton), new FrameworkPropertyMetadata(null));

		public FontWeight? ButtonFontWeight {
			get { return (FontWeight?)GetValue(ButtonFontWeightProperty); }
			set { SetValue(ButtonFontWeightProperty, value); }
		}
		public static readonly DependencyProperty ButtonFontWeightProperty =
			DependencyProperty.Register("ButtonFontWeight", typeof(FontWeight?), typeof(RichButton), new FrameworkPropertyMetadata(null));
	}
}
