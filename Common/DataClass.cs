using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using C1.Data;
using Common.DataObjects;

namespace Common
{
    /// <summary>
    /// C1SchemaDef container class: main class of the data library.
    /// Contains the C1SchemaDef component managing the data schema. 
    /// </summary>
    public class DataClass : System.ComponentModel.Component
    {
        /// <summary>
        /// C1SchemaDef component managing the data schema.
        /// Needs to be public in order to allow access to the library.
        /// </summary>
        public C1.Data.C1SchemaDef schemaDef;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public DataClass(System.ComponentModel.IContainer container)
        {
            if (container != null)
            {
                container.Add(this);
            }
            InitializeComponent();
        }

        public DataClass()
        {
            /// <summary>
            /// Required for Windows.Forms Class Composition Designer support
            /// </summary>
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.schemaDef = new C1.Data.C1SchemaDef();
            this.schemaDef.DataObjectsAssemblyFlags = C1.Data.DataObjectsAssemblyFlags.All;
        }
        #endregion
    }
}
