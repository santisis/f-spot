/*
 * FSpot.PhotoQuery.cs
 * 
 * Author(s):
 *	Larry Ewing  <lewing@novell.com>
 * 	Stephane Delcroix  <stephane@delcroix.org>
 *
 * This is free software. See COPYING for details.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using FSpot.Query;

namespace FSpot {
	public class PhotoQuery : FSpot.IBrowsableCollection {
		private Photo [] photos;
		private PhotoStore store;
		private Term terms;
		private Tag [] tags;
		private string extra_condition;
		
		// Constructor
		public PhotoQuery (PhotoStore store)
		{
			this.store = store;
			// Note: this is to let the query pick up
			// 	 photos that were added or removed over dbus
			this.store.ItemsAddedOverDBus += delegate { RequestReload(); };
			this.store.ItemsRemovedOverDBus += delegate { RequestReload(); };
			this.store.ItemsChanged += MarkChanged;

			photos = store.Query ((Tag [])null, null, Range, RollSet, RatingRange);
		}

		public int Count {
			get { return photos.Length;}
		}
		
		public bool Contains (IBrowsableItem item) {
			return IndexOf (item) >= 0;
		}

		// IPhotoCollection Interface
		public event FSpot.IBrowsableCollectionChangedHandler Changed;
		public event FSpot.IBrowsableCollectionChangedHandler PreChanged;
		public event FSpot.IBrowsableCollectionItemsChangedHandler ItemsChanged;
		
		public IBrowsableItem this [int index] {
			get { return photos [index]; }
		}

		public Photo [] Photos {
			get { return photos; }
		}

		public IBrowsableItem [] Items {
			get { return (IBrowsableItem [])photos; }
		}
		
		public PhotoStore Store {
			get { return store; }
		}
		

		//Query Conditions
		private Dictionary<Type, IQueryCondition> conditions;
		private Dictionary<Type, IQueryCondition> Conditions {
			get {
				if (conditions == null)
					conditions = new Dictionary<Type, IQueryCondition> ();
				return conditions;
			}
		}

		internal bool SetCondition (IQueryCondition condition)
		{
			if (condition == null)
				throw new ArgumentNullException ("condition");
			if (Conditions.ContainsKey (condition.GetType ()) && Conditions [condition.GetType ()] == condition)
				return false;
			Conditions [condition.GetType ()] = condition;
			return true;
		}

		internal IQueryCondition GetCondition<T> ()
		{
			if (Conditions.ContainsKey (typeof (T)))
				return Conditions [typeof (T)];
			return null;
		}

		internal bool UnSetCondition<T> ()
		{
			if (!Conditions.ContainsKey (typeof(T)))
				return false;
			Conditions.Remove (typeof(T));
			return true;
		}

		public Term Terms {
			get {
				return terms;
			}
			set {
				terms = value;
				untagged = false;
				RequestReload ();
			}
		}

		public string ExtraCondition {
			get {
				return extra_condition;
			}
			
			set {
				if (extra_condition == value)
					return;

				extra_condition = value;

				if (value != null)
					untagged = false;

 				RequestReload ();
 			}
 		}
		
		public DateRange Range {
			get { return GetCondition<DateRange> () as DateRange; }
			set {
				if (value == null && UnSetCondition<DateRange> () || value != null && SetCondition (value))
					RequestReload ();
			}
		}
		
		private bool untagged = false;
		public bool Untagged {
			get { return untagged; }
			set {
				if (untagged != value) {
					untagged = value;
					if (untagged)
						extra_condition = null;
					RequestReload ();
				}
			}
		}

		public RollSet RollSet {
			get { return GetCondition<RollSet> () as RollSet; }
			set {
				if (value == null && UnSetCondition<RollSet> () || value != null && SetCondition (value))
					RequestReload ();
			}
		}

		public RatingRange RatingRange {
			get { return GetCondition<RatingRange> () as RatingRange; }
			set {
				if (value == null && UnSetCondition<RatingRange>() || value != null && SetCondition (value))
					RequestReload ();
			}
		}

		public void RequestReload ()
		{
			if (untagged)
				photos = store.Query (new UntaggedCondition (), Range, RollSet, RatingRange);
			else
				photos = store.Query (terms, extra_condition, Range, RollSet, RatingRange);

			//this event will allow resorting the query content
			if (PreChanged != null)
				PreChanged (this);

			if (Changed != null)
				Changed (this);
		}
		
		public int IndexOf (IBrowsableItem photo)
		{
			return System.Array.IndexOf (photos, photo);
		}
		
		public void Commit (int index, bool metadata_changed, bool data_changed)
		{
			Commit (new int [] {index}, metadata_changed, data_changed);
		}

		public void Commit (int [] indexes, bool metadata_changed, bool data_changed)
		{
			List<Photo> to_commit = new List<Photo>();
			foreach (int index in indexes)
				to_commit.Add (photos [index]);
			store.Commit (to_commit.ToArray (), metadata_changed, data_changed);
		}

		private void MarkChanged (object sender, DbItemEventArgs args)
		{
			List<int> indexes = new List<int>();
			foreach (DbItem item in args.Items) {
				Photo photo = item as Photo;
				int index = IndexOf (photo);

				// Ignore photos that are not in the query
				if (index > -1) 
					indexes.Add (index);
			}

			PhotoEventArgs photo_args = args as PhotoEventArgs;

			if (indexes.Count > 0 && ItemsChanged != null)
				ItemsChanged (this, new BrowsableEventArgs(indexes.ToArray (),
							photo_args.MetadataChanged, photo_args.DataChanged));
		}

		public void MarkChanged (int index, bool metadata_changed, bool data_changed)
		{
			MarkChanged (new int [] {index}, metadata_changed, data_changed);
		}

		public void MarkChanged (int [] indexes, bool metadata_changed, bool data_changed)
		{
			List<Photo> to_emit = new List<Photo> ();
			foreach (int index in indexes)
				to_emit.Add (photos [index]);
			store.EmitChanged (to_emit.ToArray (), metadata_changed, data_changed);
		}

		[Obsolete ("You should provide info on what changed!")]
		public void MarkChanged (int index)
		{
			MarkChanged (new int [] {index});
		}

		[Obsolete ("You should provide info on what changed!")]
		private void MarkChanged (params int [] indexes)
		{
			ItemsChanged (this, new BrowsableEventArgs (indexes));
		}
	}
}
