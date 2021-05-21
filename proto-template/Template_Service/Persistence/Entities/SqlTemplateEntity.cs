using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Template_Service.Persistence.Entities {
    public class SqlTemplateEntity {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public SqlTemplateEntity() { }

        public SqlTemplateEntity(string name) {
            this.Name = name;
        }
    }
}