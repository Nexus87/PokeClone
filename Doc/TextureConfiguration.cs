﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.0.30319.18020.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=true)]
public partial class Texture {
    
    private string keyField;
    
    private FileFormat fileFormatField;
    
    private string fileNameField;
    
    /// <remarks/>
    public string Key {
        get {
            return this.keyField;
        }
        set {
            this.keyField = value;
        }
    }
    
    /// <remarks/>
    public FileFormat FileFormat {
        get {
            return this.fileFormatField;
        }
        set {
            this.fileFormatField = value;
        }
    }
    
    /// <remarks/>
    public string FileName {
        get {
            return this.fileNameField;
        }
        set {
            this.fileNameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public enum FileFormat {
    
    /// <remarks/>
    xna,
    
    /// <remarks/>
    image,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=true)]
public partial class SpriteSheet {
    
    private string mapFileField;
    
    private FileFormat fileFormatField;
    
    private string fileNameField;
    
    /// <remarks/>
    public string MapFile {
        get {
            return this.mapFileField;
        }
        set {
            this.mapFileField = value;
        }
    }
    
    /// <remarks/>
    public FileFormat FileFormat {
        get {
            return this.fileFormatField;
        }
        set {
            this.fileFormatField = value;
        }
    }
    
    /// <remarks/>
    public string FileName {
        get {
            return this.fileNameField;
        }
        set {
            this.fileNameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=true)]
public partial class TextureConfig {
    
    private string contentRootField;
    
    private Texture[] texturesField;
    
    private SpriteSheet[] spriteSheetsField;
    
    /// <remarks/>
    public string ContentRoot {
        get {
            return this.contentRootField;
        }
        set {
            this.contentRootField = value;
        }
    }
    
    /// <remarks/>
    public Texture[] Textures {
        get {
            return this.texturesField;
        }
        set {
            this.texturesField = value;
        }
    }
    
    /// <remarks/>
    public SpriteSheet[] SpriteSheets {
        get {
            return this.spriteSheetsField;
        }
        set {
            this.spriteSheetsField = value;
        }
    }
}
