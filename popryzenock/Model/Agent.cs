//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace popryzenock.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Remoting.Messaging;
    using System.Windows.Media;

    public partial class Agent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Agent()
        {
            this.AgentPriorityHistory = new HashSet<AgentPriorityHistory>();
            this.ProductSale = new HashSet<ProductSale>();
            this.Shop = new HashSet<Shop>();
        }

        public int ID { get; set; }
        public int AgentTypeID { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Logo { get; set; }
        public string Address { get; set; }
        public int Priority { get; set; }
        public string DirectorName { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }
        public virtual int sale { get; set; }
        public virtual int percent { get; set; }


        public virtual AgentType AgentType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgentPriorityHistory> AgentPriorityHistory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSale> ProductSale { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shop> Shop { get; set; }
        public string AgentTypeIDi
        {
            get
            {
                string Name = "";
                if (this.AgentTypeID == AgentType.ID)
                {
                    Name = AgentType.Title + " | Наименование агента: " + this.Title;
                    return Name;
                }
                else
                {
                    return Name;
                }

            }
        }
        public string ImagePath
        {
            get
            {
                var Path = "\\Resources\\" + this.Logo;
                return Path;
            }
        }
        public string PriorirtyNumber
        {
            get
            {
                var Pr = Convert.ToInt32(this.Priority);
                var PrConvertd = Convert.ToString(Pr);
                return PrConvertd;
            }
        }
        
        public string PersenteSale
        {
            get
            {
                
                int sum = 0;
                double fsum = 0;
                foreach (ProductSale ps in this.ProductSale)
                {
                    List<ProductMaterial> mtr = new List<ProductMaterial> { };
                    mtr = popryzenockEntities.GetContext().ProductMaterial.Where(ProductMaterial => ProductMaterial.ProductID == ps.ProductID).ToList();
                    foreach (ProductMaterial mt in mtr)
                    {
                        double f = decimal.ToDouble(mt.Material.Cost);
                        fsum += f * (double)mt.Count;
                    }
                    fsum = fsum * ps.ProductCount;
                    if (ps.SaleDate.AddDays(365).CompareTo(DateTime.Today) > 0)
                        sum += ps.ProductCount;
                }
                this.sale = sum;
                this.percent = 0;
                if (fsum >= 10000 && fsum < 50000) this.percent = 5;
                if (fsum >= 50000 && fsum < 150000) this.percent = 10;
                if (fsum >= 150000 && fsum < 500000) this.percent = 20;
                if (fsum >= 500000) this.percent = 25;

                return percent.ToString();


            }



        }
        public string SaleYear
        {
            get
            {

                int sum = 0;
                double fsum = 0;
                foreach (ProductSale ps in this.ProductSale)
                {
                    List<ProductMaterial> mtr = new List<ProductMaterial> { };
                    mtr = popryzenockEntities.GetContext().ProductMaterial.Where(ProductMaterial => ProductMaterial.ProductID == ps.ProductID).ToList();
                    foreach (ProductMaterial mt in mtr)
                    {
                        double f = decimal.ToDouble(mt.Material.Cost);
                        fsum += f * (double)mt.Count;
                    }
                    fsum = fsum * ps.ProductCount;
                    if (ps.SaleDate.AddDays(365).CompareTo(DateTime.Today) > 0)
                        sum += ps.ProductCount;
                }
                this.sale = sum;
                this.percent = 0;
                if (fsum >= 10000 && fsum < 50000) this.percent = 5;
                if (fsum >= 50000 && fsum < 150000) this.percent = 10;
                if (fsum >= 150000 && fsum < 500000) this.percent = 20;
                if (fsum >= 500000) this.percent = 25;

                return sale.ToString();


            }




        }
        public string Background
        {
            get
            {
                if (this.percent >= 25)
                {
                    return "#7fff00";
                }
                else return "#fff";
            }
        }
    }
        
        


}

