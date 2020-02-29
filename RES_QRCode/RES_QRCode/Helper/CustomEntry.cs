using Xamarin.Forms;

namespace RES_QRCode.Helper
{
    public class CustomEntry: Entry
    {
        #region Constructors

        public CustomEntry()
            : base()
        {

        }

        #endregion

        #region Properties

        public static BindableProperty BorderColorProperty = BindableProperty.Create<CustomEntry, Color>(o => o.BorderColor, Color.Transparent);

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }


        public static BindableProperty BorderWidthProperty = BindableProperty.Create<CustomEntry, float>(o => o.BorderWidth, 0);

        public float BorderWidth
        {
            get { return (float)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }

        public static BindableProperty BorderRadiusProperty = BindableProperty.Create<CustomEntry, float>(o => o.BorderRadius, 0);

        public float BorderRadius
        {
            get { return (float)GetValue(BorderRadiusProperty); }
            set { SetValue(BorderRadiusProperty, value); }
        }

        public static BindableProperty LeftPaddingProperty = BindableProperty.Create<CustomEntry, int>(o => o.LeftPadding, 5);

        public int LeftPadding
        {
            get { return (int)GetValue(LeftPaddingProperty); }
            set { SetValue(LeftPaddingProperty, value); }
        }

        public static BindableProperty RightPaddingProperty = BindableProperty.Create<CustomEntry, int>(o => o.RightPadding, 5);

        public int RightPadding
        {
            get { return (int)GetValue(RightPaddingProperty); }
            set { SetValue(RightPaddingProperty, value); }
        }

        public static BindableProperty TopPaddingProperty = BindableProperty.Create<CustomEntry, int>(o => o.LeftPadding, 5);

        public int TopPadding
        {
            get { return (int)GetValue(TopPaddingProperty); }
            set { SetValue(TopPaddingProperty, value); }
        }

        public static BindableProperty BottomPaddingProperty = BindableProperty.Create<CustomEntry, int>(o => o.LeftPadding, 5);

        public int BottomPadding
        {
            get { return (int)GetValue(BottomPaddingProperty); }
            set { SetValue(BottomPaddingProperty, value); }
        }

        #endregion
    }
}
