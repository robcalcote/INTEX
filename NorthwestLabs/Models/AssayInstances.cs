﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthwestLabs.Models
{
    // AssayInstances Table
    [Table("AssayInstances")]
    public class AssayInstances
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Assay Instance ID")]
        public int AssayInstanceID { get; set; }
        // Link back to SampleTests Table
        public virtual ICollection<SampleTests> sampletests { get; set; }

        [StringLength(255, ErrorMessage = "Assay Results must not exceed 255 characters.")]
        [DisplayName("Assay Results")]
        public string AssayResults { get; set; }

        // Link to the Compounds Table
        [DisplayName("LT Number")]
        [Required(ErrorMessage = "LT Number is required")]
        [ForeignKey("compounds")]
        public int LTNumber { get; set; }
        public virtual Compounds compounds { get; set; }

        // Link to the AssayTypes Table
        [DisplayName("Assay Type ID")]
        [Required(ErrorMessage = "Assay Type ID is required")]
        [ForeignKey("assaytypes")]
        public int AssayTypeID { get; set; }
        public virtual AssayTypes assaytypes { get; set; }
    }
}