// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.42
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Mono.Addins.Gui {
    
    
    internal partial class ManageSitesDialog {
        
        private Gtk.HBox hbox67;
        
        private Gtk.ScrolledWindow scrolledwindow17;
        
        private Gtk.TreeView repoTree;
        
        private Gtk.VBox vbox88;
        
        private Gtk.Button btnAdd;
        
        private Gtk.Button btnRemove;
        
        private Gtk.Button closebutton2;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget Mono.Addins.Gui.ManageSitesDialog
            this.Name = "Mono.Addins.Gui.ManageSitesDialog";
            this.Title = Mono.Unix.Catalog.GetString("Extension Repository Management");
            this.TypeHint = ((Gdk.WindowTypeHint)(1));
            this.BorderWidth = ((uint)(6));
            this.DefaultWidth = 600;
            this.DefaultHeight = 300;
            // Internal child Mono.Addins.Gui.ManageSitesDialog.VBox
            Gtk.VBox w1 = this.VBox;
            w1.Name = "dialog-vbox10";
            w1.Spacing = 6;
            // Container child dialog-vbox10.Gtk.Box+BoxChild
            this.hbox67 = new Gtk.HBox();
            this.hbox67.Name = "hbox67";
            this.hbox67.Spacing = 12;
            this.hbox67.BorderWidth = ((uint)(6));
            // Container child hbox67.Gtk.Box+BoxChild
            this.scrolledwindow17 = new Gtk.ScrolledWindow();
            this.scrolledwindow17.CanFocus = true;
            this.scrolledwindow17.Name = "scrolledwindow17";
            this.scrolledwindow17.VscrollbarPolicy = ((Gtk.PolicyType)(1));
            this.scrolledwindow17.HscrollbarPolicy = ((Gtk.PolicyType)(1));
            this.scrolledwindow17.ShadowType = ((Gtk.ShadowType)(1));
            // Container child scrolledwindow17.Gtk.Container+ContainerChild
            this.repoTree = new Gtk.TreeView();
            this.repoTree.CanFocus = true;
            this.repoTree.Name = "repoTree";
            this.repoTree.HeadersVisible = false;
            this.repoTree.HeadersClickable = true;
            this.scrolledwindow17.Add(this.repoTree);
            this.hbox67.Add(this.scrolledwindow17);
            Gtk.Box.BoxChild w3 = ((Gtk.Box.BoxChild)(this.hbox67[this.scrolledwindow17]));
            w3.Position = 0;
            // Container child hbox67.Gtk.Box+BoxChild
            this.vbox88 = new Gtk.VBox();
            this.vbox88.Name = "vbox88";
            this.vbox88.Spacing = 6;
            // Container child vbox88.Gtk.Box+BoxChild
            this.btnAdd = new Gtk.Button();
            this.btnAdd.CanFocus = true;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseStock = true;
            this.btnAdd.UseUnderline = true;
            this.btnAdd.Label = "gtk-add";
            this.vbox88.Add(this.btnAdd);
            Gtk.Box.BoxChild w4 = ((Gtk.Box.BoxChild)(this.vbox88[this.btnAdd]));
            w4.Position = 0;
            w4.Expand = false;
            w4.Fill = false;
            // Container child vbox88.Gtk.Box+BoxChild
            this.btnRemove = new Gtk.Button();
            this.btnRemove.CanFocus = true;
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.UseStock = true;
            this.btnRemove.UseUnderline = true;
            this.btnRemove.Label = "gtk-delete";
            this.vbox88.Add(this.btnRemove);
            Gtk.Box.BoxChild w5 = ((Gtk.Box.BoxChild)(this.vbox88[this.btnRemove]));
            w5.Position = 1;
            w5.Expand = false;
            w5.Fill = false;
            this.hbox67.Add(this.vbox88);
            Gtk.Box.BoxChild w6 = ((Gtk.Box.BoxChild)(this.hbox67[this.vbox88]));
            w6.Position = 1;
            w6.Expand = false;
            w6.Fill = false;
            w1.Add(this.hbox67);
            Gtk.Box.BoxChild w7 = ((Gtk.Box.BoxChild)(w1[this.hbox67]));
            w7.Position = 0;
            // Internal child Mono.Addins.Gui.ManageSitesDialog.ActionArea
            Gtk.HButtonBox w8 = this.ActionArea;
            w8.Name = "dialog-action_area10";
            w8.Spacing = 10;
            w8.BorderWidth = ((uint)(6));
            w8.LayoutStyle = ((Gtk.ButtonBoxStyle)(4));
            // Container child dialog-action_area10.Gtk.ButtonBox+ButtonBoxChild
            this.closebutton2 = new Gtk.Button();
            this.closebutton2.CanDefault = true;
            this.closebutton2.CanFocus = true;
            this.closebutton2.Name = "closebutton2";
            this.closebutton2.UseStock = true;
            this.closebutton2.UseUnderline = true;
            this.closebutton2.Label = "gtk-close";
            this.AddActionWidget(this.closebutton2, -7);
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.Show();
            this.btnAdd.Clicked += new System.EventHandler(this.OnAdd);
            this.btnRemove.Clicked += new System.EventHandler(this.OnRemove);
        }
    }
}
