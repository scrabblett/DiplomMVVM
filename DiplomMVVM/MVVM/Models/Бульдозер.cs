//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DiplomMVVM.MVVM.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Бульдозер
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Бульдозер()
        {
            this.Отчет = new HashSet<Отчет>();
        }
    
        public int ID { get; set; }
        public string Модель { get; set; }
        public Nullable<double> Длина_отвала { get; set; }
        public Nullable<double> Высота_отвала { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Отчет> Отчет { get; set; }
    }
}
