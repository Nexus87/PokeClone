namespace GameEngine.GUI.Configuration
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = true)]
    public partial class SpriteSheet
    {
        private string mapFileField;

        private FileFormat fileFormatField;

        private string fileNameField;

        /// <remarks/>
        public string MapFile
        {
            get { return this.mapFileField; }
            set { this.mapFileField = value; }
        }

        /// <remarks/>
        public FileFormat FileFormat
        {
            get { return this.fileFormatField; }
            set { this.fileFormatField = value; }
        }

        /// <remarks/>
        public string FileName
        {
            get { return this.fileNameField; }
            set { this.fileNameField = value; }
        }
    }
}