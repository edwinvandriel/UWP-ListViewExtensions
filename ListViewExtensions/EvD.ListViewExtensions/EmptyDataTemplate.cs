using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EvD.ListViewExtensions
{
    /// <summary>
    /// AlternateRowTemplate extension extends your <see cref="ListViewBase"/> control with 
    /// an attached <see cref="DependencyProperty"/> property to provide alternate row template functionality
    /// </summary>
    public static class EmptyDataTemplate
    {
        private static ControlTemplate _originalTemplate;
        private static ListViewBase _originalListViewBase;

        /// <summary>
        /// Attached <see cref="DependencyProperty"/> for binding a <see cref="DataTemplate"/> as an alternate row template to a <see cref="ListViewBase"/>
        /// </summary>
        public static readonly DependencyProperty DataTemplateProperty = DependencyProperty.RegisterAttached(
            "ControlTemplate",
            typeof(ControlTemplate),
            typeof(EmptyDataTemplate),
            new PropertyMetadata(null, OnControlTemplatePropertyChanged));

        /// <summary>
        /// Gets the <see cref="ControlTemplate"/> associated with the specified <see cref="ListViewBase"/>
        /// </summary>
        /// <param name="obj">The <see cref="ListViewBase"/> to get the associated <see cref="ControlTemplate"/> from</param>
        /// <returns>The <see cref="ControlTemplate"/> associated with the <see cref="ListViewBase"/></returns>
        public static ControlTemplate GetControlTemplate(ListViewBase obj)
        {
            return (ControlTemplate)obj.GetValue(DataTemplateProperty);
        }

        /// <summary>
        /// Sets the <see cref="ControlTemplate"/> associated with the specified <see cref="ListViewBase"/>
        /// </summary>
        /// <param name="obj">The <see cref="ListViewBase"/> to associate the <see cref="ControlTemplate"/> with</param>
        /// <param name="value">The <see cref="ControlTemplate"/> for binding to the <see cref="ListViewBase"/></param>
        public static void SetControlTemplate(ListViewBase obj, ControlTemplate value)
        {
            obj.SetValue(DataTemplateProperty, value);
        }

        private static void OnControlTemplatePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            _originalListViewBase = sender as ListViewBase;

            if (_originalListViewBase == null)
            {
                return;
            }

            _originalListViewBase.Items.VectorChanged -= Items_VectorChanged;

            if (DataTemplateProperty != null)
            {
                _originalListViewBase.Items.VectorChanged += Items_VectorChanged;
            }
        }

        private static void Items_VectorChanged(IObservableVector<object> sender, IVectorChangedEventArgs @event)
        {
            if (_originalTemplate == null)
            {
                _originalTemplate = _originalListViewBase.Template;
            }

            SetProperTemplate(sender.Count, @event.CollectionChange);
        }

        private static void SetProperTemplate(int count, CollectionChange collectionChange = CollectionChange.Reset)
        {
            if (count == 0)
            {
                SetEmptyTemplate();
            }
            else
            {
                SetOriginalTemplate(collectionChange);
            }
        }

        private static void SetEmptyTemplate()
        {
            var emptyTemplate = GetControlTemplate(_originalListViewBase);

            if (_originalListViewBase.Template == emptyTemplate)
            {
                return;
            }

            _originalListViewBase.Template = emptyTemplate;
        }

        private static void SetOriginalTemplate(CollectionChange collectionChange)
        {
            if (_originalTemplate == null)
            {
                return;
            }

            if (collectionChange != CollectionChange.Reset && _originalListViewBase.Template != _originalTemplate)
            {
                _originalListViewBase.Template = _originalTemplate;
            }
        }
    }
}
