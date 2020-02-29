using System.ComponentModel;
using Android.Views;
using RES_QRCode.Helper;
using RES_QRCode.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace RES_QRCode.Droid.Renderers
{
    class CustomEntryRenderer:EntryRenderer
    {
        #region Private fields and properties

        private BorderRenderer _renderer;
        private const GravityFlags DefaultGravity = GravityFlags.CenterVertical;

        #endregion

        #region Parent override

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || this.Element == null)
                return;
            Control.Gravity = DefaultGravity;
            var CustomEntry = Element as CustomEntry;
            UpdateBackground(CustomEntry);
            UpdatePadding(CustomEntry);
            UpdateTextAlighnment(CustomEntry);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Element == null)
                return;
            var CustomEntry = Element as CustomEntry;
            if (e.PropertyName == CustomEntry.BorderWidthProperty.PropertyName ||
                e.PropertyName == CustomEntry.BorderColorProperty.PropertyName ||
                e.PropertyName == CustomEntry.BorderRadiusProperty.PropertyName ||
                e.PropertyName == CustomEntry.BackgroundColorProperty.PropertyName)
            {
                UpdateBackground(CustomEntry);
            }
            else if (e.PropertyName == CustomEntry.LeftPaddingProperty.PropertyName ||
                e.PropertyName == CustomEntry.RightPaddingProperty.PropertyName)
            {
                UpdatePadding(CustomEntry);
            }
            else if (e.PropertyName == Entry.HorizontalTextAlignmentProperty.PropertyName)
            {
                UpdateTextAlighnment(CustomEntry);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (_renderer != null)
                {
                    _renderer.Dispose();
                    _renderer = null;
                }
            }
        }

        #endregion

        #region Utility methods

        private void UpdateBackground(CustomEntry CustomEntry)
        {
            if (_renderer != null)
            {
                _renderer.Dispose();
                _renderer = null;
            }
            _renderer = new BorderRenderer();

            Control.Background = _renderer.GetBorderBackground(CustomEntry.BorderColor, CustomEntry.BackgroundColor, CustomEntry.BorderWidth, CustomEntry.BorderRadius);
        }

        private void UpdatePadding(CustomEntry CustomEntry)
        {
            Control.SetPadding((int)Forms.Context.ToPixels(CustomEntry.LeftPadding), (int)Forms.Context.ToPixels(CustomEntry.TopPadding),
                (int)Forms.Context.ToPixels(CustomEntry.RightPadding), (int)Forms.Context.ToPixels(CustomEntry.BottomPadding));
        }

        private void UpdateTextAlighnment(CustomEntry CustomEntry)
        {
            var gravity = DefaultGravity;
            switch (CustomEntry.HorizontalTextAlignment)
            {
                case Xamarin.Forms.TextAlignment.Start:
                    gravity |= GravityFlags.Start;
                    break;
                case Xamarin.Forms.TextAlignment.Center:
                    gravity |= GravityFlags.CenterHorizontal;
                    break;
                case Xamarin.Forms.TextAlignment.End:
                    gravity |= GravityFlags.End;
                    break;
            }
            Control.Gravity = gravity;
        }

        #endregion
    }
}