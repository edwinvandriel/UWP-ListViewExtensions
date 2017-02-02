using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EvD.ListViewExtensions
{
    /// <summary>
    /// AlternateRowTemplate extension extends your <see cref="ListViewBase"/> control with 
    /// an attached <see cref="DependencyProperty"/> property to provide alternate row template functionality
    /// </summary>
    public static class AlternateRowTemplate
    {
        /// <summary>
        /// Attached <see cref="DependencyProperty"/> for binding a <see cref="DataTemplate"/> as an alternate row template to a <see cref="ListViewBase"/>
        /// </summary>
        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.RegisterAttached(
            "ItemTemplate",
            typeof(DataTemplate),
            typeof(AlternateRowTemplate),
            new PropertyMetadata(null, OnItemTemplatePropertyChanged));

        /// <summary>
        /// Gets the <see cref="DataTemplate"/> associated with the specified <see cref="ListViewBase"/>
        /// </summary>
        /// <param name="obj">The <see cref="ListViewBase"/> to get the associated <see cref="DataTemplate"/> from</param>
        /// <returns>The <see cref="DataTemplate"/> associated with the <see cref="ListViewBase"/></returns>
        public static DataTemplate GetItemTemplate(ListViewBase obj)
        {
            return (DataTemplate)obj.GetValue(ItemTemplateProperty);
        }

        /// <summary>
        /// Sets the <see cref="DataTemplate"/> associated with the specified <see cref="ListViewBase"/>
        /// </summary>
        /// <param name="obj">The <see cref="ListViewBase"/> to associate the <see cref="DataTemplate"/> with</param>
        /// <param name="value">The <see cref="DataTemplate"/> for binding to the <see cref="ListViewBase"/></param>
        public static void SetItemTemplate(ListViewBase obj, DataTemplate value)
        {
            obj.SetValue(ItemTemplateProperty, value);
        }

        private static void OnItemTemplatePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ListViewBase listViewBase = sender as ListViewBase;

            if (listViewBase == null)
            {
                return;
            }

            listViewBase.ContainerContentChanging -= ItemTemplateContainerContentChanging;

            if (ItemTemplateProperty != null)
            {
                listViewBase.ContainerContentChanging += ItemTemplateContainerContentChanging;
            }
        }

        private static void ItemTemplateContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            var itemContainer = args.ItemContainer as ListViewItem;
            var itemIndex = sender.IndexFromContainer(itemContainer);

            if (itemIndex % 2 == 0)
            {
                itemContainer.ContentTemplate = GetItemTemplate(sender);
            }
            else
            {
                itemContainer.ContentTemplate = sender.ItemTemplate;
            }
        }

    }
}
