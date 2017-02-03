using System.Collections.Generic;
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
        private static Dictionary<int, ControlTemplate> _originalTemplates = new Dictionary<int, ControlTemplate>();

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
            ListViewBase listViewBase = sender as ListViewBase;

            if (listViewBase == null)
            {
                return;
            }

            if (DataTemplateProperty != null)
            {
                var listViewIdentifier = listViewBase.GetHashCode();

                listViewBase.Items.VectorChanged += (s, e) =>
                {
                    if (_originalTemplates.ContainsKey(listViewIdentifier) == false)
                    {
                        _originalTemplates.Add(listViewIdentifier, listViewBase.Template);
                    }

                    SetProperTemplate(listViewBase, s.Count, e.CollectionChange);
                };
            }
        }

        private static void SetProperTemplate(ListViewBase listViewBase, int count, CollectionChange collectionChange = CollectionChange.Reset)
        {
            if (count == 0)
            {
                SetEmptyTemplate(listViewBase);
            }
            else
            {
                SetOriginalTemplate(listViewBase, collectionChange);
            }
        }

        private static void SetEmptyTemplate(ListViewBase listViewBase)
        {
            var emptyTemplate = GetControlTemplate(listViewBase);

            if (listViewBase.Template == emptyTemplate)
            {
                return;
            }

            listViewBase.Template = emptyTemplate;
        }

        private static void SetOriginalTemplate(ListViewBase listViewBase, CollectionChange collectionChange)
        {
            var listViewIdentifier = listViewBase.GetHashCode();

            if (_originalTemplates.ContainsKey(listViewIdentifier) == false)
            {
                return;
            }

            if (collectionChange != CollectionChange.Reset && listViewBase.Template != _originalTemplates[listViewIdentifier])
            {
                listViewBase.Template = _originalTemplates[listViewIdentifier];
            }
        }
    }
}
