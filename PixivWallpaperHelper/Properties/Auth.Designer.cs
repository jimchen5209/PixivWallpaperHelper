﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這段程式碼是由工具產生的。
//     執行階段版本:4.0.30319.42000
//
//     對這個檔案所做的變更可能會造成錯誤的行為，而且如果重新產生程式碼，
//     變更將會遺失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace PixivWallpaperHelper.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.10.0.0")]
    internal sealed partial class Auth : global::System.Configuration.ApplicationSettingsBase {
        
        private static Auth defaultInstance = ((Auth)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Auth())));
        
        public static Auth Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string KEY_PIXIV_ACCESS_TOKEN {
            get {
                return ((string)(this["KEY_PIXIV_ACCESS_TOKEN"]));
            }
            set {
                this["KEY_PIXIV_ACCESS_TOKEN"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string KEY_PIXIV_REFRESH_TOKEN {
            get {
                return ((string)(this["KEY_PIXIV_REFRESH_TOKEN"]));
            }
            set {
                this["KEY_PIXIV_REFRESH_TOKEN"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public long KEY_PIXIV_USER_ID {
            get {
                return ((long)(this["KEY_PIXIV_USER_ID"]));
            }
            set {
                this["KEY_PIXIV_USER_ID"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string KEY_PIXIV_USER_USERNAME {
            get {
                return ((string)(this["KEY_PIXIV_USER_USERNAME"]));
            }
            set {
                this["KEY_PIXIV_USER_USERNAME"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string KEY_PIXIV_USER_NAME {
            get {
                return ((string)(this["KEY_PIXIV_USER_NAME"]));
            }
            set {
                this["KEY_PIXIV_USER_NAME"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string KEY_PIXIV_USER_IMG {
            get {
                return ((string)(this["KEY_PIXIV_USER_IMG"]));
            }
            set {
                this["KEY_PIXIV_USER_IMG"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool KEY_PIXIV_USER_LOGIN {
            get {
                return ((bool)(this["KEY_PIXIV_USER_LOGIN"]));
            }
            set {
                this["KEY_PIXIV_USER_LOGIN"] = value;
            }
        }
    }
}
