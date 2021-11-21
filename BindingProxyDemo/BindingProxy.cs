using System.Windows;
using Serilog;

namespace BindingProxyDemo
{
    public class BindingProxy : Freezable
    {
        #region Overrides of Freezable

        protected override Freezable CreateInstanceCore() => new BindingProxy();

        #endregion

        public object Data
        {
            get => this.GetValue(DataProperty);
            set => this.SetValue(DataProperty, value);
        }
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnDataChanged)));

        private static void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Log.Debug("Proxy data changed from {oldVal} to {newVal}", e.OldValue, e.NewValue);
        }

    }
}
