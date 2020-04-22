using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vouchers
{
    public class VoucherDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public int VoucherID { get; set; }

        public int AccountID { get; set; }

        [ForeignKey("AccountID")]
        public virtual BalanceAccount Account { get; set; }

        public string Text { get; set; }

        public int Amount { get; set; }

        public int VATRate { get; set; }

        public string Comment { get; set; }

        [NotMapped]
        public string TempId { get; set; }

    }
}