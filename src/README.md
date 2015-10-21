# Ditto Labs

Here are some things that we're experimenting with...

---

## Ditto ModelFactory

> The Ditto ModelFactory was originally part of the main [Ditto source-code repository](https://github.com/leekelleher/umbraco-ditto/).

---

## Ditto for Archetype

Exploring a way to enable Ditto to map `ArchetypeModel` and `ArchetypeFieldsetModel` objects to a POCO/model.

The approach our experiment has taken is to convert an `ArchetypeFieldsetModel` into an `IPublishedContent`, then apply Ditto's `.As<T>` method in typical usage.

> *Note:* For an alternative approach to using Ditto with Archetype, take a look at @micklaw's [Ditto Resolvers](https://github.com/micklaw/Ditto.Resolvers) project.

