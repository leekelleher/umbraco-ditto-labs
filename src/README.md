# Ditto Labs

Here are some things that we're experimenting with...

---

## Ditto ModelFactory

> The Ditto ModelFactory was originally part of the main [Ditto source-code repository](https://github.com/leekelleher/umbraco-ditto/).

[The original documentation can be found here](http://umbraco-ditto.readthedocs.org/en/latest/publishedcontentmodelfactory/).

---

## Ditto for Archetype

Exploring a way to enable Ditto to map `ArchetypeModel` and `ArchetypeFieldsetModel` objects to a POCO/model.

The approach our experiment has taken is to convert an `ArchetypeFieldsetModel` into an `IPublishedContent`, then apply Ditto's `.As<T>` method in typical usage.

> *Note:* Given the success of this experiment, we have submitted [a pull-request to the Archetype core](https://github.com/imulus/Archetype/pull/303) codebase!

---

# Future ideas

Here are a few other ideas where we would like to experiment using Ditto.

* **Contrib project** - a series of custom ValueResolvers and TypeConverters that re-use other areas of the Umbraco core, such as Membership helpers and Relations.
* **Merchello** - taking from the learnings of a recent [Merchello/Ditto workshop](https://github.com/BarryFogarty/Merchello.UkFest.Workshop) implementation, we'd explore developing re-usable components.
