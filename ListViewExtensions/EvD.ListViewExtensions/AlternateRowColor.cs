using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace EvD.ListViewExtensions
{
	public static class AlternateRowColor
	{
		public static readonly DependencyProperty ColorProperty = DependencyProperty.RegisterAttached(
			"Color",
			typeof(Brush),
			typeof(AlternateRowColor),
			new PropertyMetadata(null, OnColorPropertyChanged));

		public static Brush GetColor(DependencyObject obj)
		{
			return (Brush)obj.GetValue(ColorProperty);
		}

		public static void SetColor(DependencyObject obj, Brush value)
		{
			obj.SetValue(ColorProperty, value);
		}

		private static void OnColorPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
		{
			ListViewBase listViewBase = (ListViewBase)sender;

			listViewBase.ContainerContentChanging -= ContainerContentChanging;

			if (ColorProperty != null)
			{
				listViewBase.ContainerContentChanging += ContainerContentChanging;
			}
		}

		private static void ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
		{
			var itemContainer = args.ItemContainer as ListViewItem;
			var index = sender.IndexFromContainer(itemContainer);

			if (index % 2 == 0)
				itemContainer.Background = GetColor(sender);
			else
				itemContainer.Background = null;
		}
	}
}
