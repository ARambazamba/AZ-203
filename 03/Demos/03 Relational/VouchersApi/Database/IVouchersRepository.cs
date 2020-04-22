using System;
using System.Collections.Generic;

namespace Vouchers
{
    public interface IVouchersRepository
    {
        IEnumerable<Voucher> GetAllVouchers();
        VoucherViewModel GetVoucher(Int32 ID);
        Voucher CreateVoucher(Voucher currentVoucher);
        Voucher UpdateVoucher(Voucher currentVoucher);
        void DeleteVoucher(int ID);
        VoucherDetail CreateVoucherDetail(VoucherDetail voucherDetail);
        VoucherDetail UpdateVoucherDetail(VoucherDetail voucherDetail);
        void DeleteVoucherDetail(int ID);
        IEnumerable<Voucher> GetMarkedVouchers();
    }
}