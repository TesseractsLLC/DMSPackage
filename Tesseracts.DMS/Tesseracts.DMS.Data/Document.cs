//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tesseracts.DMS.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Document
    {
        public long Id { get; set; }
        public string UniqueFileName { get; set; }
        public string FileSavedPlatform { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public Nullable<long> DocumentTagType { get; set; }
        public string DocumentTagValue { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    
        public virtual DocumentTag DocumentTag { get; set; }
    }
}
