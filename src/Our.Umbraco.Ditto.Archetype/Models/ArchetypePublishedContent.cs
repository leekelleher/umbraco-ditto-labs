namespace Our.Umbraco.Ditto.Archetype
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using global::Archetype.Models;
	using global::Umbraco.Core;
	using global::Umbraco.Core.Models;
	using global::Umbraco.Core.Models.PublishedContent;

	public class ArchetypePublishedContent : IPublishedContent
	{
		private ArchetypeFieldsetModel _fieldset;

		public ArchetypePublishedContent(ArchetypeFieldsetModel fieldset)
		{
			_fieldset = fieldset;
		}

		public IEnumerable<IPublishedContent> Children
		{
			get { return null; }
		}

		public IEnumerable<IPublishedContent> ContentSet
		{
			get { return null; }
		}

		public PublishedContentType ContentType
		{
			get { return null; }
		}

		public DateTime CreateDate
		{
			get { return DateTime.MinValue; }
		}

		public int CreatorId
		{
			get { return 0; }
		}

		public string CreatorName
		{
			get { return null; }
		}

		public string DocumentTypeAlias
		{
			get { return _fieldset.Alias; }
		}

		public int DocumentTypeId
		{
			get { return -1; }
		}

		public int GetIndex()
		{
			return -1;
		}

		public IPublishedProperty GetProperty(string alias, bool recurse)
		{
			var property = _fieldset.Properties.FirstOrDefault(x => x.Alias.InvariantEquals(alias));

			return new ArchetypePublishedProperty(property);
		}

		public IPublishedProperty GetProperty(string alias)
		{
			return this.GetProperty(alias, false);
		}

		public int Id
		{
			get { return -1; }
		}

		public bool IsDraft
		{
			get { return _fieldset.Disabled; }
		}

		public PublishedItemType ItemType
		{
			get { return PublishedItemType.Content; }
		}

		public int Level
		{
			get { return 0; }
		}

		public string Name
		{
			get { return _fieldset.Alias; }
		}

		public IPublishedContent Parent
		{
			get { return null; }
		}

		public string Path
		{
			get { return null; }
		}

		public ICollection<IPublishedProperty> Properties
		{
			get
			{
				return _fieldset.Properties
					.Select(x => new ArchetypePublishedProperty(x))
					.Cast<IPublishedProperty>()
					.ToList();
			}
		}

		public int SortOrder
		{
			get { return 0; }
		}

		public int TemplateId
		{
			get { return 0; }
		}

		public DateTime UpdateDate
		{
			get { return DateTime.MinValue; }
		}

		public string Url
		{
			get { return null; }
		}

		public string UrlName
		{
			get { return null; }
		}

		public Guid Version
		{
			get { return Guid.Empty; }
		}

		public int WriterId
		{
			get { return 0; }
		}

		public string WriterName
		{
			get { return null; }
		}

		public object this[string alias]
		{
			get
			{
				var property = this.GetProperty(alias);
				return property == null ? null : property.Value;
			}
		}
	}
}