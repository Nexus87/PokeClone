namespace GameEngine.GUI.Configuration
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = true)]
    public partial class TextureConfig
    {
        private string contentRootField;

        private Texture[] texturesField;

        private SpriteSheet[] spriteSheetsField;

        /// <remarks/>
        public string ContentRoot
        {
            get { return this.contentRootField; }
            set { this.contentRootField = value; }
        }

        /// <remarks/>
        public Texture[] Textures
        {
            get { return this.texturesField; }
            set { this.texturesField = value; }
        }

        /// <remarks/>
        public SpriteSheet[] SpriteSheets
        {
            get { return this.spriteSheetsField; }
            set { this.spriteSheetsField = value; }
        }
    }
}