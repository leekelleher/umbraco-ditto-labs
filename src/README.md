# Ditto Labs

Here are some things that we're experimenting with...

## Ditto for Archetype

Exploring a way to enable Ditto to map `ArchetypeModel` and `ArchetypeFieldsetModel` objects to a POCO/model.

> *Note:* If you need a production-ready library that already does this, please do take a look at @micklaw's [Ditto Resolvers](https://github.com/micklaw/Ditto.Resolvers) project.

Where our experiment differs from the Ditto Resolver project is that we would like to remove the `ArchetypeFieldsetModel` inheritance constraint and switch from using a `ValueResolver` over to a `TypeConverter`.  *It's apples :apple: & oranges :tangerine:.*

### TODO 

* [x] Implement `ValueResolver` at property level
* [ ] Implement `TypeConverter` at property level
* [ ] Implement `ConversionHandler` attributes and events

