### UWP ListViewExtensions

This is a library with UWP ListViewBase extensions.


- Alternate row backgroundcolor (AlternateRowColor)
- Alternate row itemtemplate (AlternateRowTemplate)
- Empty data controltemplate (EmptyDataTemplate)

![Sample](ListViewExtensions/EvD.ListViewExtensions.SampleApp/Assets/Screenshots/Screen1.PNG)

Just include a namespace on your page like <pre><code>xmlns:extensions="using:EvD.ListViewExtensions"</code></pre>

And then extend your ListView with the alternate row color attached property
<pre><code>&lt;ListView extensions:AlternateRowColor.Color="LightGray"&gt;&lt;/ListView&gt;</code></pre>

Or extend it with a custom alternate itemtemplate attached property
<pre><code>&lt;ListView extensions:AlternateRowTemplate.ItemTemplate="{StaticResource AlternateTemplate}"&gt;&lt;/ListView&gt;</code></pre>

Or add an controltemplate when your bound items are count zero
<pre><code>&lt;ListView extensions:EmptyDataTemplate.ControlTemplate="{StaticResource EmptyTemplate}"&gt;&lt;/ListView&gt;</code></pre>

Or combine them all. Just like in the sample app. Super simple.

Download also available on [nuget](https://www.nuget.org/packages/EvD.ListViewExtensions).