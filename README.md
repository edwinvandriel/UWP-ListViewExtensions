### UWP ListViewExtensions

This is a library with UWP ListViewBase extensions.


- Alternate row color (AlternateRowColor)
- Alternate row itemtemplate (AlternateRowTemplate)

![Sample](ListViewExtensions/EvD.ListViewExtensions.SampleApp/Assets/Screenshots/Screen1.PNG)

Just include a namespace on your page like <pre><code>xmlns:extensions="using:EvD.ListViewExtensions"</code></pre>

And then extend your ListView with the alternate row color attached property
<pre><code>&lt;ListView extensions:AlternateRowColor.Color="LightGray"&gt;&lt;/ListView&gt;</code></pre>

Or extend it with a custom alternate itemtemplate attached property
<pre><code>&lt;ListView extensions:AlternateRowTemplate.ItemTemplate="{StaticResource AlternateTemplate}"&gt;&lt;/ListView&gt;</code></pre>

Or combine both. Super simple.

Download also available on [nuget](https://www.nuget.org/packages/EvD.ListViewExtensions).