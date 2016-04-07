# Ditto Labs

Here are some things that we're experimenting with...

---

## Community Contrib

Community contributed set of custom ValueResolvers and TypeConverters that re-use other areas of the Umbraco core, such as Membership helpers and Relations.

---

## Ditto ModelFactory

> The Ditto ModelFactory was originally part of the main [Ditto source-code repository](https://github.com/leekelleher/umbraco-ditto/).

---

## Ditto for Archetype

Since having our [pull-request accepted in the Archetype core](https://github.com/imulus/Archetype/pull/303) codebase, we can now easily use Ditto to map Archetype data.

The regular usage would be...

    @Model.Content.GetPropertyValue<ArchetypeModel>("alias").ToPublishedContentSet().As<MyModel>();

This labs repo offers custom extension methods and TypeConverter to help make mapping simpler.

    @Model.Content.GetPropertyValue<ArchetypeModel>("alias").As<MyModel>();

or

```csharp
[DittoArchetype]
public MyModel MyProperty { get; set; }
```

---

# Future ideas

Here are other areas that we would like to experiment with using Ditto.

* **Merchello** - taking from the learnings of a recent [Merchello/Ditto workshop](https://github.com/BarryFogarty/Merchello.UkFest.Workshop) implementation, we'd explore developing re-usable components.
* **Vorto** - see example gist by @JimBobSquarePants - https://gist.github.com/JimBobSquarePants/80762a1233bbb112689e
