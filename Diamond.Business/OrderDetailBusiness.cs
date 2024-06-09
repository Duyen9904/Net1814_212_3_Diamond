using Diamond.Business.Base;
using Diamond.Common;
using Diamond.Data;
using Diamond.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Diamond.Business
{
    public interface IOrderDetailBusiness
    {
        Task<IBusinessResult> getAll();
        Task<IBusinessResult> GetById(string code);

        Task<IBusinessResult> Save(Orderdetail orderdetail);
        Task<IBusinessResult> Update(Orderdetail orderdetail);
        Task<IBusinessResult> DeleteById(string code);
    }
    public class OrderDetailBusiness : IOrderDetailBusiness
    {
        private readonly UnitOfWork _unitOfWork;
        public OrderDetailBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> getAll()
        {
            try
            {
                var orderDetails = await _unitOfWork.OrderDetailRepository.GetAllAsync();

                if (orderDetails == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orderDetails);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
        public async Task<IBusinessResult> GetById(string code)
        {
            try
            {
                var orderDetails = await _unitOfWork.OrderDetailRepository.GetByIdAsync(code);

                if (orderDetails == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orderDetails);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
        public async Task<IBusinessResult> Save(Orderdetail Orderdetail)
        {
            try
            {
                //int result = await _OrderDetailRepository.CreateAsync(Orderdetail);
                int result = await _unitOfWork.OrderDetailRepository.CreateAsync(Orderdetail);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IBusinessResult> Update(Orderdetail Orderdetail)
        {
            try
            {
                //int result = await _OrderDetailRepository.UpdateAsync(Orderdetail);
                int result = await _unitOfWork.OrderDetailRepository.UpdateAsync(Orderdetail);

                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }

        public async Task<IBusinessResult> DeleteById(string code)
        {
            try
            {
                //var Orderdetail = await _OrderDetailRepository.GetByIdAsync(code);
                var Orderdetail = await _unitOfWork.OrderDetailRepository.GetByIdAsync(code);
                if (Orderdetail != null)
                {
                    //var result = await _OrderDetailRepository.RemoveAsync(Orderdetail);
                    var result = await _unitOfWork.OrderDetailRepository.RemoveAsync(Orderdetail);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                    }
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }

    }


}
